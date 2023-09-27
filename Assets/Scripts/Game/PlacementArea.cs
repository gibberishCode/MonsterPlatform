using Core;
public class PlacementArea : ICell
{
    public int X { get; }

    public int Y { get; }

    public PlacementArea(int x, int y)
    {
        X = x;
        Y = y;
    }
}