using System;
using System.Collections.Generic;

namespace uburubur
{
    class BFS
    {
        private Queue<Tuple<Node, char>> queue;
        private int treasureFound;
        private List<Tuple<Node, char>> visited = new List<Tuple<Node, char>>();
        private List<Tuple<Node, char>> treasureFoundTuple;
        private List<List<Tuple<Node, char>>> tempListPath = new List<List<Tuple<Node, char>>>();
        private List<Tuple<Node, char>> treasureAdded = new List<Tuple<Node, char>>();
        private List<Tuple<Node, char>> path = new List<Tuple<Node, char>>();
        private List<Tuple<Node, int>> nodeSamePath = new List<Tuple<Node, int>>();
        public BFS()
        {
            queue = new Queue<Tuple<Node, char>>();
            treasureFound = 0;
            treasureFoundTuple = new List<Tuple<Node, char>>();

        }

        public void inqueue(Node node)
        {
            if (node.getLeft() != null && notVisited(node.getLeft()))
            {
                var tuple = new Tuple<Node, char>(node.getLeft(), 'L');
                queue.Enqueue(tuple);
            }

            if (node.getRight() != null && notVisited(node.getRight()))
            {
                var tuple = new Tuple<Node, char>(node.getRight(), 'R');
                queue.Enqueue(tuple);
            }

            if (node.getUp() != null && notVisited(node.getUp()))
            {
                var tuple = new Tuple<Node, char>(node.getUp(), 'U');
                queue.Enqueue(tuple);
            }

            if (node.getDown() != null && notVisited(node.getDown()))
            {
                var tuple = new Tuple<Node, char>(node.getDown(), 'D');
                queue.Enqueue(tuple);
            }
        }

        public void search(MazeGraph maze)
        {
            int starti = maze.getStart().getX();
            int startj = maze.getStart().getY();
            maze.setPosition(maze.getStart());
            inqueue(maze.getPosition());
            visited.Add(new Tuple<Node, char>(maze.getStart(), 'S'));

            while (queue.Count != 0 && treasureFound < maze.getTreasure())
            {
                var tuple = queue.Dequeue();
                if (tuple.Item1.getValue() == 'T')
                {
                    treasureFound++;
                    bool found = false;
                    foreach (var a in treasureFoundTuple)
                    {
                        if (a.Item1.getX() == tuple.Item1.getX() && a.Item1.getY() == tuple.Item1.getY())
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        treasureFoundTuple.Add(tuple);
                    }
                }
                Node temp = maze.FindNode(tuple.Item1.getX(), tuple.Item1.getY());
                maze.setPosition(temp);
                inqueue(maze.getPosition());
                if (notVisited(tuple.Item1))
                {
                visited.Add(tuple);
                }
            }
        }

        public List<Tuple<Node, char>> pathTreasure(Tuple<Node, char> treasureLoc)
        {
            int i = treasureLoc.Item1.getX();
            int j = treasureLoc.Item1.getY();
            List<Tuple<Node, char>> temppath = new List<Tuple<Node, char>>();
            visited.Reverse();
            bool found = false;
            char direction = ' ';
            while (!found)
            {
                for (int k = 0; k < visited.Count; k++)
                {
                    if (visited[k].Item1.getX() == i && visited[k].Item1.getY() == j)
                    {
                        if (visited[k].Item1.getValue() == 'T')
                        {
                            treasureAdded.Add(visited[k]);
                        }
                        if (visited[k].Item1.getValue() == 'K')
                        {
                            found = true;
                            break;
                        }
                        else
                        {
                            temppath.Add(visited[k]);
                        }
                        direction = visited[k].Item2;
                        switch (direction)
                        {
                            case 'U':
                                i++;
                                break;
                            case 'D':
                                i--;
                                break;
                            case 'L':
                                j++;
                                break;
                            case 'R':
                                j--;
                                break;
                        }
                    }
                }
            }
            visited.Reverse();
            return temppath;
        }
        public bool notAdded(Node node)
        {
            foreach (var treasureNode in treasureAdded)
            {
                if (treasureNode.Item1.getX() == node.getX() && treasureNode.Item1.getY() == node.getY())
                {
                    return false;
                }
            }
            return true;
        }
        public void findPath(int idx)
        {
            if (idx != -1)
            {
                if (notAdded(treasureFoundTuple[idx].Item1))
                {
                    if (idx + 1 == treasureFoundTuple.Count)
                    {

                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0, 0, '0'), '0');
                        for (int i = 0; i < visited.Count; i++)
                        {
                            if (visited[i].Item1.getX() == x && visited[i].Item1.getY() == y)
                            {
                                flagNode = visited[i];
                                break;
                            }
                        }
                        List<Tuple<Node, char>> tempPath = pathTreasure(flagNode);
                        tempListPath.Add(tempPath);
                        findPath(idx - 1);
                    }

                    else if (idx > 0)
                    {
                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0, 0, '0'), '0');
                        for (int i = 0; i < visited.Count; i++)
                        {
                            if (visited[i].Item1.getX() == x && visited[i].Item1.getY() == y)
                            {
                                flagNode = visited[i];
                                break;
                            }
                        }
                        List<Tuple<Node, char>> tempPath = pathTreasure(flagNode);
                        tempListPath.Add(tempPath);
                        findPath(idx - 1);
                    }

