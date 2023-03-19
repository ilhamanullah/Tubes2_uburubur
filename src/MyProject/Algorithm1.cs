using System;

namespace uburubur{
    class BFS {
        private Queue<Tuple<Node, char>> queue;
        private int treasureFound;
        private List<Tuple<Node, char>> visited = new List<Tuple<Node, char>>();
        private Tuple<Node, char> treasureFoundTuple;
        private List<Tuple<Node, char>> path = new List<Tuple<Node, char>>();
        public BFS(){
            queue = new Queue<Tuple<Node, char>>();
            treasureFound = 0;

        }

        public void inqueue (Node node){
            // Console.WriteLine("Starti: " + starti);
            // Console.WriteLine("Startj: " + startj);
            // Console.WriteLine(maze.getContent(1,3));
            if (node.getRight() != null && notVisited(node.getRight())){
                    var tuple = new Tuple<Node, char>(node.getRight(), 'R');
                    queue.Enqueue(tuple);
                    Console.WriteLine("a");
            }
            if (node.getLeft() != null && notVisited(node.getLeft())){
                    var tuple = new Tuple<Node, char>(node.getLeft(), 'L');
                    queue.Enqueue(tuple);
                    // Console.WriteLine("b");
            }
            if (node.getUp() != null && notVisited(node.getUp())){
                    var tuple = new Tuple<Node, char>(node.getUp(), 'U');
                    queue.Enqueue(tuple);
                    // Console.WriteLine("c");
            }
            if (node.getDown() != null && notVisited(node.getDown())){
                    var tuple = new Tuple<Node, char>(node.getDown(), 'D');
                    queue.Enqueue(tuple);
                    // Console.WriteLine("d");
            }
            
        }
        public void search (MazeGraph maze){
            int starti = maze.getStart().getX();
            int startj = maze.getStart().getY();
            maze.setPosition(maze.getStart());
            Console.WriteLine("Starti: " + starti);
            Console.WriteLine("Startj: " + startj);
            inqueue(maze.getPosition());
            visited.Add(new Tuple<Node, char>(maze.getStart(), 'S'));
            while (treasureFound < maze.getTreasure()){
                // Console.WriteLine(queue.Count());
                var tuple = queue.Dequeue();
                if (tuple.Item1.getValue() == 'T'){
                    treasureFound++;
                    treasureFoundTuple = tuple;
                }
                // Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
                maze.setPosition(tuple.Item1);
                inqueue(maze.getPosition());
                visited.Add(tuple);
                // queue.Dequeue();
            }
        }


        public void PrintQueue(){
            Queue<Tuple<Node, char>> temp = new Queue<Tuple<Node, char>>();
            temp = queue;
            while (temp.Count() != 0){
                var tuple = temp.Dequeue();
                Console.Write(tuple.Item1.getValue() + " " + tuple.Item1.getX() + " " + tuple.Item1.getY() + " " + tuple.Item2 + " ");
            }
            Console.WriteLine();
        }

        public void printVisited(){
            Console.WriteLine(visited.Count());
            foreach (var tuple in visited){
                Console.Write(tuple.Item1.getValue() + " " + tuple.Item1.getX() + " " + tuple.Item1.getY() + " " + tuple.Item2 + " ");
            Console.WriteLine();
        }
        }
        
