using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class IfAction : Action
{
    List<Action> ifActions, elseActions;
    Condition condition;

    public IfAction(Condition condition, List<Action> ifActions, List<Action> elseActions)
    {
        this.condition = condition;
        this.ifActions = ifActions;
        this.elseActions = elseActions;
    }

    public override void Run(float delta, Tank tank)
    {
        if (condition.Evaluate(delta, tank))
        {
            tank.AddActionsToBeginning(ifActions);
        }
        else
        {
            if (elseActions.Count > 0)
                tank.AddActionsToBeginning(elseActions);
        }
        done = true;
    }
    public override string ToString()
    {
        return "checking " + condition + ".";
    }
}
