using UnityEngine;
using System.Collections;

public class RacerInfoReturn : MonoBehaviour {

	public int thisRacerID;
	public int thisCurrentGate;

	public int thisRacerLap;

	public int thisLapMS;
	public int thisLapS;
	public int thisLapM;

	public int bestLapMS;
	public int bestLapS;
	public int bestLapM;

	public int overalMS;
	public int overalS;
	public int overalM;

	public Transform nextAPObject;

	void Start () 
	{
		if (thisRacerID > 8)
		{
			thisRacerID = 8;
		}

		// Zero out times
		thisRacerLap = 0;
		thisLapMS = 0;
		thisLapS = 0;
		thisLapM = 0;

		bestLapMS = 0;
		bestLapS = 0;
		bestLapM = 0;

		overalMS = 0;
		overalS = 0;
		overalM = 0;
	}
	

	void Update () 
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.collider.gameObject.tag == "Gate")
		{
			GetComponent<ShipController>().respawnPosition = other.collider.gameObject.transform.position;
			GetComponent<ShipController>().respawnRotation = Quaternion.Euler(0, other.collider.gameObject.transform.eulerAngles.y, 0);

			string nodeName = other.collider.name;
			string nodeID = nodeName.Remove(0, 9).ToString();
			thisCurrentGate = int.Parse(nodeID);
			if (GameObject.Find("RaceGate_" + (thisCurrentGate + 4)))
			{
				nextAPObject = GameObject.Find("RaceGate_" + (thisCurrentGate + 4)).transform.Find("thisGateAPHelper");
				
			} else 
			{
				nextAPObject = GameObject.Find("RaceGate_" + 0).transform.Find("thisGateAPHelper");
			}
		}
	}
}
