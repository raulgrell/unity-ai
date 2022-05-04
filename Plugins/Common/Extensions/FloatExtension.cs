﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtension
{
    public static float ConvertToRange(this float oldValue, float oldMin, float oldMax, float newMin, float newMax)
    {
        return (((oldValue - oldMin) * (newMax - newMin)) / (oldMax - oldMin) + newMin);
    }

    public static float RoundTo(this float value, int numDecimalPoints)
    {
        int roundingFactor = (int)Mathf.Pow(10, numDecimalPoints);
        return Mathf.Round(value * roundingFactor) / roundingFactor;
    }

    public static float RoundToNearest(this float value, float nearestValue)
    {
        float diff = value % nearestValue;

        float result = 0f;

        if (diff < nearestValue / 2f)
            result = value - diff;
        else
            result = value + nearestValue - diff;

        return result;
    }

    public static float PercentageOf(this float value, float pcOfValue)
    {
        return ((pcOfValue - (pcOfValue - value)) / pcOfValue) * 100f;
    }

    public static float PercentageOf(this float value, float pcOfValue, int numDecimalPoints)
    {
        return value.PercentageOf(pcOfValue).RoundTo(numDecimalPoints);
    }

    //Snaps the float value to the nearest number that is divisible by the provided divisible value
    public static float GetNearestWholeMultipleOf(this float value, float multipleOfValue)
    {
        var output = Mathf.Round(value / multipleOfValue);
        if (output == 0 && value > 0) output += 1;
        output *= multipleOfValue;

        return output;
    }
}