///////////////////////////////////////////////////////////////////////////////
//
// Author: Tyler Waddell, waddelltw@etsu.edu
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 6
// Description: This is where Node class is. It represents a Node in an AVL tree.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// This is the Node class. This represents nodes in a tree. These nodes will hold Book objects.
    /// </summary>
    internal class Node
    {
        public Book Data { get; set; } // The data the the nodes hold (Books)

        public int Height => Math.Max(AVLTree.Height(LeftChild), AVLTree.Height(RightChild)) + 1; // The node constantly keeps track of its height.
        
        public Node LeftChild { get; set; } // The left child of the node.

        public Node RightChild { get; set; } // The right child of the node.

        /// <summary>
        /// This is the constructor for the Node class. You have to have a book object to make a Node object.
        /// </summary>
        /// <param name="data">The incoming book to be added in the node.</param>
        public Node(Book data)
        {
            Data = data;
        }
    }
}
