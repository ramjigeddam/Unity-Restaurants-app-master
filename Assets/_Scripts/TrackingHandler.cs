﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityGLTF;

public class TrackingHandler : MonoBehaviour, ITrackableEventHandler
{
	#region PROTECTED_MEMBER_VARIABLES

	protected TrackableBehaviour mTrackableBehaviour;
	protected TrackableBehaviour.Status m_PreviousStatus;
	protected TrackableBehaviour.Status m_NewStatus;

	#endregion // PROTECTED_MEMBER_VARIABLES

	#region UNITY_MONOBEHAVIOUR_METHODS

	#region Public variables
	public GameObject downloadedModel;
	public List<GameObject> augmentedObj = new List<GameObject>();
	#endregion

	protected virtual void Start()
	{

		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
	}

	protected virtual void OnDestroy()
	{
		if (mTrackableBehaviour)
			mTrackableBehaviour.UnregisterTrackableEventHandler(this);
	}

	#endregion // UNITY_MONOBEHAVIOUR_METHODS

	#region PUBLIC_METHODS

	/// <summary>
	///     Implementation of the ITrackableEventHandler function called when the
	///     tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		m_PreviousStatus = previousStatus;
		m_NewStatus = newStatus;

		Debug.Log("Trackable " + mTrackableBehaviour.TrackableName +
				  " " + mTrackableBehaviour.CurrentStatus +
				  " -- " + mTrackableBehaviour.CurrentStatusInfo);

		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
				 newStatus == TrackableBehaviour.Status.NO_POSE)
		{
			OnTrackingLost();
		}
		else
		{
			// For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
			// Vuforia is starting, but tracking has not been lost or found yet
			// Call OnTrackingLost() to hide the augmentations
			OnTrackingLost();
		}
	}

	#endregion // PUBLIC_METHODS

	#region PROTECTED_METHODS

	protected virtual void OnTrackingFound()
	{
		if (downloadedModel != null)
			downloadedModel.SetActive(true);

		// Disable the all augmented Object

		for (int i = 0; i < augmentedObj.Count; i++)
		{
			augmentedObj[i].SetActive(false);
		}

		// Send info to ScrollViewHandler script.
		// Enable only single Object

		augmentedObj[GameObject.FindObjectOfType<ScrollViewHandler>().trackingInt].SetActive(true);

		if (mTrackableBehaviour)
		{
			var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
			var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
			var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

			// Enable rendering:
			foreach (var component in rendererComponents)
				component.enabled = true;

			// Enable colliders:
			foreach (var component in colliderComponents)
				component.enabled = true;

			// Enable canvas':
			foreach (var component in canvasComponents)
				component.enabled = true;
		}
	}


	protected virtual void OnTrackingLost()
	{
		if (downloadedModel != null)
			downloadedModel.SetActive(false);

		// Disable the all augmented Object

		for (int i = 0; i < augmentedObj.Count; i++)
		{
			augmentedObj[i].SetActive(false);
		}

		if (mTrackableBehaviour)
		{
			var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
			var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
			var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

			// Disable rendering:
			foreach (var component in rendererComponents)
				component.enabled = false;

			// Disable colliders:
			foreach (var component in colliderComponents)
				component.enabled = false;

			// Disable canvas':
			foreach (var component in canvasComponents)
				component.enabled = false;
		}
	}

	#endregion // PROTECTED_METHODS
}
