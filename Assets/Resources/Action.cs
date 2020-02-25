using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public abstract class Action : ICloneable
{
    public bool done = false;
    public List<object> variable = new List<object>();
    public abstract void Run(float delta, Tank tank);
    public object Clone()
    {
        using (MemoryStream ms = new MemoryStream())
        {
            IFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            ms.Position = 0;
            return bf.Deserialize(ms);
        }
    }
}
[Serializable]
public abstract class Condition : ICloneable
{
    public List<object> variable = new List<object>();
    public abstract bool Evaluate(float delta, Tank tank);
    public object Clone()
    {
        using (MemoryStream ms = new MemoryStream())
        {
            IFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            ms.Position = 0;
            return bf.Deserialize(ms);
        }
    }
}
[Serializable]
public abstract class Loop : Action
{
    protected int action = 0;
    public List<Action> LoopBody = new List<Action>();
    protected List<Action> SaveLoop = new List<Action>();
}
public enum Directions
{
    FORWARD,
    BACK,
    LEFT,
    RIGHT,
    UP,
    DOWN
}