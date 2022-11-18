///////////////////////////////////////////////////////////////////////////////
//
// Author: Tyler Waddell, waddelltw@etsu.edu
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 6
// Description: This is where Book class is. This class reprsents the details of a book.
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
    /// This is the book class. This class simulates the details of a book like the author, title, publisher, etc.
    /// </summary>
    internal class Book
    {
        // Below are a few properties of the book class.

        public string Title { get; set; } // The title of the book.

        public string Author { get; set; } // The author of the book.

        public int Pages { get; set; } // The number of pages in the book

        public string Publisher { get; set; } // The publisher of the book

        /// <summary>
        /// This is the constructor of the book class. Whenever a Book object is created is must have these parameters.
        /// </summary>
        /// <param name="title">The title of the book</param>
        /// <param name="author">The author of the book.</param>
        /// <param name="pages">The number of pages in the book</param>
        /// <param name="publisher">The publisher of the book</param>
        public Book(string title, string author, string pages, string publisher)
        {
            Title = title;
            
            Author = author;
            
            Pages = Convert.ToInt32(pages);
            
            Publisher = publisher;
        }

        /// <summary>
        /// This method displays the Book object's details such as the title, author, publisher, etc.
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"Title: {Title}\nAuthor: {Author}\nPages: {Pages}\nPublisher: {Publisher}\n");
        }
    }
}
