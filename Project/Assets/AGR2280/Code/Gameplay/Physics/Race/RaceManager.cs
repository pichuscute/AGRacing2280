using UnityEngine;
using System.Collections;

public class RaceManager : MonoBehaviour {

	// Use this for initialization
    public enum RaceType
    {
        RT_SpeedLap,
        RT_Single,
        RT_Eliminator,
        RT_HeadToHead,
        RT_Zone,
    };
    public int gateCount;
    RaceType thisRace;

	void Start () 
    {
	    // Speed lap test
        thisRace = RaceType.RT_SpeedLap;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (thisRace == RaceType.RT_SpeedLap)
        {

        }
	}
}
