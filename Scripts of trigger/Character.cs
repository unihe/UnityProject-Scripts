using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public static Character instance;
    public bool tClassroom = false;
    public bool tDoor = false;
    public bool tCar = false;
    public bool tRace = false;
    public bool tFootball = false;
    public bool tArmy = false;
    public bool tBuilding = false;


    void Start()
    {
        instance = this;
        if (Data.instance.vBJTU.x < -1150.3F&&SceneManager.GetActiveScene().name.Equals("Main---BJTU")) { gameObject.GetComponent<Transform>().position = Data.instance.vBJTU; }
        else if(Data.instance.vClassroom.x >0 && SceneManager.GetActiveScene().name.Equals("Classroom")) { gameObject.GetComponent<Transform>().position = Data.instance.vClassroom; }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (tClassroom)
            {
                Data.instance.vClassroom = gameObject.transform.position;
                SceneManager.LoadScene("Fruit");
            }
            else if (tDoor)
            {
                SceneManager.LoadScene("Main---BJTU");
            }
            else if (tBuilding)
            {
                Data.instance.vBJTU = gameObject.transform.position;
                SceneManager.LoadScene("Classroom");
            }
            else if (tCar)
            {
                Data.instance.vBJTU = gameObject.transform.position;
                SceneManager.LoadScene("Main Menu");
            }
            else if (tRace)
            {
                Data.instance.vBJTU = gameObject.transform.position;
                SceneManager.LoadScene("start screen");
            }
            else if (tFootball)
            {
                Data.instance.vBJTU = gameObject.transform.position;
                SceneManager.LoadScene("football");
            }
            else if (tArmy)
            {
                Data.instance.vBJTU = gameObject.transform.position;
                SceneManager.LoadScene("Éä»÷");
            }
        }
        
    }
}
