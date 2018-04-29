using UnityEngine;

public class Bender : MonoBehaviour
{
	[SerializeField] float BendSpeed;
	SteamVR_Controller.Device device;
	uDesktopDuplication.Texture texture;
	bool IsGripping, IsPadTouching;
	void Start ()
	{
		device = GetComponent<InputDevice> ().device;
	}
	void Update ()
	{
		IsPadTouching = device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad);
		if (!IsGripping && device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger) || device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger))
		{
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
			{
				if (hit.collider.tag == "GripObject")
				{
					texture = hit.collider.GetComponent<uDesktopDuplication.Texture> ();
					IsGripping = true;
				}
			}
		}
		if (IsGripping && IsPadTouching)
		{
			var pos = device.GetAxis ();
			if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad) && Mathf.Abs (pos.x) > Mathf.Abs (pos.y))
			{
				texture.bend = (pos.x > 0);
			}
			else
			{
				texture.radius += pos.y * BendSpeed;
			}
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger))
		{
			IsGripping = false;
			texture = null;
		}
	}
}