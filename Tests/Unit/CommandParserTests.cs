using System.Linq;
using API.Helpers;
using Main.Commands;
using Xunit;

namespace Tests.Unit
{
    public class CommandParserTests
    {
        private readonly IParseCommands _commandParser;

        public CommandParserTests()
        {
            _commandParser = new CommandParser(new CommandBuilder());
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("     ")]
        [InlineData("")]
        [InlineData("sadfsdf")]
        [InlineData("FBRL")]
        [InlineData("F-B-R-L")]
        [InlineData("A,E,Q,P")]
        public void returns_empty_list_when_invalid_arguments(string rawCommands)
        {
            var commands = _commandParser.Parse(rawCommands);
            
            Assert.Empty(commands);
        }

        [Fact]
        public void returns_single_command_when_one_argument()
        {
            var commands = _commandParser.Parse("F");
            
            Assert.Single(commands);
            Assert.IsType<ForwardCommand>(commands.First());
        }
        
        [Fact]
        public void can_filter_out_invalid_arguments()
        {
            var commands = _commandParser.Parse("F,W,B").ToList();
            
            Assert.Equal(2, commands.Count);
            Assert.IsType<ForwardCommand>(commands[0]);
            Assert.IsType<BackwardCommand>(commands[1]);
        }
        
        [Fact]
        public void can_parse_valid_arguments()
        {
            var commands = _commandParser.Parse("F,B,L").ToList();
            
            Assert.Equal(3, commands.Count);
            Assert.IsType<ForwardCommand>(commands[0]);
            Assert.IsType<BackwardCommand>(commands[1]);
            Assert.IsType<RotateLeftCommand>(commands[2]);
        }
    }
}