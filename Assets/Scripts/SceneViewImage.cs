using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class SceneViewImage : MonoBehaviour
{
	RenderTexture renderTexture;
	[SerializeField] RawImage image;
	void Update ()
	{
		Camera cam = SceneView.GetAllSceneCameras () [0];
		//SceneView.GetAllSceneCameras () [0].targetTexture = renderTexture;
		Debug.Log (cam.activeTexture);
		if (cam.activeTexture)
		{
			renderTexture = cam.activeTexture;
			Texture2D tex = new Texture2D (renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
			RenderTexture.active = renderTexture;
			tex.ReadPixels (new Rect (0, 0, renderTexture.width, renderTexture.height), 0, 0);
			tex.Apply ();
			image.texture = tex;
		}
	}
}