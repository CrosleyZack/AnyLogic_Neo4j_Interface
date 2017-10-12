using System;
using System.Collections.Generic;
using Neo4j.Driver.V1;

namespace AnyLogic_Neo4j_Interface
{
    class Graph
    {
        //private members to store database connection info
        private String connectionString;
        private IDriver driver;
        private ISession session;

        //private members to store database related info, for speeding up execution
        private List<Double> knownTimes;


        private Graph()
        {
            connectionString = String.Empty;
            driver = null;
            session = null;
            knownTimes = new List<Double>();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public static Graph GetInstance()
        {
            return new Graph();
        }

        /// <summary>
        ///     Creates connection from 
        /// </summary>
        /// <param name="connection">Connection String to the Neo4j Database</param>
        /// <param name="username">String username for logging into Neo4j Database Server</param>
        /// <param name="password">String password for logging into Neo4j Database Server</param>
        /// <returns>True if setup is successful, false otherwise</returns>
        public Boolean SetupConnection(String connection, String username = "neo4j", String password = "neo4j")
        {
            try
            {
                connectionString = connection;
                driver = GraphDatabase.Driver(connectionString, AuthTokens.Basic(username, password));
                session = driver.Session();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        ///     Checks that the connection tot he database is valid.
        /// </summary>
        /// <returns>True if connection is valid, False otherwise.</returns>
        public Boolean TestConnection()
        {
            //if no session is established, return false
            if(session == null)
            {
                return false;
            }
            //if there is a session and we can't run the command, return false. Else, return true.
            try
            {
                IStatementResult response = session.Run("MATCH (n) RETURN n");
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Setters
        /// 

        /// <summary>
        ///     Creates node to store ALP file string
        /// </summary>
        /// <returns>True if node successfully created, False otherwise.</returns>
        public Boolean AddAlpFile(String alpFile)
        {
            try
            {
                IStatementResult response = session.Run("MERGE (a:ALP {value: '" + alpFile + "'}) " +
                                                        "RETURN a");
                //Validate response
                IRecord record = response.Peek();
                object recordValue = record[0];
                INode node = (INode)recordValue;
                if (node.Labels[0] != "ALP")
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        ///     Creates node for parameter storage, other parameters branch off from this
        /// </summary>
        /// <returns>True if node successfully created, False otherwise.</returns>
        private Boolean AddParamSourceNode()
        {
            try
            {
                IStatementResult response = session.Run("MERGE (i:initialParams) RETURN i");
                //Validate response
                IRecord record = response.Peek();
                object recordValue = record[0];
                INode node = (INode)recordValue;
                if(node.Labels[0] != "initialParams")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        ///     Adds a new parameter node to the graph
        /// </summary>
        /// <param name="name">Name of the parameter being stored</param>
        /// <param name="value">Value of the parameter being stored</param>
        /// <returns>True if stored successfully, false otherwise</returns>
        public Boolean AddParameterNode(String name, Double value)
        {
            //if we don't have a source parameter node defined, attempt to add it.
            if(!ParamSourceExists())
            {
                Boolean response = AddParamSourceNode();
                if(!response) { return false; }
            }

            //attempt to get database results
            try
            {
                IStatementResult response = session.Run("MATCH (p:initialParams) " +
                                      "MERGE (p)-[:param]->(i:parameter {name: '" + name + "', value: " + Convert.ToString(value) + "}) " +
                                      "RETURN i");
                //validate success
                IRecord record = response.Peek();
                object recordValue = record[0];
                INode node = (INode)recordValue; //if we can cast this to a node, then it returned a node. It wouldn't do this on failure, so we know it succeeded
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        ///     Adds a node to indicate a new unit of time
        /// </summary>
        /// <param name="timeNodeId">ID for this day</param>
        /// <returns>True if successful, False otherwise</returns>
        private Boolean AddTimeNode(Double timeNodeId)
        {
            try
            {
                //add the new day node
                IStatementResult response = session.Run("MERGE (d:day {id: " + timeNodeId + "}) " +
                                                        "RETURN d");
                //validate return
                IRecord record = response.Peek();
                object recordValue = record[0];
                INode node = (INode)recordValue;
                if (Convert.ToDouble(node.Properties["id"]) != timeNodeId)
                {
                    return false;
                }

                //add connection from previous day to this day
                Boolean toreturn = true;
                if(timeNodeId >= 1)
                    toreturn = AddConnectionBetweenTimeIntervals(timeNodeId, timeNodeId - 1);
                //all else has gone well up to here, so this will be the last indicator of success or failure.
                return toreturn;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        ///     Create a next day edge between two time nodes
        /// </summary>
        /// <param name="sourceId">First time node</param>
        /// <param name="destId">Second time node</param>
        /// <returns>True if successful, false otherwise</returns>
        private Boolean AddConnectionBetweenTimeIntervals(Double sourceId, Double destId)
        {
            //if IDs aren't valid, return false
            if(sourceId < 0 || destId < 0)
            {
                return false;
            }

            try
            {
                IStatementResult response = session.Run("MATCH (d:day), (c:day) " +
                                           "WHERE d.id = " + destId + " AND c.id = " + sourceId + " " +
                                           "MERGE (c)-[n:nextday]->(d) " +
                                           "RETURN n");
                IRecord record = response.Peek();
                object edgeValue = record[0];
                IRelationship edge = (IRelationship)edgeValue;
                if (edge.Type == "nextday")
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        /// <summary>
        ///     Add new variable node at the specified time
        /// </summary>
        /// <param name="timeId">timestamp of variable</param>
        /// <param name="name">String name for variable</param>
        /// <param name="value">Double value for variable</param>
        /// <returns></returns>
        public Boolean AddVariableNode(Double timeId, String name, Double value)
        {
            //if the day node specified doesn't exist, add it. If this process fails, return false.
            if (!TimeStampExists(timeId)) {
                Boolean result = AddTimeNode(timeId);
                if(!result) { return false; }
            }

            try
            {
                IStatementResult response = session.Run("MATCH (d:day) " +
                                           "WHERE d.id = " + Convert.ToString(timeId) + " " +
                                           "MERGE (d)-[:var]->(i:variable {name: '" + name + "', value: " + Convert.ToString(value) + "}) " +
                                           "RETURN i");
                IRecord record = response.Peek();
                object recordValue = record[0];
                INode node = (INode)recordValue;
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        ///     Remove all nodes an edges for graph. Currently runs at beginning of program for avoiding repeated nodes.
        /// </summary>
        public Boolean EmptyGraph()
        {
            try
            {
                IStatementResult response = session.Run("MATCH (n) " +
                                                        "DETACH DELETE n");
                IRecord record = response.Peek();
                if (record == null)
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Getters

        /// <summary>
        ///  Check if the param source node exists
        /// </summary>
        /// <returns>True if exists, false otherwise</returns>
        private Boolean ParamSourceExists()
        {
            try
            {
                IStatementResult response = session.Run("MATCH (i:initialParams) RETURN i");
                //Validate response
                IRecord record = response.Peek();
                object recordValue = record[0];
                INode node = (INode)recordValue;
                if (node.Labels[0] != "initialParams")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e) { return false;  }

        }

        /// <summary>
        ///     Checks if the time stamp node for this time exists
        /// </summary>
        /// <param name="timeID"></param>
        /// <returns>True if it exists, false otherwise</returns>
        private Boolean TimeStampExists(Double timeID)
        {
            try
            {
                IStatementResult response = session.Run("MATCH (d:day) " +
                                                        "WHERE d.id = " + Convert.ToString(timeID) + " " +
                                                        "RETURN d");
                //validate return
                IRecord record = response.Peek();
                object recordValue = record[0];
                INode node = (INode)recordValue;
                if (Convert.ToDouble(node.Properties["id"]) != timeID)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        ///     Retrieve list of name, value pairs for initial parameters
        /// </summary>
        /// <returns></returns>
        public List<Tuple<String, Double>> GetInitialParams()
        {
            List<Tuple<String, Double>> toReturn = new List<Tuple<string, double>>();

            try
            {
                IStatementResult response = session.Run("MATCH (p:parameter) " +
                                                        "RETURN p.name, p.value");
                foreach (IRecord record in response)
                {
                    //get name and var from record
                    string name = Convert.ToString(record.Values["p.name"]);
                    Double value = Convert.ToDouble(record.Values["p.value"]);
                    toReturn.Add(new Tuple<string, double>(name, value));
                }

                return toReturn;
            }
            catch(Exception e) { return null; }
        }

        /// <summary>
        ///     Gets list of name, value pairs for all variables at the specified time interval
        /// </summary>
        /// <param name="timeID"></param>
        /// <returns>The List of pairs if successful, null otherwise</returns>
        public List<Tuple<String, Double>> GetVariablesAtTimeInterval(Double timeID)
        {
            List<Tuple<String, Double>> toReturn = new List<Tuple<string, double>>();

            try
            {
                IStatementResult response = session.Run("MATCH (d:day)-[Lvar]->(v:variable) " +
                                                        "WHERE d.id = " + Convert.ToString(timeID) + " " +
                                                        "RETURN v");
                foreach (IRecord record in response)
                {
                    INode node = (INode)record.Values["v"];
                    IReadOnlyDictionary<String, object> properties = node.Properties;
                    String name = Convert.ToString(properties["name"]);
                    Double value = Convert.ToDouble(properties["value"]);
                    toReturn.Add(new Tuple<string, double>(name, value));
                }

                return toReturn;
            }
            catch(Exception e) { return null;  }
        }

        /// <summary>
        ///     Returns the ALP file from the database
        /// </summary>
        /// <returns>The string file if successful, null otherwise</returns>
        public String GetALPFile()
        {
            try
            {
                IStatementResult response = session.Run("MATCH (a:ALP)" +
                                                        "RETURN a.value");
                IRecord soleRecord = response.Peek();
                String value = Convert.ToString(soleRecord.Values["a.value"]);
                return value;
            }
            catch(Exception e) { return null; }
        }

        /// <summary>
        ///     Retrieves all time ids in the graph. 
        /// </summary>
        /// <returns>List of times if successful, null otherwise.</returns>
        public List<Double> GetTimeIds()
        {
            List<Double> toreturn = new List<double>();
            try
            {
                IStatementResult response = session.Run("MATCH (d:day)" +
                                                        "RETURN d.id " +
                                                        "ORDER BY d.id");
                foreach (IRecord r in response)
                {
                    Double value = Convert.ToDouble(r.Values["d.id"]);
                    toreturn.Add(value);
                }

                return toreturn;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
