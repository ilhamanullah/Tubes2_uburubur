using System;

namespace uburubur{
    class BFS {
        private Queue<Tuple<char, int, int, char>> queue;
        private int treasureFound;
        private List<Tuple<char, int, int, char>> visited = new List<Tuple<char, int, int, char>>();
        public BFS(){
            queue = new Queue<Tuple<char, int, int, char>>();
            treasureFound = 0;

        }

        public void inqueue (Maze maze, int starti, int startj){
            // Console.WriteLine("Starti: " + starti);
            // Console.WriteLine("Startj: " + startj);
            // Console.WriteLine(maze.getContent(1,3));
            if (startj + 1 < 4){
                if (maze.getContent(starti, startj + 1) == 'R' || maze.getContent(starti, startj + 1) == 'T'){
                    var tuple = new Tuple<char, int, int, char>(maze.getContent(starti, startj + 1), starti, startj + 1, 'R');
                    queue.Enqueue(tuple);
                    Console.WriteLine("a");
                }
            }
            // if (startj - 1 >= 0){
            //     if (maze.getContent(starti, startj - 1) == 'R' || maze.getContent(starti, startj - 1) == 'T'){
            //         var tuple = new Tuple<char, int, int, char>(maze.getContent(starti, startj - 1), starti, startj - 1, 'L');
            //         queue.Enqueue(tuple);
            //         Console.WriteLine("b");
            //     }
            // }
            if (starti + 1 < 4){
                if (maze.getContent(starti + 1, startj) == 'R' || maze.getContent(starti + 1, startj) == 'T'){
                    var tuple = new Tuple<char, int, int, char>(maze.getContent(starti + 1, startj), starti + 1, startj, 'D');
                    queue.Enqueue(tuple);
                    Console.WriteLine("b");
                }
            }
            // if (starti - 1 >= 0){
            //     if (maze.getContent(starti - 1, startj) == 'R' || maze.getContent(starti - 1, startj) == 'T'){
            //         var tuple = new Tuple<char, int, int, char>(maze.getContent(starti - 1, startj), starti - 1, startj, 'U');
            //         queue.Enqueue(tuple);
            //     }
            // }
            
        }
        public void search (Maze maze){
            int starti = maze.getStarti();
            int startj = maze.getStartj();
            Console.WriteLine("Starti: " + starti);
            Console.WriteLine("Startj: " + startj);
            inqueue(maze, starti, startj);
            for (int i = 0; i < 10; i++){
                Console.WriteLine(queue.Count());
                var tuple = queue.Dequeue();
                if (tuple.Item1 == 'T'){
                    treasureFound++;
                }
                Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
                inqueue(maze, tuple.Item2, tuple.Item3);
                visited.Add(tuple);
                // queue.Dequeue();
            }
        }


        public void PrintQueue(){
            Queue<Tuple<char, int, int, char>> temp = new Queue<Tuple<char, int, int, char>>();
            temp = queue;
            while (temp.Count() != 0){
                var tuple = temp.Dequeue();
                Console.Write(tuple.Item1 + " ");
            }
            Console.WriteLine();
        }

        public void printVisited(){
            Console.WriteLine(visited.Count());
            foreach (var tuple in visited){
                Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3 + " " + tuple.Item4);
            Console.WriteLine();
        }
        }
    }

    class DFS {
        private Stack<Tuple<char, int, int, char>> stack;
        private int treasureFound;
        private List<Tuple<char, int, int, char>> visited = new List<Tuple<char, int, int, char>>();
        public DFS(){
            stack = new Stack<Tuple<char, int, int, char>>();
            treasureFound = 0;

        }

        public void inqueue (Maze maze, int starti, int startj){
            // Console.WriteLine("Starti: " + starti);
            // Console.WriteLine("Startj: " + startj);
            // Console.WriteLine(maze.getContent(1,3));
            if (startj + 1 < 4 && notVisited(starti, startj + 1)){
                if (maze.getContent(starti, startj + 1) == 'R' || maze.getContent(starti, startj + 1) == 'T'){
                    var tuple = new Tuple<char, int, int, char>(maze.getContent(starti, startj + 1), starti, startj + 1, 'R');
                    stack.Push(tuple);
                    Console.WriteLine("a");
                }
            }
            // if (startj - 1 >= 0){
            //     if (maze.getContent(starti, startj - 1) == 'R' || maze.getContent(starti, startj - 1) == 'T'){
            //         var tuple = new Tuple<char, int, int, char>(maze.getContent(starti, startj - 1), starti, startj - 1, 'L');
            //         queue.Enqueue(tuple);
            //         Console.WriteLine("b");
            //     }
            // }
            if (starti + 1 < 4 && notVisited(starti + 1, startj)){
                if (maze.getContent(starti + 1, startj) == 'R' || maze.getContent(starti + 1, startj) == 'T'){
                    var tuple = new Tuple<char, int, int, char>(maze.getContent(starti + 1, startj), starti + 1, startj, 'D');
                    stack.Push(tuple);
                    Console.WriteLine("b");
                }
            }
            if (starti - 1 >= 0 && notVisited(starti - 1, startj)){
                if (maze.getContent(starti - 1, startj) == 'R' || maze.getContent(starti - 1, startj) == 'T'){
                    var tuple = new Tuple<char, int, int, char>(maze.getContent(starti - 1, startj), starti - 1, startj, 'U');
                    stack.Push(tuple);

                }
            }
            
        }

        public void search (Maze maze){
            int starti = maze.getStarti();
            int startj = maze.getStartj();
            Console.WriteLine("Starti: " + starti);
            Console.WriteLine("Startj: " + startj);
            inqueue(maze, starti, startj);
            for (int i = 0; i < 7; i++){
                Console.WriteLine(stack.Count());
                var tuple = stack.Pop();
                if (tuple.Item1 == 'T'){
                    treasureFound++;
                }
                Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
                inqueue(maze, tuple.Item2, tuple.Item3);
                visited.Add(tuple);
                // queue.Dequeue();
            }
        }

        public void PrintStack(){
            Stack<Tuple<char, int, int, char>> temp = new Stack<Tuple<char, int, int, char>>();
            temp = stack;
            while (temp.Count() != 0){
                var tuple = temp.Pop();
                Console.Write(tuple.Item1 + " ");
            }
            Console.WriteLine();
        }

        public void printVisited(){
            Console.WriteLine(visited.Count());
            foreach (var tuple in visited){
                Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3 + " " + tuple.Item4);
            Console.WriteLine();
        }
        }

        public bool notVisited(int a, int b){
            foreach (var t in visited){
                if (t.Item2 == a && t.Item3 == b){
                    return false;
                }
            }
            return true;
        }

    }

}