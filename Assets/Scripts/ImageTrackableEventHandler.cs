using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTrackableEventHandler : DefaultTrackableEventHandler {
	public GameObject canvasObj;
	protected override void Start()
	{
		base.Start();
	}
	protected override  void OnDestroy()
	{
		base.OnDestroy();
	}
	protected override void OnTrackingFound(){
		print("识别到当前对象");
		Vuforia.CameraDevice.Instance.Stop();
		canvasObj?.SetActive(true);
	}
	protected override void OnTrackingLost(){
		print("失去识别对象");
	}
}
