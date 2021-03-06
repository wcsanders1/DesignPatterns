﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonClientLib
{
    public class Tree<T>
    {
        private static int CursorTop;

        private const int PRINT_BUFFER = 50;
        private const int MAX_AMOUNT_TO_PRINT = 3;

        private readonly int LineLength = 250;
        
        /// <summary>
        /// Unique key of a node
        /// </summary>
        public T Key { get; }

        /// <summary>
        /// Array of strings that can be printed with each node
        /// </summary>
        public List<string> Info { get; } = new List<string>();
        
        private int Level { get; set; }
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
            Info.AddRange(info);

            // Thses X and Y positions and the level are default. The actual values are set each time another
            // node is added to the tree.
            XPosition = Console.WindowWidth;
            YPosition = 0;
            Level = 0;
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
            SetLevel(newNode);
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
        /// Returns the greatest amount of children that the node or any
        /// of its subnodes has.
        /// </summary>
        /// <param name="tree">Top node from which to search for the greatest number of children</param>
        /// <returns>Greatest amount of children</returns>
        public int GetGreatestNumberOfChildren(Tree<T> tree)
        {
            int getGreatestNumberOfChildren(Tree<T> subTree, int numberOfChildren = 0)
            {
                if (subTree.Children.Count > numberOfChildren)
                {
                    numberOfChildren = subTree.Children.Count;
                }

                foreach (var child in subTree.Children)
                {
                    var childChildren = getGreatestNumberOfChildren(child, numberOfChildren);
                    if (childChildren > numberOfChildren)
                    {
                        numberOfChildren = childChildren;
                    }
                }

                return numberOfChildren;
            }

            return getGreatestNumberOfChildren(tree);
        }

        /// <summary>
        /// Returns the amount of the greatest descending branches of a tree.
        /// </summary>
        /// <param name="node">Tree from which to find greatest number of descending branches</param>
        /// <returns>Number of greatest descending branches</returns>
        public int GetGreatestNumberOfDescendingBranches(Tree<T> node)
        {
            int getGreatestNumberOfDescendingBranches(Tree<T> subNode, int numBranchesDown = 0)
            {
                numBranchesDown++;
                var greatestNumDescendingBranches = 0;
                if (subNode.Children.Count == 0)
                {
                    return numBranchesDown;
                }

                foreach (var child in subNode.Children)
                {
                    var childBranches = getGreatestNumberOfDescendingBranches(child, numBranchesDown);
                    if (childBranches > greatestNumDescendingBranches)
                    {
                        greatestNumDescendingBranches = childBranches;
                    }
                }
                
                return greatestNumDescendingBranches;
            }

            return getGreatestNumberOfDescendingBranches(node);
        }

        /// <summary>
        /// Prints a graphical tree to the console if the number of children of
        /// any node doesn't exceed the specified range: <see cref="MAX_AMOUNT_TO_PRINT"/>; otherwise, prints text only.
        /// </summary>
        /// <param name="node">The parent node from which to print</param>
        public void PrintTree(Tree<T> node)
        {
            if (node.GetGreatestNumberOfChildren(node) > MAX_AMOUNT_TO_PRINT ||
                node.GetGreatestNumberOfDescendingBranches(node) > MAX_AMOUNT_TO_PRINT)
            {
                PrintText(node);
            }
            else
            {
                PrintGraphicalTree(node, Console.CursorTop);
            }
        }
        
        private void PrintText(Tree<T> node, int spacesToIndent = 0)
        {

            var curTextColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.CursorLeft = spacesToIndent;
            Console.WriteLine(node.Key);
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            foreach (var item in node.Info)
            {
                Console.CursorLeft = spacesToIndent + 2;
                Console.WriteLine(item);
            }

            foreach (var child in node.Children)
            {
                PrintText(child, spacesToIndent + 2);
            }

            Console.ForegroundColor = curTextColor;
        }

        private void PrintGraphicalTree(Tree<T> node, int yPosition, int call = 0)
        {
            PrintNode(node, yPosition);

            if (node.Parent != null)
            {
                PrintBranch(node.Parent, node);
            }

            foreach (var child in node.Children)
            {
                PrintGraphicalTree(child, yPosition, call + 1);
            }

            if (call == 0)
            {
                Console.CursorTop = CursorTop;
            }

            Console.WriteLine();
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

        private void PrintBranch(Tree<T> parent, Tree<T> child)
        {
            Console.CursorTop -= (child.YPosition - parent.YPosition) - parent.Info.Count + child.Info.Count;

            if (parent.Children.Count == 1)
            {
                Console.CursorLeft = parent.XPosition / 2;
                Console.WriteLine("|");
                Console.CursorLeft = parent.XPosition / 2;
                Console.WriteLine("|");

                return;
            }
            
            var positions = parent.Children.OrderBy(c => c.XPosition).Select(c => c.XPosition / 2).ToList();
            var parentPosition = parent.XPosition / 2;
            
            var halfDistanceFirst = parentPosition - positions[0];
            var halfDistanceSecond = positions[positions.Count - 1] - parentPosition;
            Console.CursorLeft = positions[0];
            Console.Write(new string('_', halfDistanceFirst));
            Console.Write("|");
            Console.WriteLine(new string('_', halfDistanceSecond));

            Console.CursorLeft = positions[0];

            for (int i = 0; i < positions.Count; i++)
            {
                Console.CursorLeft = positions[i];
                Console.Write("|");
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
            var currentPosition = parentPosition - ((LineLength / 2) - (PRINT_BUFFER * node.Level));
            var shareOfLine = (LineLength - ((PRINT_BUFFER * 2) * node.Level)) / (siblings.Count - 1);
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

            node.YPosition = node.Parent.YPosition + node.Parent.Info.Count + 3;
            if (node.Children.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    SetYPosition(child);
                }
            }
        }

        private void SetLevel(Tree<T> node)
        {
            if (node.Parent == null)
            {
                node.Level = 0;

                return;
            }
            node.Level = node.Parent.Level + 1;
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
