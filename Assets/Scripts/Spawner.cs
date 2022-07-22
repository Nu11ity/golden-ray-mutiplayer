using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float startTimeBtwSpawns;

    private float timeBtwSpawns;
    private byte maxPlayers = 2;


    private void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom.PlayerCount != maxPlayers)
            return;

        if(timeBtwSpawns <= 0)
        {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            PhotonNetwork.Instantiate(enemy.name, spawnPosition, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
