using Main.Commands;
using Main.Navigation;
using Xunit;

namespace Tests.Unit
{
    public class ForwardCommandTests
    {
        private const int GridMaxSize = 9;
        private readonly ForwardCommand _forwardCommand;

        public ForwardCommandTests()
        {
            _forwardCommand = new ForwardCommand();
        }
        
        [Fact]
        public void can_move_rover_up()
        {
            var currentPosition = new Position(4,9, Heading.North);

            var newPosition = _forwardCommand.Execute(currentPosition);
            
            Assert.Equal(4, newPosition.Coordinates.X);
            Assert.Equal(currentPosition.Coordinates.Y+1, newPosition.Coordinates.Y);
            Assert.Equal(Heading.North, newPosition.Heading);
        }

        [Fact]
        public void can_move_rover_right()
        {
            var currentPosition = new Position(2, 8, Heading.East);

            var newPosition = _forwardCommand.Execute(currentPosition);
            
            Assert.Equal(currentPosition.Coordinates.X+1, newPosition.Coordinates.X);
            Assert.Equal(8, newPosition.Coordinates.Y);
            Assert.Equal(Heading.East, newPosition.Heading);
        }

        [Fact]
        public void can_move_rover_down()
        {
            var currentPosition = new Position(5, GridMaxSize, Heading.South);

            var newPosition = _forwardCommand.Execute(currentPosition);
            
            Assert.Equal(5, newPosition.Coordinates.X);
            Assert.Equal(currentPosition.Coordinates.Y-1, newPosition.Coordinates.Y);
            Assert.Equal(Heading.South, newPosition.Heading);
        }

        [Fact]
        public void can_move_rover_left()
        {
            var currentPosition = new Position(GridMaxSize, 2, Heading.West);

            var newPosition = _forwardCommand.Execute(currentPosition);
            
            Assert.Equal(currentPosition.Coordinates.X-1, newPosition.Coordinates.X);
            Assert.Equal(2, newPosition.Coordinates.Y);
            Assert.Equal(Heading.West, newPosition.Heading);
        }
    }
}