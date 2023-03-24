using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using uburubur;

namespace UburUbur
{
    public partial class Form1 : Form
    {
        private static bool checkBFS;
        private static bool checkDFS;
        private MazeGraph graph;
        private int speed;
        private List<char> steps;
        private long exetime;
        private string fileName;
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
                fileName = openFileDialog.FileName;

                // Console.Write("Enter your file: ");
                // string name;
                // name = Console.ReadLine();
                try
                {
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
                catch (Exception err)
                { 
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBox.Show(err.Message, "Invalid File", button, MessageBoxIcon.Error);
                }
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
            try
            {
                if(fileName == null)
                {
                    throw new Exception("Please Input File First");
                }
                if(checkBFS ==false && checkDFS == false)
                {
                    throw new Exception("Please Select Algorithm First");
                }
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
            if (checkDFS)
            {
                Stopwatch sw = Stopwatch.StartNew();
                DFS route = new DFS();
                route.DFSsearch(graph);
                List<Node> visited = route.getVisited();
                sw.Stop();
                exetime = sw.ElapsedMilliseconds;
                route.reversePath();
                List<Node> path = route.getPath();
                route.makeSteps();
                steps =route.getSteps();
                foreach (Node node in path)
                {
                    await Task.Delay(speed);
                    if (dataGridView1.Rows[node.getX()].Cells[node.getY()].Style.BackColor == Color.LightGreen)
                    {
                        dataGridView1.Rows[node.getX()].Cells[node.getY()].Style.BackColor = Color.Green;
                    }
                    else
                    {
                    dataGridView1.Rows[node.getX()].Cells[node.getY()].Style.BackColor = Color.LightGreen;

                    }
                }
                label3.Text = "";
                foreach (var Char in steps)
                {
                    
                        label3.Text += Char + " - ";
                    
                }
                label6.Text = Convert.ToString(exetime + " ms");
                label8.Text = Convert.ToString(visited.Count);


                }
            if (checkBFS)
            {
                BFS route = new BFS();
                Stopwatch sw = Stopwatch.StartNew();
                route.search(graph);
                sw.Stop();
                exetime = sw.ElapsedMilliseconds;
                List <Tuple<Node, char>> visited = new List<Tuple<Node, char>>();
                dataGridView1.Rows[graph.getStart().getX()].Cells[graph.getStart().getY()].Style.BackColor = Color.LightBlue;
                visited = route.getVisited();
                int i = 0;
                for (int j = 0; i < visited.Count; j++)
                {
                    await Task.Delay(speed);
                    dataGridView1.Rows[visited[j].Item1.getX()].Cells[visited[j].Item1.getY()].Style.BackColor = Color.LightBlue;
                    if (i == 0)
                    {
                        dataGridView1.Rows[graph.getStart().getX()].Cells[graph.getStart().getY()].Style.BackColor = Color.Yellow;
                    }
                    if (i > 0)
                    {
                        dataGridView1.Rows[visited[j-1].Item1.getX()].Cells[visited[j-1].Item1.getY()].Style.BackColor = Color.Yellow;
                    }
                    i++;
                    
                }
                label8.Text = Convert.ToString(visited.Count);
                label6.Text = Convert.ToString(exetime) + " ms";
                route.findPath(graph.getTreasure() - 1);
                route.makePath();
                route.savePath();
                dataGridView1.Rows[graph.getStart().getX()].Cells[graph.getStart().getY()].Style.BackColor = Color.LightGreen;
                List <Tuple<Node, char>> path = route.getPath();
                foreach (var Tuple in path)
                {
                    await Task.Delay(speed);
                    if (dataGridView1.Rows[Tuple.Item1.getX()].Cells[Tuple.Item1.getY()].Style.BackColor == Color.LightGreen)
                    {
                        dataGridView1.Rows[Tuple.Item1.getX()].Cells[Tuple.Item1.getY()].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        dataGridView1.Rows[Tuple.Item1.getX()].Cells[Tuple.Item1.getY()].Style.BackColor = Color.LightGreen;
                    }
                }
                label3.Text = "";
                foreach (var Tuple in path)
                {

                    label3.Text += Tuple.Item2 + " - ";

                }
            }
            }
            catch (Exception err)
            {
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBox.Show(err.Message, "Invalid File", button, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
            int temp = Convert.ToInt32(textBox1.Text);
            if (temp >= 0 && temp <= 1000)
            {
                speed = temp;
                trackBar1.Value = speed/100;
            }
            else
            {
                speed = 500;
                trackBar1.Value = 5;
                textBox1.Text = "500";
            }

            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(trackBar1.Value*100);
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
