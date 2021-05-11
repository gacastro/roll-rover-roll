using System.Linq;

namespace Main.Navigation
{
    public class NavigationController : IControlNavigation
    {
        private readonly IAmTopology _topology;

        public NavigationController(IAmTopology topology)
        {
            _topology = topology;
        }

        public Position AdjustEdges(Position position)
        {
            var result = position;

            var northEdgeBroken = position.Coordinates.Y > _topology.MaxLength;
            if (northEdgeBroken)
            {
                result = new Position(position.Coordinates.X, 0, position.Heading);
            }

            var southEdgeBroken = position.Coordinates.Y < 0;
            if (southEdgeBroken)
            {
                result = new Position(position.Coordinates.X, _topology.MaxLength, position.Heading);
            }

            var westEdgeBroken = position.Coordinates.X < 0;
            if (westEdgeBroken)
            {
                result = new Position(_topology.MaxWidth, position.Coordinates.Y, position.Heading);
            }

            var eastEdgeBroken = position.Coordinates.X > _topology.MaxWidth;
            if (eastEdgeBroken)
            {
                result = new Position(0, position.Coordinates.Y, position.Heading);
            }

            return result;
        }

        public bool DetectCollision(Position position)
        {
            return _topology.Obstacles.Any(
                obstacle =>
                    obstacle.X == position.Coordinates.X
                    && obstacle.Y == position.Coordinates.Y);
        }
    }
}