using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public InputField UserName;
    public InputField RoomName;
    public Transform RoomContent;
    public RoomItem RoomItemPrefab;
    public List<RoomItem> RoomList = new List<RoomItem>();

    public void OnLoginClick()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.NickName = UserName.text;
            PhotonNetwork.CreateRoom(RoomName.text, new RoomOptions { MaxPlayers = 15, PublishUserId = true }, TypedLobby.Default);
            // PhotonNetwork.JoinRoom(RoomName.text);
        }
        // Otherwise establish a new connection. We can then connect via OnConnectedToMaster
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate: " + roomList.Count);
        foreach (var r in RoomList)
        {
            Destroy(r);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            var r = Instantiate(RoomItemPrefab, RoomContent);
            RoomList.Add(r);
            r.SetRoomData(roomList[i], UserName.text);
        }
    }
}
