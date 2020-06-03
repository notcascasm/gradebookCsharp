using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
  public interface IBook
  {
    void AddGrade(double grade);
    Statistics GetStatistics();
    string Name { get; }
    event gradeAddedDelegate GradeAdded;
  }

  public abstract class Book : NamedObject, IBook
  {
    public Book(string name) : base(name)
    {

    }

    public abstract event gradeAddedDelegate GradeAdded;

    public abstract void AddGrade(double grade);

    public abstract Statistics GetStatistics();
  }

  public class NamedObject
  {
    public NamedObject(string name)
    {
      Name = name;
    }

    public string Name
    {
      get;
      set;
    }
  }

  public delegate void gradeAddedDelegate(object sender, EventArgs args);

  public class DiskBook : Book
  {
    public DiskBook(string name) : base(name)
    {

    }

    public override event gradeAddedDelegate GradeAdded;

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

      using (var reader = File.OpenText($"{Name}.txt"))
      {
        var line = reader.ReadLine();
        while (line != null)
        {
          var number = double.Parse(line);
          result.Add(number);
          line = reader.ReadLine();
        }
      }


      return result;
    }
  }

  public class InMemoryBook : Book
  {
    public InMemoryBook(string name) : base(name)
    {
      Name = name;
      grades = new List<double>();
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

    public override event gradeAddedDelegate GradeAdded;

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
      }
    }

    public override Statistics GetStatistics()
    {
      var result = new Statistics();

      var index = 0;
      while (index < grades.Count)
      {
        result.Add(grades[index]);
        index++;
      }

      return result;
    }

    private List<double> grades;

    public const string CATEGORY = "Science";
  }
}
