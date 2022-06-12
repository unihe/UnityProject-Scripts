using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{
    public static Data instance;
    public int grade = 0;               //academic performance
    public int speculationScore = 0;    //Participation in activities
    public int mentalState = 100;       //Describe the player health
    public float livingExpenses = 1500;
    public Vector3 vBJTU;
    public Vector3 vClassroom;
    public GameObject videoPlayer;
    public GameObject player;

    private void Awake()
    {
        //Set Start Position
        //校门vBJTU = new Vector3(-13932.42F, 1467.87F, -5133.6F);
        //操场vBJTU = new Vector3(-14150.2998F, 1467.87F, -5309.81006F);
        //教室vBJTU = new Vector3(-14033.7998F, 1467.87F, -5130.5F);
        if (SceneManager.GetActiveScene().name.Equals("Main---BJTU")&& vBJTU.x!=0) { vBJTU = new Vector3(-14150.2998F, 1467.87F, -5309.81006F); }
        else if(SceneManager.GetActiveScene().name.Equals("Classroom")&& vClassroom.x==0) { vClassroom = new Vector3(10F, 0.758999825F, 6.67000008F); }
        if (instance == null)                               //Initialize if there is no object
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grade > 8) {
            videoPlayer.SetActive(true);
        }
    }
}
