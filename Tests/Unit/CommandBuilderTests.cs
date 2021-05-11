using System;
using System.Collections.Generic;
using API.Helpers;
using Main.Commands;
using Xunit;

namespace Tests.Unit
{
    public class CommandBuilderTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void can_build_commands(string commandType, Type expectedCommandType)
        {
            var commandBuilder = new CommandBuilder();
            var command = commandBuilder.Build(commandType);
            
            Assert.IsType(expectedCommandType, command);
        }
        
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { "f", typeof(ForwardCommand) },
                new object[] { "b", typeof(BackwardCommand) },
                new object[] { "l", typeof(RotateLeftCommand) },
                new object[] { "r", typeof(RotateRightCommand) },
                new object[] { "k", typeof(DefaultCommand) },
                new object[] { "kretsdf£$%", typeof(DefaultCommand) }
            };
    }
}