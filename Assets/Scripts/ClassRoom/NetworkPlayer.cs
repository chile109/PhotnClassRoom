using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;
using Photon.Pun;
using TMPro;

public class NetworkPlayer : MonoBehaviour
{
    public string NickName = "Player";

    public int ID = 0;

    private Canvas canvas;

    private PhotonView photonView;

    private PhotonVoiceView photonVoiceView;

    [SerializeField]
    private Image bubleSprite;

    [SerializeField]
    private Image speakerSprite;

    [SerializeField]
    private TMP_Text infoText;

    [SerializeField]
    private bool isRaisedHand = false;

    [SerializeField]
    private bool isSpeaker = false;


    void Awake()
    {
        this.canvas = this.GetComponent<Canvas>();
        if (this.canvas != null && this.canvas.worldCamera == null) { this.canvas.worldCamera = Camera.main; }
        this.photonView = this.GetComponent<PhotonView>();
        this.photonVoiceView = this.GetComponentInParent<PhotonVoiceView>();
    }

    // Update is called once per frame
    void Update()
    {
        this.bubleSprite.enabled = this.isRaisedHand && !this.isSpeaker;
        this.speakerSprite.enabled = this.isSpeaker;
        this.speakerSprite.color = this.photonVoiceView.IsSpeaking ? Color.red : Color.black;
        this.infoText.text = NickName + "_" + this.ID;
        this.photonVoiceView.RecorderInUse.IsRecording = this.isSpeaker;

        if (this.photonView.IsMine)
        {
            TestRaisedHand();
        }
    }

    public void ClickRollCall()
    {
        this.photonView.RPC("RollCall", RpcTarget.All);
    }

    public void ClickMute()
    {
        this.photonView.RPC("Mute", RpcTarget.All);
    }

    #region Photon RPC

    [PunRPC]
    void RaisedHand()
    {
        this.isRaisedHand = !this.isRaisedHand;
        Debug.Log("RaisedHand: " + this.isRaisedHand);
    }

    [PunRPC]
    void RollCall()
    {
        this.isSpeaker = true;
        this.isRaisedHand = false;
        Debug.Log("OnRollCall");
    }

    [PunRPC]
    void Mute()
    {
        this.isSpeaker = false;
        Debug.Log("OnMute");
    }

    #endregion

    void TestRaisedHand()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.photonView.RPC("RaisedHand", RpcTarget.All);
        }
    }
}
