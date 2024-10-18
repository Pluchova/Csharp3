using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using Xunit;

namespace ToDoList.Test
{
    public class GetTests
    {

        [Fact]
        public void Get_All_ItemsWhenThereNoItems_ReturnsNotFound()
        {
            //Arrange
            ToDoItemsController.items.Clear();
            var controller = new ToDoItemsController();

            //Act
            var result = controller.Read();
            var resultResult = result.Result;

            //Assert
            Assert.IsType<NotFoundResult>(resultResult);
        }

        [Fact]
        public void Get_All_ItemsWhenThereSomeItems_ReturnsOk()
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

            //Act
            var result = controller.Read();

            //Assert
            var resultOkObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnItems = (resultOkObjectResult.Value as IEnumerable<ToDoItemGetResponseDto>).ToList();

            Assert.Single(returnItems);
            Assert.Equal(newToDoItem.Name, returnItems[0].Name);
        }

        [Fact]
        public void Get_ItemsById_IfExists_ReturnsOk()
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

            //Act
            var result = controller.ReadById(newToDoItem.ToDoItemId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<ToDoItemGetResponseDto>(okResult.Value);
            Assert.Equal(1, item.Id);
        }

        [Fact]
        public void Get_ItemsById_IfNotExists_ReturnsNotFound()
        {
            //Arrange
            ToDoItemsController.items.Clear();
            var controller = new ToDoItemsController();

            //Act
            var result = controller.ReadById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }



    }
}
