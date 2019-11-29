using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
  public delegate void GradeAddedDelegate(object sender, EventArgs args);

  public class NamedObject
  {
    public NamedObject(string name)
    {
      Name = name;
    }

    public string Name { get; set; }
  }

  public interface IBook
  {
    void AddGrade(double grade);
    Statistics GetStatistics();
    string Name { get; }
    event GradeAddedDelegate GradeAdded;
  }

  public abstract class Book : NamedObject, IBook
  {
    public Book(string name) : base(name)
    {
    }

    public abstract event GradeAddedDelegate GradeAdded;
    public abstract void AddGrade(double grade);
    public abstract Statistics GetStatistics();
  }

  public class DiskBook : Book
  {
    public DiskBook(string name) : base(name)
    {
    }

    public override event GradeAddedDelegate GradeAdded;

    public override void AddGrade(double grade)
    {
      using (var writer = File.AppendText($"{Name}.txt"))
      {
        writer.WriteLine(grade);
        if (GradeAdded != null)
        {
          GradeAdded(this, new EventArgs());
        }
      }
    }

    public override Statistics GetStatistics()
    {
      var result = new Statistics();

      using (var file = File.OpenText($"{Name}.txt"))
      {
        var line = file.ReadLine();

        while (line != null)
        {
          var grade = double.Parse(line);
          result.Add(grade);
          line = file.ReadLine();
        }
      }

      return result;

      throw new NotImplementedException();
    }
  }

  public class InMemoryBook : Book
  {
    public InMemoryBook(string name) : base(name)
    {
      grades = new List<double>();
    }

    public void AddGrade(char letter)
    {
      switch (letter)
      {
        case 'A':
          AddGrade(90);
          break;

        case 'B':
          AddGrade(80);
          break;

        case 'C':
          AddGrade(70);
          break;

        case 'D':
          AddGrade(60);
          break;

        default:
          AddGrade(0);
          break;
      }
    }

    public override void AddGrade(double grade)
    {
      if (grade <= 100 && grade >= 0)
      {
        grades.Add(grade);
        if (GradeAdded != null)
        {
          GradeAdded(this, new EventArgs());
        }
      }
      else
      {
        throw new ArgumentException($"Invalid {nameof(grade)}");
      }
    }

    public override event GradeAddedDelegate GradeAdded;

    public override Statistics GetStatistics()
    {
      var result = new Statistics();

      foreach (double grade in grades)
      {
        result.Add(grade);
      }

      return result;
    }

    private List<double> grades;
    public const string CATEGORY = "Science";
  }
}