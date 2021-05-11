using System.Collections.Generic;
using Main.Commands;

namespace API.Helpers
{
    public class CommandParser : IParseCommands
    {
        private readonly IBuildCommands _commandBuilder;

        public CommandParser(IBuildCommands commandBuilder)
        {
            _commandBuilder = commandBuilder;
        }
        
        public IReadOnlyCollection<ICommandRover> Parse(string requestedCommands)
        {
            if (string.IsNullOrWhiteSpace(requestedCommands))
            {
                return new List<ICommandRover>();
            }
            
            var unparsedCommands = requestedCommands.Split(',');

            return unparsedCommands.Length == 1
                ? OneOrEmpty(unparsedCommands[0]) 
                : MultipleOrEmpty(unparsedCommands);
        }

        private IReadOnlyCollection<ICommandRover> OneOrEmpty(string unparsedCommand)
        {
            var command = _commandBuilder.Build(unparsedCommand);
            
            return command is DefaultCommand 
                ? new List<ICommandRover>()
                : new List<ICommandRover> {command};
        }

        private IReadOnlyCollection<ICommandRover> MultipleOrEmpty(IEnumerable<string> unparsedCommands)
        {
            var commands = new List<ICommandRover>();
            
            //could do it with Linq if the team felt comfortable with it.
            //this is more performant but Linq is (for some) more readable
            //open discussion I guess - I'd take the team's preference for consistency 
            foreach (var unparsedCommand in unparsedCommands)
            {
                var command = _commandBuilder.Build(unparsedCommand);
                if (command is not DefaultCommand)
                {
                    commands.Add(command);
                }
            }

            return commands;
        }
    }
}