using Main.Commands;
using Main.Navigation;
using Xunit;

namespace Tests.Unit
{
    public class BackWardCommandTests
    {
        private const int GridMaxSize = 9;
        private readonly BackwardCommand _backwardCommand;

        public BackWardCommandTests()
        {
            _backwardCommand = new BackwardCommand();
        }
        
        [Fact]
        public void can_move_rover_down()
        {
            var currentPosition = new Position(2, 1, Heading.North);
            
            var newPosition = _backwardCommand.Execute(currentPosition);
            
            Assert.Equal(2, newPosition.Coordinates.X);
            Assert.Equal(currentPosition.Coordinates.Y-1, newPosition.Coordinates.Y);
            Assert.Equal(Heading.North, newPosition.Heading);
        }
        
        [Fact]
        public void can_move_rover_left()
        {
            var currentPosition = new Position(GridMaxSize, 4, Heading.East);

            var newPosition = _backwardCommand.Execute(currentPosition);

            Assert.Equal(currentPosition.Coordinates.X-1, newPosition.Coordinates.X);
            Assert.Equal(4, newPosition.Coordinates.Y);
            Assert.Equal(Heading.East, newPosition.Heading);
        }
        
        [Fact]
        public void can_move_rover_up()
        {
            var currentPosition = new Position(3, 6, Heading.South);

            var newPosition = _backwardCommand.Execute(currentPosition);
            
            Assert.Equal(3, newPosition.Coordinates.X);
            Assert.Equal(currentPosition.Coordinates.Y+1, newPosition.Coordinates.Y);
            Assert.Equal(Heading.South, newPosition.Heading);
        }
        
        [Fact]
        public void can_move_rover_right()
        {
            var currentPosition = new Position(5, 1, Heading.West);

            var newPosition = _backwardCommand.Execute(currentPosition);
            
            Assert.Equal(currentPosition.Coordinates.X+1, newPosition.Coordinates.X);
            Assert.Equal(1, newPosition.Coordinates.Y);
            Assert.Equal(Heading.West, newPosition.Heading);
        }
    }
}