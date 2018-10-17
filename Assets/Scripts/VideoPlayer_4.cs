using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class VideoPlayer_4 : MonoBehaviour {

	public RawImage[] rawImages;
	public VideoClip[] videoClips;
	public VideoPlayer[] videoPlayers;
	public RectTransform horizontalRectRect;
	public RectTransform backRect;
	public RectTransform sliderRect;

	public int currentVideoIndex = -1;

	public GameObject close;
	public GameObject next;
	public GameObject last;
	public GameObject whiteDot;
	public Button playButton;

	/// <summary>
	/// 移动方向
	/// 1代表下一个
	/// 0代表上一个
	/// </summary>
	private int direction = 1;
	void OnEnable () {
		StartCoroutine (AsyncInit ());
	}

	void OnDisable () {
		//StopCoroutine (AsyncInit ());
		//StartCoroutine (DisposeIenumator ());
		for (var i = 0; i < 5; i++) {
		videoPlayers[i].Stop ();
		rawImages[i].texture = null;
		videoPlayers[i].targetTexture = null;
		}
		playButton.gameObject.SetActive(false);
		backRect.anchoredPosition = Vector2.zero;
		horizontalRectRect.anchoredPosition = new Vector2 (2560, 0);
		sliderRect.anchoredPosition = new Vector2 (-640, 65);
	}
	public void Next () {
		//videoPlayers[currentVideoIndex].Pause();
		currentVideoIndex++;
		direction = 1;
		MoveUI ();
	}
	public void Last () {
		//videoPlayers[currentVideoIndex].Pause();
		currentVideoIndex--;
		direction = 0;
		MoveUI ();
	}
	private void MoveUI () {
		var temp = direction == 1 ? -1 : 1;
		next.SetActive (currentVideoIndex == 4 ? false : true);
		last.SetActive (currentVideoIndex == -1 ? false : true);
		playButton.gameObject.SetActive (false);
		
		whiteDot?.SetActive (false);
		horizontalRectRect.DOAnchorPosX (horizontalRectRect.anchoredPosition.x + temp * 1280, 1);
		backRect.DOAnchorPosX (backRect.anchoredPosition.x + temp * 1280, 1);
		sliderRect.DOAnchorPosX (sliderRect.anchoredPosition.x + temp * 229, 1).OnComplete (() => {
			whiteDot?.SetActive (true);
			playButton.gameObject.SetActive (currentVideoIndex == 4 ? false : true);
			playButton.gameObject.SetActive (currentVideoIndex == -1 ? false : true);
		});

	}
	public void PlayVideo () {
		videoPlayers[currentVideoIndex].Play ();
		playButton.gameObject.SetActive (false);
	}

	public void Close () {
		
		Vuforia.CameraDevice.Instance.Start ();
		Vuforia.CameraDevice.Instance.SetFocusMode (Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		this.gameObject.SetActive (false);

	}
	private RenderTexture[] CreateRenderTexture () {
		RenderTexture[] rts = new RenderTexture[5];
		for (var i = 0; i < 5; i++) {
			rts[i] = new RenderTexture (1280, 800, 16, RenderTextureFormat.ARGB32);
		}
		return rts;
	}
	IEnumerator AsyncInit () {
		yield return null;
		next.SetActive (true);
		last.SetActive (false);
		whiteDot.SetActive (true);
		currentVideoIndex = -1;
		RenderTexture[] rts = CreateRenderTexture ();
		for (var i = 0; i < 5; i++) {
			rawImages[i].texture = rts[i];
			videoPlayers[i].targetTexture = rts[i];
			videoPlayers[i].clip = videoClips[i];
			videoPlayers[i].Play ();
			videoPlayers[i].Pause ();
		}
		yield return null;
	}
}