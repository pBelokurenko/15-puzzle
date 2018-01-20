using System;
using System.Collections.Generic;

public class EngineEvent 
{
    List<Action> customEvent;

    public EngineEvent()
    {
        customEvent = new List<Action>();
    }

    public void AddAction(Action action)
    {
        if (customEvent.FindIndex((a)=> action == a) < 0)
            customEvent.Add(action);
    }

    public void RemoveAction(Action action)
    {
        customEvent.Remove(action);
    }

    public void Execute()
    {
        for (int i = 0; i < customEvent.Count; i++)
            customEvent[i]();
    }

    public int Count
    {
        get
        {
            return customEvent.Count;
        }
    }

    public void Clear()
    {
        customEvent.Clear();
    }

    ~EngineEvent()
    {
        Clear();
    }
}
