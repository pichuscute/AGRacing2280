using UnityEngine;
using UnityEditor;
using System.Collections;

public class AGRTools_PadPlacer : EditorWindow {
	
	public enum Pads { Speed, Weapon, Start};
	public Pads currentPad = Pads.Speed;

	public GameObject SpeedPrefab;
	public GameObject WeaponPrefab;
	public GameObject StartPrefab;

	public bool isPlacing;

	// Raycast
	Ray ray;
	RaycastHit hit;
	Vector3 hitLoc;
	Quaternion hitRot;

	[MenuItem("AGR2280Tools/Pad Placer")]
	public static void ShowWindow()
	{
		// Make the window
		EditorWindow thisWindow = EditorWindow.GetWindow (typeof(AGRTools_PadPlacer));
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
		GUILayout.Label("TRACK PAD PLACEMENT", EditorStyles.boldLabel);
		GUI.EndGroup();

		isPlacing = EditorGUILayout.BeginToggleGroup("Enabled", isPlacing);

		GUI.BeginGroup(new Rect(0, 20, 400, 600));

		currentPad = (Pads)EditorGUILayout.EnumPopup("Place:", currentPad);
		SpeedPrefab = (GameObject)EditorGUILayout.ObjectField("Speed Prefab:",SpeedPrefab, typeof(GameObject), true);
		WeaponPrefab = (GameObject)EditorGUILayout.ObjectField("Weapon Prefab:",WeaponPrefab, typeof(GameObject), true);
		StartPrefab = (GameObject)EditorGUILayout.ObjectField("Start Prefab:",StartPrefab, typeof(GameObject), true);

		EditorGUILayout.Separator();

		GUILayout.Label("This tool is not for accurate placement.");
		GUILayout.Label("You use this tool to quickly place pads where you want them,");
		GUILayout.Label("it will rotate the pad to the track normal but you will have");
		GUILayout.Label("to apply the final rotations yourself (I reccomend you have");
		GUILayout.Label("the handle space set to local when doing so).");
		GUI.EndGroup();

		EditorGUILayout.EndToggleGroup();
	}

	void CreateNewPad()
	{
		GameObject newPad;
		if (currentPad == Pads.Speed)
		{
			newPad = Instantiate(SpeedPrefab) as GameObject;
			newPad.transform.position = new Vector3(hitLoc.x, hitLoc.y + 4, hitLoc.z);
			RaycastHit padHit;
			if (Physics.Raycast(newPad.transform.position, -Vector3.up, out padHit))
			{
				Debug.Log("Place success!");
				//newPad.transform.rotation = Quaternion.LookRotation(newPad.transform.forward, padHit.normal);
				newPad.transform.up = padHit.normal;
			}
			newPad.transform.position = hitLoc;
			newPad.name = "Speed Pad";
		}

		if (currentPad == Pads.Weapon)
		{
			newPad = Instantiate(WeaponPrefab) as GameObject;
			newPad.transform.position = new Vector3(hitLoc.x, hitLoc.y + 4, hitLoc.z);
			RaycastHit padHit;
			if (Physics.Raycast(newPad.transform.position, -Vector3.up, out padHit))
			{
				Debug.Log("Place success!");
				//newPad.transform.rotation = Quaternion.LookRotation(newPad.transform.forward, padHit.normal);
				newPad.transform.up = padHit.normal;
			}
			newPad.transform.position = hitLoc;
			newPad.name = "Weapon Pad";
		}

		if (currentPad == Pads.Start)
		{
			newPad = Instantiate(StartPrefab) as GameObject;
			newPad.transform.position = new Vector3(hitLoc.x, hitLoc.y + 4, hitLoc.z);
			RaycastHit padHit;
			if (Physics.Raycast(newPad.transform.position, -Vector3.up, out padHit))
			{
				Debug.Log("Place success!");
				//newPad.transform.rotation = Quaternion.LookRotation(newPad.transform.forward, padHit.normal);
				newPad.transform.up = padHit.normal;
			}
			newPad.transform.position = hitLoc;
			newPad.name = "StartPad_Unassigned";
		}
	}
}
