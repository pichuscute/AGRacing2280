using UnityEngine;
using System.Collections;

public class ButtonCorrection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3 (Camera.main.WorldToViewportPoint(transform.position).x - 0.5f, transform.position.y, transform.position.z);
	}
}
