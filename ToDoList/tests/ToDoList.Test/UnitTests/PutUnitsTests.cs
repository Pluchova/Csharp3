namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Http;
using NSubstitute.ReturnsExtensions;
using NSubstitute.ExceptionExtensions;

public class PutUnitTests
{
    [Fact]
    public void Put_UpdateByIdWhenItemUpdated_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 1;
        var testItem = new ToDoItem
        {
            ToDoItemId = testId,
            Name = "Old Name",
            Description = "Old Description",
            IsCompleted = false
        };

        var updatedItem = new ToDoItemUpdateRequestDto("Updated Name", "Updated Description", true);
        repositoryMock.ReadById(testId).Returns(testItem);

        // Act
        var result = controller.UpdateById(testId, updatedItem);
        // Assert

        Assert.IsType<NoContentResult>(result);

        repositoryMock.Received(1).Update(Arg.Is<ToDoItem>(item =>
            item.ToDoItemId == testId &&
            item.Name == "Updated Name" &&
            item.Description == "Updated Description" &&
            item.IsCompleted == true));
    }

    [Fact]
    public void Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadById(Arg.Any<int>()).ReturnsNull();


        int testId = 1;
        var testItem = new ToDoItem
        {
            ToDoItemId = testId,
            Name = "Old Name",
            Description = "Old Description",
            IsCompleted = false
        };
        var updatedItem = new ToDoItemUpdateRequestDto("Updated Name", "Updated Description", true);
        //Act

        var result = controller.UpdateById(testId, updatedItem);
        //Assert

        Assert.IsType<NotFoundResult>(result);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.Received(0).Update(Arg.Any<ToDoItem>());

    }
    [Fact]
    public void Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadById(Arg.Any<int>()).Throws(new Exception());

        int testId = 1;
        var testItem = new ToDoItem
        {
            ToDoItemId = testId,
            Name = "Old Name",
            Description = "Old Description",
            IsCompleted = false
        };
        var updatedItem = new ToDoItemUpdateRequestDto("Updated Name", "Updated Description", true);

        // Act
        var result = controller.UpdateById(testId, updatedItem);


        // Assert

        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.Received(0).Update(Arg.Any<ToDoItem>());
    }

}
