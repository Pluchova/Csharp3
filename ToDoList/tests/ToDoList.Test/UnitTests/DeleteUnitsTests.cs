using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Http;
using NSubstitute.ReturnsExtensions;
using NSubstitute.ExceptionExtensions;
namespace ToDoList.Test.UnitTests;

public class DeleteUnitsTests
{
    [Fact]

    public void Delete_DeleteByIdValidItemId_ReturnsNoContent()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadById(Arg.Any<int>()).Returns(new ToDoItem());

        int testId = 1;
        //Act

        var result = controller.DeleteById(testId);


        //Assert

        Assert.IsType<NoContentResult>(result);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.Received(1).DeleteById(testId);
    }

    [Fact]

    public void Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadById(Arg.Any<int>()).ReturnsNull();

        int testId = 1;
        //Act

        var result = controller.DeleteById(testId);


        //Assert

        Assert.IsType<NotFoundResult>(result);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.Received(0).DeleteById(testId);

    }

    [Fact]

    public void Delete_DeleteByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadById(Arg.Any<int>()).Throws(new Exception());
        int testId = 1;
        // Act
        var result = controller.DeleteById(testId) as ObjectResult;

        // Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, result?.StatusCode);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.Received(0).DeleteById(testId);
    }

}
