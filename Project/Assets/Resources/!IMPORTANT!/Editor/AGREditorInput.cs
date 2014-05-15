using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AGREditorInputObject))] 
public class AGREditorInput : Editor {

	public static Event AGR2280EditorInput;
	public static KeyCode input;
    public static Ray camera;

    bool keyDown;

    void OnSceneGUI()
    {
        camera = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        input = KeyCode.None;
        if (!keyDown)
        {
            input = KeyCode.None;
        }

        if (Event.current.type == EventType.keyDown && !keyDown)
        {
            input = Event.current.keyCode;
            keyDown = true;
        }

        if (Event.current.type == EventType.keyUp)
        {
            keyDown = false;
        }
    }
}
