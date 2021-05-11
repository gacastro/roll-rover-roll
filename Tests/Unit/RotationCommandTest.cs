using Main.Commands;
using Main.Navigation;
using Xunit;

namespace Tests.Unit
{
    public class RotationCommandsTests
    {
        [Theory]
        [InlineData(1, Heading.East)]
        [InlineData(6, Heading.South)]
        [InlineData(7, Heading.West)]
        [InlineData(12, Heading.North)]
        public void can_rotate_rover_right(int rotations, Heading expectedHeading)
        {
            var rotateRightCommand = new RotateRightCommand();

            var newPosition = new Position();
            for (var rotation = 0; rotation < rotations; rotation++)
                newPosition = rotateRightCommand.Execute(newPosition);

            Assert.Equal(0, newPosition.Coordinates.X);
            Assert.Equal(0, newPosition.Coordinates.Y);
            Assert.Equal(expectedHeading, newPosition.Heading);
        }
        
        [Theory]
        [InlineData(1, Heading.West)]
        [InlineData(6, Heading.South)]
        [InlineData(7, Heading.East)]
        [InlineData(12, Heading.North)]
        public void can_rotate_rover_left(int rotations, Heading expectedHeading)
        {
            var rotateLeftCommand = new RotateLeftCommand();
            
            var newPosition = new Position();
            for (var rotation = 0; rotation < rotations; rotation++)
                newPosition = rotateLeftCommand.Execute(newPosition);

            Assert.Equal(0, newPosition.Coordinates.X);
            Assert.Equal(0, newPosition.Coordinates.Y);
            Assert.Equal(expectedHeading, newPosition.Heading);
        }
    }
}