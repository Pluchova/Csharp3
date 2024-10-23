using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using Xunit;

namespace ToDoList.Test
{
    public class PutTests
    {
        [Fact]
        public void Succesfull_UpdatebyID_ReturnsNoContent()
        {
            // Arrange
            ToDoItemsController.items.Clear();
            var controller = new ToDoItemsController();
            var newToDoItem = new ToDoItem
            {
                ToDoItemId = 1,
                Name = "jmeno",
                Description = "popis"
            };

            ToDoItemsController.items.Add(newToDoItem);

            var updatedToDoItem = new ToDoItemUpdateRequestDto(
                Name: "Updated Name",
                Description: "Updated Description",
                IsCompleted: false
            );

            //Act

            var result = controller.UpdateById(newToDoItem.ToDoItemId, updatedToDoItem);

            //Assert

            Assert.IsType<NoContentResult>(result);
            Assert.Equal("Updated Name", ToDoItemsController.items[0].Name);
            Assert.Equal("Updated Description", ToDoItemsController.items[0].Description);
        }
        [Fact]
        public void UpdateById_WhenItemNotExists_ReturnsNotFound()
        {
            //Arrange
            ToDoItemsController.items.Clear();
            var controller = new ToDoItemsController();
            var updatedToDoItem = new ToDoItemUpdateRequestDto(
                   Name: "Updated Name",
                   Description: "Updated Description",
                   IsCompleted: false
               );

            //Act
            var result = controller.UpdateById(2, updatedToDoItem);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
