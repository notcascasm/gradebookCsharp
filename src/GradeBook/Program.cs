using System;
using System.Collections.Generic;

namespace GradeBook
{
  public class Program
  {
    static void Main(string[] args)
    {
      IBook book = new DiskBook("scott's gradebook");
      book.GradeAdded += OnGradeAdded;

      Entergrades(book);
      var stats = book.GetStatistics();

      Console.WriteLine($"For the book named {book.Name}");
      Console.WriteLine($"The average grade is {stats.Average:N1}! ");
      Console.WriteLine($"The lowest grade is {stats.Low:N1}");
      Console.WriteLine($"The highest grade is {stats.High:N1}");
      Console.WriteLine($"The letter grade is {stats.Letter}");
    }

    private static void Entergrades(IBook book)
    {
      while (true)
      {
        System.Console.WriteLine("Enter a grade or press q to quit");
        var input = Console.ReadLine();

        if (input == "q")
        {
          break;
        }

        try
        {
          var grade = double.Parse(input);
          book.AddGrade(grade);
        }
        catch (Exception ex)
        {
          System.Console.WriteLine(ex.Message);
        }
        finally
        {
          System.Console.WriteLine("*****");
        }
      }
    }

    static void OnGradeAdded(object sender, EventArgs e)
    {
      System.Console.WriteLine("A grade was added");
    }
  }
}