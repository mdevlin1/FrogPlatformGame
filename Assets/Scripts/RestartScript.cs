﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RestartScript : MonoBehaviour {

	public Button but;
	// Use this for initialization
	void Start () {
		Button btn = but.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	void TaskOnClick() {
		Application.LoadLevel("start");
	}
}
