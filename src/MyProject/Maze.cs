using System;
using System.Collections.Generic;
using System.IO;

namespace Tubes2_duaAjaCukup{
class Maze
{
    private readonly char[,] _maze;
    private readonly int _size;

    public Maze(int size)
    {
        _size = size;
        _maze = new char[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                _maze[i, j] = 'x';
            }
        }
    }

    public void createMaze(string input)
    {
        string text = File.ReadAllText("../../doc/" + input +".txt");

        string[] rows = text.Split('\n');
        

        for (int i = 0; i < rows.Length; i++)
        {
            for (int j = 0; j < rows[0].Length; j++)
            {
                _maze[i, j] = rows[i][j];
            }
        }

        }
    

    public void PrintMaze()
{

    Console.WriteLine("Bentuk Maze: ");
    Console.WriteLine();

    for (int i = 0; i < _size; i++)
    {
        for (int j = 0; j < _size; j++)
        {
            Console.Write(_maze[i, j] + " ");
        }
        Console.WriteLine();
    }
}


}
}