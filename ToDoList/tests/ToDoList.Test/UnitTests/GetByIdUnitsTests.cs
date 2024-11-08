namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;


public class GetByIdUnitTests
{
    [Fact]
    public void Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testItem = new ToDoItem
        {
            Name = "testName",
            Description = "testDescription",
            IsCompleted = false
        };
        repositoryMock.ReadById(Arg.Any<int>()).Returns(testItem);

        int testId = 1;
        //Act

        var result = controller.ReadById(testId);
        var resultResult = result.Result;
        //Assert
        Assert.IsType<OkObjectResult>(resultResult);
        repositoryMock.Received(1).ReadById(testId);

    }

    [Fact]
    public void Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadById(Arg.Any<int>()).ReturnsNull();

        int testId = 1;
        //Act

        var result = controller.ReadById(testId);
        var resultResult = result.Result;
        //Assert

        Assert.IsType<NotFoundResult>(resultResult);
        repositoryMock.Received(1).ReadById(testId);

    }
    [Fact]
    public void Get_ReadByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadById(Arg.Any<int>()).Throws(new Exception());

        int testId = 1;

        // Act
        var result = controller.ReadById(testId);
        var resultResult = result.Result;

        // Assert

        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
        repositoryMock.Received(1).ReadById(testId);
    }
}
