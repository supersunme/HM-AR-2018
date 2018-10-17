using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class TouchFocous : MonoBehaviour {

	void Awake()
	{
		Screen.SetResolution(1280,800,true);
	}

	// Use this for initialization
	void Start () {
		Vuforia.CameraDevice.Instance.SetFocusMode (Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			//Vuforia.CameraDevice.Instance.Start ();
			Vuforia.CameraDevice.Instance.SetFocusMode (Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		}
	}
}