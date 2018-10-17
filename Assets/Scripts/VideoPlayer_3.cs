using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class VideoPlayer_3 : MonoBehaviour {
	public RawImage rawImage;
	public VideoPlayer videoPlayer;
	public VideoClip[] videoClips = new VideoClip[2];
	public GameObject txtDescriptionImage;
	public Button clickButton;
	void OnEnable () {
		RenderTexture rt = CreateRenderTexture ();
		rawImage.texture = rt;
		videoPlayer.targetTexture = rt;
		videoPlayer.clip = videoClips[0];
		
		videoPlayer.playOnAwake = true;
		videoPlayer.isLooping = false;
		videoPlayer.waitForFirstFrame = true;
		videoPlayer.Play ();
		videoPlayer.loopPointReached += OnPlayComplete;
		clickButton.onClick.AddListener (() => {
			clickButton.gameObject?.SetActive (false);
			txtDescriptionImage?.SetActive (true);
		});
	}
	private void OnPlayComplete (VideoPlayer source) {
		if (videoPlayer.clip == videoClips[0]) {
			videoPlayer.clip = null;
			videoPlayer.clip = videoClips[1];
			videoPlayer.playOnAwake = true;
			videoPlayer.isLooping = true;
			videoPlayer.waitForFirstFrame = true;
			videoPlayer.Play ();
		} else if (videoPlayer.clip == videoClips[1]) {
			//txtDescriptionImage?.SetActive (true);
			clickButton.gameObject?.SetActive (true);
		}
	}

	void OnDisable () {

		videoPlayer.Stop ();
		videoPlayer.loopPointReached -= OnPlayComplete;
		rawImage.texture = null;
		videoPlayer.targetTexture = null;
		videoPlayer.clip = null;
		txtDescriptionImage?.SetActive (false);
		clickButton.onClick.RemoveAllListeners ();
		clickButton.gameObject?.SetActive (false);
	}

	public void Close () {
		this.gameObject.SetActive (false);
		Vuforia.CameraDevice.Instance.Start ();
		Vuforia.CameraDevice.Instance.SetFocusMode (Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
	}
	private RenderTexture CreateRenderTexture () {
		var rt = new RenderTexture (1280, 800, 16, RenderTextureFormat.ARGB32);
		return rt;
	}
}