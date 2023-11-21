using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testWorkIntellectSoft.API.Controllers;
using testWorkIntellectSoft.API.Services;
using testWorkIntellectSoft.API.Test.Mockdata;

namespace testWorkIntellectSoft.API.Test.Controllers
{
    public class TestUserController
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(a => a.GetUsersAsync(0, null)).ReturnsAsync(UserDTOMockData.GetUsers());
            var sut = new UserController(userService.Object);

            var result = (OkObjectResult)await sut.GetUsers();

            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task SaveAsync_ShouldCall_IUserService_SaveAsync_AtleastOnce()
        {
            var userService = new Mock<IUserService>();
            var newTodo = UserDTOMockData.NewUser();
            var sut = new UserController(userService.Object);

            var result = await sut.CreateUser(newTodo);

            userService.Verify(_ => _.CreateUserAsync(newTodo), Times.Exactly(1));
        }
    }
}
