using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShootAction : Action
{
    int howMany;
    float delay = 1f;
    public ShootAction(int howMany = 1)
    {
        this.howMany = howMany;
    }

    public override void Run(float delta, Tank tank)
    {
        if (Time.time - tank.lastShot > delay)
        {
            ShootABullet(tank);
            tank.lastShot = Time.time;
            howMany--;
        }

        if (howMany == 0)
            done = true;
    }

    void ShootABullet(Tank tank)
    {

        GameObject.Instantiate(Resources.Load("Bullet"), tank.transform.position + tank.transform.forward, tank.transform.rotation);
    }

    public override string ToString()
    {
        return "shooting " + (howMany > 1 ? howMany + " shots" : "");
    }
}