using UnityEngine;
using System.Collections;

public class EngineAudio : MonoBehaviour {

	// Use this for initialization

	public GameObject Player;
	public float pitch;
	public float volume;

	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		pitch = Player.GetComponent<ShipController>().shipThrust / Player.GetComponent<ShipController>().shipEngineMaxSpeed;
		pitch = Mathf.Clamp(pitch, 0.5f, pitch);
		GetComponent<AudioSource>().pitch = pitch;
	}
}
