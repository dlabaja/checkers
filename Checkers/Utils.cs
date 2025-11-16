namespace Checkers;

public static class Utils
{
    public static int FindClosestToX(int x, List<int> xList, bool returnHigher = true)
    {
        if (xList.Count == 1)
        {
            return xList[0];
        }
        
        if (xList.Contains(x))
        {
            return x;
        }

        var closest = xList[0];
        foreach (var item in xList)
        {
            if (Math.Abs(x - item) < Math.Abs(x - closest))
            {
                closest = item;
            }
        }

        var second = 2 * x - closest;
        if (xList.Contains(second))
        {
            return returnHigher ? Math.Max(closest, second) : Math.Min(closest, second);
        }
        return closest;
    }

    public static bool NumberInRange(int x, int min, int maxExcluded)
    {
        return x >= min && x < maxExcluded;
    }
}
