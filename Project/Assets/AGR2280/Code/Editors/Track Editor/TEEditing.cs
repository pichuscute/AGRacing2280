using UnityEngine;
using System.Collections;

public class TEEditing : MonoBehaviour {

	public GUISkin skin;
	public GUIStyle titleText;

	bool toolBoxActive = true;
	float toolBoxWidth;
	
	void Start () 
	{
	
	}
	

	void Update () 
	{
		if (toolBoxActive)
		{
			toolBoxWidth = Mathf.Lerp(toolBoxWidth, 0.1f, Time.deltaTime * 8);
		} else
		{
			toolBoxWidth = Mathf.Lerp(toolBoxWidth, 0f, Time.deltaTime * 8);
		}
	}

	void OnGUI()
	{
		float SW = Screen.width;
		float SH = Screen.height;

		// Draw Toolbox
		GUI.skin = skin;

		// Toolbox Tab
		GUI.Box(new Rect(0, 0, SW * toolBoxWidth + 24, SH), "", skin.customStyles[0]);

		toolBoxActive = GUI.Toggle(new Rect(SW * toolBoxWidth, 0, SW * toolBoxWidth + 24, SH), toolBoxActive, "", skin.toggle);
		GUI.BeginGroup(new Rect(0,0, SW * toolBoxWidth, SH));
	
		GUI.Box(new Rect(0,0, SW * 0.1f, SH), "");

		GUI.Label(new Rect(SW * 0.1f / 2 - 50, SH * 0.002f, 100, 100), "Toolbox", titleText);

		// Reset Camera Position
		if (GUI.Button(new Rect(SW * 0.1f / 2 - 50, SH * 0.05f, 100,30), "Reset Camera", skin.button))
		{
			transform.position = new Vector3(0,15,0);
		}

		GUI.EndGroup();
	}
}
