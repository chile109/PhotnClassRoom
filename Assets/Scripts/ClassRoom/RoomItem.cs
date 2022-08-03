using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomItem : MonoBehaviour
{
    [SerializeField]
    private string m_roomName = "";

    public void SetRoomData(RoomInfo room, string nickName)
    {
        m_roomName = room.Name;
        PhotonNetwork.NickName = nickName;
        GetComponentInChildren<Text>().text = room.Name + " (" + room.PlayerCount + "/" + room.MaxPlayers + ")";
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(m_roomName);
    }
}
