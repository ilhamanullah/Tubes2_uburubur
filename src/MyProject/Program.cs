using System;
using uburubur;

namespace MyApplication
{
    class MainClass
    {
        static void Main(string[] args)
        {
            MazeGraph maze = new MazeGraph();
            maze.readfile();
            maze.findStart();
            maze.createlink();
            // DFS dfs = new DFS();
            // dfs.DFSsearch(maze);
            // dfs.printVisited();
            BFS bfs = new BFS();
            bfs.search(maze);
            bfs.printVisited();

        }   
    }
}
