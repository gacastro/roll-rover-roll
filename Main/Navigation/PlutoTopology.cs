using System.Collections.Generic;

namespace Main.Navigation
{
    public class PlutoTopology : IAmTopology
    {
        public int MaxWidth { get; }
        public int MaxLength { get; }
        public IReadOnlyCollection<Coordinates> Obstacles { get; }

        public PlutoTopology(int maxLength, int maxWidth)
        {
            MaxWidth = maxWidth;
            MaxLength = maxLength;
            Obstacles = new List<Coordinates>();
        }
        
        public PlutoTopology(int maxLength, int maxWidth, IReadOnlyCollection<Coordinates> obstacles)
        {
            MaxWidth = maxWidth;
            MaxLength = maxLength;
            Obstacles = obstacles;
        }
    }
}