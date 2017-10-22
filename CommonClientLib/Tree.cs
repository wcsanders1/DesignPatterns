using System;
using System.Collections.Generic;

namespace CommonClientLib
{
    public class Tree<T>
    {
        private static int NumNodes { get; set; } = 1;

        public T Key { get; }
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
            NumNodes++;

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
                if (node.Parent != null)
                {
                    getRoot(node.Parent);
                }

                return this;
            };

            return getRoot(this);
        }

        /// <summary>
        /// Returns the numbers of nodes in the tree.
        /// </summary>
        /// <returns>Number of nodes in the tree</returns>
        public int GetNumberOfNodes()
        {
            return NumNodes;
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
                        if (retrieveNode(child) != null)
                        {
                            return child;
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
