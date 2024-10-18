using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using Xunit;

namespace ToDoList.Test
{
    public class PostTests
    {
        [Fact]
        public void Create_ReturnCreated()
        {
            // Arrange
            ToDoItemsController.items.Clear();
            var controller = new ToDoItemsController();
            var newToDoItem = new ToDoItemCreateRequestDto
            (
                Name: "jmeno",
                Description: "popis",
                IsCompleted: true
            );

            //Act
            var result = controller.Create(newToDoItem) as CreatedAtActionResult;

            //Assert

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            var responseDto = Assert.IsType<ToDoItemGetResponseDto>(result.Value);
            Assert.Equal("jmeno", responseDto.Name);
            Assert.Equal(1, responseDto.Id);
        }
    }
}
