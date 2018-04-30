using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uDesktopDuplication;

public class GripObjSetter : MonoBehaviour
{
	void Update ()
	{
		uDesktopDuplication.Texture[] list = FindObjectsOfType<uDesktopDuplication.Texture> ();
		foreach (uDesktopDuplication.Texture t in list)
		{
			GameObject obj = t.gameObject;
			MeshCollider c = (MeshCollider)obj.AddComponent (typeof (MeshCollider));
            c.convex = true;
			Rigidbody r = (Rigidbody) obj.AddComponent (typeof (Rigidbody));
			r.useGravity = false;
			obj.tag = "GripObject";
		}
		Destroy (this);
	}
}