using UnityEngine;
using System.Collections;

public class TEEditing : MonoBehaviour {

	public GUISkin skin;
	public GUIStyle titleText;

	bool toolBoxActive = true;
	float toolBoxWidth;
	float toolBoxBottomWidth;

	bool toolBoxBottom;
	
	void Start () 
	{
	
	}
	

	void Update () 
	{
		if (toolBoxActive)
		{
			if (toolBoxBottom)
			{
				toolBoxBottomWidth = Mathf.Lerp(toolBoxBottomWidth, 0.8f, Time.deltaTime * 8);
			} else
			{
				toolBoxBottomWidth = Mathf.Lerp(toolBoxBottomWidth, 0.13f, Time.deltaTime * 8);
			}
			toolBoxWidth = Mathf.Lerp(toolBoxWidth, 0.1f, Time.deltaTime * 8);
		} else
		{
			toolBoxBottomWidth = Mathf.Lerp(toolBoxBottomWidth, 0, Time.deltaTime * 8);
			toolBoxWidth = Mathf.Lerp(toolBoxWidth, 0f, Time.deltaTime * 8);
		}
	}

	void OnGUI()
	{
		float SW = Screen.width;
		float SH = Screen.height;

		// Draw Toolbox
		GUI.skin = skin;


		// Bottom Panel
		
		GUI.Box(new Rect(0, SH * 0.87f, SW * toolBoxBottomWidth + 12, SH), "", skin.customStyles[0]);
		toolBoxBottom = GUI.Toggle(new Rect(SW * toolBoxBottomWidth, SH * 0.6f, SW * toolBoxBottomWidth + 12, SH), toolBoxBottom, "", skin.toggle);

		GUI.BeginGroup(new Rect(0,SH * 0.2f, SW * toolBoxBottomWidth, SH));

		GUI.Box(new Rect(0,SH * 0.68f, SW * 0.8f, SH), "");
		
		GUI.EndGroup();

		// Toolbox Tab
		GUI.Box(new Rect(0, 0, SW * toolBoxWidth + 12, SH), "", skin.customStyles[0]);

		toolBoxActive = GUI.Toggle(new Rect(SW * toolBoxWidth, 0, SW * toolBoxWidth + 12, SH), toolBoxActive, "", skin.toggle);
		GUI.BeginGroup(new Rect(0,0, SW * toolBoxWidth, SH));
	
		GUI.Box(new Rect(0,0, SW * 0.1f, SH), "");

		GUI.Label(new Rect(SW * 0.1f / 2 - 50, SH * 0.002f, 100, 100), "Toolbox", titleText);

		// Reset Camera Position
		if (GUI.Button(new Rect(SW * 0.1f / 2 - 50, SH * 0.05f, 100,30), "Bottom bar", skin.button))
		{

		}

		GUI.EndGroup();


	}
}
