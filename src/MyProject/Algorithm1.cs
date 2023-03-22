using System;

namespace uburubur{
    class BFS {
        private Queue<Tuple<Node, char>> queue;
        private int treasureFound;
        private List<Tuple<Node, char>> visited = new List<Tuple<Node, char>>();
        private List<Tuple<Node, char>> treasureFoundTuple;
        private List<Tuple<Node,char>> treasureAdded = new List<Tuple<Node, char>>();
        private List<Tuple<Node, char>> path = new List<Tuple<Node, char>>();
        // private int treasurePath;
        public BFS(){
            queue = new Queue<Tuple<Node, char>>();
            treasureFound = 0;
            treasureFoundTuple = new List<Tuple<Node, char>>();

        }

        public void inqueue (Node node){
            
            
            // Console.WriteLine("node skrg x : " + node.getX() + "  y : " + node.getY());
            // if(node.getY() != 0 && notVisited(node.getLeft())){
            //     Console.WriteLine("kiri belom dibaca");
            // }
            // if(node.getY() != 3 && notVisited(node.getRight())){
            //     Console.WriteLine("kanan belom dibaca");
            // }
            // if(node.getX() != 0 && notVisited(node.getUp())){
            //     Console.WriteLine("atas belom dibaca");
            // }
            // if(node.getX() != 3 && notVisited(node.getDown())){
            //     Console.WriteLine("Bawah belom dibaca");
            // }
            if(node.getLeft() == null){
                Console.WriteLine("gaada node kiri");
            }
            if(node.getRight() == null){
                Console.WriteLine("gaada node kanan");
            }
            if(node.getUp() == null){
                Console.WriteLine("gaada node atas");
            }
            if(node.getDown() == null){
                Console.WriteLine("gaada node bawah");
            }

            Console.WriteLine("visited node");
            foreach (var tuple in visited){
                Console.Write("(" + tuple.Item1.getX() + "," + tuple.Item1.getY() + "), ");
            }
            Console.WriteLine("\n");


            if (node.getLeft() != null && notVisited(node.getLeft())){
                    var tuple = new Tuple<Node, char>(node.getLeft(), 'L');
                    queue.Enqueue(tuple);
                    Console.WriteLine("L");
            }



            // Console.WriteLine("node kanan " + "(" + node.getRight().getX() + "," + node.getRight().getY() + ")");

            if (node.getUp() != null && notVisited(node.getUp())){
                    var tuple = new Tuple<Node, char>(node.getUp(), 'U');
                    queue.Enqueue(tuple);
                    Console.WriteLine("U");
            }

            if (node.getRight() != null && notVisited(node.getRight())){
                    var tuple = new Tuple<Node, char>(node.getRight(), 'R');
                    queue.Enqueue(tuple);
                    Console.WriteLine("R");
            }
            if (node.getDown() != null && notVisited(node.getDown())){
                    var tuple = new Tuple<Node, char>(node.getDown(), 'D');
                    queue.Enqueue(tuple);
                    Console.WriteLine("D");
            }
        }

