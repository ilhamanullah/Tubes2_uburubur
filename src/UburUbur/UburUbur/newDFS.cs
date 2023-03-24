using System;
using System.Collections.Generic;
using System.Linq;

namespace uburubur{
    class DFS{
        private Stack<Node> stack;
        private Stack<Node> path;
        private List<Node> visited;
        private int treasureVisited;
        private List<char> steps;

        public DFS(){
            this.stack = new Stack<Node>();
            this.path = new Stack<Node>();
            this.visited = new List<Node>();
            this.steps = new List<char>();
        }

        public void DFSsearch(MazeGraph graph){
            stack.Push(graph.getStart());
            visited.Add(graph.getStart());
            // path.Push(graph.getStart());
            while (stack.Count != 0){
                Console.WriteLine("---------------------");
                Node node = stack.Pop();
                path.Push(node);
                visited.Add(node);
                if (node.getValue() == 'T'){
                    Console.WriteLine("Treasure found!");
                    treasureVisited++;
                }
                if (treasureVisited == graph.getTreasure()){
                    Console.WriteLine("All treasure found!");
                    break;
                }
                
                if (!exploreable(path.Peek())){
                    List<Node> temp = new List<Node>();
                    Console.WriteLine("Dead End");
                    bool found = false;
                    while (!found){
                        Console.WriteLine("Masuk");
                        Node n = path.Pop();
                        temp.Add(n);
                        if (exploreable(n)){
                            found = true;
                        }
                    }
                    for (int i = temp.Count - 1; i >= 0; i--){
                        path.Push(temp[i]);
                    }
                    for (int i = 1; i < temp.Count; i++){
                        path.Push(temp[i]);
                    }
                }
                Console.WriteLine("Node Checking: " + node.getValue() + " " + node.getX() + " " + node.getY());
                if (node.getLeft() != null && notVisited(node.getLeft())){
                    Node temp = graph.FindNode(node.getLeft().getX(), node.getLeft().getY());
                    stack.Push(temp);
                    // path.Push(temp);
                }
                if (node.getUp() != null && notVisited(node.getUp())){
                    Node temp = graph.FindNode(node.getUp().getX(), node.getUp().getY());
                    stack.Push(temp);
                    // path.Push(temp);
                }
                if (node.getRight() != null && notVisited(node.getRight())){
                    Node temp = graph.FindNode(node.getRight().getX(), node.getRight().getY());
                    stack.Push(temp);
                    // path.Push(temp);
                }
                if (node.getDown() != null && notVisited(node.getDown())){
                    Node temp = graph.FindNode(node.getDown().getX(), node.getDown().getY());
                    stack.Push(temp);
                    // path.Push(temp);
                }

            }
            
        }

       

        public bool exploreable(Node node){
            if (node.getLeft() != null && notVisited(node.getLeft())){
                return true;
            }
            if (node.getUp() != null && notVisited(node.getUp())){
                return true;
            }
            if (node.getRight() != null && notVisited(node.getRight())){
                
                return true;

            }
            if (node.getDown() != null && notVisited(node.getDown())){
                // Console.WriteLine(node.getDown().getValue() + " " + node.getDown().getX() + " " + node.getDown().getY());
                return true;
            }
            return false;
        }


        public bool notVisited(Node node){
            foreach (Node n in visited){
                if (n.getX() == node.getX() && n.getY() == node.getY()){
                    return false;
                }
            }
            return true;
            
        }

        public void printPath(){
            foreach (Node n in path){
                Console.WriteLine(n.getValue() + " " + n.getX() + " " + n.getY());
            }
        }

        public void reversePath(){
            Stack<Node> temp = new Stack<Node>();
            while (path.Count != 0){
                temp.Push(path.Pop());
            }
            path = temp;
        }

        public void makeSteps(){
            Stack<Node> temp = new Stack<Node>();
            temp = path;
            Node prev = temp.Pop();
            while (temp.Count != 0)
            {
                Node curr = temp.Pop();
                if (curr.getX() == prev.getX() - 1 && curr.getY() == prev.getY())
                {
                    steps.Add('U');
                }
                else if (curr.getX() == prev.getX() + 1 && curr.getY() == prev.getY())
                {
                    steps.Add('D');
                }
                else if (curr.getX() == prev.getX() && curr.getY() == prev.getY() - 1)
                {
                    steps.Add('L');
                }
                else if (curr.getX() == prev.getX() && curr.getY() == prev.getY() + 1)
                {
                    steps.Add('R');
                }
                prev = curr;
            }
        }

        public void printSteps(){
            foreach (char c in steps){
                Console.WriteLine(c);
            }
        }
        public List<Node> getPath()
        {
            return path.ToList();
        }

        public List<char> getSteps() {
            return steps;
        }

        

    }
}
