using System;
using System.Collections.Generic;   
using System.IO;

namespace uburubur{
class MazeGraph{
    private int width;
    private int height;
    private char[,] maze;
    private List<Node> nodes;
    private int treasure;
    private Node start;
    private Node position;

    public MazeGraph(){
        // this.width = x;
        // this.height = x;
        // this.maze = new char[x,x];
        this.treasure = 0;
        this.nodes = new List<Node>();
    }

    public void readfile(string fileName)
    {
        string[] rows = null;

            
        string path = $"{fileName}";
        rows = File.ReadAllLines(path);
        int tcount = 0;
        int kcount = 0;
        for (int i = 0 ; i < rows.Length; i++){
                    for (int j = 0; j < rows[0].Length; j++){
                        if (rows[i][j] == 'K'){
                            kcount++;
                        }
                        else if(rows[i][j] == 'R'){
                        }
                        else if(rows[i][j] == 'T'){
                        tcount++;
                        }
                        else if(rows[i][j] == ' '){
                        }
                        else if(rows[i][j] == 'X'){
                        }
                        else{
                            throw new Exception("Invalid Character Found: " + rows[i][j]);
                        }
                    }
                }
        if (kcount != 1)
            {
                throw new Exception("Multiple Starting Point Detected");
            }
        if (tcount < 1) 
            {
                throw new Exception("No Treasure Found in Maze");
            }



        this.height = rows.Length;
        this.width = rows[0].Length/2 + 1;
        this.maze = new char [this.height, this.width];
        int a = 0;
        for (int i = 0; i < rows.Length; i++)
        {
                int b = 0;
            for (int j = 0; j < rows[0].Length; j++)
            {
                if (rows[i][j] != ' '){
                maze[a,b] = rows[i][j];
                    if (rows[i][j] == 'T'){
                        treasure++;
                    }
                b++;
                }
            }
            a++;
        }
    }

    public void findStart(){
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (maze[i,j] == 'K')
                {
                    start = new Node(i,j,'K');
                    position = start;
                    nodes.Add(start);
                }
            }
        }
    }

    public void createlink()
    {
            int tnodes = 0;
            int knodes = 0;
            int x = position.getX();
        int y = position.getY();
        Node temp = position;


        if (y > 0)
        {
            if (maze[x,y-1] != 'X')
            {
                position.setLeft(new Node(x,y-1,maze[x,y-1]));
                    if (maze[x, y-1] == 'T')
                    {
                        tnodes++;
                    }

                }
        }
        if (y < width-1)
        {
            if (maze[x,y+1] != 'X')
            {
                position.setRight(new Node(x,y+1,maze[x,y+1]));
                    if (maze[x,y+1] == 'T')
                    {
                        tnodes++;
                    }

                }
        }
        if (x > 0)
        {
            if (maze[x-1,y] != 'X')
            {
                position.setUp(new Node(x-1,y,maze[x-1,y]));
                    if (maze[x-1,y] == 'T')
                    {
                        tnodes++;
                    }
                
            }
        }
        if (x < height-1)
        {
            if (maze[x+1,y] != 'X')
            {
                position.setDown(new Node(x+1,y,maze[x+1,y]));
                if (maze[x+1,y] == 'T')
                    {
                        tnodes++;
                    }
                
            }
        }
        if (position.getLeft() != null)
        {
            if (notinNodes(position.getLeft()))
            {
                nodes.Add(position.getLeft());
                position = position.getLeft();
                createlink();
                position = temp;
            }
        }
        if (position.getRight() != null)
        {
            if (notinNodes(position.getRight()))
            {
                nodes.Add(position.getRight());
                position = position.getRight();
                createlink();
                position = temp;
            }
        }
        if (position.getUp() != null)
        {
            if (notinNodes(position.getUp()))
            {
                nodes.Add(position.getUp());
                position = position.getUp();
                createlink();
                position = temp;
            }
            
        }
        
        if (position.getDown() != null)
        {
            if (notinNodes(position.getDown()))
            {
                nodes.Add(position.getDown());
                position = position.getDown();
                createlink();
                position = temp;
            }
        }
       
        foreach (Node node in nodes)
            {
                if(node.getValue() == 'K')
                {
                    if (node.getLeft() == null && node.getRight() == null && node.getUp() == null && node.getDown() == null)
                    {
                    throw new Exception("Cannot find path from Starting Point");
                    }
                    knodes++;
                }
                if (node.getValue() == 'T')
                {
                    if (node.getLeft() == null && node.getRight() == null && node.getUp() == null && node.getDown() == null)
                    {
                    throw new Exception("Cannot find path to Treasuress");
                    }
                    tnodes++;
                }
                

            }
    }

    public bool notinNodes(Node node)
    {
        foreach (Node n in nodes)
        {
            if (n.getX() == node.getX() && n.getY() == node.getY())
            {
                return false;
            }
        }
        return true;
    }


    public Node getStart()
    {
        return start;
    }

    public Node getPosition()
    {
        return position;
    }

    public void setPosition(Node node)
    {
        position = node;
    }

    public int getTreasure()
    {
        return treasure;
    }
    public List<Node> getNodes()
    {
        return nodes;
    }
    public Node FindNode(int x, int y)
    {
        foreach (Node n in nodes)
        {
            if (n.getX() == x && n.getY() == y)
            {
                return n;
            }
        }
        return null;
    }

    public int getHeight()
        {
            return height;
        }
        public int getWidth()
        {
            return width;
        }

}
}
