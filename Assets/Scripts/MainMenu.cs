using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;
    [SerializeField] private string sceneName = "Game";

    private byte maxPlayers = 2;

    public void ChangeName()
    {
        PhotonNetwork.NickName = nameInput.text;
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
