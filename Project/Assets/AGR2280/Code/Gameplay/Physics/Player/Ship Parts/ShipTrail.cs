using UnityEngine;
using System.Collections;

public class ShipTrail : MonoBehaviour {

	// Use this for initialization

	public GameObject targetShip;
	float trailOpacity;
	float trailStartSize;
	float trailEndSize;

	public float trailStartMinSize;
	public float trailStartBoostSize;

	public float trailEndMinSize;
	public float trailEndBoostSize;

	public float trailStartBoostSpeed;
	public float trailEndBoostSpeed;

	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Set properties
		trailOpacity = targetShip.GetComponent<ShipController>().shipThrust / (targetShip.GetComponent<ShipController>().shipEngineMaxSpeed * 0.9f);
		trailOpacity = Mathf.Clamp(trailOpacity, 0, 0.5f);

		if (targetShip.GetComponent<ShipController>().shipBoostTimer > 0)
		{
			trailStartSize = Mathf.Lerp (trailStartSize, trailStartBoostSize, Time.deltaTime * trailStartBoostSpeed);
			trailEndSize = Mathf.Lerp (trailEndSize, trailEndBoostSize, Time.deltaTime * trailEndBoostSpeed);
		} else
		{
			trailStartSize = Mathf.Lerp (trailStartSize, trailStartMinSize, Time.deltaTime);
			trailEndSize = Mathf.Lerp (trailEndSize,  trailEndMinSize, Time.deltaTime);
		}

		// Apply Properties
		renderer.material.SetColor("_TintColor", new Color(1, 1, 1, trailOpacity));
		GetComponent<TrailRenderer>().startWidth = trailStartSize;
		GetComponent<TrailRenderer>().endWidth = trailEndSize;
	}
}
