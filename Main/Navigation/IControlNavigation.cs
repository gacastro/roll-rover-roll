namespace Main.Navigation
{
    public interface IControlNavigation
    {
        Position AdjustEdges(Position position);
        bool DetectCollision(Position position);
    }
}