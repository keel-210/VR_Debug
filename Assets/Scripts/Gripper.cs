using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gripper : MonoBehaviour
{
	[SerializeField] float MoveSpeed;
    [SerializeField] LineRenderer line;
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
	void FixedUpdate ()
	{
		if (IsGripping)
		{
            line.material.color = Color.red;
            Debug.Log("Gripping"+GrippingRigidbody);
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
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1,  Vector3.forward * 5);
        if (!IsGripping && device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger) || device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger))
		{
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
			{
				if (hit.collider.tag == "GripObject")
				{
                    Debug.Log("gripped");
					GrippingObject = hit.collider.gameObject;
					GrippingRigidbody = hit.collider.GetComponent<Rigidbody> ();
					ObjectDistance = (hit.collider.transform.position - transform.position).magnitude;
					IsGripping = true;
				}
			}
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger))
		{
            Debug.Log("Released");
			IsGripping = false;
			GrippingObject = null;
			GrippingRigidbody = null;
            line.material.color = Color.white;
        }
    }
}