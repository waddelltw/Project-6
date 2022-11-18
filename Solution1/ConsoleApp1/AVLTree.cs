///////////////////////////////////////////////////////////////////////////////
//
// Author: Tyler Waddell, waddelltw@etsu.edu
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 6
// Description: This is where AVLTree class is.
//              This class represents how an AVL Tree would function.
//              FOUND FROM GEEKS FOR GEEKS
//
///////////////////////////////////////////////////////////////////////////////

using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    /// <summary>
    /// This is the AVL tree class. It represents how an AVL tree would function.
    /// *************** FOUND FROM GEEKS FOR GEEKS ******************
    /// </summary>
    internal class AVLTree
    {
        public Node Root { get; set; } // The root node of the tree

        public int Count { get; private set; } // Count of how many nodes are in the tree

        // This gives the height of a node.
        /// <summary>
        /// This method gives the height of a node.
        /// </summary>
        /// <param name="node">The incoming node to be check for height.</param>
        /// <returns>Returns the height of the node</returns>
        public static int Height(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        /// <summary>
        /// This method gets the max from two number.
        /// </summary>
        /// <param name="n1">The incoming first number</param>
        /// <param name="n2">The second incoming number</param>
        /// <returns>Returns the maximum from two values</returns>
        public int Max(int n1, int n2)
        {
            return Math.Max(n1, n2);
        }


        /// <summary>
        /// This method performs a right rotation on nodes if something is deleted or added into the tree.
        /// This method will only be called once the balance of the tree is distorted.
        /// </summary>
        /// <param name="y">The incoming node to be rotated.</param>
        /// <returns>Returns the node back to the method calling it.</returns>
        public Node RightRotation(Node y)
        {
            Node x = y.LeftChild;

            Node T2 = x.RightChild;



            x.RightChild = y;

            y.LeftChild = T2;




            return x;
        }

        /// <summary>
        /// This method performs a left rotation on nodes if something is deleted or added into the tree.
        /// This method will only be called once the balance of the tree is distorted.
        /// </summary>
        /// <param name="x">The incoming node to be rotated</param>
        /// <returns>Returns the node back to the method calling it</returns>
        public Node LeftRotation(Node x)
        {
            Node y = x.RightChild;

            Node T2 = y.LeftChild;


            y.LeftChild = x;

            x.RightChild = T2;


            return y;
        }


        /// <summary>
        /// This method gets the balance of tree. This is used when adding or removing nodes from the tree.
        /// </summary>
        /// <param name="node">The incoming node to be check for balance</param>
        /// <returns>Returns the current balance of the tree</returns>
        public int GetBalance(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return Height(node.LeftChild) - Height(node.RightChild);
        }

        /// <summary>
        /// This method will allow the user to insert nodes into the AVL tree. Once a node is inserted, the tree will check to see if its balanced.
        /// </summary>
        /// <param name="node">The incoming node that will be added to</param>
        /// <param name="book">The incoming book to be added to the node</param>
        /// <param name="currentMode">The current mode that the tree is in. Shows how the tree will be sorted.</param>
        /// <returns>Returns back to the main with the newly inserted node</returns>
        public Node InsertNode(Node node, Book book, int currentMode)
        {
            string key, nodeKey, leftChildKey, rightChildKey = ""; // Find out what the tree is currently keyed on.

            Count++;

            if (node == null)
            {
                return new Node(book);
            }

            
            key = GetKey(book, currentMode);

            nodeKey = GetNodeKey(node, currentMode);



            if (key.CompareTo(nodeKey) < 0) // If the key is less than the node's key, go to the left child of that node.
            {
                node.LeftChild = InsertNode(node.LeftChild, book, currentMode);
            }


            else if(key.CompareTo(nodeKey) >= 0) // If the key is greater than or equal to the node's key, go to the right child of that node.
            {
                node.RightChild = InsertNode(node.RightChild, book, currentMode);
            }


            ///////////////////////////////////////////////////////////////////////////////////////
            /// Re-Balance the Tree
            ///////////////////////////////////////////////////////////////////////////////////////


            // Get the balance

            int balance = GetBalance(node);


            ///////////////////////////////////////////////////////////////////////////////////////


            // If the balance is distorted do this...
            if (balance > 1) 
            {
                if(node.LeftChild != null) // Check if the current node's left child is null.
                {
                    leftChildKey = GetNodeKey(node.LeftChild, currentMode); // Get the child's key

                    if(key.CompareTo(leftChildKey) <= 0)
                    {
                        return RightRotation(node); // Left- Left
                    }
                    else
                    {
                        node.LeftChild = LeftRotation(node.LeftChild); // Left - Right

                        return RightRotation(node);
                    }
                }
            }

            // If the tree's balance is distored, do this...

            if(balance < -1)
            {
                if(node.RightChild != null) // Check if the right child is null...
                {
                    rightChildKey = GetNodeKey(node.RightChild, currentMode); // Get that child's key.

                    if(key.CompareTo(rightChildKey) >= 0)
                    {
                        return LeftRotation(node); // Right - Right
                    }
                    else
                    {
                        node.RightChild = RightRotation(node.RightChild);

                        return LeftRotation(node); //Right - Left
                    }
                
                }
            }            

            return node;

        }

        /// <summary>
        /// This method deletes a node from the tree. Once this happens the tree will be checked for balance.
        /// </summary>
        /// <param name="node">The node to be deleted.</param>
        /// <param name="key">This is what the treee is currently keyed on</param>
        /// <param name="currentMode">The current mode the tree is on (title, author, publisher)</param>
        /// <returns></returns>
        public Node DeleteNode(Node node, string key, int currentMode)
        {
            Count--;
            
            // First, perform a normal delete. 
            
            if (node == null)
            {
                return node;
            }
            
            // Find the node's key

            string nodeKey;

            if (currentMode == 1)
            {
                nodeKey = node.Data.Title;
            }
            else if(currentMode == 2)
            {
                nodeKey = node.Data.Author;
            }
            else
            {
                nodeKey = node.Data.Publisher;
            }



            // If the key to be deleted is less than the 
            // root's key, then it lies in left subtree 

            if (key.CompareTo(nodeKey) < 0)
            {
                node.LeftChild = DeleteNode(node.LeftChild, key, currentMode);
            }


            // If the key to be deleted is greater than the 
            // root's key, then it lies in right subtree 

            else if (key.CompareTo(nodeKey) > 0)
            {
                node.RightChild = DeleteNode(node.RightChild, key, currentMode);
            }


            // If key is same as root's key, then this is the node 
            // To be deleted 
            else
            {

                // Node with only one child or no child 
                if ((node.LeftChild == null) || (node.RightChild == null))
                {
                    Node? temp = null;

                    if (temp == node.LeftChild)
                    {
                        temp = node.RightChild;
                    }

                    else
                    {
                        temp = node.LeftChild;
                    }


                    // No child case 
                    if (temp == null)
                    {
                        temp = node;

                        node = null;
                    }

                    else // One child case 
                    {
                        node = temp; // Copy the contents of the non-empty child
                    }

                }

                else
                {

                    // Node with two children: Get the inorder 
                    // Successor (smallest in the right subtree) 

                    Node temp = MinValueNode(node.RightChild);

                    // Copy the inorder successor's data to this node
                    
                    node.Data = temp.Data;

                    // Delete the inorder successor 

                    string tempKey;

                    if(currentMode == 1)
                    {
                        tempKey = temp.Data.Title;
                    }
                    else if(currentMode == 2)
                    {
                        tempKey = temp.Data.Author;
                    }
                    else
                    {
                        tempKey = temp.Data.Publisher;
                    }


                    node.RightChild = DeleteNode(node.RightChild, tempKey, currentMode);
                }

            }


            // If the tree had only one node then return
            
            if (node == null)
            {
                return node;
            }


            // Deleted by decree of Mr. Gillenwater
            //node.Height = Max(Height(node.LeftChild), Height(node.RightChild)) + 1;

            // Get the current balance

            int balance = GetBalance(node);


            // If this node becomes unbalanced, 
            // then there are 4 cases 
            // Left Left Case
            
            if (balance > 1 && GetBalance(node.LeftChild) >= 0)
            {
                return RightRotation(node);
            }


            // Left Right Case 

            if (balance > 1 && GetBalance(node.LeftChild) < 0)
            {
                node.LeftChild = LeftRotation(node.LeftChild);

                return RightRotation(node);
            }


            // Right Right Case 

            if (balance < -1 && GetBalance(node.RightChild) <= 0)
            {
                return LeftRotation(node);
            }


            // Right Left Case 

            if (balance < -1 && GetBalance(node.RightChild) > 0)
            {
                node.RightChild = RightRotation(node.RightChild);

                return LeftRotation(node);
            }

            return node; 
        }





        /// <summary>
        /// This is a method called MinValueNode. This searches the entire tree for the node with the lowest value.
        /// </summary>
        /// <param name="node">The node that will be checked</param>
        /// <returns>Returns the node with the minimum value within the tree.</returns>
        public Node MinValueNode(Node node)
        {
            Node current = node;

            // Loop down to find the leftmost leaf 
            while (current.LeftChild != null)
            {
                current = current.LeftChild;
            }


            return current;
        }


        /// <summary>
        /// This method displays the tree in the Inorder travseral. This is called recursively
        /// </summary>
        /// <param name="node">The incoming node whose data is to be displayed.</param>
        public void InOrder(Node node)
        {
            if (node == null)
            {
                return;
            }

            InOrder(node.LeftChild);

            node.Data.Print();

            InOrder(node.RightChild);
        }


        /// <summary>
        /// This method gets the key for whatever mode the tree is currently on.
        /// </summary>
        /// <param name="book">The incoming book to get data from.</param>
        /// <param name="currentMode">The mode the tree is currently on.</param>
        /// <returns>Returns the key for the node.</returns>
        public string GetKey(Book book, int currentMode)
        {
            string key = "";

            if (currentMode == 1)
            {
                key = book.Title;
            }
            else if (currentMode == 2)
            {
                key = book.Author;
            }
            else
            {
                key = book.Publisher;
            }

            return key;
        }


        /// <summary>
        /// This method gets what the node's current key is. This is very similar to the GetKey method.
        /// </summary>
        /// <param name="node">The incoming node to be check for the key</param>
        /// <param name="mode">The mode the node/tree is currently on.</param>
        /// <returns>The key that the node was keyed on</returns>
        public string GetNodeKey(Node node, int mode)
        {
            string key = "";

            if (mode == 1)
            {
                key = node.Data.Title;
            }
            else if (mode == 2)
            {
                key = node.Data.Author;
            }
            else
            {
                key = node.Data.Publisher;
            }

            return key;
        }


    }
    
}
