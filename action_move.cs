using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class action_move: MonoBehaviour
{
    // Update is called once per frame

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            GetComponent<Animator>().Play("meAnimation");
        }
        
    }
}
