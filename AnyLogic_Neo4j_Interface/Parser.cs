using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Data.OleDb;

namespace AnyLogic_Neo4j_Interface
{
    class Parser
    {
        /// <summary>
        ///     read file to a single string
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static String FileToString(String filename)
        {
            try
            {
                Stream alpStream = File.Open(filename, FileMode.Open);
                StreamReader alpReader = new StreamReader(alpStream);
                String alpFile = alpReader.ReadToEnd();
                return alpFile;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        ///     Read in data log from file name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Dictionary<Double, List<Tuple<String, Double>>> ReadDataLog(String filename)
        {

            Dictionary<Double, List<Tuple<String, Double>>> toreturn = new Dictionary<Double, List<Tuple<String, Double>>>();

            string con = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", filename);
            using (OleDbConnection connection = new OleDbConnection(con))
            {

                try
                {
                    //open the connection to the db query
                    connection.Open();
                }
                catch (OleDbException exception)
                {
                    return null;
                }

                //Open the first document
                OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [datasets_log$]", connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "label");
                var data = ds.Tables["label"].AsEnumerable();

                //get necessary data from table

                //get the maximum index value, which corresponds to the total number of days
                Double maximumIndex = data.Max(x => x.Field<Double>("index"));

                //for each day, get all the items associated with that day
                for (int day = 0; day <= maximumIndex; day++)
                {
                    EnumerableRowCollection query = data.Where(x => x.Field<Double>("index") == day);
                    foreach (DataRow result in query)
                    {
                        var arrayOfItems = result.ItemArray;
                        String variableName = Convert.ToString(arrayOfItems[2]);
                        Double variableValue = Convert.ToDouble(arrayOfItems[5]);
                        //if list doesn't exist, create it
                        if (!toreturn.ContainsKey(day))
                        {
                            toreturn.Add(day, new List<Tuple<String, Double>>());
                        }
                        //add item to the list at that index
                        toreturn[day].Add(new Tuple<String, Double>(variableName, variableValue));
                    }
                }
            }

            return toreturn;
        }


        /// <summary>
        ///     Get the parameters from the filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static List<Tuple<String, Double>> ReadParams(String filename)
        {
            List<Tuple<String, Double>> toreturn = new List<Tuple<String, Double>>();

            string con = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", filename);
            using (OleDbConnection connection = new OleDbConnection(con))
            {

                try
                {
                    //open the connection to the db query
                    connection.Open();
                }
                catch (OleDbException exception)
                {
                    return null;
                }

                //Open the first document
                OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [agent_parameters_log$]", connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "label");
                var data = ds.Tables["label"].AsEnumerable();

                foreach (DataRow result in data)
                {
                    var arrayOfItems = result.ItemArray;
                    String variableName = Convert.ToString(arrayOfItems[2]);
                    Double variableValue = Convert.ToDouble(arrayOfItems[3]);
                    toreturn.Add(new Tuple<String, Double>(variableName, variableValue));
                }
            }

            return toreturn;
        }
    }
}
