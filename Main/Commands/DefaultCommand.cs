using Main.Navigation;

namespace Main.Commands
{
    public class DefaultCommand : ICommandRover
    {
        public Position Execute(Position position)
        {
            return position;
        }
    }
}