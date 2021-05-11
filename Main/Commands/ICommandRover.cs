using Main.Navigation;

namespace Main.Commands
{
    public interface ICommandRover
    {
        Position Execute(Position position);
    }
}