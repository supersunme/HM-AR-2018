using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class VideoPlayer_5 : MonoBehaviour {
	public GameObject[] txtArrays;
	public GameObject[] seriableFramesArray;
	public GameObject descriptionObj;
	public int? currentShow;
	public void ClickShow (int index) {
		descriptionObj?.SetActive (false);
		if (currentShow != null) {
			txtArrays[(int) currentShow].SetActive (false);
			seriableFramesArray[(int) currentShow].SetActive (false);
		}
		txtArrays[index].SetActive (true);
		seriableFramesArray[index].SetActive (true);
		currentShow = index;
	}

	void OnEnable () {
		currentShow = null;
		descriptionObj?.SetActive (true);
		for (var i = 0; i < 3; i++) {
			txtArrays[i].SetActive (false);
			seriableFramesArray[i].SetActive (false);
		}
	}

	public void Close () {
		currentShow = null;
		descriptionObj?.SetActive (false);
		for (var i = 0; i < 3; i++) {
			txtArrays[i].SetActive (false);
			seriableFramesArray[i].SetActive (false);
		}
		Vuforia.CameraDevice.Instance.Start ();
		Vuforia.CameraDevice.Instance.SetFocusMode (Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		this.gameObject.SetActive(false);
		
	}
	void OnDisable () {

	}
}