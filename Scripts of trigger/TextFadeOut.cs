using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFadeOut : MonoBehaviour {

	//fade速度
	public float speed;
	Color color;

	void Start () {
		color = GetComponent<Text>().color;
	}
	
	void Update () {

		if (gameObject.activeSelf)
		{
			color.a -= Time.deltaTime * speed;
			if (color.a < 0)
			{
				color.a = 1.0f;
			}
			GetComponent<Text>().color = color;
		}
	
	}
}
