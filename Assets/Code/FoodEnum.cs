using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Food {
    rawChicken,
    choppedChicken,
    roastedChicken,
    onion,
    choppedOnion,
    corn,
    cornKernels,
    lettuce,
    choppedLettuce, 
    salad,
    humanChunk, 
    custom,
    none
}

public static class FoodExt
{
    public static bool IsFood(this Food _food)
    {
        return (_food != Food.none); 
    }
    public static bool UseCustomName(this Food _food)
    {
        return (_food == Food.none || _food == Food.custom);
    }
}
