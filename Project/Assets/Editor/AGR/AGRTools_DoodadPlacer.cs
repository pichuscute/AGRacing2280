using UnityEngine;
using UnityEditor;
using System.Collections;

public class AGRTools_DoodadPlacer : EditorWindow {


	public GameObject DoodadPrefab;
	public string DoodadName;

	public bool isPlacing;

	// Raycast
	Ray ray;
	RaycastHit hit;
	Vector3 hitLoc;
	Quaternion hitRot;

	[MenuItem("AGR2280Tools/Doodad Placer")]
	public static void ShowWindow()
	{
		// Make the window
		EditorWindow thisWindow = EditorWindow.GetWindow (typeof(AGRTools_DoodadPlacer));
		thisWindow.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 600);
		thisWindow.minSize = new Vector2(400,600);
		thisWindow.maxSize = new Vector2(400,600);
		
		// Check that Input Object Exists
		if (!GameObject.Find("EditorInput"))
		{
			GameObject newInput = new GameObject();
			newInput.name = "EditorInput";
			newInput.AddComponent<AGREditorInputObject>();
		}
	}
	
	void Update()
	{
		if (isPlacing)
		{
			Selection.activeObject = GameObject.Find("EditorInput");
		}

		// Update Raycast ray
		if (Camera.current != null)
		{
			ray = AGREditorInput.camera;
			// Make editor derived script for input
			if (AGREditorInput.input == KeyCode.P)
			{
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Track_Floor"))
					{
						hitLoc = hit.point;
						CreateNewPad();
					}
				}
			}
		}
	}

	void OnGUI()
	{
		// Title Group
		GUI.BeginGroup(new Rect(120, 10, 300, 100));
		GUILayout.Label("DOODAD PLACEMENT", EditorStyles.boldLabel);
		GUI.EndGroup();

		isPlacing = EditorGUILayout.BeginToggleGroup("Enabled", isPlacing);

		DoodadPrefab = (GameObject)EditorGUILayout.ObjectField("Doodad Prefab:",DoodadPrefab, typeof(GameObject), true);
		DoodadName = EditorGUILayout.TextField("Doodad Name", DoodadName);

		GUI.BeginGroup(new Rect(0, 20, 400, 600));

		EditorGUILayout.Separator();

		GUILayout.Label("This tool is the exact same as the pad placerment tool except");
		GUILayout.Label("For multipurpose and customizable use!");
		GUI.EndGroup();

		EditorGUILayout.EndToggleGroup();
	}

	void CreateNewPad()
	{
		GameObject newDoodad;
		newDoodad = Instantiate(DoodadPrefab) as GameObject;
		newDoodad.transform.position = new Vector3(hitLoc.x, hitLoc.y + 4, hitLoc.z);
		RaycastHit padHit;
		if (Physics.Raycast(newDoodad.transform.position, -Vector3.up, out padHit))
		{
			Debug.Log("Place success!");
			//newPad.transform.rotation = Quaternion.LookRotation(newPad.transform.forward, padHit.normal);
			newDoodad.transform.up = padHit.normal;
		}
		newDoodad.transform.position = hitLoc;
		newDoodad.name = DoodadName;
	}
}
