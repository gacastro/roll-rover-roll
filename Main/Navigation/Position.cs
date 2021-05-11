namespace Main.Navigation
{
    public class Position
    {
        public Coordinates Coordinates { get; }
        public Heading Heading { get; }

        public Position()
        {
            Coordinates = new Coordinates();
        }

        public Position(int x, int y, Heading heading)
        {
            Coordinates = new Coordinates(x, y);
            Heading = heading;
        }
    }
}