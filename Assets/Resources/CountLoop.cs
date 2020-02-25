using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class CountLoop : Loop
{
    int iters;
    bool first = true;
    public CountLoop(int iters, List<Action> loopBody)
    {
        this.iters = iters;
        this.LoopBody = loopBody;
        this.SaveLoop = loopBody.ConvertAll(x => (Action)x.Clone());
    }

    public override void Run(float delta, Tank tank)
    {
        if (first && iters == 0)
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
            iters--;
        }

    }
    public override string ToString()
    {
        return "looping " + iters + " times.";
    }
}