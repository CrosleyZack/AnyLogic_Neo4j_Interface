# AnyLogic_Neo4j_Interface

Middle management system between the AnyLogic model and the Microsoft HoloLens. User selects the ALP file and the Excel output
file from the AnyLogic model which will be parsed into the Neo4j database. The program uploads the full ALP file to the graph
and adds data from the datasets_log and agent_parameters_log of the excel file. Parameter names and values are stored (columns
3 and 4 of agent_parameters_log) and dataset index (time identifier), name, and value (y) are added (columns 4, 3, and 6 of 
datasets_log respectively).

The program also has a checkbox to indicate if the user wants to clear any existing database items before parsing in the AnyLogic
data. This is for readability and preventing duplicate, conflicting data.

#Neo4j

User must add the the connection string to accesses the Neo4j database using the Bolt protocol. Users should install Neo4j from
https://neo4j.com/download/ onto their computer. This should automatically configure the Bolt protocol endpoint, however that
endpoing will only be visible to the localhost. To enable open port visibility, the user must find and edit the neo4j.conf file.
The neo4j.conf file can be located at <User Folder>\AppData\Roaming\Neo4j Community Edition. In the file, users should replace the
following lines:

1. "localhost" on line 17 should be replaced with your computers ip address. This line should then look like:
dbms.connectors.default_listen_address=###.###.###.### with the appropriate ip address for your machine. NOTE: do not use 
localhost ip 127.0.0.1, but the visible ipv4 address. To find this, open commandline and type "ipconfig".

2. Lines 33 and 34 under "Bolt connector" should be uncommented, appearing as follow:
  dbms.connector.bolt.enabled=true
  dbms.connector.bolt.tls_level=OPTIONAL
  
3. Line 35 should have the listen_address for the bolt connector added. This is your ip address you found in step 1, port number 7687.
   When setup properly, the line should appear as follows:
   dbms.connector.bolt.listen_address=###.###.###.###:7687
   
Once those changes are made, save the file and restart Neo4j. You should now be able to access this graph from any computer using that
IP address.

Neo4j has default username and password "neo4j" on start, it should ask you to select a new password. Currently, the username and password
are hard coded into the middle-management system as "neo4j" and "password" respectively. Later iterations can add fields for these as
needed.
