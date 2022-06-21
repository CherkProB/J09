using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private float sensitivity = 3;
	[SerializeField] private float maxY = 80;
	[SerializeField] private float zoomSensitivity = 0.25f;
	[SerializeField] private float zoomMax = 50;
	[SerializeField] private float zoomMin = 1;
	private Vector3 offset = Vector3.back;
	private float X;
	private float Y;

	private Vector3 sceneCenter;

	private void Start()
	{
		maxY = Mathf.Abs(maxY);
		if (maxY > 90) maxY = 90;
	}

	private void Update()
	{
		if (true)//hub.GetOnScene())
		{
			//Отслеживание движения мыши
			if (Input.GetMouseButton(2))
			{
				X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
				Y += Input.GetAxis("Mouse Y") * sensitivity;
				Y = Mathf.Clamp(Y, -maxY, 0);
			}

			//Отслеживание скролла мыши
			if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoomSensitivity;
			else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoomSensitivity;
			offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

			//Изменение позиции камеры
			transform.localEulerAngles = new Vector3(-Y, X, 0);
			transform.position = transform.localRotation * offset + sceneCenter;
		}
	}

	/// <summary>
	/// Установка центра камеры, вокруг которого она будет вращаться
	/// </summary>
	/// <param name="newCenter">Центр камеры, вокруг которого она будет вращаться</param>
	public void SetSceneCenter(Vector3 newCenter)
	{
		sceneCenter = newCenter;
		transform.position = sceneCenter + offset;
	}
}
