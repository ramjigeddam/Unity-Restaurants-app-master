using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotate : MonoBehaviour
{
	#region Private variables
	private float _sensitivity; // Control the rotation speed
	private Vector3 _mouseReference; // Store the mouse position
	private Vector3 _mouseOffset; // Mouse offset value
	private Vector3 _rotation; // Apply rotation
	private bool _isRotating;
	#endregion

	private void Start()
	{
		_sensitivity = 0.2f;
		_rotation = Vector3.zero;
	}

	private void Update()
	{
		if (_isRotating)
		{
			// offset
			_mouseOffset = (Input.mousePosition - _mouseReference);

			// apply rotation
			_rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;

			// rotate
			transform.Rotate(_rotation);

			// store mouse
			_mouseReference = Input.mousePosition;
		}
	}

	private void OnMouseDown()
	{
		// rotating flag
		_isRotating = true;

		// store mouse
		_mouseReference = Input.mousePosition;
	}

	private void OnMouseUp()
	{
		// rotating flag
		_isRotating = false;
	}

}
