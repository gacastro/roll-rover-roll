using System.Collections.Generic;

namespace Main.Navigation
{
    public interface IAmTopology
    {
        public int MaxLength { get; }
        public int MaxWidth { get; }
        public IReadOnlyCollection<Coordinates> Obstacles { get; }
    }
}