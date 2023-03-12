using System;
using Tubes2_duaAjaCukup;

namespace MyApplication
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Maze maze = new Maze(4); // Create a new instance of the Maze class
            Console.Write("Enter your file: ");
            string name;
            name = Console.ReadLine();

            maze.createMaze(name); // Load the maze from file
            maze.PrintMaze(); // Print the maze to the console
        }
    }
}
