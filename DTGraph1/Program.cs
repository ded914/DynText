using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

using Neo4jClient;
using Neo4jClient.Cypher;
using Neo4jClient.SchemaManager;

namespace DTGraph1 {
    class Program {
        public static void Main(string[] args) {
            DialogResult result = MessageBox.Show(
                "About to delete any exisiting Graph and build a new test graph",
                "Warning!",
                MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) {
                throw new Exception("User Cancelled Test");
            }

            //make sure Neo4J engine is running before opening the db
            //and that 'my password' is set by successfully navigating to the Uri
            //IGraphClient graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "Friday11");

            IGraphClient graphClient = new GraphClient(new Uri("http://hobby-ccenomhijildgbkeelnangol.dbs.graphenedb.com:24789/db/data/"), "DT1", "RMEPyBQljIuJF8bYNrSB");

            try {
                graphClient.Connect();
            } catch (AggregateException) {
                //****************To catch this exception*****************
                // go to the Debug Menu -Exceptions..
                //-Common Language Runtime Exceptions-System
                //then untick the System.AggregateException User-Unhandled box
                MessageBox.Show(
                    "Unable to connect to the Server\r\nTry Connecting from a browser\r\n to make sure you can log in",
                    "Error!",
                    MessageBoxButtons.OK);
                Application.Exit();
            }
            var neoHelper = new NeoTestHelper();
            Console.WriteLine("Building a test graph please wait.");
            neoHelper.DeleteGraph(graphClient);
            graphClient.DropAllIndexes();
            using (var scope = new TransactionScope()) // using System.Transactions;
            {
                neoHelper.BuildGraph(graphClient);
                scope.Complete();
            }
        }
    }
}
