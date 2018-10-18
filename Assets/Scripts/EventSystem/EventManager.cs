using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        public delegate void EventListener(GameEvent e);

        private Dictionary<Type, List<EventListener>> eventListeners;
        private List<EventListener> onceListener;
        private List<EventListener> cache;

        public void Awake()
        {
            if (Instance == null) Instance = this;
            DontDestroyOnLoad(gameObject);
            if (Instance != this) Destroy(gameObject);
        }

        public void AddListener<T>(EventListener listener) where T : GameEvent
        {
            AddToDict(typeof(T), listener);
        }

        private bool AddToDict(Type T, EventListener listener)
        {
            if (eventListeners == null) eventListeners = new Dictionary<Type, List<EventListener>>();
            if (!eventListeners.ContainsKey(T)) eventListeners.Add(T, new List<EventListener>());
            if (!eventListeners[T].Contains(listener)) eventListeners[T].Add(listener);

            return true;
        }

        public void AddListenerOnce<T>(EventListener listener) where T : GameEvent
        {
            if (AddToDict(typeof(T), listener))
            {
                if (onceListener == null) onceListener = new List<EventListener>();
                if (cache == null) cache = new List<EventListener>();
                onceListener.Add(listener);
            }
        }

        public void RemoveListener<T>(EventListener listener) where T : GameEvent
        {
            RemoveFromDict(typeof(T), listener);
        }

        private void RemoveFromDict(Type T, EventListener listener)
        {
            if (!eventListeners.ContainsKey(T)) return;

            if (eventListeners[T].Contains(listener)) eventListeners[T].Remove(listener);
            if (eventListeners[T].Count == 0) eventListeners.Remove(T);
        }

        public void TriggerEvent(GameEvent e)
        {
            if (eventListeners == null || !eventListeners.ContainsKey(e.GetType())) return;

            if (eventListeners[e.GetType()].Count > 1)
            {
                for (var i = 0; i < eventListeners[e.GetType()].Count; i++)
                {
                    EventListener listener = eventListeners[e.GetType()][i];
                    listener(e);

                    if (onceListener == null || !onceListener.Contains(listener)) continue;

                    cache.Add(listener);
                    onceListener.Remove(listener);
                }
            }
            else
            {
                eventListeners[e.GetType()][0](e);

                if (eventListeners.ContainsKey(e.GetType()))
                {
                    if (onceListener != null && onceListener.Contains(eventListeners[e.GetType()][0]))
                    {
                        cache.Add(eventListeners[e.GetType()][0]);
                        onceListener.Remove(eventListeners[e.GetType()][0]);
                    }
                }
            }

            if (cache == null || cache.Count == 0) return;

            for (var i = 0; i < cache.Count; i++)
            {
                EventListener listener = cache[i];
                RemoveFromDict(e.GetType(), listener);
                cache.Remove(listener);
            }
        }
    }
}