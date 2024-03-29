﻿using UnityEngine;


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


public class AutoСontroller : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public float speed = 1;
    public Transform camRig;
    public Transform smog;

    //--------------

    Transform tr;
    RaycastHit hit;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Awake()
    {
        //--------------

        tr = transform;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Update()
    {
        //--------------

        tr.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime);
        tr.rotation = Quaternion.Euler(new Vector3((10 - Mathf.PingPong(Time.time, 20)) * 1.5f, tr.eulerAngles.y, 0));

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void LateUpdate()
    {
        //--------------

        camRig.position = tr.position;        

        if (Physics.Raycast(new Vector3(smog.position.x, smog.position.y + 2, smog.position.z), -tr.up, out hit, 100)) smog.position = new Vector3(smog.position.x, hit.point.y, smog.position.z);

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
