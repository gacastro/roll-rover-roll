using Main.Commands;
using Main.Navigation;

namespace Main
{
    public interface IAmRover
    {
        Position Position { get; }
        
        // considering that rover is a singleton (readme) we need to reset these coordinates
        Coordinates ObstacleCoordinates { get; set; }
        
        void Execute(ICommandRover command);
    }
}