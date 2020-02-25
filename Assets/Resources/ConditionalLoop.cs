using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class ConditionalLoop : Loop
{
    Condition condition;

    bool first = true;
    public ConditionalLoop(Condition condition, List<Action> loopBody)
    {
        this.condition = condition;
        this.LoopBody = loopBody;
        this.SaveLoop = loopBody.ConvertAll(x => (Action)x.Clone());
    }

    public override void Run(float delta, Tank tank)
    {
        if (first && !condition.Evaluate(delta, tank))
        {
            done = true;
            first = false;
            return;
        }
        if (action < LoopBody.Count)
        {
            if (!LoopBody[action].done)
            {
                LoopBody[action].Run(delta, tank);
            }
            else
            {
                action++;
            }
        }
        else
        {
            LoopBody = SaveLoop.ConvertAll(x => (Action)x.Clone());
            action = 0;
            first = true;
        }

    }
    public override string ToString()
    {
        return "looping " + condition + ".";
    }
}