                    else if (idx == 0)
                    {
                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0, 0, '0'), '0');
                        for (int i = 0; i < visited.Count; i++)
                        {
                            if (visited[i].Item1.getX() == x && visited[i].Item1.getY() == y)
                            {
                                flagNode = visited[i];
                                break;
                            }
                        }
                        List<Tuple<Node, char>> tempPath = pathTreasure(flagNode);
                        tempListPath.Add(tempPath);
                        
                    }
                }

                else
                {
                    findPath(idx - 1);
                }
            }
        }

        public bool notVisited(Node node)
        {
            foreach (var tuple in visited)
            {
                if (tuple.Item1.getX() == node.getX() && tuple.Item1.getY() == node.getY())
                {
                    return false;
                }
            }
            return true;
        }

        public void makePath()
        {
            tempListPath.Reverse();
            

            for (int i = 0; i < tempListPath.Count - 1; i++)
            {
                for (int j = 0; j < tempListPath[i].Count; j++)
                {
                    bool cek = false;
                    for (int k = 0; k < tempListPath[i + 1].Count; k++)
                    {
                        if (tempListPath[i][j].Item1.getX() == tempListPath[i + 1][k].Item1.getX()
                        && tempListPath[i][j].Item1.getY() == tempListPath[i + 1][k].Item1.getY())
                        {
                            nodeSamePath.Add(new Tuple<Node, int>(tempListPath[i][j].Item1, i));
                            cek = true;
                            break;
                        }

                        
                    }
                    if (cek) break;
                }
                tempListPath.Reverse();
                
            }
        }

        public List<Tuple<Node, char>> getPath()
        {
            return path;
        }

        public List<Tuple<Node, char>> getVisited()
        {
            return visited;
        }
        public void savePath()
        {

            tempListPath.Reverse();

            for (int i = 0; i < tempListPath.Count; i++)
            {
                // i akhir
                if (i == tempListPath.Count - 1)
                {
                    tempListPath[i].Reverse();
                    bool cek1 = false;
                    bool cek = false;
                    int idxnodesame = -1;
                    foreach (var a in nodeSamePath)
                    {
                        idxnodesame++;
                        if (a.Item2 == i - 1)
                        {
                            cek = true;
                            break;
                        }
                    }
                    if (cek)
                    {
                        for (int j = 0; j < tempListPath[i].Count; j++)
                        {
                            if (tempListPath[i][j].Item1.getX() == nodeSamePath[idxnodesame].Item1.getX()
                            && tempListPath[i][j].Item1.getY() == nodeSamePath[idxnodesame].Item1.getY())
                            {
                                cek1 = true;

                            }
                            else if (cek1)
                            {
                                path.Add(tempListPath[i][j]);
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < tempListPath[i].Count; j++)
                        {
                            path.Add(tempListPath[i][j]);
                        }
                    }
                    tempListPath[i].Reverse();
                }
                // i awal
                else if (i == 0)
                {
                    tempListPath[i].Reverse();
                    foreach (var a in tempListPath[i])
                    {
                        path.Add(a);
                    }
                    tempListPath[i].Reverse();
                    bool cek = false;
                    int idxnodesame = -1;
                    foreach (var a in nodeSamePath)
                    {
                        idxnodesame = idxnodesame + 1;
                        if (a.Item2 == i)
                        {
                            cek = true;
                            break;
                        }
                    }


                    if (cek)
                    {
                        for (int j = 1; j < tempListPath[i].Count; j++)
                        {

                            if (tempListPath[i][j].Item1.getX() == nodeSamePath[idxnodesame].Item1.getX()
                            && tempListPath[i][j].Item1.getY() == nodeSamePath[idxnodesame].Item1.getY())
                            {
                                if (tempListPath[i][j - 1].Item2 == 'L')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'R');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'R')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'L');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'U')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'D');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'D')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'U');
                                    path.Add(a);
                                }

                                break;
                                
                            }
                            else
                            {
                                if (tempListPath[i][j - 1].Item2 == 'L')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'R');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'R')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'L');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'U')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'D');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'D')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'U');
                                    path.Add(a);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 1; j < tempListPath[i].Count; j++)
                        {
                            if (tempListPath[i][j - 1].Item2 == 'L')
                            {
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'R');
                                path.Add(a);
                            }
                            else if (tempListPath[i][j - 1].Item2 == 'R')
                            {
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'L');
                                path.Add(a);
                            }
                            else if (tempListPath[i][j - 1].Item2 == 'U')
                            {
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'D');
                                path.Add(a);
                            }
                            else if (tempListPath[i][j - 1].Item2 == 'D')
                            {
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'U');
                                path.Add(a);
                            }
                        }
                        if (path[path.Count - 1].Item2 == 'L')
                        {
                            Tuple<Node, char> a = new Tuple<Node, char>(new Node(path[path.Count - 1].Item1.getX(), path[path.Count - 1].Item1.getY() + 1, 'S'), 'R');
                            path.Add(a);
                        }
                        else if (path[path.Count - 1].Item2 == 'R')
                        {
                            Tuple<Node, char> a = new Tuple<Node, char>(new Node(path[path.Count - 1].Item1.getX(), path[path.Count - 1].Item1.getY() - 1, 'S'), 'L');
                            path.Add(a);
                        }
                        else if (path[path.Count - 1].Item2 == 'U')
                        {
                            Tuple<Node, char> a = new Tuple<Node, char>(new Node(path[path.Count - 1].Item1.getX() + 1, path[path.Count - 1].Item1.getY(), 'S'), 'D');
                            path.Add(a);
                        }
                        else if (path[path.Count - 1].Item2 == 'D')
                        {
                            Tuple<Node, char> a = new Tuple<Node, char>(new Node(path[path.Count - 1].Item1.getX() - 1, path[path.Count - 1].Item1.getY(), 'S'), 'U');
                            path.Add(a);
                        }
                    }
                    
                }


                // i tengah tengah
                else
                {
                    tempListPath[i].Reverse();
                    bool cek1 = false;
                    bool ceka = false;
                    bool cekb = false;
                    int idxnodesame1 = -1;
                    int idxnodesame2 = -1;
                    foreach (var a in nodeSamePath)
                    {
                        idxnodesame1++;
                        if (a.Item2 == i - 1)
                        {
                            ceka = true;
                            break;
                        }
                    }
                    foreach (var a in nodeSamePath)
                    {
                        idxnodesame2++;
                        if (a.Item2 == i)
                        {
                            cekb = true;
                            break;
                        }
                    }

                    if (ceka)
                    {
                        for (int j = 0; j < tempListPath[i].Count; j++)
                        {
                            if (tempListPath[i][j].Item1.getX() == nodeSamePath[idxnodesame1].Item1.getX()
                             && tempListPath[i][j].Item1.getY() == nodeSamePath[idxnodesame1].Item1.getY())
                            {
                                cek1 = true;
                            }
                            else if (cek1)
                            {
                                path.Add(tempListPath[i][j]);
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < tempListPath[i].Count; j++)
                        {
                            path.Add(tempListPath[i][j]);
                        }
                    }

                    tempListPath[i].Reverse();

                    if (cekb)
                    {
                        for (int j = 1; j < tempListPath[i].Count; j++)
                        {
                            if (tempListPath[i][j].Item1.getX() == nodeSamePath[idxnodesame2].Item1.getX()
                            && tempListPath[i][j].Item1.getY() == nodeSamePath[idxnodesame2].Item1.getY())
                            {
                                if (tempListPath[i][j - 1].Item2 == 'L')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'R');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'R')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'L');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'U')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'D');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'D')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'U');
                                    path.Add(a);
                                }
                                break;
                            }
                            else
                            {
                                if (tempListPath[i][j - 1].Item2 == 'L')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'R');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'R')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'L');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'U')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'D');
                                    path.Add(a);
                                }
                                else if (tempListPath[i][j - 1].Item2 == 'D')
                                {
                                    Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'U');
                                    path.Add(a);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 1; j < tempListPath[i].Count; j++)
                        {
                            if (tempListPath[i][j - 1].Item2 == 'L')
                            {
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'R');
                                path.Add(a);

                            }
                            else if (tempListPath[i][j - 1].Item2 == 'R')
                            {
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'L');
                                path.Add(a);
                            }
                            else if (tempListPath[i][j - 1].Item2 == 'U')
                            {
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'D');
                                path.Add(a);
                            }
                            else if (tempListPath[i][j - 1].Item2 == 'D')
                            {
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'U');
                                path.Add(a);
                            }
                        }
                        tempListPath[i].Reverse();
                        if (tempListPath[i][0].Item2 == 'L')
                        {
                            Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][0].Item1.getX(), tempListPath[i][0].Item1.getY() + 1, 'S'), 'R');
                            path.Add(a);

                        }
                        else if (tempListPath[i][0].Item2 == 'R')
                        {
                            Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][0].Item1.getX(), tempListPath[i][0].Item1.getY() - 1, 'S'), 'L');
                            path.Add(a);
                        }
                        else if (tempListPath[i][0].Item2 == 'U')
                        {
                            Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][0].Item1.getX() + 1, tempListPath[i][0].Item1.getY(), 'S'), 'D');
                            path.Add(a);
                        }
                        else if (tempListPath[i][0].Item2 == 'D')
                        {
                            Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][0].Item1.getX() - 1, tempListPath[i][0].Item1.getY(), 'S'), 'U');
                            path.Add(a);
                        }
                    }
                }
            }
        }
        public void deleteDuplicate()
        {
            int i = 0;
            for (int j = 0; j < visited.Count; j++) 
            {
                if (i > 0)
                {
                    if (visited[j].Item1 == null) { }
                }
            }
        }
    }
}