using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripObjSetter : MonoBehaviour
{
	void Update ()
	{
		uDesktopDuplication.Texture[] list = FindObjectsOfType<uDesktopDuplication.Texture> ();
		foreach (uDesktopDuplication.Texture t in list)
		{
			GameObject obj = t.gameObject;
			obj.AddComponent (typeof (MeshCollider));
			Rigidbody r = (Rigidbody) obj.AddComponent (typeof (Rigidbody));
			r.useGravity = false;
			r.isKinematic = true;
			obj.tag = "GripObject";
		}
		Destroy (this);
	}
}