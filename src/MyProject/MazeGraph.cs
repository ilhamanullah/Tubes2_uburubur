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

    public MazeGraph(int x){
        this.width = x;
        this.height = x;
        this.maze = new char[x,x];
        this.treasure = 0;
        this.nodes = new List<Node>();
    }

    public void readfile()
    {
        bool valid = true;
            Console.Write("Enter your file: ");
            string name;
            name = Console.ReadLine();
        string path = $"../../test/{name}.txt";
        string[] rows = File.ReadAllLines(path);

        for (int i = 0 ; i < rows.Length; i++){
                    for (int j = 0; j < rows[0].Length; j++){
                        if (rows[i][j] != 'K' || rows[i][j] != 'X' || rows[i][j] != 'T' || rows[i][j] != ' ' || rows[i][j] != 'R'){
                            valid = false;
                        }
                    }
                }

        if (rows.Length*2-1 != rows[0].Length && !valid){
            Console.WriteLine("Invalid Size Maze");
            while (rows.Length*2-1 != rows[0].Length && valid == false)
            {
                Console.Write("Enter your file: ");
                name = Console.ReadLine();
                path = $"../../test/{name}.txt";
                rows = File.ReadAllLines(path);
                valid = true;
                for (int i = 0 ; i < rows.Length; i++){
                    for (int j = 0; j < rows[0].Length; j++){
                        if (rows[i][j] != 'K' || rows[i][j] != 'X' || rows[i][j] != 'T' || rows[i][j] != ' ' || rows[i][j] != 'R'){
                            valid = false;
                        }
                    }
                }
            }
        }
        
        height = rows.Length;
        width = rows[0].Length - height + 1;
        Console.WriteLine(height);
        Console.WriteLine(width);
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
                    // Console.WriteLine(a + " " + b);
                    // Console.WriteLine("i: " + i + " j: " + j);
        // Console.WriteLine("Masuk");
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
        int x = position.getX();
        int y = position.getY();
        Node temp = position;
        if (x > 0)
        {
            if (maze[x-1,y] != 'X')
            {
                position.setUp(new Node(x-1,y,maze[x-1,y]));
                
            }
        }
        if (x < height-1)
        {
            if (maze[x+1,y] != 'X')
            {
                position.setDown(new Node(x+1,y,maze[x+1,y]));
                
            }
        }
        if (y > 0)
        {
            if (maze[x,y-1] != 'X')
            {
                position.setLeft(new Node(x,y-1,maze[x,y-1]));
                
            }
        }
        if (y < width-1)
        {
            if (maze[x,y+1] != 'X')
            {
                position.setRight(new Node(x,y+1,maze[x,y+1]));
                
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
                Console.WriteLine("Left");
                nodes.Add(position.getDown());
                position = position.getDown();
                createlink();
                position = temp;
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
                // Console.WriteLine("Right");
                position = position.getRight();
                createlink();
                position = temp;
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

    public void printNodes()
    {
        foreach (Node n in nodes)
        {
            Console.WriteLine(n.getX() + " " + n.getY() + " " + n.getValue() + " Left: " + n.getLeft() + " Right: " + n.getRight() + " Up: " + n.getUp() + " Down: " + n.getDown());
            // if (n.getX()  == 1 && n.getY() == 2)
            // {
            //     position = n;
            // }
        }
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
    
}
}
