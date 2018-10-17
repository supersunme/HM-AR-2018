using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class LoadScene : MonoBehaviour {
	private AsyncOperation asyncOperate;

	private bool isAsyncComplete = false;

	public VideoPlayer videoPlayer;

	// Use this for initialization
	void Start () {
		StartCoroutine(DelayLoad());
		videoPlayer.loopPointReached += VideoPlayComplete;
	}

	private void VideoPlayComplete (VideoPlayer source) {
		Debug.Log("视频完成");
		if (isAsyncComplete) {
			asyncOperate.allowSceneActivation = true;
		}
	}
	void Update () {
		if (asyncOperate != null) {
			if (asyncOperate.progress >= 0.9f) {
				//asyncOperate.progress=1;
				isAsyncComplete = true;
			}
		}
	}
	IEnumerator DelayLoad () {
		yield return new WaitForSeconds (1.5f);
		asyncOperate = SceneManager.LoadSceneAsync ("main", LoadSceneMode.Single);
		asyncOperate.allowSceneActivation = false;
		yield return asyncOperate;
	}
}