using UnityEngine;

static public class Expireance
{
    static public int GetNewLevel(int currentLevel)
    {
        return (int)(100 * Mathf.Pow(1.4f, currentLevel - 2));
    }
}
