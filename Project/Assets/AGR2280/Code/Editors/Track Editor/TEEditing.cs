using UnityEngine;
using System.Collections;

public class TEEditing : MonoBehaviour {

	public GUISkin skin;
	public GUIStyle titleText;

	bool toolBoxActive = true;
	float toolBoxWidth;
	float toolBoxBottomWidth;

	bool toolBoxBottom;

	public Vector2[] baseTrackLeftShape;
	public Vector2[] baseTrackRightShape;
	public bool drawTrackShape;

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
		if (GUI.Button(new Rect(SW * 0.1f / 2 - 50, SH * 0.1f, 100,30), "Load Track", skin.button))
		{

		}

		if (GUI.Button(new Rect(SW * 0.1f / 2 - 50, SH * 0.2f, 100,30), "Save Track", skin.button))
		{
			
		}

		GUI.EndGroup();


	}

	void OnDrawGizmosSelected() 
	{
		if (drawTrackShape)
		{
			for (int i = 0; i < baseTrackLeftShape.Length; i++)
			{
				Gizmos.color = Color.blue;

				Vector3 drawPos = new Vector3(-baseTrackLeftShape[i].x, baseTrackLeftShape[i].y, 0);

				Vector3 lastDraw;
				if (i > 0)
				{
					lastDraw = new Vector3(-baseTrackLeftShape[i - 1].x, baseTrackLeftShape[i - 1].y, 0);
					Gizmos.DrawLine(lastDraw, drawPos);
				}


				Gizmos.DrawLine(new Vector3(0,0,0), new Vector3(-baseTrackLeftShape[0].x, baseTrackLeftShape[0].y, 0));
				Gizmos.DrawSphere(new Vector3(0,0,0), 1);

				Gizmos.DrawSphere(drawPos, 1);
			}
			
			for (int i = 0; i < baseTrackRightShape.Length; i++)
			{
				Gizmos.color = Color.blue;
				
				Vector3 drawPos = new Vector3(baseTrackRightShape[i].x, baseTrackRightShape[i].y, 0);
				
				Vector3 lastDraw;
				if (i > 0)
				{
					lastDraw = new Vector3(baseTrackRightShape[i - 1].x, baseTrackRightShape[i - 1].y, 0);
					Gizmos.DrawLine(lastDraw, drawPos);
				}
				
				
				Gizmos.DrawLine(new Vector3(0,0,0), new Vector3(baseTrackRightShape[0].x, baseTrackRightShape[0].y, 0));
				Gizmos.DrawSphere(new Vector3(0,0,0), 1);
				
				Gizmos.DrawSphere(drawPos, 1);
			}
		}
	}
}
