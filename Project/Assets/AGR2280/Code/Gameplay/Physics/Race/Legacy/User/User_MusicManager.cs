using UnityEngine;
using System.Collections;

public class User_MusicManager : MonoBehaviour {

	// Use this for initialization
	GameObject Target;
	float HighPass;

	void Start () 
	{
		Target = GameObject.Find ("Plr_ShipController");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Target.GetComponent<User_Ship>().publicDistance > Target.GetComponent<User_Ship>().hoverHeight + 5) 
		{
			HighPass = Mathf.Lerp (HighPass, 5000, Time.deltaTime);
		} else {
			HighPass = Mathf.Lerp (HighPass, 0, Time.deltaTime * 5);
		}

		GetComponent<AudioHighPassFilter> ().cutoffFrequency = HighPass;
	}
}
