using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public struct BookPosition
    {
        public int shkafNumber;
        public int polkaNumber;

        public BookPosition(int shkafNumber, int polkaNumber)
        {
            this.shkafNumber = shkafNumber;
            this.polkaNumber = polkaNumber;
        }
    }

    public struct Book
    {
        public string author;
        public string bookName;
        public int yearPublishing;
        public string publisherName;
        public BookPosition bookPosition;

        public Book(string dateInline)
        {
            string[] date = dateInline.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            author = date[0] + " " + date[1];
            bookName = date[2];
            yearPublishing = int.Parse(date[3]);
            publisherName = date[4];
            bookPosition = new BookPosition(int.Parse(date[5]), int.Parse(date[6]));
        }

        public Book(string author, string bookName, int yearPublishing, string publisherName, BookPosition bookPosition) : this()
        {
            this.bookName = bookName;
            this.yearPublishing = yearPublishing;
            this.publisherName = publisherName;
            this.bookPosition = bookPosition;
        }
    }

    public class Program
    {
        public static Book[] ReadDate(string filePath)
        {
            int count = File.ReadLines(filePath).Count();
            Book[] books = new Book[count];
            StreamReader sr = new StreamReader(filePath, encoding: Encoding.Default);
            string line;
            int i = 0;
            while ((line = sr.ReadLine()) != null)
            {
                books[i] = new Book(line);
                i++;
            }
            sr.Close();
            return books;
        }

        public static BookPosition GetCurrentBookPOsition(Book[] books, string author, string bookName)
        {            
            foreach (Book book in books)
            {
                if (book.author == author && book.bookName == bookName)
                {
                    return new BookPosition(book.bookPosition.shkafNumber, book.bookPosition.polkaNumber);
                }
            }            
            return new BookPosition(0, 0);
        }

        public static Book[] GetBooksByAuthor(Book[] books, string author)
        {
            int count = 0;
            foreach(Book book in books)
            {
                if (book.author == author)
                    count++;
            }
            Book[] newBooks = new Book[count];
            for(int i = 0, j = 0; i < books.Length - 1; i++)
            {
                if (books[i].author == author)
                {
                    newBooks[j++] = new Book(books[i].author, books[i].bookName, books[i].yearPublishing, books[i].publisherName, books[i].bookPosition);
                }
            }
            return newBooks;
        }

        public static int GetCountBookOfOneYear(Book[] books, int year)
        {
            int count = 0;
            foreach (Book book in books)
            {
                if (book.yearPublishing == year)
                    count++;
            }
            return count;
        }

        static void PrintDate(Book[] books)
        {
            foreach (Book book in books)
            {
                Console.WriteLine($"{book.author} {book.bookName} {book.yearPublishing} {book.publisherName} {book.bookPosition.shkafNumber} {book.bookPosition.polkaNumber}");
            }
        }

        static void Main(string[] args)
        {
            string filePath = "data.txt";
            Book[] books =  ReadDate(filePath);
            PrintDate(books);
            Console.WriteLine("Введіть автора книги яку хочете знайти:");
            string author = Console.ReadLine();
            Console.WriteLine("Введіть назву книги яку хочете знайти:");
            string bookName = Console.ReadLine();
            BookPosition pBooks = GetCurrentBookPOsition(books, author, bookName);
            Console.WriteLine(pBooks.shkafNumber + " " + pBooks.polkaNumber + "\n");
            Console.WriteLine("Введіть автора книги якого хочете знайти:");
            author = Console.ReadLine();
            Book[] books1 = GetBooksByAuthor(books, author);
            PrintDate(books1);
            Console.WriteLine("Введіть рік видавництва:");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine(GetCountBookOfOneYear(books, year));
            Console.ReadKey();
        }

    }
}
