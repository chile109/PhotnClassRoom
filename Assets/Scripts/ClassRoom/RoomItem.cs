using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomItem : MonoBehaviour
{
    [SerializeField]
    private string m_roomName = "";

    public void SetRoomName(string roomName)
    {
        m_roomName = roomName;
        GetComponentInChildren<Text>().text = roomName;
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(m_roomName);
    }
}
