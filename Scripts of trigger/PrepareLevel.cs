using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrepareLevel : MonoBehaviour {

	//计时时间
	public static int ConfigTime = 60; //seconds
	//游戏难度
	public static int LoadLevel = 2;
	//游戏难度名字
	public static string[] LevelName = new string[] { "Easy", "Medium", "Hard", "Extreme" };
    private Color LevelColor;

    public GameObject startingUI;
	public GameObject gui;
	public GameObject GetReady;
	public GameObject GO;
    public GameObject levelName;
    private MouseControl mouseControl;

    public GameObject background;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mouseControl = GameObject.Find("Game").GetComponent<MouseControl>();
        //background
        FruitDispenser.instance.setMode(1);
    }

    public void Easy()
    {
        ConfigTime = 30;
        LoadLevel = 0;
        LevelColor = new Color32(140, 253, 152, 255);
        StartCoroutine(Prepare());
    }
    public void Medium()
    {
        ConfigTime = 20;
        LoadLevel = 1;
        LevelColor = new Color32(43, 103, 243, 255);
        StartCoroutine(Prepare());
    }
    public void Hard()
    {
        ConfigTime = 50;
        LoadLevel = 2;
        LevelColor = new Color32(244, 255, 74, 255);
        StartCoroutine(Prepare());
    }
    public void Extreme()
    {
        ConfigTime = 60;
        LoadLevel = 3;
        LevelColor = new Color32(245, 61, 39, 255);
        StartCoroutine(Prepare());
    }
   
    IEnumerator Prepare()
{
        //消失
        startingUI.GetComponent<CanvasGroup>().alpha = 0;
        startingUI.GetComponent<CanvasGroup>().interactable = false;
        startingUI.GetComponent<CanvasGroup>().blocksRaycasts = false;

        //准备
        mouseControl.points = 0;
        mouseControl.ban = true;
        levelName.GetComponent<Text>().text = LevelName[LoadLevel];
        levelName.GetComponent<Text>().color = LevelColor;
        Timer.instance.timeAvailable = ConfigTime;

        //+gui
        gui.GetComponent<CanvasGroup>().alpha = 1;
        gui.GetComponent<CanvasGroup>().interactable = true;
        gui.GetComponent<CanvasGroup>().blocksRaycasts = true;

        GetReady.SetActive(true);//显示GetReady
        yield return new WaitForSecondsRealtime(2.0f);//等待2秒
        GetReady.SetActive(false);
        GO.SetActive(true);
        yield return new WaitForSecondsRealtime(1.0f);//等待1秒
        GO.SetActive(false);
        

        //开始 
        mouseControl.pause = false;
        mouseControl.ban = false;
        FruitDispenser.instance.started = true;
        Timer.instance.PauseTimer(false);
        Timer.instance.RunTimer();
    }
}