using System;
using System.Collections.Generic;
using System.IO;

namespace uburubur{
class Maze
{
    private readonly char[,] _maze;
    private readonly int _size;
    private int starti;
    private int startj;
    private int treasure;

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

public void findStart()
{
    for (int i = 0; i < _size; i++)
    {
        for (int j = 0; j < _size; j++)
        {
            if (_maze[i, j] == 'K')
            {
                starti = i;
                startj = j;
            }
        }
    }


}
public int getStarti()
{
    return starti;
}

public int getStartj()
{
    return startj;
}

public Maze getMaze()
{
    return this;
}

public char getContent(int i, int j)
{
    return _maze[i, j];
}

public void findTreasure(){
    for (int i = 0; i < _size; i++)
    {
        for (int j = 0; j < _size; j++)
        {
            if (_maze[i, j] == 'T')
            {
                treasure++;
            }
        }
    }
}

public int getTreasure(){
    return treasure;
}

}
}