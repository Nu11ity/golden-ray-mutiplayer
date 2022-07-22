using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject waitingText;

    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        scoreDisplay.text = FindObjectOfType<Score>().score.ToString();

        if(!PhotonNetwork.IsMasterClient)
        {
            restartButton.SetActive(false);
            waitingText.SetActive(true);
        }

    }

    public void OnClickRestart()
    {
        view.RPC("Restart", RpcTarget.All);
    }

    [PunRPC]
    void Restart()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
