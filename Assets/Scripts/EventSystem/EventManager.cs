using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{   
    private Dictionary<System.Enum, UnityEvent> eventDictionary;

	public static EventManager instance = null;
    
	private void Awake()
	{
		//singleton time!
        if (instance == null)
            instance = this;
		else if (instance != this)
            Destroy(gameObject);
  
        DontDestroyOnLoad(gameObject);
	}

	void Init()
    {
        if (eventDictionary == null)
        {
			eventDictionary = new Dictionary<System.Enum, UnityEvent>();
        }
    }

	public static void StartListening(System.Enum eventType, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        if (instance.eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventType, thisEvent);
        }
    }

	public static void StopListening(System.Enum eventType, UnityAction listener)
    {
        if (instance == null) return;

        UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

	public static void TriggerEvent(System.Enum eventType)
    {
        UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}