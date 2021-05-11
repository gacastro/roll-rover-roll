using Main.Commands;

namespace API.Helpers
{
    public interface IBuildCommands
    {
        ICommandRover Build(string commandType);
    }
}