using System;
using Xunit;

namespace GradeBook.Tests
{
  public class BookTests
  {
    [Fact]
    public void BookCalculatesAnAverageGrade()
    {
      InMemoryBook book = new InMemoryBook("");
      book.AddGrade(19.1);
      book.AddGrade(89.1);
      book.AddGrade(23.3);

      var result = book.GetStatistics();

      Assert.Equal(43.8, result.Average, 1);
      Assert.Equal(89.1, result.High, 1);
      Assert.Equal(19.1, result.Low, 1);
      Assert.Equal('F', result.Letter);
    }
  }
}
