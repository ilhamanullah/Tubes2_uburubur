using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uburubur;

namespace UburUbur
{
    public partial class Form1 : Form
    {
        private static bool checkBFS;
        private static bool checkDFS;
        private MazeGraph graph;
        public Form1()
        {
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // The user selected a file, you can do something with it here
                string fileName = openFileDialog.FileName;

                // Console.Write("Enter your file: ");
                // string name;
                // name = Console.ReadLine();
                graph = new MazeGraph();
                graph.readfile(fileName);
                graph.findStart();
                graph.createlink();
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.ColumnHeadersVisible = false;
                dataGridView1.Enabled = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.ScrollBars = ScrollBars.None;
                dataGridView1.MultiSelect = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;


                dataGridView1.ColumnCount = graph.getWidth();
                dataGridView1.RowCount = graph.getHeight();

                int rowHeight = dataGridView1.ClientSize.Height / dataGridView1.RowCount;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Height = rowHeight;
                }

                dataGridView1.CurrentCell = null;

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Black;
                    }
                }
                dataGridView1.Rows[graph.getStart().getX()].Cells[graph.getStart().getY()].Style.BackColor = Color.Red;
                List<Node> nodes = new List<Node>();
                nodes = graph.getNodes();
                foreach (var node in nodes)
                {
                    if (node.getValue() == 'T')
                    {
                        dataGridView1.Rows[node.getX()].Cells[node.getY()].Style.BackColor = Color.Blue;
                    }
                    if (node.getValue() == 'R')
                    {
                        dataGridView1.Rows[node.getX()].Cells[node.getY()].Style.BackColor = Color.White;
                    }
          
                }

                // Set the data source of the DataGridView to the maze graph
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                checkBFS = true;
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (radioButton2.Checked) 
            {
                checkDFS = true;   
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (checkDFS)
            {
                DFS route = new DFS();
                route.DFSsearch(graph);
                route.reversePath();
                List<Node> path = new List<Node>();
                path = route.getPath();
                foreach (Node node in path)
                {
                    await Task.Delay(500);
                    if (dataGridView1.Rows[node.getX()].Cells[node.getY()].Style.BackColor == Color.LightGreen)
                    {
                        dataGridView1.Rows[node.getX()].Cells[node.getY()].Style.BackColor = Color.Green;
                    }
                    else
                    {
                    dataGridView1.Rows[node.getX()].Cells[node.getY()].Style.BackColor = Color.LightGreen;

                    }
                }
            }
            if (checkBFS)
            {
                BFS route = new BFS();
                route.findPath(graph.getTreasure() - 1);
                route.makePath();
                route.savePath();
                List <Tuple<Node, char>> path = route.getPath();
                foreach (var Tuple in path)
                {
                    await Task.Delay(500);
                    dataGridView1.Rows[Tuple.Item1.getX()].Cells[Tuple.Item1.getY()].Style.BackColor = Color.LightGreen;
                }
            }


        }
    }
}
