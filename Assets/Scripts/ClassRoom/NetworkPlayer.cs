using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;
using Photon.Pun;
using TMPro;
using UnityEngine.InputSystem;

public class NetworkPlayer : BaseNetworkPlayer
{
    public GameObject PlayerPrefab;

    [SerializeField]
    private Image bubleSprite;

    [SerializeField]
    private Button bubbleBtn;

    [SerializeField]
    private Button speakerBtn;

    [SerializeField]
    private bool isRaisedHand = false;

    [SerializeField]
    private bool isSpeaker = false;

    public InputActionReference RaiseHandAction;

    protected override void Awake()
    {
        base.Awake();

        isRaisedHand = this.photonView.Owner.CustomProperties["isRaisedHand"] == null ? false : (bool)this.photonView.Owner.CustomProperties["isRaisedHand"];
        isSpeaker = this.photonView.Owner.CustomProperties["isSpeaker"] == null ? false : (bool)this.photonView.Owner.CustomProperties["isSpeaker"];

        if (isSpeaker)
        {
            ClassManager.Instance.AddSpeaker(this);
        }

        if (this.photonView.IsMine)
        {
            RaiseHandAction.action.performed += this.TryRaisedHand;
        }
        else
        {
            // If we are the teacher, we have full feature to interact with the student.
            if (PhotonNetwork.IsMasterClient)
            {
                SetButtonCallback();
            }
        }
    }

    private void OnDestroy()
    {
        if (this.photonView.IsMine)
        {
            RaiseHandAction.action.performed -= this.TryRaisedHand;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        this.bubleSprite.enabled = this.isRaisedHand && !this.isSpeaker;
        this.speakerSprite.enabled = this.isSpeaker;

        if (this.photonView.IsMine)
        {
            this.photonVoiceView.RecorderInUse.IsRecording = this.isSpeaker;
        }

        base.Update();
    }

    public void SetPrefab(GameObject prefab)
    {
        this.PlayerPrefab = prefab;
        prefab.transform.SetParent(this.transform);
        prefab.transform.localPosition = Vector3.zero;
        prefab.transform.localEulerAngles = Vector3.zero;
    }

    public void SetButtonCallback()
    {
        bubbleBtn.onClick.AddListener(this.ClickRollCall);
        speakerBtn.onClick.AddListener(this.ClickMute);
    }

    public void ClearPrefab()
    {
        this.PlayerPrefab = null;
    }

    public void ClickRollCall()
    {
        this.photonView.RPC("RollCall", RpcTarget.All);
    }

    public void ClickMute()
    {
        this.photonView.RPC("Mute", RpcTarget.All);
    }

    private void TryRaisedHand(InputAction.CallbackContext context)
    {
        XRInputNotifyCenter.NotifyEvent("raisehand", this.photonView);
    }

    #region Photon RPC

    [PunRPC]
    void RaisedHand()
    {
        this.isRaisedHand = !this.isRaisedHand;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "isSpeaker", this.isRaisedHand } });
        Debug.Log("RaisedHand: " + this.isRaisedHand);
    }

    [PunRPC]
    void RollCall()
    {
        this.isSpeaker = true;
        this.isRaisedHand = false;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "isSpeaker", true } });
        ClassManager.Instance.AddSpeaker(this);
        Debug.Log("OnRollCall");
    }

    [PunRPC]
    void Mute()
    {
        this.isSpeaker = false;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "isSpeaker", false } });
        ClassManager.Instance.RemoveSpeaker(this);
        Debug.Log("OnMute");
    }

    #endregion
}
