using Main.Commands;
using Main.Navigation;

namespace Main
{
    public class Rover : IAmRover
    {
        private readonly IControlNavigation _navigationController;
        public Position Position { get; private set; }
        public Coordinates ObstacleCoordinates { get; set; }

        public Rover(IControlNavigation navigationController)
        {
            _navigationController = navigationController;
            Position = new Position();
        }

        public void Execute(ICommandRover command)
        {
            var desiredPosition = command.Execute(Position);
            var adjustedPosition = _navigationController.AdjustEdges(desiredPosition);
            
            if (_navigationController.DetectCollision(adjustedPosition))
            {
                ObstacleCoordinates = adjustedPosition.Coordinates;
                return;
            }

            Position = adjustedPosition;
        }
    }
}