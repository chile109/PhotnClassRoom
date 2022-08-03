using System.Collections.Generic;
using Photon.Pun;

public class EventArgs
{
    public string EventName
    {
        get;
        set;
    }
    public PhotonView PhotonView
    {
        get;
        set;
    }
}

public interface IEventListener
{
    void OnEvent(EventArgs args);
}

public static class XRInputNotifyCenter
{
    static Dictionary<string, List<IEventListener>> eventListeners = new Dictionary<string, List<IEventListener>>();
    public static void AttachListener(string eventName, IEventListener listener)
    {
        List<IEventListener> listeners = null;
        if (!eventListeners.TryGetValue(eventName, out listeners))
        {
            listeners = new List<IEventListener>();
            eventListeners.Add(eventName, listeners);
        }
        listeners.Add(listener);
    }

    public static void DetachListener(string eventName, IEventListener listener)
    {
        List<IEventListener> listeners = null;
        if (eventListeners.TryGetValue(eventName, out listeners))
        {
            listeners.Remove(listener);
            if (listeners.Count <= 0)
            {
                eventListeners.Remove(eventName);
            }
        }
    }

    public static void NotifyEvent(string eventName, PhotonView view)
    {
        NotifyEvent(new EventArgs()
        {
            EventName = eventName,
            PhotonView = view,
        });
    }

    public static void NotifyEvent(EventArgs args)
    {
        List<IEventListener> listeners = null;
        if (eventListeners.TryGetValue(args.EventName, out listeners))
        {
            foreach (var listener in listeners)
            {
                listener.OnEvent(args);
            }
        }
    }
}
