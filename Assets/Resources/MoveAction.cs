using UnityEngine;
using System;

[Serializable]
public class MoveAction : Action
{
    double savetime;
    Directions dir;
    float speed = 3.0f;
    public MoveAction(Directions direction, double time)
    {
        savetime = time;
        dir = direction;
        variable.Add(time);
    }

    public override void Run(float delta, Tank tank)
    {
        variable[0] = (double)variable[0] - (double)delta;
        Vector3 direction = Vector3.zero;
        switch (dir)
        {
            case Directions.FORWARD:
                direction = tank.transform.forward * speed;
                break;
            case Directions.BACK:
                direction = tank.transform.forward * -speed;
                break;
            case Directions.LEFT:
                direction = tank.transform.right * -speed;
                break;
            case Directions.RIGHT:
                direction = tank.transform.right * speed;
                break;
            case Directions.UP:
                direction = tank.transform.up * speed;
                break;
            case Directions.DOWN:
                direction = tank.transform.up * -speed;
                break;
        }
        tank.gameObject.GetComponent<Rigidbody>().velocity = direction;

        if ((double)variable[0] <= 0)
            done = true;
    }
    public override string ToString()
    {
        return "going " + dir + " for " + savetime + " seconds";
    }
}
