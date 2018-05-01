using UnityEngine;

public class InputDevice : MonoBehaviour
{
	SteamVR_TrackedObject trackedObject;
	public SteamVR_Controller.Device device;
	public LineRenderer line;
	public Material GreenMaterial, RedMaterial;
	void Start ()
	{
		trackedObject = GetComponent<SteamVR_TrackedObject> ();
		device = SteamVR_Controller.Input ((int) trackedObject.index);
	}
}