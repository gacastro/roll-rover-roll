using System.Collections.Generic;
using Main.Commands;

namespace API.Helpers
{
    public interface IParseCommands
    {
        IReadOnlyCollection<ICommandRover> Parse(string requestedCommands);
    }
}