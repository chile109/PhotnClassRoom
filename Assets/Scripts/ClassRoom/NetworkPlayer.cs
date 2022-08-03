using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;
using Photon.Pun;
using TMPro;
using UnityEngine.InputSystem;

public class NetworkPlayer : MonoBehaviour
{
    public GameObject PlayerPrefab;

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

    public InputActionReference RaiseHandAction;

    void Awake()
    {
        this.canvas = this.GetComponent<Canvas>();
        if (this.canvas != null && this.canvas.worldCamera == null) { this.canvas.worldCamera = Camera.main; }
        this.photonView = this.GetComponent<PhotonView>();
        this.photonVoiceView = this.GetComponentInParent<PhotonVoiceView>();
        this.infoText.text = this.photonView.Owner.NickName + "_" + this.photonView.Owner.UserId;

        isRaisedHand = this.photonView.Owner.CustomProperties["isRaisedHand"] == null ? false : (bool)this.photonView.Owner.CustomProperties["isRaisedHand"];
        isSpeaker = this.photonView.Owner.CustomProperties["isSpeaker"] == null ? false : (bool)this.photonView.Owner.CustomProperties["isSpeaker"];

        if (isSpeaker)
        {
            ClassManager.Instance.AddSpeaker(this);
        }

        RaiseHandAction.action.performed += TryRaisedHand;
    }

    private void OnDestroy()
    {
        RaiseHandAction.action.performed -= TryRaisedHand;
    }

    // Update is called once per frame
    void Update()
    {
        this.bubleSprite.enabled = this.isRaisedHand && !this.isSpeaker;
        this.speakerSprite.enabled = this.isSpeaker;
        this.speakerSprite.color = this.photonVoiceView.IsSpeaking ? Color.red : Color.black;

        if (this.photonView.IsMine)
        {
            this.photonVoiceView.RecorderInUse.IsRecording = this.isSpeaker;
        }
    }

    public void SetPrefab(GameObject prefab)
    {
        this.PlayerPrefab = prefab;
        prefab.transform.SetParent(this.transform);
        prefab.transform.localPosition = Vector3.zero;
        prefab.transform.localEulerAngles = Vector3.zero;
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

    void TryRaisedHand(InputAction.CallbackContext context)
    {
        this.photonView.RPC("RaisedHand", RpcTarget.All);
    }
}
