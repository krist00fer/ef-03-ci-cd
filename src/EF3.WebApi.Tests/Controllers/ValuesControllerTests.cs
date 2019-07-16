using System;
using Xunit;
using FluentAssertions;
using EF3.WebApi.Controllers;

namespace EF3.WebApi.Tests.Controllers
{
    public class ValuesControllerTests
    {
        [Fact]
        public void Get_WithNoParameters_ShouldReturnTwoValues()
        {
            var controller = new ValuesController();

            var result = controller.Get().Value;

            result.Should().BeEquivalentTo("Hello", "CI/CD");
        }

        [Fact]
        public void Get_WithSingleParameter_ShouldReturnSingleValue()
        {
            var controller = new ValuesController();

            var result = controller.Get(1).Value;

            result.Should().Be("value");
        }

        [Fact]
        public void Post_WithAnyValues_ShouldNotCreateAnySideEffects()
        {
            var controller = new ValuesController();

            controller.Post("Hello test!");

            // No side effects
        }
    }
}
