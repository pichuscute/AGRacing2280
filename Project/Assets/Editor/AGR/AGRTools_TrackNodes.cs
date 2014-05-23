using UnityEditor;
using UnityEngine;
using System.Collections;
public class AGRTools_TrackNodes : EditorWindow {

    // Variables

    public enum editingMode
    {
        spline,
        nodes,
        nav
    }
    public editingMode eM = editingMode.spline;
    public bool isEditing;

    // Node Tools
    public int nodeCount;
    public int selectedNode;
    public int nodeRotationIncrement;
    public Quaternion lastNodeRot;

    // Materials
    public Material nodeMat;

	// Raycast
	Ray ray;
	RaycastHit hit;
	Vector3 hitLoc;

    public enum rotAxis
    {
        x,
        y,
        z
    }
    public rotAxis rotationAxis;

    // Prefabs
    public Object NodeArrow;

    // Create the tool menu
    [MenuItem("AGR2280Tools/Node Editor")]
	public static void ShowWindow()
	{
        // Make the window
        EditorWindow thisWindow = EditorWindow.GetWindow (typeof(AGRTools_TrackNodes));
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
    
	void Update () 
    {
        // Stop Selection if we are editing the spline
        if (eM == editingMode.spline && isEditing)
        {
            Selection.activeObject = GameObject.Find("EditorInput");

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
							CreateNewNode();
						}
					}
				}
			}
	            // Reset Nodes
	            if (AGREditorInput.input == KeyCode.R && nodeCount > 0)
	            {
	                for (int i = 0; i < nodeCount + 1; i++)
	                {
	                    GameObject.DestroyImmediate(GameObject.Find("RaceGate_" + i));
	                }
	                nodeCount = 0;
	            }

				// Delete Last Node
				if (AGREditorInput.input == KeyCode.L)
				{
					if (nodeCount > 0)
					{
						GameObject.DestroyImmediate(GameObject.Find("RaceGate_" + nodeCount));
						nodeCount --;
					}
				}

				// Auto Centre Node
				if (AGREditorInput.input == KeyCode.B)
				{
					GameObject nodeToCentre = GameObject.Find("RaceGate_" + nodeCount);

					RaycastHit castHit;
					Vector3 direction = nodeToCentre.transform.TransformDirection(Vector3.right);
					if (Physics.Raycast(nodeToCentre.transform.position, direction, out castHit))
					{
						nodeToCentre.transform.position = new Vector3(castHit.point.x, nodeToCentre.transform.position.y, castHit.point.z);
					}

                    if (Physics.Raycast(nodeToCentre.transform.position, -direction, out castHit))
					{
						nodeToCentre.transform.position = ((castHit.point - nodeToCentre.transform.position) * 0.5f) + nodeToCentre.transform.position;
					}
				}

            // Auto Rotate Node
            if (AGREditorInput.input == KeyCode.G)
            {
                GameObject nodeToCentre = GameObject.Find("RaceGate_" + nodeCount);
                
                RaycastHit castHit;
                Quaternion firstRot = Quaternion.identity;
                if (Physics.Raycast(nodeToCentre.transform.position, -Vector3.up, out castHit))
                {
                    Quaternion wantedRot = Quaternion.LookRotation(Vector3.Cross(nodeToCentre.transform.forward, castHit.normal), castHit.normal);
                    nodeToCentre.transform.rotation = wantedRot;
                }
               
            }

            // Manual Rotation

            if (AGREditorInput.input == KeyCode.M)
            {
                GameObject node = GameObject.Find ("RaceGate_" + nodeCount);
                node.transform.rotation = lastNodeRot;

            }

            if (AGREditorInput.input == KeyCode.N)
            {
                GameObject node = GameObject.Find ("RaceGate_" + nodeCount);
                node.transform.rotation = Quaternion.Euler (0,0,0);
            }

            if (AGREditorInput.input == KeyCode.Comma)
            {
                GameObject node = GameObject.Find ("RaceGate_" + (nodeCount - 1));
                node.transform.Rotate(-Vector3.up * nodeRotationIncrement);
            }

            if (AGREditorInput.input == KeyCode.Period)
            {
                GameObject node = GameObject.Find ("RaceGate_" + (nodeCount - 1));
                node.transform.Rotate(Vector3.up * nodeRotationIncrement);
            }

			if (AGREditorInput.input == KeyCode.Slash)
			{
				GameObject node = GameObject.Find ("RaceGate_" + (nodeCount - 1));
				GameObject node2 = GameObject.Find ("RaceGate_" + (nodeCount - 2));
				node.transform.rotation = node2.transform.rotation;
			}

            // Quick Switch Rotation
            if (AGREditorInput.input == KeyCode.Semicolon)
            {
                rotationAxis = rotAxis.x;
            }

            if (AGREditorInput.input == KeyCode.At)
            {
                rotationAxis = rotAxis.y;
            }

            if (AGREditorInput.input == KeyCode.Hash)
            {
                rotationAxis = rotAxis.z;
            }
        }
	}

	void OnGUI()
	{
        // Title Group
        GUI.BeginGroup(new Rect(120, 10, 300, 100));
    	   GUILayout.Label("TRACK NODE CREATION", EditorStyles.boldLabel);
        GUI.EndGroup();

        // Editing Group#
        GUI.BeginGroup(new Rect(0, 20, 400, 600));
        //-
        isEditing = EditorGUILayout.BeginToggleGroup("Node Editing", isEditing);

        eM = (editingMode)EditorGUILayout.EnumPopup("Editing Mode:", eM);
        

        // Spline editing mode
        if (eM == editingMode.spline)
        {
            GUILayout.Label("------------------------------------------------------------------------------");
            GUILayout.Label("Object selection is locked, you can only select nodes!", EditorStyles.boldLabel);
            GUILayout.Label("Shortcuts (Use while holding C):");
            GUILayout.Label("L - delete current node");
            GUILayout.Label("G - auto rotate current node");
            GUILayout.Label("< - decrease rotation");
            GUILayout.Label("> - increase rotation");
            GUILayout.Label("M - duplicate last node rotation");
            GUILayout.Label("N - reset node rotation");
            GUILayout.Label("(;,',#) - switch rotation axis");
            GUILayout.Label("B - auto centre node");
            GUILayout.Label("R - delete all nodes and reset node count");
            GUILayout.Label("------------------------------------------------------------------------------");
            GUILayout.Label("Hold P and Left click to place nodes!", EditorStyles.boldLabel);
            GUILayout.Label("Node Count: " + nodeCount);

            selectedNode = EditorGUILayout.IntField("Select Node by ID:", selectedNode); 
			nodeRotationIncrement = EditorGUILayout.IntField("Rotation Increment:", nodeRotationIncrement); 
          
			GUILayout.Label("------------------------------------------------------------------------------");
			GUILayout.Label("Information:", EditorStyles.boldLabel);
			GUILayout.Label("* Nodes will only be placed if the surface you click on is part");
			GUILayout.Label("  of the track floor layer!");
			GUILayout.Label("* The auto centre node feature will look for walls in the track");
			GUILayout.Label("  wall layer, if you do not have this setup then it will not work!");
			GUILayout.Label("* When you place a node it will check for the walls of the track to");
			GUILayout.Label("  setup it's size, if this ever fails select the node and edit the");
			GUILayout.Label("  positions manually in the node editing mode");
        }
        EditorGUILayout.EndToggleGroup();
        //-
        GUI.EndGroup();
	}

	void CreateNewNode()
	{
        // Set last node rot
        if (nodeCount > 0)
        {
            //lastNodeRot = GameObject.Find("RaceGate_" + nodeCount).transform.rotation;
        }

		// Check for node counter
		if (!GameObject.Find ("NodeCount")) 
		{
			// Create if not found
			GameObject nodeCounter = new GameObject();
			nodeCounter.AddComponent<Ed_NodeCount>();
			nodeCounter.GetComponent<Ed_NodeCount>().thisSceneNodeCount = nodeCount;
			nodeCounter.name = "NodeCount";
		} else 
		{
			GameObject.Find("NodeCount").GetComponent<Ed_NodeCount>().thisSceneNodeCount = nodeCount;
		}
		
		//Create Node

		// Load Materials
		nodeMat = Resources.Load("!Important!/Editor/Nodes/NodeMaterial") as Material;
		NodeArrow = Resources.Load("AGREditing/NodeArrow") as Object;

		// Create Node
		GameObject newNode = GameObject.CreatePrimitive(PrimitiveType.Cube);
		newNode.transform.position = Vector3.zero;
		newNode.transform.position = new Vector3(hitLoc.x, hitLoc.y + (newNode.transform.localScale.y / 2) + 5.5f, hitLoc.z);
		newNode.name = "RaceGate_" + nodeCount;
		newNode.renderer.material = nodeMat;
		newNode.AddComponent<NodeID>();
		newNode.AddComponent<RaceGateRender>();
		newNode.AddComponent<NodeInformation>();
		newNode.GetComponent<NodeInformation>().trackWidth = 85;
		newNode.GetComponent<NodeID>().thisNodeID = nodeCount;
		newNode.transform.localScale = new Vector3(85, 50, 3);

		newNode.GetComponent<BoxCollider>().isTrigger = true;
		newNode.layer = LayerMask.NameToLayer("Ignore Raycast");
		newNode.tag = "Gate";

		GameObject nodeArrow = Instantiate(NodeArrow) as GameObject;
		nodeArrow.transform.position = Vector3.zero;
		nodeArrow.transform.parent = newNode.transform;
		nodeArrow.transform.localPosition = new Vector3(0, 0, 0);
		nodeArrow.transform.localRotation = Quaternion.Euler(0, 90, 0);
		nodeArrow.transform.localScale = new Vector3(0.39f, 0.039f, 0.024f);
		nodeArrow.AddComponent<RaceGateRender>();

		// Create helper locations
		Material aiPoint = Resources.Load("AGREditing/Materials/GateNode") as Material;
		GameObject aiHelper = GameObject.CreatePrimitive(PrimitiveType.Cube);
		aiHelper.AddComponent<RaceGateRender>();
		aiHelper.transform.position = newNode.transform.position;
		aiHelper.transform.parent = newNode.transform;
		aiHelper.name = "thisGateAIHelper";
		aiHelper.renderer.material = aiPoint;
		aiHelper.GetComponent<BoxCollider>().enabled = false;

		Material APPoint = Resources.Load("AGREditing/Materials/AGRArrow") as Material;
		GameObject APHelper = GameObject.CreatePrimitive(PrimitiveType.Cube);
		APHelper.AddComponent<RaceGateRender>();
		APHelper.transform.position = newNode.transform.position;
		APHelper.transform.parent = newNode.transform;
		APHelper.name = "thisGateAPHelper";
		APHelper.renderer.material = APPoint;
		APHelper.GetComponent<BoxCollider>().enabled = false;

		
		//Increase node count
		nodeCount++;

	}
}
