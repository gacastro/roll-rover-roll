using Main.Navigation;

namespace Main.Commands
{
    public class RotateRightCommand: ICommandRover
    {
        public Position Execute(Position position)
        {
            return position.Heading == Heading.West 
                ? new Position(position.Coordinates.X, position.Coordinates.Y, Heading.North)
                : new Position(position.Coordinates.X, position.Coordinates.Y, position.Heading+1);
        }
    }
}