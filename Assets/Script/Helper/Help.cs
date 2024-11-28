using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Help
{
    // Start is called before the first frame update
    public static List<T> Shuffle<T>(List<T> list)
    {
        List<T> shuffledList = new List<T>(list);
        System.Random rng = new System.Random(); // Pseudo-random number generator
                                                 // Random number generator
        int n = shuffledList.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);  // Random index between 0 and i
            // Swap shuffledList[i] with shuffledList[j]
            T temp = shuffledList[i];
            shuffledList[i] = shuffledList[j];
            shuffledList[j] = temp;
        }
        return shuffledList;  // Return the shuffled list
    }
}
