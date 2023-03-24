using System;
using System.Collections.Generic;

namespace uburubur{
    class BFS {
        private Queue<Tuple<Node, char>> queue;
        private int treasureFound;
        private List<Tuple<Node, char>> visited = new List<Tuple<Node, char>>();
        private List<Tuple<Node, char>> treasureFoundTuple;
        private List<List<Tuple<Node,char>>> listPath;
        private List<List<Tuple<Node , char>>> tempListPath = new List<List<Tuple<Node, char>>>();
        private List<Tuple<Node,char>> treasureAdded = new List<Tuple<Node, char>>();
        private List<Tuple<Node, char>> path = new List<Tuple<Node, char>>();
        private List<Node> nodeSamePath;
        // private int treasurePath;
        public BFS(){
            queue = new Queue<Tuple<Node, char>>();
            treasureFound = 0;
            treasureFoundTuple = new List<Tuple<Node, char>>();
            nodeSamePath = new List<Node>();
 
        }
 
        public void inqueue (Node node){
 
            // if(node.getLeft() == null){
            //     Console.WriteLine("gaada node kiri");
            // }
            // if(node.getRight() == null){
            //     Console.WriteLine("gaada node kanan");
            // }
            // if(node.getUp() == null){
            //     Console.WriteLine("gaada node atas");
            // }
            // if(node.getDown() == null){
            //     Console.WriteLine("gaada node bawah");
            // }
 
            // Console.WriteLine("visited node");
            // foreach (var tuple in visited){
            //     Console.Write("(" + tuple.Item1.getX() + "," + tuple.Item1.getY() + "), ");
            // }
            // Console.WriteLine("\n");
 
 
            if (node.getLeft() != null && notVisited(node.getLeft())){
                    var tuple = new Tuple<Node, char>(node.getLeft(), 'L');
                    queue.Enqueue(tuple);
                    Console.WriteLine("L");
            }
 
            if (node.getRight() != null && notVisited(node.getRight())){
                    var tuple = new Tuple<Node, char>(node.getRight(), 'R');
                    queue.Enqueue(tuple);
                    Console.WriteLine("R");
            }
 
            if (node.getUp() != null && notVisited(node.getUp())){
                    var tuple = new Tuple<Node, char>(node.getUp(), 'U');
                    queue.Enqueue(tuple);
                    Console.WriteLine("U");
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
 
            while (queue.Count != 0){
                // Console.WriteLine("===");
                // Console.WriteLine(queue.Count());
                var tuple = queue.Dequeue();
                // Console.WriteLine("Tuple: " + tuple.Item1.getValue() + " " + tuple.Item2 + " ");
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
 
 
        // public void PrintQueue(){
        //     Queue<Tuple<Node, char>> temp = new Queue<Tuple<Node, char>>();
        //     temp = queue;
        //     while (temp.Count() != 0){
        //         var tuple = temp.Dequeue();
        //         Console.Write(tuple.Item1.getValue() + " " + tuple.Item1.getX() + " " + tuple.Item1.getY() + " " + tuple.Item2 + " ");
        //     }
        //     Console.WriteLine();
        // }
 
        // public void printVisited(){
        //     Console.WriteLine(visited.Count());
        //     foreach (var tuple in visited){
        //         Console.Write(tuple.Item1.getValue() + " " + tuple.Item1.getX() + " " + tuple.Item1.getY() + " " + tuple.Item2 + " ");
        //     Console.WriteLine();
        // }
        // }
 
        public List<Tuple<Node,char>> pathTreasure(Tuple<Node,char> treasureLoc)
        {
            int i = treasureLoc.Item1.getX();
            int j = treasureLoc.Item1.getY();
            List<Tuple<Node,char>> temppath = new List<Tuple<Node,char>>();
            visited.Reverse();
            bool found = false;
            char direction = ' ';
            // Console.WriteLine("Pathsds: ");
            // foreach (var a in visited){
            //     Console.WriteLine(a.Item1.getX() + " " + a.Item1.getY() + " " + a.Item1.getValue() + " " + a.Item2);
            // }
            while (!found){
                // Console.WriteLine("visited count : " + visited.Count());
                // Console.Write(direction + " ");
                for(int k = 0; k < visited.Count; k++){
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
            // Console.WriteLine("idx :" + idx);
            // Console.WriteLine("treasurefound : " + treasureFoundTuple.Count());
            if(idx != -1){
                if(notAdded(treasureFoundTuple[idx].Item1)){
                    if(idx + 1 == treasureFoundTuple.Count ){
 
                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0,0,'0'), '0');
                        for(int i = 0 ; i < visited.Count; i++){
                            if(visited[i].Item1.getX() == x && visited[i].Item1.getY() == y){
                                flagNode = visited[i];
                                break;
                            }
                        }
                        // treasureAdded.Add(flagNode);
                        List<Tuple<Node, char>> tempPath =  pathTreasure(flagNode);
                        tempListPath.Add(tempPath);
                        // tempPath.Reverse();
                        // foreach(var tuple in tempPath){
                        //     // path.Add(tuple);
                        // }
                        // tempPath.Reverse();
                        // for (int i = 1; i < tempPath.Count(); i++)
                        // {
                        //     // path.Add(tempPath[i]);
                        //     if(tempPath[i-1].Item2 == 'L'){
                        //         Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'R');
                        //         // path.Add(a);
                        //     }
                        //     else if(tempPath[i-1].Item2 == 'R'){
                        //         Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'L');
                        //         // path.Add(a);
                        //     }
                        //     else if(tempPath[i-1].Item2 == 'U'){
                        //         Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'D');
                        //         // path.Add(a);
                        //     }
                        //     else if(tempPath[i-1].Item2 == 'D'){
                        //         Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'U');
                        //         // path.Add(a);
                        //     }
                        //  }
                        findPath(idx - 1);
                    }
 
                    else if(idx > 0){
                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0,0,'0'), '0');
                        for(int i = 0 ; i < visited.Count; i++){
                            if(visited[i].Item1.getX() == x && visited[i].Item1.getY() == y){
                                flagNode = visited[i];
                                break;
                            }
                        }
                        // treasureAdded.Add(flagNode);
                        List<Tuple<Node, char>> tempPath =  pathTreasure(flagNode);
                        tempListPath.Add(tempPath);
                        // tempPath.Reverse();
                        // // foreach(var tuple in tempPath){
                        // //     path.Add(tuple);
                        // // }
                        // for(int i = 1 ; i < tempPath.Count(); i++){
                        //     // path.Add(tempPath[i]);
                        // }
 
                        // tempPath.Reverse();
                        // for (int i = 1; i < tempPath.Count(); i++)
                        // {
                        //     // path.Add(tempPath[i]);
                        //     if(tempPath[i-1].Item2 == 'L'){
                        //         Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'R');
                        //         // path.Add(a);
                        //     }
                        //     else if(tempPath[i-1].Item2 == 'R'){
                        //         Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'L');
                        //         // path.Add(a);
                        //     }
                        //     else if(tempPath[i-1].Item2 == 'U'){
                        //         Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'D');
                        //         // path.Add(a);
                        //     }
                        //     else if(tempPath[i-1].Item2 == 'D'){
                        //         Tuple<Node,char> a = new Tuple<Node, char>(new Node(tempPath[i].Item1.getX(), tempPath[i].Item1.getY(), tempPath[i].Item1.getValue()), 'U');
                        //         // path.Add(a);
                        //     }
                        // }
                        findPath(idx - 1);
                    }
 
                    else if(idx == 0){
                        int x = treasureFoundTuple[idx].Item1.getX();
                        int y = treasureFoundTuple[idx].Item1.getY();
                        Tuple<Node, char> flagNode = new Tuple<Node, char>(new Node(0,0,'0'), '0');
                        for(int i = 0 ; i < visited.Count; i++){
                            if(visited[i].Item1.getX() == x && visited[i].Item1.getY() == y){
                                flagNode = visited[i];
                                break;
                            }
                        }
                        // treasureAdded.Add(flagNode);
                        List<Tuple<Node, char>> tempPath =  pathTreasure(flagNode);
                        tempListPath.Add(tempPath);
                        // tempPath.Reverse();
                        // for(int i = 1; i < tempPath.Count(); i++){
                        //     // path.Add(tempPath[i]);
                        // }
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
        public void makePath(){
            foreach(var a in tempListPath){
                for(int i = 0 ; i < a.Count; i ++){
                    Console.WriteLine("x : " + a[i].Item1.getX() + " y : " + a[i].Item1.getY());
                }
                Console.WriteLine("\n");
            }
 
            for(int i = 0 ; i < tempListPath.Count - 1; i++){
                // tempListPath[i].Reverse();
                // tempListPath[i+1].Reverse();
                // Node x = new Node(-9,-9,'0');
                for(int j = 0 ; j < tempListPath[i].Count; j++){
                    bool cek = false;
                    for(int k = 0 ; k < tempListPath[i+1].Count; k++){
                        if(tempListPath[i][j].Item1.getX() == tempListPath[i+1][k].Item1.getX() 
                        && tempListPath[i][j].Item1.getY() == tempListPath[i+1][k].Item1.getY()){
                            // if()
                            nodeSamePath.Add(tempListPath[i][j].Item1);
                            cek = true;
                            break;
                        }
 
                        // else{
                        //     continue;
                        // }
                    }
                    if(cek) break;
                }
                // tempListPath[i].Reverse();
                // tempListPath[i+1].Reverse();
            }
        }
        
        public List<Tuple<Node, char>> getPath()
        {
            return path;
        }
        public void savePath(){
            Console.WriteLine("templistpath : " + tempListPath.Count);
            Console.WriteLine("nodesamepath count : " + nodeSamePath.Count);
            tempListPath.Reverse();
            nodeSamePath.Reverse();
            for(int i = 0 ; i < tempListPath.Count; i++){
                // Console.WriteLine("nodesamepath i x :" + nodeSamePath[i].getX() + " y : " + nodeSamePath[i].getY());
                // i awal
                if(i == 0){
                    Console.WriteLine("i awal");
                    tempListPath[i].Reverse();
                    foreach(var a in tempListPath[i]){
                        path.Add(a);
                    }
                    tempListPath[i].Reverse();
                    for(int j = 1; j < tempListPath[i].Count; j++){
                        Console.WriteLine("templistPath x :" + tempListPath[i][j].Item1.getX());
                        Console.WriteLine("templistPath y :" + tempListPath[i][j].Item1.getY());
                        // Console.WriteLine("nodesamepath :" + nodeSamePath[i]);
                        if(tempListPath[i][j].Item1.getX() == nodeSamePath[i].getX()
                        && tempListPath[i][j].Item1.getY() == nodeSamePath[i].getY()){
                            if(tempListPath[i][j-1].Item2 == 'L'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'R');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'R'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'L');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'U'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'D');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'D'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'U');
                                path.Add(a);
                            }
 
                            break;
                            Console.WriteLine("sssdadasdas");
                        }
                        else{
                            Console.WriteLine("pathcount : " + path.Count);
                            if(tempListPath[i][j-1].Item2 == 'L'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'R');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'R'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'L');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'U'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'D');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'D'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'U');
                                path.Add(a);
                            }
                            Console.WriteLine("rrrrrr");
                        } 
                    }
                }
 
