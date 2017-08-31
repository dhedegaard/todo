using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using todo.ViewModels;
using Xunit;

namespace todo.tests
{
    public class AuthControllerTest
    {
        private readonly AuthController _controller = new AuthController(null, null, null);

        [Fact]
        public void Login_Get()
        {
            var result = _controller.Login();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<LoginUser>(viewResult.Model);
        }

        [Fact]
        public void Register_Get()
        {
            var result = _controller.Register();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<RegisterUser>(viewResult.Model);
        }
    }
}