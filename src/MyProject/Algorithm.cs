// using System;

// namespace uburubur{
//     class BFS {
//         private Queue<Tuple<char, int, int, char>> queue;
//         private int treasureFound;
//         private List<Tuple<char, int, int, char>> visited = new List<Tuple<char, int, int, char>>();
//         private Tuple<char, int, int, char> treasureFoundTuple;
//         private List<Tuple<char, int, int, char>> path = new List<Tuple<char, int, int, char>>();
//         public BFS(){
//             queue = new Queue<Tuple<char, int, int, char>>();
//             treasureFound = 0;

//         }

//         public void inqueue (Maze maze, int starti, int startj){
//             // Console.WriteLine("Starti: " + starti);
//             // Console.WriteLine("Startj: " + startj);
//             // Console.WriteLine(maze.getContent(1,3));
//             if (startj + 1 < 4){
//                 if (maze.getContent(starti, startj + 1) == 'R' || maze.getContent(starti, startj + 1) == 'T'){
//                     var tuple = new Tuple<char, int, int, char>(maze.getContent(starti, startj + 1), starti, startj + 1, 'R');
//                     queue.Enqueue(tuple);
//                     Console.WriteLine("a");
//                 }
//             }
//             if (startj - 1 >= 0){
//                 if (maze.getContent(starti, startj - 1) == 'R' || maze.getContent(starti, startj - 1) == 'T'){
//                     var tuple = new Tuple<char, int, int, char>(maze.getContent(starti, startj - 1), starti, startj - 1, 'L');
//                     queue.Enqueue(tuple);
//                     Console.WriteLine("b");
//                 }
//             }
//             if (starti + 1 < 4){
//                 if (maze.getContent(starti + 1, startj) == 'R' || maze.getContent(starti + 1, startj) == 'T'){
//                     var tuple = new Tuple<char, int, int, char>(maze.getContent(starti + 1, startj), starti + 1, startj, 'D');
//                     queue.Enqueue(tuple);
//                     Console.WriteLine("b");
//                 }
//             }
//             if (starti - 1 >= 0){
//                 if (maze.getContent(starti - 1, startj) == 'R' || maze.getContent(starti - 1, startj) == 'T'){
//                     var tuple = new Tuple<char, int, int, char>(maze.getContent(starti - 1, startj), starti - 1, startj, 'U');
//                     queue.Enqueue(tuple);
//                 }
//             }
            
//         }
//         public void search (Maze maze){
//             int starti = maze.getStarti();
//             int startj = maze.getStartj();
//             visited.Add(new Tuple<char, int, int, char>(maze.getContent(starti, startj), starti, startj, 'S'));
//             Console.WriteLine("Starti: " + starti);
//             Console.WriteLine("Startj: " + startj);
//             inqueue(maze, starti, startj);
//             for (int i = 0; i < 10; i++){
//                 Console.WriteLine(queue.Count());
//                 var tuple = queue.Dequeue();
//                 if (tuple.Item1 == 'T'){
//                     treasureFound++;
//                     treasureFoundTuple = tuple;
//                 }
//                 Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
//                 inqueue(maze, tuple.Item2, tuple.Item3);
//                 visited.Add(tuple);
//                 // queue.Dequeue();
//             }
//         }


//         public void PrintQueue(){
//             Queue<Tuple<char, int, int, char>> temp = new Queue<Tuple<char, int, int, char>>();
//             temp = queue;
//             while (temp.Count() != 0){
//                 var tuple = temp.Dequeue();
//                 Console.Write(tuple.Item1 + " ");
//             }
//             Console.WriteLine();
//         }

//         public void printVisited(){
//             Console.WriteLine(visited.Count());
//             foreach (var tuple in visited){
//                 Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3 + " " + tuple.Item4);
//             Console.WriteLine();
//         }
//         }
//     }

//     class DFS {
//         private Stack<Tuple<char, int, int, char>> stack;
//         private int treasureFound;
//         private List<Tuple<char, int, int, char>> visited = new List<Tuple<char, int, int, char>>();
//         private Tuple<char, int, int, char> treasureFoundTuple;
//         private List<Tuple<char, int, int, char>> path = new List<Tuple<char, int, int, char>>();
//         public DFS(){
//             stack = new Stack<Tuple<char, int, int, char>>();
//             treasureFound = 0;

//         }

