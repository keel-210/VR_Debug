using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gripper : MonoBehaviour
{
	[SerializeField] float MoveSpeed;
	SteamVR_Controller.Device device;
	float ObjectDistance;
	Vector2 PadPosition;
	bool IsGripping, IsPadTouching;
	GameObject GrippingObject;
	Rigidbody GrippingRigidbody;
	void Start ()
	{
		device = GetComponent<InputDevice> ().device;
	}
	void FixedUodate ()
	{
		if (IsGripping)
		{
			GrippingRigidbody.position = transform.position + transform.forward * ObjectDistance;
			if (IsPadTouching)
			{
				ObjectDistance += PadPosition.y * MoveSpeed;
			}
		}
	}
	void Update ()
	{
		PadPosition = device.GetAxis ();
		IsPadTouching = device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad);
		if (!IsGripping && device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger) || device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger))
		{
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
			{
				if (hit.collider.tag == "GripObject")
				{
					GrippingObject = hit.collider.gameObject;
					GrippingRigidbody = hit.collider.GetComponent<Rigidbody> ();
					ObjectDistance = (hit.collider.transform.position - transform.position).magnitude;
					IsGripping = true;
				}
			}
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger))
		{
			IsGripping = false;
			GrippingObject = null;
			GrippingRigidbody = null;
		}
	}
}