using UnityEngine;
using System.Collections;

public class SpringTest : MonoBehaviour {

	// Use this for initialization

	float fallSpeed;
    public float baseSpringForce;
    public float baseHeight = 5.5f;
	public float damp;
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position , -Vector3.up, out hit))
        {

            if (hit.distance < baseHeight)
            {
				fallSpeed = Mathf.Lerp(fallSpeed, 85, Time.deltaTime * 5);
				float hoverForce;
				float distance = (transform.position.y - (hit.point.y + baseHeight));
				float length = Vector3.Magnitude(transform.position - hit.point);
				//hoverForce = -baseSpringForce * (distance - 8) - (damp * fallSpeed);
				//hoverForce *= hit.distance - baseHeight;
				float mass = 1 * fallSpeed;
				float discrepancy = baseHeight - hit.distance;
				//hoverForce = hit.distance - baseSpringForce + (fallSpeed / 8);
				//hoverForce *= hit.distance - baseHeight;
				hoverForce = (hit.distance + baseHeight) - baseSpringForce;
				hoverForce *= hit.distance - ((baseHeight * damp) + distance);
                print (hoverForce);
				rigidbody.AddForce(Vector3.up * hoverForce);
			} else
			{
				fallSpeed = Mathf.Lerp(fallSpeed, 115 * 6 , Time.deltaTime * 1.8f);
			}            
        } else
		{ 
			fallSpeed = Mathf.Lerp(fallSpeed, 115 * 6 , Time.deltaTime * 1.8f);
		}

        rigidbody.AddForce(-Vector3.up * fallSpeed);
	}
}
