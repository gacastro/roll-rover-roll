using Main.Navigation;

namespace Main.Commands
{
    public class BackwardCommand : ICommandRover
    {
        public Position Execute(Position position)
        {
            Position result;
            
            switch (position.Heading)
            {
                case Heading.North:
                    result =  new Position(position.Coordinates.X, position.Coordinates.Y-1, position.Heading);
                    break;
                case Heading.East:
                    result =  new Position(position.Coordinates.X-1, position.Coordinates.Y, position.Heading);
                    break;
                case Heading.South:
                    result =  new Position(position.Coordinates.X, position.Coordinates.Y+1, position.Heading);
                    break;
                case Heading.West:
                    result =  new Position(position.Coordinates.X+1, position.Coordinates.Y, position.Heading);
                    break;
                default:
                    result = new Position();
                    break;
            }

            return result;
        }
    }
}