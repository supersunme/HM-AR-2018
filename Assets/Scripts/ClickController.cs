using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class ClickController : MonoBehaviour {

	public VideoConfigs videoConfig;

	void Start () {
		this.GetComponent<Button> ().onClick.AddListener (() => {
			VideoPlayer_1.Instance.InitVideo (videoConfig);
		});
	}
}