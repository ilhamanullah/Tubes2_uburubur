using System;

namespace uburubur
{
    class Node
    {
        private int x;
        private int y;
        private char value;
        private Node left;
        private Node up;
        private Node right;
        private Node down;

        public Node(int x, int y, char value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public char getValue()
        {
            return value;
        }

        public void setLeft(Node left)
        {
            this.left = left;
        }

        public void setUp(Node up)
        {
            this.up = up;
        }

        public void setRight(Node right)
        {
            this.right = right;
        }

        public void setDown(Node down)
        {
            this.down = down;
        }

        public Node getLeft()
        {
            return left;
        }

        public Node getUp()
        {
            return up;
        }

        public Node getRight()
        {
            return right;
        }

        public Node getDown()
        {
            return down;
        }

        public void printNode()
        {
            Console.WriteLine("Node: " + x + " " + y + " " + value);
        }
    }
    
}