using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Event : ScriptableObject
{
    /// <summary>
    /// A scriptable object used to create events
    /// </summary>
    private readonly List<EventListener> listeners = 
        new List<EventListener>();

    public void Raise()
    {
        Debug.Log(listeners.Count);
        for (int i = listeners.Count-1;i>=0; i-- )
        {
            listeners[i].OnEventRaised();
        }
    }

    public void AddListener(EventListener listener)
    {
        if (!listeners.Contains(listener)){
            listeners.Add(listener);
        }
    }

    public void RemoveListener(EventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
