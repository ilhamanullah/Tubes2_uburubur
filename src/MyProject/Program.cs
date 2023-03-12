using System;
using uburubur;

namespace MyApplication
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Maze maze = new Maze(4); // Create a new instance of the Maze class
            Console.Write("Enter your file: ");
            // string name;
            // name = Console.ReadLine();

            maze.createMaze("file"); // Load the maze from file
            maze.PrintMaze(); // Print the maze to the console
            maze.findStart();
            BFS queue = new BFS();
            queue.search(maze);
            Console.WriteLine();
            Console.WriteLine("Queue BFS: ");
            queue.printVisited();

            DFS stack = new DFS();
            stack.search(maze);
            Console.WriteLine();
            Console.WriteLine("Stack DFS: ");
            stack.printVisited();
            
        }   
    }
}
