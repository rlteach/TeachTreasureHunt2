using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {


	private	RectTransform	mImageRect;

	public	int	Health	{
		set {
			mImageRect.sizeDelta = new Vector2 (Mathf.Clamp (value, 0f, 100f),mImageRect.sizeDelta.y);
		}
	}

	void Start () {
		RectTransform[] tRT = GetComponentsInChildren<RectTransform> ();
		if (tRT.Length == 3) {
			mImageRect = tRT[2];
			Health = 0;
		} else {
			Debug.LogError ("HealthBar needs 2 RectTransforms with Images");
		}
	}

	void	LateUpdate() {
		transform.LookAt (Camera.main.transform);
	}

}
