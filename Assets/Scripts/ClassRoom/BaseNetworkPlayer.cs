using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;
using Photon.Pun;
using TMPro;

public class BaseNetworkPlayer : MonoBehaviour
{
    protected Canvas canvas;

    protected PhotonView photonView;

    protected PhotonVoiceView photonVoiceView;

    [SerializeField]
    protected Image speakerSprite;

    [SerializeField]
    protected TMP_Text infoText;

    protected virtual void Awake()
    {
        this.canvas = this.GetComponent<Canvas>();
        if (this.canvas != null && this.canvas.worldCamera == null) { this.canvas.worldCamera = Camera.main; }
        this.photonView = this.GetComponent<PhotonView>();
        this.photonVoiceView = this.GetComponentInParent<PhotonVoiceView>();
        this.infoText.text = this.photonView.Owner.NickName;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        this.speakerSprite.color = this.photonVoiceView.IsSpeaking ? Color.red : Color.black;
    }
}
