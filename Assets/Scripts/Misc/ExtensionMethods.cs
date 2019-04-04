using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Extension method to check if a layer is in a layer mask
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
