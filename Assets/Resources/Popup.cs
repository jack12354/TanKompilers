using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Popup : MonoBehaviour
{
    public string Body = "FORWARD 20\nTURN 90\nSHOOT";
    Vector2 enabledPos = new Vector2(10, 10);
    Vector2 disabledPos = new Vector2(10000, 10000);
    bool fakeEnabled = false;
    bool redTeam;
    public Tank tank;
    public KeyCode hotkey;
    List<Action> actions = new List<Action>();

    Compiler compiler;
    // Use this for initialization
    void Start()
    {
        compiler = new Compiler();
        Body = "MOVE FORWARD 1\nTURN RIGHT 90\nMOVE RIGHT 1\nSHOOT 2";
        if (tank.GetComponent<Renderer>().material.color == Color.red)
            redTeam = true;
        else
            redTeam = false;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(hotkey))
        {
            SendAction();
        }
    }

    public void ShowPopup()
    {
        foreach (var p in GameObject.FindObjectsOfType<Popup>())
        {
            p.HidePopup();
        }
        fakeEnabled = true;
        GetComponent<Renderer>().material.color = redTeam ? Color.yellow : Color.cyan;
    }
    public void HidePopup()
    {
        fakeEnabled = false;
        GUIUtility.keyboardControl = 0;

        GetComponent<Renderer>().material.color = redTeam ? Color.red : Color.blue;
        //This is where the compiler runs and compiles the new action set
        try
        {
            actions = compiler.Compile(Body);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError(e.InnerException);
            Debug.LogError(e.StackTrace);
            Debug.LogError(e.Source);
        }
    }
    void SendAction()
    {
        foreach (Action a in actions)
        {
            Debug.Log(a);
        }
        if (actions.Count == 0)
            actions = compiler.Compile(Body);
        tank.ChangeActions(actions.ConvertAll(x => (Action)x.Clone()));
    }

    void OnGUI()
    {
        name = GUI.TextField(new Rect(fakeEnabled ? enabledPos.x : disabledPos.x, fakeEnabled ? enabledPos.y : disabledPos.y, 200, 20), name, 25);
        Body = GUI.TextArea(new Rect(fakeEnabled ? enabledPos.x : disabledPos.x, 25f + (fakeEnabled ? enabledPos.y : disabledPos.y), 200, 200), Body);
        if (GUI.Button(new Rect(fakeEnabled ? enabledPos.x : disabledPos.x, 230.0f + (fakeEnabled ? enabledPos.y : disabledPos.y), 75, 30), "DONE"))
        {
            HidePopup();
        }
    }
}