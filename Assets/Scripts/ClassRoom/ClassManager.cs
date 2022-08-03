using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ClassManager : MonoBehaviour, IEventListener
{
    static ClassManager _instance = null;

    public static ClassManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ClassManager>();
                if (_instance == null)
                {
                    var _cache = new GameObject("ClassManager");
                    _instance = _cache.AddComponent<ClassManager>();
                }
            }

            return _instance;
        }
    }

    public List<GameObject> StudentPrefabPool;

    public int maxSpeakersPerRoom = 7;

    public List<NetworkPlayer> SpeakerList = new List<NetworkPlayer>();

    private void Awake()
    {
        XRInputNotifyCenter.AttachListener("raisehand", this);

        for (int i = 0; i < maxSpeakersPerRoom; i++)
        {
            StudentPrefabPool[i].SetActive(false);
        }
    }

    public void AddSpeaker(NetworkPlayer player)
    {
        if (SpeakerList.Count == maxSpeakersPerRoom)
        {
            Debug.LogWarning("Max speakers reached");
            return;
        }

        if (!SpeakerList.Contains(player))
        {
            SpeakerList.Add(player);
            var speakerObj = StudentPrefabPool[StudentPrefabPool.Count - 1];
            speakerObj.SetActive(true);
            player.SetPrefab(speakerObj);
            StudentPrefabPool.Remove(speakerObj);
        }
    }

    public void RemoveSpeaker(NetworkPlayer player)
    {
        if (SpeakerList.Contains(player))
        {
            player.PlayerPrefab.SetActive(false);
            player.PlayerPrefab.transform.SetParent(this.transform);
            StudentPrefabPool.Add(player.PlayerPrefab);
            player.ClearPrefab();
            SpeakerList.Remove(player);
        }
    }

    public void OnEvent(EventArgs args)
    {
        switch (args.EventName)
        {
            case "raisehand":
                args.PhotonView.RPC("RaisedHand", RpcTarget.All);
                break;
        }
    }

    private void OnDestroy()
    {
        XRInputNotifyCenter.DetachListener("raisehand", this);
    }
}
