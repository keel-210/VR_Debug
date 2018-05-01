using UnityEngine;

public class Scaler : MonoBehaviour
{
	[SerializeField] float ScaleSpeed;
	LineRenderer line;
	SteamVR_Controller.Device device;
	bool IsGripping, IsPadTouching;
	Transform GrippingTransform;
	Modes modes;
	void Update ()
	{
		if (device == null)
		{
			device = GetComponent<InputDevice> ().device;
			line = GetComponent<InputDevice> ().line;

		}
		IsPadTouching = device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad);
		if (!IsGripping && device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger) || device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger))
		{
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
			{
				if (hit.collider.tag == "GripObject")
				{
					GrippingTransform = hit.collider.transform;
					IsGripping = true;
					line.material = GetComponent<InputDevice> ().RedMaterial;
				}
			}
		}
		if (IsGripping && IsPadTouching)
		{
			var pos = device.GetAxis ();
			switch (modes)
			{
				case Modes.X:
					GrippingTransform.localScale += Vector3.right * ScaleSpeed;
					break;
				case Modes.Y:
					GrippingTransform.localScale += Vector3.up * ScaleSpeed;
					break;
				case Modes.Z:
					GrippingTransform.localScale += Vector3.forward * ScaleSpeed;
					break;
				case Modes.All:
					GrippingTransform.localScale += Vector3.one * ScaleSpeed;
					break;
			}
			if (Mathf.Abs (pos.x) > Mathf.Abs (pos.y))
			{
				if (pos.x > 0) //Right
				{
					modes = Modes.Z;
				}
				else // Left
				{
					modes = Modes.X;
				}
			}
			else
			{
				if (pos.y > 0) //Up
				{
					modes = Modes.Y;
				}
				else // Down
				{
					modes = Modes.All;
				}
			}
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger))
		{
			IsGripping = false;
			GrippingTransform = null;
			line.material = GetComponent<InputDevice> ().GreenMaterial;
		}
	}
	enum Modes
	{
		X,
		Y,
		Z,
		All
	}
}