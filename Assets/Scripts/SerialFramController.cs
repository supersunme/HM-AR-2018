using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SerialFramController : MonoBehaviour {

	public Sprite[] Frames { get { return frames; } set { frames = value; } }

	[SerializeField]
	private Sprite[] frames = null;
	[SerializeField] private float frameRatate = 20;
	public bool IgnoreTimeScale { get { return ignoreTimeScale; } set { ignoreTimeScale = value; } }

	[SerializeField]
	private bool ignoreTimeScale = true;
	[SerializeField]
	private bool loop;
	public bool Loop { get { return loop; } set { loop = value; } }

	[SerializeField]
	private AnimationCurve curve = new AnimationCurve (new Keyframe (0, 1, 0, 0), new Keyframe (1, 1, 0, 0));
	public event Action FinishEvent;
	private SpriteRenderer spriteRenderer;
	private Image image;

	private int currentFramIndex = 0;
	private float timer = 0.0f;
	private float currentFramerate = 20.0f;

	void Reset () {
		currentFramIndex = frameRatate < 0 ? frames.Length - 1 : 0;
	}
	public void Play () {
		this.enabled = true;
	}
	public void Pause () {
		this.enabled = false;
	}
	public void Stop () {
		Pause ();
		Reset ();
	}
	void Start () {
		image = this.GetComponent<Image> ();
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
#if UNITY_EDITOR
		if (image == null && spriteRenderer == null) {
			Debug.LogWarning ("Image或者Spriterender组件为空");
		}
#endif
	}
	void Update () {
		if (frames == null || frames.Length == 0) {
			this.enabled = false;
			return;
		} else {
			float curveValue = curve.Evaluate ((float) currentFramIndex / frames.Length);
			float curveFramerate = curveValue * frameRatate;
			if (curveFramerate != 0) {
				float time = ignoreTimeScale?Time.unscaledTime : Time.time;
				//Debug.Log (time);
				float interval = Mathf.Abs (1 / curveFramerate);
				print (time + "time");
				print (timer + "timer");
				if (time - timer > interval) {
				//	print (time - timer);
					DoUpdate ();

				}
			}
		}
	}
	void DoUpdate () {
		//计算新的索引
		int nextIndex = currentFramIndex + (int) Mathf.Sign (currentFramerate);
		//	Debug.Log(nextIndex);
		//索引越界，表示已经到结束帧
		if (nextIndex < 0 || nextIndex >= frames.Length) {
			//广播事件
			if (FinishEvent != null) {
				FinishEvent ();
			}
			//非循环模式，禁用脚本
			if (loop == false) {
				currentFramIndex = Mathf.Clamp (currentFramIndex, 0, frames.Length - 1);
				this.enabled = false;
				return;
			}
		}
		//钳制索引
		currentFramIndex = nextIndex % frames.Length;
		//更新图片
		if (image != null) {
			image.sprite = frames[currentFramIndex];
		} else if (spriteRenderer != null) {
			spriteRenderer.sprite = frames[currentFramIndex];
		}
		//设置计时器为当前时间
		timer = ignoreTimeScale ? Time.unscaledTime : Time.time;
	}
}