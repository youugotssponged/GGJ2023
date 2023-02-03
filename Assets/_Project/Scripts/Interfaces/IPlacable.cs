public interface IPlacable<T> where T : ITower
{
    // method should check if it can = True, if it cant = doesn't place & False
    public bool PlaceTower(T Tower);
}
