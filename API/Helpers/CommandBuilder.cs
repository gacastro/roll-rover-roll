using Main.Commands;

namespace API.Helpers
{
    public class CommandBuilder : IBuildCommands
    {
        public ICommandRover Build(string commandType)
        {
            switch (commandType.ToLowerInvariant())
            {
                case "f":
                    return new ForwardCommand();
                case "b":
                    return new BackwardCommand();
                case "r":
                    return new RotateRightCommand();
                case "l":
                    return new RotateLeftCommand();
            }

            return new DefaultCommand();
        }
    }
}