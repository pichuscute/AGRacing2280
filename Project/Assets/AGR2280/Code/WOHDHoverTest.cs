using UnityEngine;
using System.Collections;

public class WOHDHoverTest : MonoBehaviour {

	// Raycast Locations
	public float RaycastOffset;
	Vector3 backCastPos;
	Vector3 frontCastPos;

	// Gravity
	float airGravity = 115 * 6;
	float trackGravity = 85;
	float shipGravity;

	// Hovering
	bool isGrounded;
	float rideHeight = 5.5f;
	float currentRideHeight;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		isGrounded = true;
		backCastPos = transform.TransformPoint(new Vector3(0,0, -RaycastOffset));
		frontCastPos = transform.TransformPoint(new Vector3(0,0, RaycastOffset));

		RaycastHit hit;
		if (Physics.Raycast(frontCastPos, -Vector3.up, out hit))
		{
			Debug.DrawLine(frontCastPos, hit.point);
			currentRideHeight = hit.distance;
			if (currentRideHeight < rideHeight)
			{
				isGrounded = true;

				float springCost = 85f;
				Vector3 spring = hit.point - transform.position;
				float length = spring.magnitude;
				float displacement = length - rideHeight;

				Vector3 springN = spring / length;
				Vector3 restoreForce = springN*(displacement*springCost);

				rigidbody.AddForceAtPosition(Vector3.up * restoreForce.y, frontCastPos);
				print(restoreForce);

			}
		}

		RaycastHit hit2;
		if (Physics.Raycast(backCastPos, -Vector3.up, out hit2))
		{
			Debug.DrawLine(backCastPos, hit2.point);
			currentRideHeight = hit2.distance;
			if (currentRideHeight < rideHeight)
			{
				isGrounded = true;
				
				float springCost = 85f;
				Vector3 spring = hit2.point - transform.position;
				float length = spring.magnitude;
				float displacement = length - rideHeight;
				
				Vector3 springN = spring / length;
				Vector3 restoreForce = springN*(displacement*springCost);

				rigidbody.AddForceAtPosition(Vector3.up * restoreForce.y, backCastPos);
				
			}
		}

		// Apply Hover Forces
		if (isGrounded)
		{
			shipGravity = Mathf.Lerp (shipGravity, trackGravity, Time.deltaTime * 5);
		} else 
		{
			shipGravity = Mathf.Lerp (shipGravity, airGravity, Time.deltaTime * 1.8f);
		}
		rigidbody.AddForce(new Vector3(0, -shipGravity, 0));
	}
}
