using System.Collections.Generic;
using Main.Navigation;
using Xunit;

namespace Tests.Unit
{
    public class NavigationControllerTests
    {
        private const int GridMaxSize = 9;
        private readonly NavigationController _navigationController;

        public NavigationControllerTests()
        {
            var topology = new PlutoTopology(GridMaxSize,GridMaxSize);
            _navigationController = new NavigationController(topology);
        }

        [Fact]
        public void no_need_to_adjust()
        {
            var position = new Position(3, 4, Heading.North);
            var adjustedPosition = _navigationController.AdjustEdges(position);
            
            Assert.Equal(3, adjustedPosition.Coordinates.X);
            Assert.Equal(4, adjustedPosition.Coordinates.Y);
            Assert.Equal(Heading.North, adjustedPosition.Heading);
        }

        [Fact]
        public void can_adjust_north_edges()
        {
            var position = new Position(3, 10, Heading.North);
            var adjustedPosition = _navigationController.AdjustEdges(position);
            
            Assert.Equal(3, adjustedPosition.Coordinates.X);
            Assert.Equal(0, adjustedPosition.Coordinates.Y);
            Assert.Equal(Heading.North, adjustedPosition.Heading);
        }
        
        [Fact]
        public void can_adjust_south_edges()
        {
            var position = new Position(7, -1, Heading.South);
            var adjustedPosition = _navigationController.AdjustEdges(position);
            
            Assert.Equal(7, adjustedPosition.Coordinates.X);
            Assert.Equal(GridMaxSize, adjustedPosition.Coordinates.Y);
            Assert.Equal(Heading.South, adjustedPosition.Heading);
        }
        
        [Fact]
        public void can_adjust_west_edges()
        {
            var position = new Position(-1, 7, Heading.West);
            var adjustedPosition = _navigationController.AdjustEdges(position);
            
            Assert.Equal(GridMaxSize, adjustedPosition.Coordinates.X);
            Assert.Equal(7, adjustedPosition.Coordinates.Y);
            Assert.Equal(Heading.West, adjustedPosition.Heading);
        }
        
        [Fact]
        public void can_adjust_east_edges()
        {
            var position = new Position(10, 3, Heading.West);
            var adjustedPosition = _navigationController.AdjustEdges(position);
            
            Assert.Equal(0, adjustedPosition.Coordinates.X);
            Assert.Equal(3, adjustedPosition.Coordinates.Y);
            Assert.Equal(Heading.West, adjustedPosition.Heading);
        }
        
        [Theory]
        [InlineData(2,3, true)]
        [InlineData(3,3, false)]
        [InlineData(8,2, true)]
        [InlineData(8,7, false)]
        public void can_detect_collisions(int x, int y, bool expect)
        {
            var obstacles = new List<Coordinates> {new(2,3), new (8,2)};
            var topology = new PlutoTopology(9, 9, obstacles);
            var navigationController = new NavigationController(topology);


            var position = new Position(x, y, Heading.South);
            var result = navigationController.DetectCollision(position);
            
            Assert.Equal(expect, result);
        }
    }
}