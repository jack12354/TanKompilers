using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Tank : MonoBehaviour
{
    double health = 100;
    //double ammo = 10;
    int action = 0;
    public float lastShot = 0;
    List<Action> actions = new List<Action>();
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (health <= 0)
            Debug.Log(name.ToUpper() + " HAS DIED");
    }

    public void TakeDamage(double damage = 10.0)
    {
        health -= damage;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        if (action < actions.Count)
        {
            if (!actions[action].done)
            {
                //Debug.Log (name + " is " + actions [action]);
                actions[action].Run(Time.deltaTime, this);
            }
            else
            {
                //	Debug.Log (name + " finished " + actions [action]);
                action++;
                if (action < actions.Count)
                    Debug.Log(name + " " + actions[action]);
            }
        }
        else
        {
            actions.Clear();
            action = 0;
        }
    }

    public void ChangeActions(List<Action> newActions)
    {
        Debug.Log(name + " got new instructions");
        actions.Clear();
        actions = newActions;
        action = 0;
    }

    public void AddActionsToBeginning(List<Action> newActions)
    {
        Debug.Log(name + " added new instructions");
        actions.InsertRange(action, newActions);
    }

    public void AddActionsToEnd(List<Action> newActions)
    {
        Debug.Log(name + " added new instructions for later");
        actions.AddRange(newActions);
    }

    public void ReplaceCurrentActions(List<Action> newActions)
    {
        Debug.Log(name + " got updated instructions");
        if (actions.Count > 0)
            actions.RemoveAt(action);
        actions.InsertRange(action, newActions);
    }
}
