using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurnAction : Action
{
    double savedegrees;
    Directions dir;
    float speed = 90.0f; // degrees / second
    public TurnAction(Directions direction, double degrees)
    {
        savedegrees = degrees;
        dir = direction;
        variable.Add(degrees);
    }

    public override void Run(float delta, Tank tank)
    {
        float change = speed * delta;
        if ((double)variable[0] < change)
            change = (float)(double)variable[0];
        variable[0] = (double)variable[0] - change;
        switch (dir)
        {
            case Directions.LEFT:
                tank.transform.Rotate(tank.transform.up, -change);
                break;
            case Directions.RIGHT:
                tank.transform.Rotate(tank.transform.up, change);
                break;
            case Directions.UP:
                tank.transform.Rotate(tank.transform.right, -change);
                break;
            case Directions.DOWN:
                tank.transform.Rotate(tank.transform.right, change);
                break;
        }

        if ((double)variable[0] <= 0)
            done = true;


    }

    public override string ToString()
    {
        return "turning " + dir + " " + savedegrees + " degrees";
    }
}