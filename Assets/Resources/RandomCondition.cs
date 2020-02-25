using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class RandomCondition : Condition
{
    public override bool Evaluate(float delta, Tank tank)
    {
        return UnityEngine.Random.Range(0, 100) < 50;
    }

    public override string ToString()
    {
        return "Random Condition";
    }
}
