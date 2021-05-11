using Main.Navigation;

namespace Main.Commands
{
    public class RotateLeftCommand: ICommandRover
    {
        public Position Execute(Position position)
        {
            return position.Heading == Heading.North 
                ? new Position(position.Coordinates.X, position.Coordinates.Y, Heading.West)
                : new Position(position.Coordinates.X, position.Coordinates.Y, position.Heading-1);
        }
    }
}