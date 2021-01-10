using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TsBuilder;
using Xunit;

namespace TsBuilderTest
{
    public class EnvironmentTest
    {
        [Fact]
        public void IsBuildEnvironment()
        {
            // Arrange
            var args = new string[] { "--production" };

            // Act
            TsBuilder.Environment.SetEnvironmentType(args);

            // Assert
            Assert.Equal(EnvironmentType.Prod, TsBuilder.Environment.Type);
        }

        [Fact]
        public void IsDebugEnvironment()
        {
            // Arrange
            string[] args = null;

            // Act
            TsBuilder.Environment.SetEnvironmentType(args);

            // Assert
            Assert.Equal(EnvironmentType.Debug, TsBuilder.Environment.Type);
        }

        [Fact]
        public void IsFailedEnvironment()
        {
            // Arrange
            var args = new string[] { "--stage" };

            // Act
            Action action = () => TsBuilder.Environment.SetEnvironmentType(args);

            // Assert
            var exception = Assert.Throws<Exception>(action);
            Assert.Equal("environment is not defined", exception.Message);

        }
    }
}
