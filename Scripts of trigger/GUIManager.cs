using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public Text guiPoints;

	MouseControl mouseControl;

	bool gamePause = false;

	public Text pauseButtonText;

	void Start () {

		mouseControl = GameObject.Find("Game").GetComponent<MouseControl>();
	
	}
	
	void Update () {

		guiPoints.text = "Points:  " + mouseControl.points;
	
	}

	public void Pause()
	{
		Rigidbody[] rs = GameObject.FindObjectsOfType<Rigidbody>();

		gamePause = !gamePause;

		if (gamePause)
		{
			foreach(Rigidbody r in rs)
			{
				r.Sleep();
				pauseButtonText.text = "Resume";
				FruitDispenser.instance.pause = true;
				Timer.instance.PauseTimer(gamePause);
			}
		}
		else
		{
			foreach(Rigidbody r in rs)
			{
				r.WakeUp();
				pauseButtonText.text = "Pause";
				FruitDispenser.instance.pause = false;
				Timer.instance.PauseTimer(gamePause);
			}
		}
	}
}
