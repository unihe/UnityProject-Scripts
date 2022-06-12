using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public GameObject videoPlayer;
    public GameObject cube;
    public int timeToStop;
    void Start()
    {
        videoPlayer.SetActive(false);   

    }
    void OnTriggerEnter (Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            videoPlayer.SetActive(true);
            Destroy(videoPlayer, timeToStop);
            Destroy(cube, timeToStop);

        }
    }
    
}
