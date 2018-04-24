using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConsoleLogger : MonoBehaviour
{
	[SerializeField] Text text;
	void Awake ()
	{
		Application.logMessageReceived += OnConsoleLog;
	}
	void OnConsoleLog (string logText, string stackTrace, LogType type)
	{
		if (string.IsNullOrEmpty (logText))
		{
			return;
		}
		text.text += logText + System.Environment.NewLine;
	}
}