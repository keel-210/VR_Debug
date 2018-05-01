using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gripper : MonoBehaviour
{
	[SerializeField] float MoveSpeed;
	LineRenderer line;
	SteamVR_Controller.Device device;
	float ObjectDistance;
	Vector2 PadPosition;
	bool IsGripping, IsPadTouching;
	Rigidbody GrippingRigidbody;
	void FixedUpdate ()
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
		if (device == null)
		{
			device = GetComponent<InputDevice> ().device;
			line = GetComponent<InputDevice> ().line;

		}
		PadPosition = device.GetAxis ();
		IsPadTouching = device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad);
		line.SetPosition (0, Vector3.zero);
		line.SetPosition (1, Vector3.forward * 10);
		if (!IsGripping && device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger) || device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger))
		{
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
			{
				if (hit.collider.tag == "GripObject")
				{
					GrippingRigidbody = hit.collider.GetComponent<Rigidbody> ();
					ObjectDistance = (hit.point - transform.position).magnitude;
					line.material = GetComponent<InputDevice> ().RedMaterial;
					IsGripping = true;
				}
			}
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger))
		{
			IsGripping = false;
			GrippingRigidbody = null;
			line.material = GetComponent<InputDevice> ().GreenMaterial;
		}
	}
}