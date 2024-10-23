using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using Xunit;
using Xunit.Abstractions;

namespace ToDoList.Test
{
    public class DeleteTests
    {
        [Fact]
        public void DeleteById_WhenItemExists_RemovesItemAndReturnsNoContent()
        {
            // Arrange
            ToDoItemsController.items.Clear();
            var controller = new ToDoItemsController();
            var newToDoItem = new Domain.Models.ToDoItem()
            {
                ToDoItemId = 1,
                Name = "jmeno",
                Description = "popis"
            };

            ToDoItemsController.items.Add(newToDoItem);
            Assert.Single(ToDoItemsController.items);
            //Act

            var result = controller.DeleteById(1);

            //Assert

            Assert.IsType<NoContentResult>(result);
            Assert.Empty(ToDoItemsController.items);
        }


        [Fact]
        public void DeleteById_WhenItemNotExists_ReturnsNotFound()
        {
            //Arrange
            ToDoItemsController.items.Clear();
            var controller = new ToDoItemsController();

            //Act
            var result = controller.DeleteById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}