        public void search (MazeGraph maze){
            // maze.printNodes();
            int starti = maze.getStart().getX();
            int startj = maze.getStart().getY();
            maze.setPosition(maze.getStart());
            Console.WriteLine("Starti: " + starti);
            Console.WriteLine("Startj: " + startj);

            // Console.WriteLine(maze.getPosition().getX());
            // Console.WriteLine(maze.getPosition().getY());
            
            inqueue(maze.getPosition());
            visited.Add(new Tuple<Node, char>(maze.getStart(), 'S'));
            // Console.WriteLine("fdsfdfdfdfdfd");

            while (queue.Count() != 0){
                Console.WriteLine("===");
                // Console.WriteLine(queue.Count());
                var tuple = queue.Dequeue();
                Console.WriteLine("Tuple: " + tuple.Item1.getValue() + " " + tuple.Item2 + " ");
                if (tuple.Item1.getValue() == 'T'){
                    Console.WriteLine("TREASURE FOUND");
                    bool found = false;
                    foreach(var a in treasureFoundTuple){
                        if(a.Item1.getX() == tuple.Item1.getX() && a.Item1.getY() == tuple.Item1.getY()){
                            found = true;
                            break;
                        }
                    }
                    if(!found){
                        treasureFoundTuple.Add(tuple);
                    }
                }
                // Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
                Node temp = maze.FindNode(tuple.Item1.getX(), tuple.Item1.getY());
                maze.setPosition(temp);
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
        
        public List<Tuple<Node,char>> pathTreasure(Tuple<Node,char> treasureLoc)
        {
            int i = treasureLoc.Item1.getX();
            int j = treasureLoc.Item1.getY();
            List<Tuple<Node,char>> temppath = new List<Tuple<Node,char>>();
            visited.Reverse();
            bool found = false;
            char direction = ' ';
            Console.WriteLine("Pathsds: ");
            foreach (var a in visited){
                Console.WriteLine(a.Item1.getX() + " " + a.Item1.getY() + " " + a.Item1.getValue() + " " + a.Item2);
            }
            while (!found){
                // Console.WriteLine("visited count : " + visited.Count());
                // Console.Write(direction + " ");
                for(int k = 0; k < visited.Count(); k++){
                // Console.WriteLine("k :" + k);
                    if (visited[k].Item1.getX() == i && visited[k].Item1.getY() == j){
                        if (visited[k].Item1.getValue() == 'T') {
                            treasureAdded.Add(visited[k]);
                        }
                        if (visited[k].Item1.getValue() == 'K'){
                            found = true;
                            break;
                        }
                        else {
                            temppath.Add(visited[k]);
                        }
                        direction = visited[k].Item2;
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
            visited.Reverse();
            // temppath.Reverse();
            return temppath;
        }

        // public Tuple<Node, char> findVisited(Node node){
        //     Tuple<Node, char> temp = new Tuple<Node, char>(new Node(0,0,'0'), '0');
        //     for(int k = 0; k < visited.Count(); k++){
        //         if(visited[k].Item1.getX() == node.getX() && visited[k].Item1.getY() == node.getY()){
        //             temp = visited[k];
        //         }
        //     }
        //     return temp;
        // }
        public bool notAdded(Node node)
        {
            foreach(var treasureNode in treasureAdded)
            {
                if (treasureNode.Item1.getX() == node.getX() && treasureNode.Item1.getY() == node.getY())
                {
                    return false;
                }
            }
            return true;
        }
        public void findPath(int idx){
            Console.WriteLine("idx :" + idx);
            Console.WriteLine("treasurefound : " + treasureFoundTuple.Count());
            if(idx != -1){

                if(notAdded(treasureFoundTuple[idx].Item1)){

                    if(idx + 1 == treasureFoundTuple.Count()){
                        
                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0,0,'0'), '0');
                        for(int i = 0 ; i < visited.Count(); i++){
                            if(visited[i].Item1.getX() == x && visited[i].Item1.getY() == y){
                                flagNode = visited[i];
                                break;
                            }
                        }
                        // treasureAdded.Add(flagNode);
                        List<Tuple<Node, char>> tempPath =  pathTreasure(flagNode);
                        tempPath.Reverse();
                        foreach(var tuple in tempPath){
                            path.Add(tuple);
                        }
                        tempPath.Reverse();
                        for (int i = 1; i < tempPath.Count(); i++)
                        {
                            // path.Add(tempPath[i]);
                            if(tempPath[i-1].Item2 == 'L'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'R');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'R'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'L');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'U'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'D');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'D'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'U');
                                path.Add(a);
                            }
                        }
                        findPath(idx - 1);
                    }

                    else if(idx > 0){
                        
                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0,0,'0'), '0');
                        for(int i = 0 ; i < visited.Count(); i++){
                            if(visited[i].Item1.getX() == x && visited[i].Item1.getY() == y){
                                flagNode = visited[i];
                                break;
                            }
                        }
                        // treasureAdded.Add(flagNode);
                        List<Tuple<Node, char>> tempPath =  pathTreasure(flagNode);
                        tempPath.Reverse();
                        // foreach(var tuple in tempPath){
                        //     path.Add(tuple);
                        // }
                        for(int i = 1 ; i < tempPath.Count(); i++){
                            path.Add(tempPath[i]);
                        }

                        tempPath.Reverse();
                        for (int i = 1; i < tempPath.Count(); i++)
                        {
                            // path.Add(tempPath[i]);
                            if(tempPath[i-1].Item2 == 'L'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'R');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'R'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'L');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'U'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'D');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'D'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'U');
                                path.Add(a);
                            }
                        }
                        findPath(idx - 1);
                    }

                    else if(idx == 0){
                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0,0,'0'), '0');
                        for(int i = 0 ; i < visited.Count(); i++){
                            if(visited[i].Item1.getX() == x && visited[i].Item1.getY() == y){
                                flagNode = visited[i];
                                break;
                            }
                        }
                        // treasureAdded.Add(flagNode);
                        List<Tuple<Node, char>> tempPath =  pathTreasure(flagNode);
                        tempPath.Reverse();
                        for(int i = 1; i < tempPath.Count(); i++){
                            path.Add(tempPath[i]);
                        }
                        // foreach(var tuple in tempPath){
                        //     path.Add(tuple);
                        // }
                    }

                }
                
                else {
                    findPath(idx - 1);
                }
            }

        }
        // public void findPath(int idx){
        //     treasureFoundTuple.Reverse();
        //     int i = treasureFoundTuple[idx].Item1.getX();
        //     int j = treasureFoundTuple[idx].Item1.getY();
        //     List<Tuple<Node, char>> ari = new List<Tuple<Node, char>>();
        //     visited.Reverse();
        //     bool found = false;
        //     char direction = ' ';
        //     // Console.WriteLine("Path: ");
        //     while (!found){
        //         // Console.Write(direction + " ");
        //         for(int k = 0; k < visited.Count(); k++){
        //             if (visited[k].Item1.getX() == i && visited[k].Item1.getY() == j){
        //                 if (visited[k].Item1.getValue() == 'T'){
        //                     treasurePath++;
        //                 }
        //                 if (visited[k].Item1.getValue() == 'K'){
        //                     if (treasurePath == treasureFound){
        //                         for (int x = 0; x < ari.Count(); x++){
        //                             path.Add(ari[x]);
        //                         }
        //                     path.Add(visited[k]);
        //                     found = true; 
        //                     }
        //                     else{
        //                         List<Tuple<Node, char>> temp = new List<Tuple<Node, char>>();
        //                         temp = ari;
        //                         ari.Reverse();
        //                         // temp.Reverse();
        //                         for (int x = 1; i < temp.Count() - 1; x++){
        //                             path.Add(temp[x]);
        //                         }
        //                         findPath(idx+1);
        //                     }
        //                 }
        //                 else{
        //                     ari.Add(visited[k]);
        //                 }
        //                 direction = visited[k].Item2;
        //         // Console.WriteLine("i: " + i);
        //         // Console.WriteLine("j: " + j);
        //         //         Console.WriteLine("Direction: " + direction);
        //                 switch (direction){
        //                     case 'U':
        //                         i++;
        //                         break;
        //                     case 'D':
        //                         i--;
        //                         break;
        //                     case 'L':
        //                         j++;
        //                         break;
        //                     case 'R':
        //                         j--;
        //                         break;
        //                 }
        //             }
        //         }
        //         }
        //     // path.Reverse();
        //     }

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
        private List<Tuple<Node, char>> treasureFoundTuple;
        private List<Tuple<Node,char>> treasureAdded = new List<Tuple<Node, char>>();
        private List<Tuple<Node, char>> path = new List<Tuple<Node, char>>();
        public DFS(){
            stack = new Stack<Tuple<Node, char>>();
            treasureFound = 0;
            treasureFoundTuple = new List<Tuple<Node, char>>();
        }

        public void inqueue (Node node){

            Console.WriteLine("node skrg x : " + node.getX() + "  y : " + node.getY());

            if(node.getLeft() == null){
                Console.WriteLine("gaada node kiri");
            }
            if(node.getRight() == null){
                Console.WriteLine("gaada node kanan");
            }
            if(node.getUp() == null){
                Console.WriteLine("gaada node atas");
            }
            if(node.getDown() == null){
                Console.WriteLine("gaada node bawah");
            }

            Console.WriteLine("visited node");
            foreach (var tuple in visited){
                Console.Write("(" + tuple.Item1.getX() + "," + tuple.Item1.getY() + "), ");
            }
            Console.WriteLine("\n");
            
            if (node.getLeft() != null && notVisited(node.getLeft())){
                    var tuple = new Tuple<Node, char>(node.getLeft(), 'L');
                    stack.Push(tuple);
                    // Console.WriteLine("b");
            }
            if (node.getRight() != null && notVisited(node.getRight())){
                    var tuple = new Tuple<Node, char>(node.getRight(), 'R');
                    stack.Push(tuple);
                    // Console.WriteLine("a");
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
            Console.WriteLine("ffdfdfdfdfd\n");
            int starti = maze.getStart().getX();
            int startj = maze.getStart().getY();
            maze.setPosition(maze.getStart());
            Console.WriteLine("Starti: " + starti);
            Console.WriteLine("Startj: " + startj);
            inqueue(maze.getPosition());
            visited.Add(new Tuple<Node, char>(maze.getStart(), 'S'));
            // Console.WriteLine("total treasure : " + maze.getTreasure());
            while (stack.Count() != 0){

                // Console.WriteLine(queue.Count());
                var tuple = stack.Pop();
                if (tuple.Item1.getValue() == 'T'){
                    bool found = false;
                    foreach(var a in treasureFoundTuple){
                        if(a.Item1.getX() == tuple.Item1.getX() && a.Item1.getY() == tuple.Item1.getY()){
                            found = true;
                            break;
                        }
                    }
                    if(!found){
                        treasureFoundTuple.Add(tuple);
                    }
                    // treasureFound++;
                    // treasureFoundTuple = tuple;
                }
                // Console.WriteLine("Tuple: " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
                Node temp = maze.FindNode(tuple.Item1.getX(), tuple.Item1.getY());
                maze.setPosition(temp);
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

        public List<Tuple<Node,char>> pathTreasure(Tuple<Node,char> treasureLoc){
            int i = treasureLoc.Item1.getX();
            int j = treasureLoc.Item1.getY();
            List<Tuple<Node,char>> temppath = new List<Tuple<Node, char>>();
            visited.Reverse();
            bool found = false;
            char direction = ' ';
            Console.WriteLine("Pathsds: ");
            foreach (var a in visited){
                Console.WriteLine(a.Item1.getX() + " " + a.Item1.getY() + " " + a.Item1.getValue() + " " + a.Item2);
            }
            while(!found){
                // Console.WriteLine("asd");
                // Console.WriteLine("visitedcount :" + visited.Count());
                for(int k = 0 ; k < visited.Count(); k++){
                    // Console.WriteLine("k :" + k);
                    if (visited[k].Item1.getX() == i && visited[k].Item1.getY() == j){
                        if(visited[k].Item1.getValue() == 'T'){
                            Console.WriteLine("abc");
                            treasureAdded.Add(visited[k]);
                        }
                        if(visited[k].Item1.getValue() == 'K'){
                            Console.WriteLine("def");
                            found = true;
                            break;
                        }
                        else {
                            temppath.Add(visited[k]);
                        }
                    direction = visited[k].Item2;
                    switch(direction){
                        case 'U' :
                            i++;
                            break;
                        case 'D' :
                            i--;
                            break;
                        case 'L' :
                            j++;
                            break;
                        case 'R' :
                            j--;
                            break;
                    }
                    }
                    Console.WriteLine("i : " + i + " j : " + j);
                }
            }
            visited.Reverse();
            return temppath;
        }
        public bool notAdded(Node node){
            foreach(var treasureNode in treasureAdded){
                if(treasureNode.Item1.getX() == node.getX() && treasureNode.Item1.getY() == node.getY()){
                    return false;
                }
            }
            return true;
        }
        public void findPath(int idx){
            if(idx != -1){
                if(notAdded(treasureFoundTuple[idx].Item1)){

                    if(idx + 1 == treasureFoundTuple.Count()){
                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0,0,'0'), '0');
                        for(int i = 0 ; i < visited.Count(); i++){
                            if(visited[i].Item1.getX() == x && visited[i].Item1.getY() == y){
                                flagNode = visited[i];
                                break;
                            }
                        }
                        List<Tuple<Node, char>> tempPath = pathTreasure(flagNode);
                        tempPath.Reverse();
                        foreach(var tuple in tempPath){
                            path.Add(tuple);
                        }
                        tempPath.Reverse();
                        for(int i = 1; i < tempPath.Count(); i++){
                            if(tempPath[i-1].Item2 == 'L'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'R');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'R'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'L');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'U'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'D');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'D'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'U');
                                path.Add(a);
                            }
                        }
                        findPath(idx - 1);
                    }

                    else if(idx > 0){
                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0,0,'0'), '0');
                        for(int i = 0 ; i < visited.Count(); i++){
                            if(visited[i].Item1.getX() == x && visited[i].Item1.getY() == y){
                                flagNode = visited[i];
                                break;
                            }
                        }
                        List<Tuple<Node, char>> tempPath = pathTreasure(flagNode);
                        tempPath.Reverse();
                        for(int i = 1 ; i < tempPath.Count(); i++){
                            path.Add(tempPath[i]);
                        }

                        tempPath.Reverse();
                        for(int i = 1; i < tempPath.Count(); i++){
                            if(tempPath[i-1].Item2 == 'L'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'R');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'R'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'L');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'U'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'D');
                                path.Add(a);
                            }
                            else if(tempPath[i-1].Item2 == 'D'){
                                Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'U');
                                path.Add(a);
                            }
                        }
                        findPath(idx - 1);
                    }

                    else if(idx == 0){
                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0,0,'0'), '0');
                        for(int i = 0 ; i < visited.Count(); i++){
                            if(visited[i].Item1.getX() == x && visited[i].Item1.getY() == y){
                                flagNode = visited[i];
                                break;
                            }
                        }
                        // treasureAdded.Add(flagNode);
                        List<Tuple<Node, char>> tempPath =  pathTreasure(flagNode);
                        tempPath.Reverse();
                        for(int i = 1; i < tempPath.Count(); i++){
                            path.Add(tempPath[i]);
                        }
                    }
                }
                else {
                    findPath(idx - 1);
                }
            }
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