                // i akhir
                else if(i == tempListPath.Count - 1){
                    Console.WriteLine("i akhir");
                    tempListPath[i].Reverse();
                    bool cek1 = false;
                    Console.WriteLine("temp i .count : " + tempListPath[i].Count);
                    for(int j = 0 ; j < tempListPath[i].Count;j++){
                        Console.WriteLine("templistPath x :" + tempListPath[i][j].Item1.getX());
                        Console.WriteLine("templistPath y :" + tempListPath[i][j].Item1.getY());
                        if(tempListPath[i][j].Item1.getX() == nodeSamePath[i-1].getX()
                        && tempListPath[i][j].Item1.getY() == nodeSamePath[i-1].getY()){
                            cek1 = true;
 
                        }
                        else if(cek1){
                            path.Add(tempListPath[i][j]);
                        }
                    }
                    tempListPath[i].Reverse();
                }
 
                // i tengah tengah
                else{
                    Console.WriteLine("i tengah2");
                    tempListPath[i].Reverse();
                    bool cek1 = false;
                    for(int j = 0; j < tempListPath[i].Count; j++){
                        if(tempListPath[i][j].Item1.getX() == nodeSamePath[i-1].getX()
                        && tempListPath[i][j].Item1.getY() == nodeSamePath[i-1].getY()){
                            cek1 = true;
                            // path.Add(tempListPath[i][j]);
                        }
                        else if(cek1){
                            path.Add(tempListPath[i][j]);
                        }
                    }
                    tempListPath[i].Reverse();
                    // bool cek2 = false;
                    for(int j = 1 ; j < tempListPath[i].Count; j++){
                        if(tempListPath[i][j].Item1.getX() == nodeSamePath[i].getX()
                        && tempListPath[i][j].Item1.getY() == nodeSamePath[i].getY()){
                             if(tempListPath[i][j-1].Item2 == 'L'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'R');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'R'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'L');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'U'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'D');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'D'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'U');
                                path.Add(a);
                            }
                            break;
                        }
                        // else path.Add(tempListPath[i][j]);
                        else {
                            if(tempListPath[i][j-1].Item2 == 'L'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'R');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'R'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'L');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'U'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'D');
                                path.Add(a);
                            }
                            else if(tempListPath[i][j-1].Item2 == 'D'){
                                Tuple<Node, char> a = new Tuple<Node, char>(new Node(tempListPath[i][j].Item1.getX(), tempListPath[i][j].Item1.getY(), tempListPath[i][j].Item1.getValue()), 'U');
                                path.Add(a);
                            }
                        }
                    }
                }
            }
        }
    }
}