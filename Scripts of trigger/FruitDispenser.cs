using UnityEngine;

public class FruitDispenser : MonoBehaviour {
    //跨类控制
    public static FruitDispenser instance;

    public GameObject[] fruits;
    public GameObject[] text;
    public GameObject[] load;
    public GameObject bomb;
    private int mode = 0;

    public float z;

    public bool pause = false;
    public bool started = false;

    //每个水果发射的计时
    public float timer = 1.75f;

    public AudioSource[] sound;// fruit_launch, bombShow

    private void Awake()
    {
        instance = this;
    }

    void Update () {
        if (!started) return;
        if (pause) return;

        timer -= Time.deltaTime;

        if (timer <= 0 && !started)
        {
            timer = 0f;
        }

        //发射间隔
        if (started)
        {
            if (PrepareLevel.LoadLevel == 0)
            {
                if (timer <= 0)
                {
                    FireUp();
                    timer = 2.0f;
                }
            }
            else if (PrepareLevel.LoadLevel == 1)
            { 
                if (timer <= 0)
                {
                    FireUp();
                    timer = 1.5f;
                }
            }
            else if (PrepareLevel.LoadLevel == 2)
            {
                if (timer <= 0)
                {
                    FireUp();
                    timer = 1.0f;
                }
            }
            else if (PrepareLevel.LoadLevel == 3)
            {
                if (timer <= 0)
                {
                    FireUp();
                    timer = 0.8f;
                }
            }
        }	
	}
    public void setMode(int i) {
        mode = i;
    }
    void FireUp()
    {
        if (pause) return;

        //每次必有的水果
        Spawn(false);
        //再来点水果
        if (PrepareLevel.LoadLevel == 1 && Random.Range(0, 10) < 2)
        {
            Spawn(false);
        }
        else if (PrepareLevel.LoadLevel == 1 && Random.Range(0, 10) < 4)
        {
            Spawn(false);
        }
        else if (PrepareLevel.LoadLevel == 2 && Random.Range(0, 10) < 6)
        {
            Spawn(false);
        }
        else if(PrepareLevel.LoadLevel == 3)
        {
            if (Random.Range(0, 10) < 8) { Spawn(false); }
            if (Random.Range(0, 10) < 6) { Spawn(false); Spawn(false); }

        }

        //炸弹
        if (PrepareLevel.LoadLevel == 0 && Random.Range(0, 100) < 10)
        {
            Spawn(true);
        }
        else if (PrepareLevel.LoadLevel == 1 && Random.Range(0, 100) < 30)
        {
            Spawn(true);
        }
        else if (PrepareLevel.LoadLevel == 2 && Random.Range(0, 100) < 50)
        {
            Spawn(true);
        }
        else if (PrepareLevel.LoadLevel == 3)
        {
            if (Random.Range(0, 100) < 70) { Spawn(true); }
            if (Random.Range(0, 100) < 60) { Spawn(true); Spawn(true); }
        }
    }


    void Spawn(bool isBomb)
    {
        if (mode == 0)
        {
            load = fruits;
            SpawnFruit(isBomb);
        }
        else if (mode == 1) {
            load = text;
            SpawnCorrectText(isBomb);
        }
        
    }

    void SpawnFruit(bool isBomb)
    {
        //随机x
        float x = Random.Range(-3.1f, 3.1f);

        z = Random.Range(14f, 19.8f);

        GameObject ins;

        //Instantiate(prefab,position, rotation) ->docs.unity3d.com/Manual
        if (!isBomb)
        {
            ins = Instantiate(load[Random.Range(0, load.Length)], transform.position + new Vector3(x, -3f, z), Random.rotation) as GameObject;
            //水果生成音效
            if (sound[0] != null && !sound[0].isPlaying) { sound[0].Play(); }
        }

        else
        {
            ins = Instantiate(bomb, transform.position + new Vector3(x, -3f, z), Random.rotation) as GameObject;
            //呲花音效
            if (sound[1] != null && !sound[1].isPlaying) { sound[1].Play(); }

        }

        //初始力量
        float power = Random.Range(1.5f, 1.8f) * -Physics.gravity.y * 0.5f;//大小
        Vector3 direction = new Vector3(-x * 0.05f * Random.Range(0.3f, 0.8f), 1, 0);//方向
        direction.z = 0f;
        ins.GetComponent<Rigidbody>().velocity = direction * power;//速度矢量
        ins.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * 0.1f, ForceMode.Impulse);//力，扭矩，三维，单位球*尺寸
    }

    void SpawnCorrectText(bool isBomb)
    {
        float x = Random.Range(-3f, 3f);
        z = Random.Range(14f, 19.8f);

        GameObject ins;

        //Instantiate(prefab,position, rotation) ->docs.unity3d.com/Manual
        if (!isBomb)
        {
            ins = Instantiate(load[0], transform.position + new Vector3(x, -3f, z), transform.rotation) as GameObject;
            ins.GetComponent<VTextInterface>().RenderText = generateText(0);
            //水果生成音效
            if (sound[0] != null && !sound[0].isPlaying) { sound[0].Play(); }
        }

        else
        {
            ins = Instantiate(load[1], transform.position + new Vector3(x, -3f, z), transform.rotation) as GameObject;
            ins.GetComponent<VTextInterface>().RenderText = generateText(1);
            //水果生成音效
            if (sound[0] != null && !sound[0].isPlaying) { sound[0].Play(); }

        }

        //初始力量
        float power = Random.Range(1.2f, 1.8f) * -Physics.gravity.y * 0.5f;//大小
        Vector3 direction = new Vector3(-x * 0.05f * Random.Range(0.3f, 0.8f), 1, 0);//方向
        direction.x = 0f;
        direction.z = 0f;
        ins.GetComponent<Rigidbody>().velocity = direction * power;//速度矢量
        ins.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * 0.1f, ForceMode.Impulse);//力，扭矩，三维，单位球*尺寸
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    string generateText(int i) {
        if (i == 0)
        {
            string[] correctCal = new string[] { "x-sinx ~ (x^3)/6", "e^x - 1 ~ x", "1-cosx ~ x^2 / 2", "(1+x)^a - 1 ~ ax", "sinx ~ x", "tanx ~ x", "arctanx ~ x", "arcsinx ~ x", "ln(1+x) ~ x", "a~x - 1 ~ xlna" };
            return correctCal[Random.Range(0, correctCal.Length - 1)];
        }
        else if (i == 1)
        {
            string[] incorrectCal = new string[] { "x-sinx ~ -(x^3)/6", "e^x ~ x", "cosx-1 ~ x^2 / 2", "(1+x)^a ~ ax", "cosx ~ x", "cotx ~ x", "arccotx ~ x", "lnx ~ x", "a~x ~ xlna" };
            return incorrectCal[Random.Range(0, incorrectCal.Length - 1)];
        }
        else return null;
    }
}
