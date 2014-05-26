using UnityEngine;
using System.Collections;

public class RaceController : MonoBehaviour {


	public int racerCount;
	public GameObject[] racerObject;
	public RaceInformation.RacerType racerIDController;
	public RaceInformation.Event thisEvent;


	public enum RaceState { Overview, Countdown, Racing, Ending };
	public RaceState raceProgress;


	void Start () 
	{
		// Set to time trial for now
		thisEvent = RaceInformation.Event.Trial;

		// Set Race State to Overview
		raceProgress = RaceState.Overview;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("[KB] Thruster") || Input.GetButtonDown("[PAD] Thruster"))
		{
			if (raceProgress == RaceState.Overview)
			{
				GetComponent<Animation>().Play();
				raceProgress = RaceState.Countdown;
			}
		}
	}

	public void StartRace()
	{
		raceProgress = RaceState.Racing;
		GameObject.Find("MusicManager").GetComponent<MusicManager>().enabled = true;
	}

	public void StartdownSound()
	{
		GetComponent<AudioSource>().Play();
	}
}
