using System;
using System.Collections.Generic;
using Xunit;

namespace GradeBook.Tests
{
  public class BookTests
  {
    [Fact]
    public void GradesMustBeBetweenZeroAndOneHundred()
    {
      var book = new InMemoryBook("");
      var grades = new List<double>(3) { -10, 200, 90 };

      foreach (double grade in grades)
      {
        try
        {
          book.AddGrade(grade);
        }
        catch (ArgumentException ex)
        {
          Console.WriteLine(ex.Message);
        }
      }

      var stats = book.GetStatistics();

      Assert.Equal(90.0, stats.Average, 1);
      Assert.Equal(90.0, stats.High, 1);
      Assert.Equal(90.0, stats.Low, 1);
    }

    [Fact]
    public void BookCalculatesAnAverageGrade()
    {
      var book = new InMemoryBook("");
      book.AddGrade(81.3);
      book.AddGrade(93.2);
      book.AddGrade(77.3);

      var result = book.GetStatistics();

      Assert.Equal(83.9, result.Average, 1);
      Assert.Equal(93.2, result.High, 1);
      Assert.Equal(77.3, result.Low, 1);
      Assert.Equal('B', result.Letter);
    }
  }
}
