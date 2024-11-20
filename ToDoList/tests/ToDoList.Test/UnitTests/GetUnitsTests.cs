namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using NSubstitute.ExceptionExtensions;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Xunit.Sdk;
using System.Security.Cryptography.X509Certificates;

public class GetUnitTests
{
    [Fact]
    public void Get_ReadAndSomeItemIsAvailable_ReturnsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadAll().Returns(
            [
                new ToDoItem{
                    Name = "testName",
                    Description = "testDescription",
                    IsCompleted = false
                }
            ]
            );
        // Act
        var result = controller.Read();
        var resultResult = result.Result;
        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        repositoryMock.Received(1).ReadAll();
    }
    [Fact]
    public void Get_ReadWhenNoItemAvailable_ReturnNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        // repositoryMock.ReadAll().ReturnsNull();
        repositoryMock.ReadAll().Returns(null as IEnumerable<ToDoItem>);
        // Act
        var result = controller.Read();
        var resultResult = result.Result;
        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
        repositoryMock.Received(1).ReadAll();
    }
    [Fact]
    public void Get_ReadUnhandledException_ReturnInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadAll().Throws(new Exception());
        // Act
        var result = controller.Read();
        var resultResult = result.Result;
        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        repositoryMock.Received(1).ReadAll();
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }
}


