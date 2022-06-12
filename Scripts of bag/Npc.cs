using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{    //将要显示的背包拖入
    public GameObject MyBag;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    //背包状态
    private bool BagState;


    void Update()
    {
        //调用判断方法
        OpenMyBag();
    }

    private void OpenMyBag()
    {
        //默认状态赋值为false

        //如果按下按键i
        if (one.active==true&&two.active==true&&three.active==true)
        {
            //将状态设置为false的方面状态true

            //将背包的状态设置为状态值
            MyBag.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            MyBag.SetActive(false);
        }
    }
}
