﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Text.RegularExpressions;


namespace AnyLogic_Neo4j_Interface
{
    public partial class Form1 : Form
    {

        private Graph graph;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            graph = Graph.GetInstance();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //current controls

        /// <summary>
        ///     On submit, try to parse files into database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitBtn_Click(object sender, EventArgs e)
        {
            //if any unsolved input errors exist, back out
            if(!String.IsNullOrEmpty(alpErrorProvider.GetError(alpFileSelectedViewer))
                || !String.IsNullOrEmpty(excelErrorProvider.GetError(exlFileSelectedViewer))
                || !String.IsNullOrEmpty(serverErrorProvider.GetError(txtServerAddress))) {
                outputWindow.Text = "ERROR: You must clear all other errors before the program will run.";
                return;
            }

            //connection string initialization, attempt database connection before any parsing occurs
            if(!graph.SetupConnection(txtServerAddress.Text, username: usernameTxt.Text, password: passwordTxt.Text)) {
                outputWindow.Text = "Connection to the Neo4j server could not be established. Please try again.";
                return;
            }

            //validate server connection was successful
            if(!graph.TestConnection())
            {
                serverErrorProvider.SetError(txtServerAddress, @"Invalid URI, bolt://###.###.###.###:####");
                outputWindow.Text = "There is no Neo4j database at the address specified.";
                return;
            }

            outputWindow.Text = "Connection to Neo4j established...\n";

            //empty out old data from graph
            if (chkbxClearGraph.Checked)
            {
                if (!graph.EmptyGraph())
                {
                    outputWindow.Text = "Could not empty the graph. Please try again.";
                    return;
                }

                outputWindow.Text += "Neo4j Graph Emptied...\n";
            }

            //We don't need to get the anything from the alp file, just copy the text over
            String alpFile = Parser.FileToString(alpFileSelectedViewer.Text);

            //create data structures to store dataset logs and parameter logs
            Dictionary<Double, List<Tuple<String, Double>>> datalogDictionary = Parser.ReadDataLog(exlFileSelectedViewer.Text);
            if(datalogDictionary == null)
            {
                outputWindow.Text = "ERROR: The specified excel file could not be parsed. Please close any programs with the file open and try again.";
                return;
            }
            //
            List<Tuple<String, Double>> initialParams = Parser.ReadParams(exlFileSelectedViewer.Text);
            if(initialParams == null)
            {
                outputWindow.Text = "ERROR: The specified excel file could not be parsed. Please close any programs with the file open and try again.";
                return;
            }

            //add alp file first
            if (!graph.AddAlpFile(alpFile))
            {
                outputWindow.Text = "ERROR: Failed to add a alp file to the graph. Please try again.";
                return;
            }

            outputWindow.Text += "ALP File Added to Neo4j...\n";

            //add initial parameters to to neo4j database
            foreach (Tuple<String, Double> item in initialParams)
            {
               if(!graph.AddParameterNode(item.Item1, item.Item2))
                {
                    outputWindow.Text = "ERROR: Failed to add a parameter to the graph. Please try again.";
                    return;
                }
            }

            outputWindow.Text += "Initial Params parsed into Neo4j...\n";

            foreach (Double key in datalogDictionary.Keys)
            {

                //for each datum at that day, add it to the database
                List<Tuple<String, Double>> dataAtKey = datalogDictionary[key];
                foreach(Tuple<String,Double> item in dataAtKey)
                {
                    if(!graph.AddVariableNode(key, item.Item1, item.Item2))
                    {
                        outputWindow.Text = "ERROR: Failed to add a parameter to the graph. Please try again.";
                        return;
                    }
                }
            }

            //if we got this far, everything worked fine. Output a success message.
            outputWindow.Text = "Success, all data loaded into Neo4j! Check the browser client to view your graph.";
        }

        /// <summary>
        /// Handle alp file upload clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alpFileUploadBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                if(Path.GetExtension(file) == ".alp")
                {
                    //clear any error
                    alpErrorProvider.Clear();
                    alpFileSelectedViewer.ForeColor = Color.Black;
                    alpFileSelectedViewer.Text = openFileDialog1.FileName;
                }
                else
                {
                    alpFileSelectedViewer.ForeColor = Color.Red;
                    alpFileSelectedViewer.Text = "The file selected must be an .alp file. Please try again.";
                    alpErrorProvider.SetError(alpFileSelectedViewer, "The file selected must be an .alp file. Please try again.");
                }
            }
            else
            {
                alpFileSelectedViewer.ForeColor = Color.Red;
                alpFileSelectedViewer.Text = "An Error Occured While Opening the AnyLogic File. Please Try Again.";
                alpErrorProvider.SetError(alpFileSelectedViewer, "An Error Occured While Opening the AnyLogic File. Please Try Again.");
            }
        }

        /// <summary>
        /// handle excel file upload clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void excelFileUploadBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                if (Path.GetExtension(file) == ".xlsx")
                {
                    //clear any error
                    excelErrorProvider.Clear();
                    exlFileSelectedViewer.ForeColor = Color.Black;
                    exlFileSelectedViewer.Text = openFileDialog1.FileName;
                }
                else
                {
                    exlFileSelectedViewer.ForeColor = Color.Red;
                    exlFileSelectedViewer.Text = "The File Selected is not a .xlxs file. Please Select an Excel File.";
                    excelErrorProvider.SetError(exlFileSelectedViewer, "The File Selected is not a .xlxs file. Please Select an Excel File.");
                }
            }
            else
            {
                exlFileSelectedViewer.ForeColor = Color.Red;
                exlFileSelectedViewer.Text = "An Error Occured While Opening the Excel File. Please Try Again.";
                excelErrorProvider.SetError(exlFileSelectedViewer, "An Error Occured While Opening the Excel File. Please Try Again.");
            }
        }

        /// <summary>
        /// verify valid neo4j connection string format on leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtServerAddress_Leave(object sender, EventArgs e)
        {
            String target_uri = txtServerAddress.Text;

            //MATCH target_uri to bolt:// + d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3} + : + d{4}
            Match b = Regex.Match(target_uri, @"^bolt:\/\/\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}:\d{1,4}$"); //
            if (!b.Success)
            {
                serverErrorProvider.SetError(txtServerAddress, @"Invalid URI, bolt://###.###.###.###:####");
                outputWindow.Text = "URI should be of form bolt://###.###.###.###:####";
            }
            else
            {
                serverErrorProvider.Clear();
                outputWindow.Text = String.Empty;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //These are all from older controls, leave them until you can figure out how to remove them

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void alpFileSelectedViewer_Click(object sender, EventArgs e)
        {
        }

        private void exlFileSelectedViewer_Click(object sender, EventArgs e)
        {
        }
    }
}
