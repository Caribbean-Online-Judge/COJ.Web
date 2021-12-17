namespace COJ.Web.Infrastructure.Utils;

public static class MathUtils
{
    /// <summary>
    /// Compute percent value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="total"></param>
    /// <returns></returns>
    public static double ComputePercent(double value, double total)
    {
        return value / total * 100;
    }
}