using System;
using System.Collections.Generic;

namespace CommonClientLib
{
    public class Tree<T>
    {
        private static int CursorTop;

        private const int PRINT_BUFFER = 10;

        private readonly int LineLength = Console.WindowWidth / 2;
        
        /// <summary>
        /// Unique key of a node
        /// </summary>
        public T Key { get; }

        /// <summary>
        /// Array of strings that can be printed with each node
        /// </summary>
        public string[] Info { get; }

        private List<Tree<T>> Children { get; set; } = new List<Tree<T>>();
        private Tree<T> Parent { get; set; }
        private int XPosition { get; set; }
        private int YPosition { get; set; }

        /// <summary>
        /// Creates a new tree, with this node being the root.
        /// </summary>
        /// <param name="key">Unique identifier for the current node.</param>
        /// <param name="info">List of information specific to the node.</param>
        public Tree(T key, params string[] info)
        {
            Key = key;
            Info = info;

            // Thses X and Y positions are default. The actual positions are set each time another
            // node is added to the tree.
            XPosition = Console.WindowWidth;
            YPosition = 0;
        }

        /// <summary>
        /// Adds child node to tree if key not in tree; otherwise, returns false.
        /// </summary>
        /// <param name="key">Unique identifier for the child node.</param>
        /// <param name="info">List of information specific to the child node.</param>
        /// <returns><code>true</code> if node added</returns>
        public bool TryAddNode(T key, params string[] info)
        {
            if (NodeExists(key))
            {
                return false;
            }

            var newNode = new Tree<T>(key, info);
            newNode.Parent = this;
            Children.Add(newNode);
            SetPositions(newNode);

            return true;
        }

        /// <summary>
        /// Gets node with key.
        /// </summary>
        /// <param name="key">Key of node sought</param>
        /// <returns>Node with key</returns>
        public Tree<T> GetNode(T key)
        {
            return RetrieveNode(key);
        }

        /// <summary>
        /// Determines whether node with key exists in tree.
        /// </summary>
        /// <param name="key">Key of node sought</param>
        /// <returns><code>true</code> if node with key exists in tree</returns>
        public bool NodeExists(T key)
        {
            return RetrieveNode(key) != null;
        }

        /// <summary>
        /// Returns the root node.
        /// </summary>
        /// <returns>Root node</returns>
        public Tree<T> GetRoot()
        {
            Func<Tree<T>, Tree<T>> getRoot = null;
            getRoot = node => 
            {
                if (node.Parent == null)
                {
                    return node;
                }

                return getRoot(node.Parent);
            };

            return getRoot(this);
        }

        /// <summary>
        /// Returns the number of siblings of the node
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>Number of siblings</returns>
        public int GetNumberOfSiblings(Tree<T> node)
        {
            if (node.Parent == null)
            {
                return 0;
            }

            // Subtract 1 so the current node isn't counted as a sibling of itself
            return node.Parent.Children.Count - 1;
        }

        /// <summary>
        /// Returns the siblings of a node. If the node has no siblings, then it returns a <code>List<T></code> containing
        /// only the node passed into the method.
        /// </summary>
        /// <param name="node">Node whose siblings are returned</param>
        /// <returns>Collection of siblings</returns>
        public List<Tree<T>> GetSiblings(Tree<T> node)
        {
            var siblings = new List<Tree<T>>();
            if (node.Parent == null)
            {
                siblings.Add(node);

                return siblings;
            }

            siblings.AddRange(node.Parent.Children);

            return siblings;
        }

        /// <summary>
        /// Prints the tree to the console.
        /// </summary>
        public void PrintTree(Tree<T> node, int yPosition, int call = 0)
        {
            PrintNode(node, yPosition);
            foreach (var child in node.Children)
            {
                PrintTree(child, yPosition, call + 1);
            }

            if (call == 0)
            {
                Console.CursorTop = CursorTop;
            }
        }

        private void PrintNode(Tree<T> node, int yPosition)
        {
            var prevColor = Console.ForegroundColor;
            var key = node.Key.ToString();
            
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.CursorTop = yPosition + node.YPosition;
            Console.CursorLeft = (node.XPosition - key.Length) / 2;
            Console.WriteLine(key);
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            foreach (var item in node.Info)
            {
                Console.CursorLeft = (node.XPosition - item.Length) / 2;
                Console.WriteLine(item);
            }

            Console.ForegroundColor = prevColor;

            if (Console.CursorTop > CursorTop)
            {
                CursorTop = Console.CursorTop;
            }
        }

        private void SetPositions(Tree<T> node)
        {
            SetXPosition(node);
            SetYPosition(node);
        }


        private void SetXPosition(Tree<T> node)
        {
            if (node.Parent == null)
            {
                node.XPosition = Console.WindowWidth;

                return;
            }

            var parentPosition = node.Parent.XPosition;
            if (GetNumberOfSiblings(node) == 0)
            {
                node.XPosition = parentPosition;

                return;
            }

            var siblings = node.Parent.Children;
            var currentPosition = parentPosition - (LineLength / 2);
            var shareOfLine = (LineLength) / (siblings.Count - 1);
            foreach (var sibling in siblings)
            {
                sibling.XPosition = currentPosition;
                
                if (sibling.Children.Count > 0)
                {
                    foreach (var child in sibling.Children)
                    {
                        SetXPosition(child);
                    }
                }

                currentPosition += shareOfLine;
            }
        }

        private void SetYPosition(Tree<T> node)
        {
            if (node.Parent == null)
            {
                node.YPosition = 0;

                return;
            }

            node.YPosition = node.Parent.YPosition + node.Parent.Info.Length + 2;
            if (node.Children.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    SetYPosition(child);
                }
            }
        }

        private void PrintChildNodes(int startPos, int endpos, List<Tree<T>> nodes)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintBranch(startPos, endpos);
            Console.ForegroundColor = prevColor;
        }

        private void PrintBranch(int startPos, int endPos)
        {
            var centerPosition = (((endPos - startPos) / 2) + PRINT_BUFFER);
            var lineLength = centerPosition - PRINT_BUFFER - 1;

            PrintDownBranch(centerPosition);

            Console.Write(new string(' ', startPos));
            Console.Write(new string('_', lineLength));
            Console.Write('|');
            Console.Write(new string('_', lineLength));
        }

        private void PrintDownBranch(int position)
        {
            Console.Write(new string(' ', position - 1));
            Console.WriteLine("|");
        }

        private Tree<T> RetrieveNode(T key)
        {
            Func<Tree<T>, Tree<T>> retrieveNode = null;
            retrieveNode = node =>
            {
                if (node.Key.Equals(key))
                {
                    return node;
                }

                if (node.Children != null && node.Children.Count > 0)
                {
                    foreach (var child in node.Children)
                    {
                        var soughtChild = retrieveNode(child);
                        if (soughtChild != null)
                        {
                            return soughtChild;
                        }
                    }
                }

                return null;
            };

            var rootNode = GetRoot();

            return retrieveNode(rootNode);
        }
    }
}
