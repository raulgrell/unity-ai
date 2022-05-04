using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatRandom
{
    private readonly List<int> history;
    private readonly int maxHistory;

    public FloatRandom(int historySize)
    {
        maxHistory = historySize;
        history = new List<int>();
    }

    public int NextBinary()
    {
        int value = Random.Range(0, 2);
        if (history.Count > maxHistory)
            history.RemoveAt(0);
        if (FilterValue(value))
            value = FlipValue(value);
        history.Add(value);
        return value;
    }

    private int FlipValue(int value)
    {
        return value == 1 ? 0 : 1;
    }

    private bool FilterValue(int value)
    {
        if (FourRunsBinaryRule(value))
            return true;
        if (FourRepetitionsPatternBinaryRule(value))
            return true;
        if (TwoRepetitionsPatternBinaryRule(value))
            return true;
        return false;
    }

    private bool FourRunsBinaryRule(float value)
    {
        int historySize = history.Count;
        if (historySize < 3)
            return false;
        
        for (int i = historySize - 1; i >= historySize - 3; i--)
        {
            if (history[i] != value)
                return false;
        }

        if (Random.Range(0, 4) == 0)
            return false;
        
        return true;
    }

    private bool FourRepetitionsPatternBinaryRule(float value)
    {
        int historySize = history.Count;
        if (historySize < 7)
            return false;
        
        if (history[historySize - 1] != value)
            return false;
        
        int count = 0;
        for (int i = historySize - 2; i >= historySize - 7; i -= 2)
        {
            if (history[i] == history[i - 1])
                count++;
        }

        if (count < 3)
            return false;
        
        return true;
    }


    private bool TwoRepetitionsPatternBinaryRule(float value)
    {
        int historySize = history.Count;
        if (historySize < 5)
            return false;
        
        if (history[historySize - 1] != value || history[historySize - 2] != value)
            return false;
        
        for (int i = historySize - 3; i >= historySize - 5; i--)
        {
            if (history[i] == value)
                return false;
        }

        return true;
    }
}