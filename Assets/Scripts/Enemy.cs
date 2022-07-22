using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject deathVFX;

    private PlayerController[] players;
    private PlayerController nearestPlayer;
    private Score score;
    private PhotonView view;

    private void Start()
    {
        players = FindObjectsOfType<PlayerController>();
        score = FindObjectOfType<Score>();
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!view.IsMine)
            return;

        float distanceOne = Vector2.Distance(transform.position, players[0].transform.position);
        float distanceTwo = Vector2.Distance(transform.position, players[1].transform.position);

        if (distanceOne < distanceTwo)
            nearestPlayer = players[0];
        else
            nearestPlayer = players[1];

        if (nearestPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if(PhotonNetwork.IsMasterClient)
        {
            if (collision.tag == "GoldenRay")
            {
                score.AddScore();
                view.RPC("SpawnParticle", RpcTarget.All);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }     
    }

    [PunRPC]
    void SpawnParticle()
    {
        Instantiate(deathVFX, transform.position, Quaternion.identity);
    }
}
