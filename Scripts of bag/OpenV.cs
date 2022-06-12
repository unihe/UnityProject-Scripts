using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenV : MonoBehaviour
{    //将要显示的背包拖入
    public GameObject MyBag;
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
        BagState = MyBag.activeSelf;
        //如果按下按键i
        if (Input.GetKeyDown(KeyCode.F))
        {
            //将状态设置为false的方面状态true
            BagState = !BagState;
            //将背包的状态设置为状态值
            MyBag.SetActive(BagState);
        }
    }
}