using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Tooltip("Maximum number of players per room. If the room is full, a new random one will be created. 0 = No Max.")]
    [SerializeField]
    private byte maxPlayersPerRoom = 0;

    [Tooltip("If true, the JoinRoomName will try to be Joined On Start. If false, need to call JoinRoom yourself.")]
    public bool JoinRoomOnStart = true;

    [Tooltip("If true, do not destroy this object when moving to another scene")]
    public bool dontDestroyOnLoad = true;

    public string JoinRoomName = "RandomRoom";

    [Tooltip("Name of the Player object to spawn. Must be in a /Resources folder.")]
    public string TeacherName = "";
    public string StudentName = "";


    [Tooltip("Optional GUI Text element to output debug information.")]
    public TMP_Text DebugText;

    public Transform SpawnTransform;

    void Awake()
    {
        // Required if you want to call PhotonNetwork.LoadLevel() 
        PhotonNetwork.AutomaticallySyncScene = true;

        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        // Connect to Random Room if Connected to Photon Server
        if (PhotonNetwork.IsConnected)
        {
            if (JoinRoomOnStart)
            {
                LogText("Joining Room : " + JoinRoomName);
                PhotonNetwork.JoinRoom(JoinRoomName);
            }
        }
        // Otherwise establish a new connection. We can then connect via OnConnectedToMaster
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        LogText("Room does not exist. Creating <color=yellow>" + JoinRoomName + "</color>");
        PhotonNetwork.CreateRoom(JoinRoomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom, PublishUserId = true }, TypedLobby.Default);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed Failed, Error : " + message);
    }

    public override void OnConnectedToMaster()
    {
        LogText("Connected to Master Server. \n");

        if (JoinRoomOnStart)
        {
            LogText("Joining Room : <color=aqua>" + JoinRoomName + "</color>");
            PhotonNetwork.JoinRoom(JoinRoomName);
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        float playerCount = PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null ? PhotonNetwork.CurrentRoom.PlayerCount : 0;

        LogText("Connected players : " + playerCount);
    }

    public override void OnJoinedRoom()
    {

        LogText("Joined Room. Creating Remote Player Representation.");

        // Network Instantiate the object used to represent our player. This will have a View on it and represent the player 
        object[] data = new object[1];
        data[0] = PhotonNetwork.LocalPlayer.UserId;

        var prefabName = PhotonNetwork.IsMasterClient ? TeacherName : StudentName;
        GameObject player = PhotonNetwork.Instantiate(prefabName, SpawnTransform.position, Quaternion.identity, 0, data);
        player.transform.SetParent(SpawnTransform);
        player.transform.localPosition = new Vector3(0, 0, -1);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        LogText("Disconnected from PUN due to cause : " + cause);

        if (!PhotonNetwork.ReconnectAndRejoin())
        {
            LogText("Reconnect and Joined.");
        }

        base.OnDisconnected(cause);
    }

    void LogText(string message)
    {
        if (DebugText)
        {
            DebugText.text += "\n" + message;
        }

        Debug.Log(message);
    }
}

