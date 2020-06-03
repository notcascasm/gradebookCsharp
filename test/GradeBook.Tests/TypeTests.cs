using System;
using Xunit;

namespace GradeBook.Tests
{
  public delegate string WriteLogDelegate(string logMessage);

  public class TypeTests
  {
    int count = 0;
    [Fact]
    public void WriteLogDelegateCanPointToMethod()
    {
      WriteLogDelegate log = AddCount;

      log += ReturnMessage;

      var result = log("hello!");
      Assert.Equal("hello!", result);
      Assert.Equal(2, count);
    }
    string AddCount(string message)
    {
      count++;
      return message;
    }

    string ReturnMessage(string message)
    {
      count++;
      return message;
    }

    [Fact]
    public void test1()
    {
      var x = GetInt();
      SetInt(ref x);

      Assert.Equal(42, x);
    }

    private void SetInt(ref int x)
    {
      x = 42;
    }

    private int GetInt()
    {
      return 3;
    }

    [Fact]
    public void CSharpCanPassByRef()
    {
      InMemoryBook book1 = GetBook("Book1");
      GetBookSetname(ref book1, "New Name");

      Assert.Equal("New Name", book1.Name);
    }

    private void GetBookSetname(ref InMemoryBook book, string name)
    {
      book = new InMemoryBook(name);
    }


    [Fact]
    public void CSharpIsPssByValue()
    {
      InMemoryBook book1 = GetBook("Book1");
      GetBookSetname(book1, "New Name");

      Assert.Equal("Book1", book1.Name);
    }

    private void GetBookSetname(InMemoryBook book, string name)
    {
      book = new InMemoryBook(name);
    }

    [Fact]
    public void GetBookReturnsDifferentObjects()
    {
      InMemoryBook book1 = GetBook("Book1");
      InMemoryBook book2 = GetBook("Book2");

      Assert.Equal("Book1", book1.Name);
      Assert.Equal("Book2", book2.Name);
      Assert.NotSame(book1, book2);
    }

    [Fact]
    public void TwoVarsCanReferenceSameObject()
    {
      InMemoryBook book1 = GetBook("Book1");
      InMemoryBook book2 = book1;

      Assert.Same(book1, book2);
      Assert.True(Object.ReferenceEquals(book1, book2));
    }

    private InMemoryBook GetBook(string name)
    {
      return new InMemoryBook(name);
    }
  }
}
