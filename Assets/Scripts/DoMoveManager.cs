using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class DoMoveManager : MonoBehaviour {
	public RectTransform containerRect;
	public RectTransform sliderRect;
	public Button nextBtn;
	public Button lastBtn;
	public GameObject whiteDotObj;
	private int index = 0;

	private Vector2 containerStartPos;
	private Vector2 sliderRectStartPos;

	void OnEnable()
	{
		index=0;
		lastBtn.gameObject.SetActive(false);
		nextBtn.gameObject.SetActive(true);
		whiteDotObj.SetActive(true);
	}
	void OnDisable()
	{
		index=0;
		containerRect.anchoredPosition=containerStartPos;
		sliderRect.anchoredPosition=sliderRectStartPos;
		whiteDotObj.SetActive(false);
	}
	void Start () {
		containerStartPos = containerRect.anchoredPosition;
		sliderRectStartPos = sliderRect.anchoredPosition;
		lastBtn.gameObject.SetActive (false);
	}
	public void MoveNextContainer () {
		index++;
		lastBtn.gameObject.SetActive (index == 0 ? false : true);
		nextBtn.gameObject.SetActive (index == 8 ? false : true);
		lastBtn.GetComponent<Image> ().raycastTarget = false;
		nextBtn.GetComponent<Image> ().raycastTarget = false;
		whiteDotObj.SetActive (false);

		if (index > 8) return;
		sliderRect.DOAnchorPosX (sliderRect.anchoredPosition.x - 229, 1);
		containerRect.DOAnchorPosX (containerRect.anchoredPosition.x - 1280, 1).OnComplete (() => {
			lastBtn.GetComponent<Image> ().raycastTarget = true;
			nextBtn.GetComponent<Image> ().raycastTarget = true;
			whiteDotObj.SetActive (true);

		});
	}
	public void MoveLastContainer () {
		index--;
		lastBtn.gameObject.SetActive (index == 0 ? false : true);
		nextBtn.gameObject.SetActive (index == 8 ? false : true);
		lastBtn.GetComponent<Image> ().raycastTarget = false;
		nextBtn.GetComponent<Image> ().raycastTarget = false;
		whiteDotObj.SetActive (false);
		if (index < 0) return;
		sliderRect.DOAnchorPosX (sliderRect.anchoredPosition.x + 229, 1);
		containerRect.DOAnchorPosX (containerRect.anchoredPosition.x + 1280, 1).OnComplete (() => {
			lastBtn.GetComponent<Image> ().raycastTarget = true;
			nextBtn.GetComponent<Image> ().raycastTarget = true;
			whiteDotObj.SetActive (true);
		});
	}
	public void Close () {
		containerRect.anchoredPosition = containerStartPos;
		sliderRect.anchoredPosition = sliderRectStartPos;
		index = 0;
		lastBtn.gameObject.SetActive (false);
		whiteDotObj.SetActive (false);
		Vuforia.CameraDevice.Instance.Start ();
		Vuforia.CameraDevice.Instance.SetFocusMode (Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		this.gameObject.SetActive (false);
	
	}
}