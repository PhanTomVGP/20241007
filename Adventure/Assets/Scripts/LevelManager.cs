using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitBeforeRespawning;
    public bool respawning;

    private PlayerController player;
    public Vector3 respawnPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Respawn()
    {
        if (!respawning)
        {
            respawning = true;
            StartCoroutine(ReSpawnCo());
        }
    }

    IEnumerator ReSpawnCo()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitBeforeRespawning);
        player.transform.position = respawnPoint;
        player.gameObject.SetActive(true);
        respawning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        respawnPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
