using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public static Timer instance;

    bool run = false;
    public bool showTimeLeft = true;
    bool timeEnd = false;

    float startTime = 0.0f;
    float curTime = 0.0f;
    string curStrTime = string.Empty;
    bool pause = false;

    public float timeAvailable = 30f; // 30 seconds
    float showTime = 0;

    public Text guiTimer;

    EndLevelScript endLevelScript;

    private void Start()
    {
        endLevelScript = GameObject.Find("FinishedUI").GetComponent<EndLevelScript>();
    }

    private void Awake()
    {
        instance = this;
    }

    public void RunTimer()
    {
        run = true;
        startTime = Time.time;
        showTimeLeft = true;
    }

    public void PauseTimer(bool b)
    {
        pause = b;
    }
	
	void Update () {

        if (pause)
        {
            startTime = startTime + Time.deltaTime;
            return;
        }

        if (run)
        {
            curTime = Time.time - startTime;
        }

        if (showTimeLeft)
        {
            showTime = timeAvailable - curTime;
            if (showTime < 0)
            {
                timeEnd = true;
                showTime = 0;
                showTimeLeft = false;

                //弹出UI界面，停止游戏
                endLevelScript.Show();
            }
        }

        int minutes = (int) (showTime / 60);
        int seconds = (int) (showTime % 60);
        int fraction = (int) ((showTime * 100) % 100);

        curStrTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        guiTimer.text = "Time: " + curStrTime;
	
	}
}
