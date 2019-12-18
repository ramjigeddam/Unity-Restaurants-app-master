using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewHandler : MonoBehaviour
{
	#region Private variables
	[SerializeField]
	private RectTransform panel; //  Hold the ScrollPanel
	[SerializeField]
	private RectTransform centerRect; // Hold the Center Recttransform
	[SerializeField]
	private Button[] scrollBtns; // Hold the all the buttons

	private float[] btn_Distance; // Calculate the distance with center
	private float[] btn_Distance_Reposition;
	private bool dragScrollRect; // To control the dragging
	private int btnDistance; // Holding the distance between the buttons
	private int minBtnDistance; // Hold the shorted distance between the buttons and center
	int btnLenght;
	private bool isOnce = false;
	#endregion

	#region Public variables
	public int trackingInt;
	#endregion

	private void Start()
	{
		btnLenght = scrollBtns.Length;
		btn_Distance = new float[btnLenght];
		btn_Distance_Reposition = new float[btnLenght];
		btnDistance = (int)Mathf.Abs(scrollBtns[1].GetComponent<RectTransform>().anchoredPosition.x - scrollBtns[0].GetComponent<RectTransform>().anchoredPosition.x);

	}

	private void Update()
	{
		for (int i = 0; i < scrollBtns.Length; i++)
		{
			btn_Distance_Reposition[i] = centerRect.GetComponent<RectTransform>().position.x - scrollBtns[i].GetComponent<RectTransform>().position.x;
			btn_Distance[i] = (float)Mathf.Abs(centerRect.transform.position.x - scrollBtns[i].transform.position.x);

			if (btn_Distance_Reposition[i] > Screen.width + 50)
			{
				float curX = scrollBtns[i].GetComponent<RectTransform>().anchoredPosition.x;
				float curY = scrollBtns[i].GetComponent<RectTransform>().anchoredPosition.y;
				Vector2 newAnchroedPos = new Vector2(curX + (btnLenght * btnDistance), curY);
				scrollBtns[i].GetComponent<RectTransform>().anchoredPosition = newAnchroedPos;
			}

			if (btn_Distance_Reposition[i] < -Screen.width + 50)
			{
				float curX = scrollBtns[i].GetComponent<RectTransform>().anchoredPosition.x;
				float curY = scrollBtns[i].GetComponent<RectTransform>().anchoredPosition.y;
				Vector2 newAnchroedPos = new Vector2(curX - (btnLenght * btnDistance), curY);
				scrollBtns[i].GetComponent<RectTransform>().anchoredPosition = newAnchroedPos;
			}
		}

		float minDistance = Mathf.Min(btn_Distance);

		for (int j = 0; j < scrollBtns.Length; j++)
		{
			if (minDistance == btn_Distance[j])
			{
				minBtnDistance = j;
			}
		}

		if (!dragScrollRect)
		{
			LerpToBtn(-scrollBtns[minBtnDistance].GetComponent<RectTransform>().anchoredPosition.x);
		}

	}

	void LerpToBtn(float position)
	{
		float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 30f);

		if (Mathf.Abs(newX) >= Mathf.Abs(position) - 1 && Mathf.Abs(newX) <= Mathf.Abs(position) + 1 && !isOnce)
		{
			isOnce = true;
			trackingInt = minBtnDistance;

			for (int i = 0; i < GameObject.FindObjectOfType<TrackingHandler>().augmentedObj.Count; i++)
			{
				GameObject.FindObjectOfType<TrackingHandler>().augmentedObj[i].SetActive(false);
			}

			GameObject.FindObjectOfType<TrackingHandler>().augmentedObj[minBtnDistance].SetActive(true);
			GameObject.FindObjectOfType<TrackingHandler>().augmentedObj[minBtnDistance].transform.localPosition = Vector3.zero;
			GameObject.FindObjectOfType<TrackingHandler>().augmentedObj[minBtnDistance].transform.localRotation = Quaternion.Euler(0, 0, 0);
			iTween.ScaleTo(scrollBtns[minBtnDistance].gameObject, iTween.Hash("scale", new Vector3(0.9f, 0.9f, 0.9f), "time", 0.1f, "dealy", 0.05f, "easetype", iTween.EaseType.linear));
		}


		Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
		panel.anchoredPosition = newPosition;
	}

	public void StartDragging()
	{
		for (int i = 0; i < scrollBtns.Length; i++)
		{
			iTween.ScaleTo(scrollBtns[minBtnDistance].gameObject, iTween.Hash("scale", new Vector3(0.7f, 0.7f, 0.7f), "time", 0.1f, "dealy", 0.01f, "easetype", iTween.EaseType.linear));
		}
		isOnce = false;
		dragScrollRect = true;
	}
	public void EndDragging()
	{
		dragScrollRect = false;
	}
}
