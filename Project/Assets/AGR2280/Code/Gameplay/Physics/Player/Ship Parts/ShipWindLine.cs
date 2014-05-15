using UnityEngine;
using System.Collections;

public class ShipWindLine : MonoBehaviour {

	// Use this for initialization
	public GameObject Target;
	Color WindOpacity;
	
	float rotY;
	
	// Update is called once per frame
	void FixedUpdate() 
	{
		if (Target.GetComponent<ShipController>().shipBoostTimer > 0)
		{
			WindOpacity = Color.Lerp(WindOpacity, new Color(1, 1, 1, 1), Time.deltaTime * 8);
		} else
		{
			WindOpacity = Color.Lerp(WindOpacity, new Color(1, 1, 1, 0), Time.deltaTime * 2);
		}
		
		renderer.material.SetColor("_TintColor", WindOpacity);
		rotY = 180 + -Target.GetComponent<ShipController>().rotationForce * 8;
		transform.parent.localRotation = Quaternion.Euler(0, rotY, 0);
		
	}
}
