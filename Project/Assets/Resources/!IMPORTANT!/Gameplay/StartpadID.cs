using UnityEngine;
using System.Collections;

public class StartpadID : MonoBehaviour {

	// Use this for initialization
	public float StartID;
	void Start () 
	{
		if (StartID > 8)
		{
			StartID = 8;
		}

		if (StartID < 1)
		{
			StartID = 1;
		}

		transform.name = "StartPad_" + StartID.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
