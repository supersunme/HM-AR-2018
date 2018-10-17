using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;
public class VideoPlayer_1 : MonoBehaviour {
	private static VideoPlayer_1 _instance;
	public static VideoPlayer_1 Instance { get { return _instance; } }
	void Awake () {
		_instance = this;
	}
	public VideoPlayer currentVideoPlayer;
	public RawImage rawImage;

	public GameObject backButton;
	public GameObject playButton;
	//private VideoConfigs 
	public void InitVideo (VideoConfigs videoConfig) {
		RenderTexture rt = CreateRenderTexture ();
		rawImage.texture = rt;
		currentVideoPlayer.targetTexture = rt;
		if (videoConfig.videoClip != null) {
			currentVideoPlayer.clip = videoConfig.videoClip;
			currentVideoPlayer.waitForFirstFrame = true;
			currentVideoPlayer.Play ();
			currentVideoPlayer.Pause ();

			currentVideoPlayer?.gameObject.SetActive (true);
			if (videoConfig.isAwake == false) {
				backButton?.SetActive (true);
				playButton?.SetActive (true);
			} else {
				backButton?.SetActive (true);
				playButton?.SetActive (false);
				currentVideoPlayer.Play ();
			}
			currentVideoPlayer.isLooping = videoConfig.isLoop;
		}
	}
	public void CloseVideo () {
		currentVideoPlayer.Stop ();
		currentVideoPlayer?.gameObject.SetActive (false);
		backButton?.SetActive (false);
		playButton?.SetActive (false);
		rawImage.texture = null;
	}
	public void Play () {
		currentVideoPlayer?.gameObject.SetActive (true);
		currentVideoPlayer?.Play ();
		playButton?.SetActive (false);
	}
	public void Close(){
		Vuforia.CameraDevice.Instance.Start ();
		Vuforia.CameraDevice.Instance.SetFocusMode (Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		this.gameObject.SetActive(false);
	}
	private RenderTexture CreateRenderTexture () {
		RenderTexture rt = new RenderTexture (1280, 800, 16, RenderTextureFormat.ARGB32);
		return rt;
	}
}