using UnityEngine;
using System.Collections;

public class RaceController : MonoBehaviour {


	public int racerCount;
	public GameObject[] racerObject;
	public RaceInformation.RacerType racerIDController;
	public RaceInformation.Event thisEvent;


	void Start () 
	{
		// Set to speed lap for now
		thisEvent = RaceInformation.Event.SpeedLap;

	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
