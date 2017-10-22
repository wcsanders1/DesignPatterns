using System;
using System.Collections.Generic;

namespace CommonClientLib
{
    public class Tree<T>
    {
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

        /// <summary>
        /// Creates a new tree, with this node being the root.
        /// </summary>
        /// <param name="key">Unique identifier for the current node.</param>
        /// <param name="info">List of information specific to the node.</param>
        public Tree(T key, params string[] info)
        {
            Key = key;
            Info = info;
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
        /// Prints the tree to the console.
        /// </summary>
        public void PrintTree()
        {
            Console.WriteLine();
            PrintRoot();
        }

        private void PrintRoot()
        {
            var root = GetRoot();
            var rootKey = root.Key.ToString();
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(new string(' ', (Console.WindowWidth - rootKey.Length) / 2));
            Console.WriteLine(rootKey);
            Console.ForegroundColor = prevColor;
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
