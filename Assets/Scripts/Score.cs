using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Score : MonoBehaviour
{
    public int score { get; private set; }

    [SerializeField] private TMP_Text scoreDisplay;

    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void AddScore()
    {
        view.RPC("AddScoreRPC", RpcTarget.All);
    }

    [PunRPC]
    void AddScoreRPC()
    {
        score++;
        scoreDisplay.text = score.ToString();
    }
}
