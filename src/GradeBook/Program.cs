using System;
using System.Collections.Generic;

namespace GradeBook
{
  class Program
  {
    static void Main(string[] args)
    {
      IBook book = new DiskBook("Austin's GradeBook");
      book.GradeAdded += OnGradeAdded;

      EnterGrades(book);

      var stats = book.GetStatistics();

      Console.WriteLine($"The gradebook name is {book.Name}");
      Console.WriteLine($"The high grade is {stats.High}");
      Console.WriteLine($"The low grade is {stats.Low}");
      Console.WriteLine($"The average is {stats.Average:N2}");
      Console.WriteLine($"The letter is {stats.Letter}");
    }

    private static void EnterGrades(IBook book)
    {
      while (true)
      {
        Console.WriteLine("Enter a grade or press q to quit.");
        var input = Console.ReadLine();

        if (input == "q")
        {
          Console.WriteLine("breaking");
          break;
        }

        try
        {
          var grade = double.Parse(input);
          book.AddGrade(grade);
        }
        catch (ArgumentException ex)
        {
          Console.WriteLine(ex.Message);
        }
        catch (FormatException ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
    }

    static void OnGradeAdded(object sender, EventArgs e)
    {
      Console.WriteLine("A grade was added");
    }
  }
}
