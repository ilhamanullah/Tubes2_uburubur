using System;
using uburubur;

namespace MyApplication
{
    class MainClass
    {
        static void Main(string[] args)
        {
            // Maze maze = new Maze(4); // Create a new instance of the Maze class
            // Console.Write("Enter your file: ");
            // string name;
            // name = Console.ReadLine();

            // maze.createMaze(name); // Load the maze from file
            // maze.PrintMaze(); // Print the maze to the console
            // maze.findStart();
            // BFS queue = new BFS();
            // queue.search(maze);
            // Console.WriteLine();
            // Console.WriteLine("Queue BFS: ");
            // queue.printVisited();

            // DFS stack = new DFS();
            // stack.search(maze);
            // Console.WriteLine();
            // Console.WriteLine("Stack DFS: ");
            // stack.printVisited();
            // stack.findPath();
            // stack.printPath();
            MazeGraph graph = new MazeGraph();
            // Console.Write("Enter your file: ");
            // string name;
            // name = Console.ReadLine();
            graph.readfile();
            graph.findStart();
            graph.createlink();
            // Console.WriteLine("--------");
            // graph.printNodes();
            // Console.WriteLine("--------");
            BFS queue = new BFS();
            queue.search(graph);
            Console.WriteLine();
            Console.WriteLine("Queue BFS: ");
            // queue.printVisited();
            queue.findPath(graph.getTreasure() - 1);
            Console.WriteLine("ROUTE :");
            queue.printPath();
            Console.WriteLine();

            DFS stack = new DFS();
            stack.search(graph);
            Console.WriteLine();
            Console.WriteLine("Stack DFS: ");
            // stack.printVisited();
            stack.findPath(graph.getTreasure() - 1);
            Console.WriteLine("ROUTE :");
            stack.printPath();
            // stack.printPath();
        }   
    }
}