        public void findPath(){
            int i = treasureFoundTuple.Item1.getX();
            int j = treasureFoundTuple.Item1.getY();
            visited.Reverse();
            bool found = false;
            char direction = ' ';
            // Console.WriteLine("Path: ");
            while (!found){
                // Console.Write(direction + " ");
                for(int k = 0; k < visited.Count(); k++){
                    if (visited[k].Item1.getX() == i && visited[k].Item1.getY() == j){
                        path.Add(visited[k]);
                // Console.WriteLine("KONTOL");
                        if (visited[k].Item1.getValue() == 'K'){
                            found = true;
                            break;
                        }
                        direction = visited[k].Item2;
                // Console.WriteLine("i: " + i);
                // Console.WriteLine("j: " + j);
                //         Console.WriteLine("Direction: " + direction);
                        switch (direction){
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
            path.Reverse();
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

        public void printPath()
        {
            foreach (var tuple in path){
                Console.WriteLine("Tuple: " + tuple.Item1.getValue() + " " + tuple.Item1.getX() + " " + tuple.Item1.getY() + " " + tuple.Item2);
            }
        }
    }

    class DFS {
        private Stack<Tuple<Node, char>> stack;
        private int treasureFound;
        private List<Tuple<Node, char>> visited = new List<Tuple<Node, char>>();
        private Tuple<Node, char> treasureFoundTuple;
        private List<Tuple<Node, char>> path = new List<Tuple<Node, char>>();
        public DFS(){
            stack = new Stack<Tuple<Node, char>>();
            treasureFound = 0;

        }

        public void inqueue (Node node){
            // Console.WriteLine("Starti: " + starti);
            // Console.WriteLine("Startj: " + startj);
            // Console.WriteLine(maze.getContent(1,3));
            if (node.getRight() != null && notVisited(node.getRight())){
                    var tuple = new Tuple<Node, char>(node.getRight(), 'R');
                    stack.Push(tuple);
                    Console.WriteLine("a");
            }
            if (node.getLeft() != null && notVisited(node.getLeft())){
                    var tuple = new Tuple<Node, char>(node.getLeft(), 'L');
                    stack.Push(tuple);
                    // Console.WriteLine("b");
            }
            if (node.getUp() != null && notVisited(node.getUp())){
                    var tuple = new Tuple<Node, char>(node.getUp(), 'U');
                    stack.Push(tuple);
                    // Console.WriteLine("c");
            }
            if (node.getDown() != null && notVisited(node.getDown())){
                    var tuple = new Tuple<Node, char>(node.getDown(), 'D');
                    stack.Push(tuple);
                    // Console.WriteLine("d");
            }
            
        }
        public void search (MazeGraph maze){
            int starti = maze.getStart().getX();
            int startj = maze.getStart().getY();
            maze.setPosition(maze.getStart());
            Console.WriteLine("Starti: " + starti);
            Console.WriteLine("Startj: " + startj);
            inqueue(maze.getPosition());
            visited.Add(new Tuple<Node, char>(maze.getStart(), 'S'));
            while (treasureFound < maze.getTreasure()){
                // Console.WriteLine(queue.Count());
                var tuple = stack.Pop();
                if (tuple.Item1.getValue() == 'T'){
                    treasureFound++;
                    treasureFoundTuple = tuple;
                }
                // Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
                maze.setPosition(tuple.Item1);
                inqueue(maze.getPosition());
                visited.Add(tuple);
                // queue.Dequeue();
            }
        }

        public void PrintStack(){
            Stack<Tuple<Node, char>> temp = new Stack<Tuple<Node, char>>();
            temp = stack;
            while (temp.Count() != 0){
                var tuple = temp.Pop();
                Console.Write(tuple.Item1.getValue() + " " + tuple.Item1.getX() + " " + tuple.Item1.getY() + " " + tuple.Item2 + " ");
            }
            Console.WriteLine();
        }

        public void printVisited(){
            Console.WriteLine(visited.Count());
            foreach (var tuple in visited){
                Console.Write(tuple.Item1.getValue() + " " + tuple.Item1.getX() + " " + tuple.Item1.getY() + " " + tuple.Item2 + " ");
            Console.WriteLine();
        }
        }

        public void findPath(){
            int i = treasureFoundTuple.Item1.getX();
            int j = treasureFoundTuple.Item1.getY();
            visited.Reverse();
            bool found = false;
            char direction = ' ';
            // Console.WriteLine("Path: ");
            while (!found){
                // Console.Write(direction + " ");
                for(int k = 0; k < visited.Count(); k++){
                    if (visited[k].Item1.getX() == i && visited[k].Item1.getY() == j){
                        path.Add(visited[k]);
                // Console.WriteLine("KONTOL");
                        if (visited[k].Item1.getValue() == 'K'){
                            found = true;
                            break;
                        }
                        direction = visited[k].Item2;
                // Console.WriteLine("i: " + i);
                // Console.WriteLine("j: " + j);
                //         Console.WriteLine("Direction: " + direction);
                        switch (direction){
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
            path.Reverse();
            }
        public void printPath(){
            foreach (var tuple in path){
                Console.WriteLine("Tuple: " + tuple.Item1.getValue() + " " + tuple.Item1.getX() + " " + tuple.Item1.getY() + " " + tuple.Item2);
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

    }
}