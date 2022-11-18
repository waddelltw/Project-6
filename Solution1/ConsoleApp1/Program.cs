///////////////////////////////////////////////////////////////////////////////
//
// Author: Tyler Waddell, waddelltw@etsu.edu
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 6
// Description: This is where the program runs. It will read from the books.csv file and make a AVL tree from it.
//              DISCLAIMER: IT WORKS FOR SMALL SETS OF DATA...... 
//
///////////////////////////////////////////////////////////////////////////////


using ConsoleApp1;
using System;
using System.Collections.Concurrent;
using System.IO.IsolatedStorage;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace CSCI_1250_Template_2022_Fall
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This statement was for testing the issue about duping and deleting...

            Console.SetOut(new StreamWriter("C:\\Users\\tyler\\Downloads\\books-output2.log"));
           
            // Below this is creating a ne AVLTree object and delcaring one set to null.

            AVLTree tree = new AVLTree();

            AVLTree tempTree = null;

            // This variable represents the currentState of the tree.
            // If this equals 1, the tree will be sorted by titles. If it equals 2, it will be sorted by authors.
            // For anything else, it will be sorted by publisher.

            int currentState = 1; 

            ReadFromFile(tree);

            //tree.InsertNode(tree.Root, new Book("Zen & The Art of Motorcycle Maintenance", "TW", "50", "RNGPub"));

            //tree.InOrder(tree.Root);

            //tree.DeleteNode(tree.Root, "Zen & The Art of Motorcycle Maintenance"); 


            // Here is where various tests were done to find our what was causing the dupes and deletions...
            // Nothing was found.
            // However, everything seemed find with smaller sets of data.


            Console.WriteLine("+++ TITLE +++");
            tree.InOrder(tree.Root); // Title Inorder

            
            
            Console.WriteLine("REORDER");
            Console.WriteLine(tree.Count);
            tree.Root = ReOrder(tempTree, tree, 2, currentState); // Sort the tree by authors
            Console.WriteLine(tree.Count);

            currentState++; // The tree is now sorted by Authors.



            Console.WriteLine();

            Console.WriteLine();

            Console.WriteLine();




            Console.WriteLine("+++ AUTHOR +++");
            tree.InOrder(tree.Root); // Author In order

            Console.WriteLine("REORDER");
            Console.WriteLine(tree.Count);
            tree.Root = ReOrder(tempTree, tree, 3, currentState); // Now sort the tree by publishers
            Console.WriteLine(tree.Count);

            currentState++; // The tree is now sorted by publishers.

            Console.WriteLine();

            Console.WriteLine();

            Console.WriteLine();



            Console.WriteLine("+++ PUBLISHER +++");
            
            Console.WriteLine(tree.Count);
           
            tree.InOrder(tree.Root); 
            
            Console.WriteLine(tree.Count);


            // Console.WriteLine();

            // Console.WriteLine();

            // Console.WriteLine();

            // Console.WriteLine();

            // tree.Root = ReOrder(tempTree, tree, 2, currentState);

            // currentState--;

            // tree.InOrder(tree.Root); // Back to Author...

            Console.Out.Close(); // Put whatever was outputed on the console to a file...
        }

        /// <summary>
        /// This method reads from the file and stores each Book object into the tree.
        /// This is placed in its own method becasue of its size.
        /// </summary>
        /// <param name="tree">The tree that will have nodes added to it.</param>
        public static void ReadFromFile(AVLTree tree)
        {
            StreamReader reader = new("C:\\Users\\tyler\\Downloads\\books.csv");

            string line = reader.ReadLine();

            while (line != null)
            {
                char[] characters = line.ToCharArray(); // Convert the read line into a character array. 

                // These will be used to concateinate each character in there respective variables.

                string title = "";

                string fullName = "";

                string numberOfPages = "";

                string publisher = "";

                char nextCharacter;

                // We start at step 1 because we need to get the title first.

                int step = 1;

                for (int i = 0; i < characters.Length; i++)
                {
                    // This is for reading the book title.

                    if (step == 1)
                    {
                        // If a quote is not found, do this. 

                        if (characters[i] != '"')
                        {
                            // If a comma is found, do this.

                            if (characters[i] == ',')
                            {
                                // Look at the next character after the comma...

                                nextCharacter = characters[i + 1];

                                // If the next character is a space ' ', then add the comma in.

                                if (nextCharacter == ' ')
                                {
                                    title += characters[i];

                                    continue;
                                }

                                // If the next character is not a space ' ', then go to the next step to get the name of the author.

                                else
                                {
                                    step++;

                                    continue;
                                }
                            }

                            // Concateate the character to the string.

                            title += characters[i];
                        }
                    }

                    // This is for reading in the author's name.

                    else if (step == 2)
                    {
                        if (characters[i] != '"')
                        {
                            if (characters[i] == ',')
                            {
                                nextCharacter = characters[i + 1];

                                if (nextCharacter == ' ')
                                {
                                    fullName += characters[i];

                                    continue;
                                }
                                else
                                {
                                    step++;

                                    continue;
                                }
                            }

                            fullName += characters[i];
                        }
                    }


                    // This is for reading in the number of pages.

                    else if (step == 3)
                    {
                        if (characters[i] != '"')
                        {
                            if (characters[i] == ',')
                            {
                                step++;

                                continue;
                            }

                            numberOfPages += characters[i];
                        }
                    }

                    // Step 4 is for reading in the publisher.

                    else if (step == 4)
                    {
                        if (i != characters.Length)
                        {
                            publisher += characters[i];
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                // Add the new object into the tree being made.

                tree.Root = tree.InsertNode(tree.Root, new Book(title, fullName, numberOfPages, publisher), 1);

                // Read the next line

                line = reader.ReadLine();
            }

            //When done, close the file.

            reader.Close();
        }


        /// <summary>
        /// This reorders the tree depending on the state the user wants to go to next. 
        /// This will reorder the tree by either title, author, or publisher
        /// </summary>
        /// <param name="tempTree">The temporary tree that will be filled in a certain way. This will replace the original tree.</param>
        /// <param name="originalTree">The tree that will have all of its nodes removed and added to the temporary tree.</param>
        /// <param name="nextState">This represents the next state you want the tree to be in next. If this equal 1, then the tree will be sorted on titles.
        /// If this is 2, then the tree will be sorted on authors. If this is anything else, it will be sorted by publishers.</param>
        /// <param name="currentMode">The current mode the tree is in. If this is 1, then the tree is already sorted by titles.
        /// If it is 2, then the tree is sorted by authors. If it is anything else, the tree is sorted by publishers</param>
        /// <returns>Returns the root node for the newly sorted tree</returns>
        public static Node ReOrder(AVLTree tempTree, AVLTree originalTree, int nextState, int currentMode)
        {
            Book book; // Declare a Book object.

            string key; // Declare a string called 'key'.

            tempTree = null; // Set the tempTree to null if it was already used.

            if (tempTree == null)
            {
                tempTree = new(); // Make the new AVL tree object
            }
            

            // While the original tree's root is not null, do this...

            while (originalTree.Root != null)
            {
                book = originalTree.Root.Data; // Get the root's book data.

                key = originalTree.GetKey(book, currentMode); // Find what key (title, author, publisher) we need.

                tempTree.Root = tempTree.InsertNode(tempTree.Root, book, nextState); // Add the book into the new tree
                
                originalTree.Root = originalTree.DeleteNode(originalTree.Root, key, currentMode); // Delete the node from the original tree.
            }

            // Whenever this ends, return the temporary tree's root back to Main.

            return tempTree.Root;
            
        }
    }
}