//         public void inqueue (Maze maze, int starti, int startj){
//             // Console.WriteLine("Starti: " + starti);
//             // Console.WriteLine("Startj: " + startj);
//             // Console.WriteLine(maze.getContent(1,3));
//             if (startj + 1 < 4 && notVisited(starti, startj + 1)){
//                 if (maze.getContent(starti, startj + 1) == 'R' || maze.getContent(starti, startj + 1) == 'T'){
//                     var tuple = new Tuple<char, int, int, char>(maze.getContent(starti, startj + 1), starti, startj + 1, 'R');
//                     stack.Push(tuple);
//                     // Console.WriteLine("a");
//                 }
//             }
//             if (startj - 1 >= 0 && notVisited(starti, startj - 1)){
//                 if (maze.getContent(starti, startj - 1) == 'R' || maze.getContent(starti, startj - 1) == 'T'){
//                     var tuple = new Tuple<char, int, int, char>(maze.getContent(starti, startj - 1), starti, startj - 1, 'L');
//                     stack.Push(tuple);
//                     // Console.WriteLine("b");
//                 }
//             }
//             if (starti + 1 < 4 && notVisited(starti + 1, startj)){
//                 if (maze.getContent(starti + 1, startj) == 'R' || maze.getContent(starti + 1, startj) == 'T'){
//                     var tuple = new Tuple<char, int, int, char>(maze.getContent(starti + 1, startj), starti + 1, startj, 'D');
//                     stack.Push(tuple);
//                     // Console.WriteLine("b");
//                 }
//             }
//             if (starti - 1 >= 0 && notVisited(starti - 1, startj)){
//                 if (maze.getContent(starti - 1, startj) == 'R' || maze.getContent(starti - 1, startj) == 'T'){
//                     var tuple = new Tuple<char, int, int, char>(maze.getContent(starti - 1, startj), starti - 1, startj, 'U');
//                     stack.Push(tuple);

//                 }
//             }
            
//         }

//         public void search (Maze maze){
//             int starti = maze.getStarti();
//             int startj = maze.getStartj();
//             visited.Add(new Tuple<char, int, int, char>(maze.getContent(starti, startj), starti, startj, 'S'));
//             Console.WriteLine("Starti: " + starti);
//             Console.WriteLine("Startj: " + startj);
//             inqueue(maze, starti, startj);
//             while(treasureFound < 2){
//                 // Console.WriteLine("AAAAAA");
//                 var tuple = stack.Pop();
//                 if (tuple.Item1 == 'T'){
//                     treasureFound++;
//                     treasureFoundTuple = tuple;
//                 }
//                 // Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
//                 inqueue(maze, tuple.Item2, tuple.Item3);
//                 visited.Add(tuple);
//                 // queue.Dequeue();
//             }
//         }

//         public void PrintStack(){
//             Stack<Tuple<char, int, int, char>> temp = new Stack<Tuple<char, int, int, char>>();
//             temp = stack;
//             while (temp.Count() != 0){
//                 var tuple = temp.Pop();
//                 Console.Write(tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3 + " " + tuple.Item4 + " ");
//             }
//             Console.WriteLine();
//         }

//         public void printVisited(){
//             Console.WriteLine(visited.Count());
//             foreach (var tuple in visited){
//                 Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3 + " " + tuple.Item4);
//             Console.WriteLine();
//         }
//         }

//         public void findPath(){
//             int i = treasureFoundTuple.Item2;
//             int j = treasureFoundTuple.Item3;
//             visited.Reverse();
//             bool found = false;
//             char direction = ' ';
//             // Console.WriteLine("Path: ");
//             while (!found){
//                 // Console.Write(direction + " ");
//                 for(int k = 0; k < visited.Count(); k++){
//                     if (visited[k].Item2 == i && visited[k].Item3 == j){
//                         path.Add(visited[k]);
//                 // Console.WriteLine("KONTOL");
//                         if (visited[k].Item1 == 'K'){
//                             found = true;
//                             break;
//                         }
//                         direction = visited[k].Item4;
//                 // Console.WriteLine("i: " + i);
//                 // Console.WriteLine("j: " + j);
//                 //         Console.WriteLine("Direction: " + direction);
//                         switch (direction){
//                             case 'U':
//                                 i++;
//                                 break;
//                             case 'D':
//                                 i--;
//                                 break;
//                             case 'L':
//                                 j++;
//                                 break;
//                             case 'R':
//                                 j--;
//                                 break;
//                         }
//                     }
//                 }
//                 }
//             path.Reverse();
//             }
//         public void printPath(){
//             foreach (var tuple in path){
//                 Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3 + " " + tuple.Item4);
//             }
//         }

//         public bool notVisited(int a, int b){
//             foreach (var t in visited){
//                 if (t.Item2 == a && t.Item3 == b){
//                     return false;
//                 }
//             }
//             return true;
//         }

//     }
// }