using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour {

    Vector2 screenInp;

    bool fire = false;
    bool fire_prev = false;
    bool fire_down = false;
    bool fire_up = false;

    public LineRenderer trail;

    Vector2 start, end;

    Vector3[] trailPositions = new Vector3[10];//Trail中10个

    int index;
    int linePart = 0;
    float lineTimer = 1.0f;

    float trail_alpha = 0f;
    int raycastCount = 10;

    //积分
    public int points;

    bool started = false;
    public bool pause = false;
    public bool ban = false;

    //果汁效果预制品
    public GameObject[] splashPrefab;
    public GameObject[] splashFlatPrefab;

    public AudioSource[] sounds;//knife, explode, fruit1, fruit2, fruit3, bingo

    void Start () {
    }

    void BlowObject(RaycastHit hit)
    {
        string label = hit.collider.gameObject.tag;
        if (label != "destroyed" && label != "effects")//碰撞到没被贴标签的
        {
            if (hit.collider.tag == "red") index = 0;
            if (hit.collider.tag == "yellow") index = 1;
            if (hit.collider.tag == "green") index = 2;

            //水果泼溅效果
            if (label == "red" || label == "yellow" || label == "green")
            {
                //生成切开的水果的部分
                hit.collider.gameObject.GetComponent<ObjectKill>().OnKill();

                Vector3 splashPoint = hit.point;
                splashPoint.z = 4f;
                Instantiate(splashPrefab[index], splashPoint, Quaternion.identity);
                splashPoint.z = 9.9f;//贴图靠后
                Instantiate(splashFlatPrefab[index], splashPoint, Quaternion.identity);
                //泼溅音效
                if (sounds[index+2] != null) { sounds[index+2].Play(); }

                points += (PrepareLevel.LoadLevel+1);//分数
            }

            //切到炸弹
            else if (label == "bomb")
            {
                hit.collider.gameObject.GetComponent<ObjectKill>().OnKill();
                points -= 5;
                points = points < 0 ? 0 : points;//points不能<0
                //炸弹生成音效停止
                FruitDispenser.instance.sound[1].Stop();
                //爆炸音效
                if (sounds[1] != null) { sounds[1].Play(); }
            }

            //切到正确
            else if (label == "correct_text")
            {
                //答对
                if (sounds[5] != null) { sounds[5].Play(); }

                points += 1;//分数
            }

            //切到错误
            else if (label == "incorrect_text")
            {
                hit.collider.gameObject.GetComponent<ObjectKill>().OnKill();
                points -= 5;//分数
                points = points < 0 ? 0 : points;//points不能<0
                //炸弹生成音效停止
                FruitDispenser.instance.sound[1].Stop();
                //爆炸音效
                if (sounds[1] != null) { sounds[1].Play(); }
            }

            Destroy(hit.collider.gameObject);
            hit.collider.gameObject.tag = "destroyed";
        }
    }
	
	void Update () {
        screenInp.x = Input.mousePosition.x;
        screenInp.y = Input.mousePosition.y;

        fire_down = false;
        fire_up = false;

        fire = Input.GetMouseButton(0);
        if (fire && !fire_prev) fire_down = true;
        if (!fire && fire_prev) fire_up = true;
        fire_prev = fire;

        //控制画线
        Control();

        //设置线段的相应颜色
        Color c1 = new Color(1, 1, 0, trail_alpha);
        Color c2 = new Color(1, 0, 0, trail_alpha);
        trail.SetColors(c1, c2);

        if (trail_alpha > 0) trail_alpha -= Time.deltaTime;
	}

    void Control()
    {
        //线段开始
        if (fire_down && !pause && !ban)
        {
            trail_alpha = 1.0f;//控制消失

            start = screenInp;
            end = screenInp;

            started = true;

            linePart = 0;
            lineTimer = 0.25f;
            AddTrailPosition();
        }

        //鼠标拖动中
        if (fire && started &&!pause &&!ban)
        {
            start = screenInp;

            var a = Camera.main.ScreenToWorldPoint(new Vector3(start.x, start.y, 10));
            var b = Camera.main.ScreenToWorldPoint(new Vector3(end.x, end.y, 10));

            //判断用户的鼠标（触屏）移动大于0.1后，我们认为这是一个有效的移动，就可以进行一次“采样”(sample)
            if (Vector3.Distance(a, b) > 0.1f)
            {
                linePart++;
                lineTimer = 0.25f;
                AddTrailPosition();

                //拖动音效
                if (sounds[0] != null && !sounds[0].isPlaying)  { sounds[0].Play();}
            }

            trail_alpha = 0.75f;

            end = screenInp;
        }

        //线的alpha值大于0.5的时候，可以做射线检测（鼠标轨迹分段，作射线，判断是否与物体相交）
        if (trail_alpha > 0.5f)
        {
            for (var p = 0; p < 8; p++)
            { 
                for (var i = 0; i < raycastCount; i++)
                {
                    Vector3 s = Camera.main.WorldToScreenPoint(trailPositions[p]);
                    Vector3 e = Camera.main.WorldToScreenPoint(trailPositions[p+1]);
                    Ray ray = Camera.main.ScreenPointToRay(Vector3.Lerp(s, e, i / raycastCount));

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("fruit")))//仅删除fruit层
                    {
                        BlowObject(hit);
                    }
                }

            }
        }

        if (trail_alpha <= 0) linePart = 0;

        //根据时间加入一个点
        lineTimer -= Time.deltaTime;
        if (lineTimer <= 0f)
        {
           linePart++;
           AddTrailPosition();
           lineTimer = 0.01f;
        }

        if (fire_up && started) started = false;

        //拷贝线段的数据到linerenderer
        SendTrailPosition();
    }

    void AddTrailPosition()
    {
        if (linePart <= 9)
        {
            for (int i = linePart; i <= 9; i++)
            {
                trailPositions[i] = Camera.main.ScreenToWorldPoint(new Vector3(start.x, start.y, 10));
            }
        }
        else//超出数组长度
        {
            for (int ii = 0; ii <= 8; ii++)
            {
                trailPositions[ii] = trailPositions[ii + 1];
            }

            trailPositions[9] = Camera.main.ScreenToWorldPoint(new Vector3(start.x, start.y, 10));
        }
    }

    void SendTrailPosition()
    {
        var index = 0;
        foreach(Vector3 v in trailPositions)
        {
            trail.SetPosition(index++, v);
        }
    }

    public void Pause()
    {
        if (!ban)
        {
            pause = !pause;
            if (FruitDispenser.instance.sound[1].isPlaying)
            {
                FruitDispenser.instance.sound[1].Stop();
            }
        }
        else return;
        
    }
}
