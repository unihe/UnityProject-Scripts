using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject panel;
    public Rigidbody m_rigidbody;

    private float targetspeed;
    private float curspeed;
    private float maxforwardspeed = 3f;
    private float maxforwaedspeed = -3f;
    public float distance = 5f;
    public float hight = 3f;
    public float useTime = 1.5f;
   public RotationAxes m_axes = RotationAxes.MouseXAndY;
    public float m_sensitivityX = 8f;
    public float m_sensitivityY = 8f;
    public float m_minimumX = -360f;
    public float m_maximumX = 360f;
    public float m_minimumY = -45f;
    public float m_maximumY = 45f;
    float m_rotationY = 0f;
    public float rigidbodyForce = 7f;

    Vector3 addForce;
    public GameObject data;
    private Rigidbody rb;
    public int count1;
    public int count2;
    public int count3;
    public int count4;
    public int count5;
    public int count6;
    public int water;
    public int book;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    public GameObject health;
    public bool isclick = false;
    public Vector3 mJumpSpees { get; private set; }
    public Animation ani;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bike") {
            Destroy(other.gameObject);
            count1 += 1;
            data.GetComponent<Data>().mentalState += 1;
            UIManager._instance.AddForceNumByint(1);
        }
        if (count1 >= 1) {
            one.SetActive(true);
        }
        if (other.gameObject.tag == "bottle")
        {
            Destroy(other.gameObject);
            count2 += 1;
            data.GetComponent<Data>().mentalState += 1;
            UIManager._instance.AddForceNumByint(1);
        }
        if (count2 >= 1)
        {
           two.SetActive(true);
        }
        if (other.gameObject.tag == "lemon")
        {
            Destroy(other.gameObject);
            count3 += 1;
            data.GetComponent<Data>().mentalState += 1;
            UIManager._instance.AddForceNumByint(1);
        }
        if (count3 >= 1)
        {
           three.SetActive(true);
        }




        if (other.gameObject.tag == "milk")
        {
            Destroy(other.gameObject);
            count4 += 1;
            data.GetComponent<Data>().mentalState += 1;
            UIManager._instance.AddForceNumByint(1);
        }
        if (count4 >= 1)
        {
            four.SetActive(true);
        }
        if (other.gameObject.tag == "apple")
        {
            Destroy(other.gameObject);
            count5 += 1;
            data.GetComponent<Data>().mentalState += 1;
            UIManager._instance.AddForceNumByint(1);
        }
        if (count5 >= 1)
        {
            five.SetActive(true);
        }
        if (other.gameObject.tag == "bread")
        {
            Destroy(other.gameObject);
            count6 += 1;
            data.GetComponent<Data>().mentalState += 1;
            UIManager._instance.AddForceNumByint(1);
        }
        if (count6 >= 1)
        {
            six.SetActive(true);
        }
    }

   void Start() {
        ani = GetComponent<Animation>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        if (GetComponent<Rigidbody>()) {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public enum RotationAxes {
        MouseXAndY=0,
        MouseX=1,
        MouseY=2
    }
}
