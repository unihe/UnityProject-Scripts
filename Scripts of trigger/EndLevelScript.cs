using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndLevelScript : MonoBehaviour {
	public Text guiPoints;
    private MouseControl mouseControl;
	public GameObject gui;
	public GameObject finishedUI;
	public GameObject startingUI;

	public AudioSource sound;
	public GameObject data;
	private void Awake()
	{
		if (GameObject.Find("Data") == null)
		{
			Instantiate(data);
		}
	}
	public void Show() {
		mouseControl = GameObject.Find("Game").GetComponent<MouseControl>();
		FruitDispenser.instance.started = false;
		mouseControl.Pause();
		mouseControl.pause = true;
		mouseControl.ban = true;
		if (sound != null && !sound.isPlaying) { sound.Play(); }//结束音效
		guiPoints.text = "Points: " + mouseControl.points;
		//data.GetComponent<Data>().grade += mouseControl.points;
		data.GetComponent<Data>().grade += 1;
		UIManager._instance.AddintelligenceByint(2);
		finishedUI.GetComponent<CanvasGroup>().alpha = 1;
		finishedUI.GetComponent<CanvasGroup>().interactable = true;
		finishedUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}
	
	public void Restart () {
		//-GUI
		gui.GetComponent<CanvasGroup>().alpha = 0;
		gui.GetComponent<CanvasGroup>().interactable = false;
		gui.GetComponent<CanvasGroup>().blocksRaycasts = false;

		//-结束面板
		finishedUI.GetComponent<CanvasGroup>().alpha = 0;
		finishedUI.GetComponent<CanvasGroup>().interactable = false;
		finishedUI.GetComponent<CanvasGroup>().blocksRaycasts = false;

		//+开始面板
		startingUI.GetComponent<CanvasGroup>().alpha = 1;
		startingUI.GetComponent<CanvasGroup>().interactable = true;
		startingUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void ExitGame()
	{
		SceneManager.LoadScene("Classroom");
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
