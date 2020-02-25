using UnityEngine;
using System.Collections.Generic;
using System;

public class Compiler
{
    public List<Action> Compile(string input)
    {
        List<Action> actions = new List<Action>();
        //and here's where the magic happens
        System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(" +");
        input = rgx.Replace(input, " ");
        input = input.ToUpper().Trim();
        string[] lines = input.Split("\n".ToCharArray());
        string[][] program = new string[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            program[i] = lines[i].Trim().Split(' ');
            for (int j = 0; j < program[i].Length; j++)
            {
                program[i][j].Trim();
            }
        }
        Debug.Log("Compiling " + program.Length + " lines");
        int line = 0;
        while (line < program.Length)
            actions.Add(Parse(program, ref line));

        return actions;
    }

    Action Parse(string[][] program, ref int line)
    {
        //	List<Action> actions = new List<Action> ();
        Action a = null;
        switch (program[line][0])
        {
            case "MOVE":
                a = Move(program[line]);
                break;
            case "TURN":
                a = Turn(program[line]);
                break;
            case "SHOOT":
                a = Shoot(program[line]);
                break;
            case "IF":
                a = If(program, ref line);
                break;
            case "LOOP":
                a = Loop(program, ref line);
                break;
            case "ELSE":
            case "ENDIF":
            case "ENDLOOP":
                line++;
                return null;// actions;
        }
        line++;
        return a;

        //	actions.Add (a);
    }

    Action Move(string[] line)
    {
        Directions dir = (Directions)(-1);
        double time;
        switch (line[1])
        {
            case "FORWARD":
                dir = Directions.FORWARD;
                break;
            case "BACK":
                dir = Directions.BACK;
                break;
            case "LEFT":
                dir = Directions.LEFT;
                break;
            case "RIGHT":
                dir = Directions.RIGHT;
                break;
            case "UP":
                dir = Directions.UP;
                break;
            case "DOWN":
                dir = Directions.DOWN;
                break;
        }
        time = Double.Parse(line[2]);
        Debug.Log("Parsing Move " + dir + " " + time);
        return new MoveAction(dir, time);
    }

    Action Turn(string[] line)
    {
        Directions dir = (Directions)(-1);
        double degrees;
        switch (line[1])
        {
            case "LEFT":
                dir = Directions.LEFT;
                break;
            case "RIGHT":
                dir = Directions.RIGHT;
                break;
            case "UP":
                dir = Directions.UP;
                break;
            case "DOWN":
                dir = Directions.DOWN;
                break;
        }
        degrees = Double.Parse(line[2]);
        Debug.Log("Parsing Turn " + degrees + " " + dir);
        return new TurnAction(dir, degrees);
    }

    Action Shoot(string[] line)
    {
        int shots = 1;
        if (line.Length > 1)
            shots = Int32.Parse(line[1]);
        Debug.Log("Parsing Shoot " + shots);
        return new ShootAction(shots);
    }

    Action If(string[][] program, ref int line)
    {
        Condition condition = null;
        List<Action> ifActions = new List<Action>(), elseActions = new List<Action>();

        Debug.Log("Parsing if statement");
        condition = ParseCondition(program[line]);
        line++;
        Debug.Log("Parsing if actions");
        while (program[line][0] != "ELSE" && program[line][0] != "ENDIF")
        {
            Debug.Log("Parsing " + program[line][0] + " from if");
            ifActions.Add(Parse(program, ref line));
        }

        if (line < program.Length && program[line][0] == "ELSE")
        {
            Debug.Log("begin else");
            line++;
            Debug.Log("Parsing else actions");
            while (program[line][0] != "ENDIF")
                elseActions.Add(Parse(program, ref line));
        }
        Debug.Log("End if");
        return new IfAction(condition, ifActions, elseActions);
    }

    Condition ParseCondition(string[] line)
    {
        // always the second token onwards
        return new RandomCondition();
    }

    Condition RandomCondition(string[] line)
    {
        Debug.Log("Parsing Random Chance Condition");
        return new RandomCondition();
    }

    Action Loop(string[][] program, ref int line)
    {
        Debug.Log("Parsing loop");
        int iters = 0;
        if (int.TryParse(program[line][1], out iters))
        {
            return countloop(iters, program, ref line);
        }
        else
        {
            return conditionalLoop(program, ref line);
        }
    }

    Action countloop(int iters, string[][] program, ref int line)
    {
        // Handled Above
        line++;
        List<Action> body = new List<Action>();
        Debug.Log("Parsing loop actions");
        while (line < program.Length && program[line][0] != "ENDLOOP")
        {
            Debug.Log("Parsing " + program[line][0] + " into loop");
            body.Add(Parse(program, ref line));
        }
        return new CountLoop(iters, body);
    }

    Action conditionalLoop(string[][] program, ref int line)
    {
        List<Action> body = new List<Action>();
        Debug.Log("Parsing loop conditional");
        Condition cond = ParseCondition(program[line]);
        line++;
        Debug.Log("Parsing loop actions");
        while (program[line][0] != "ENDLOOP")
        {
            Debug.Log("Parsing " + program[line][0] + " into loop");
            body.Add(Parse(program, ref line));
        }
        Debug.Log("End loop");
        return new ConditionalLoop(cond, body);
    }
}
