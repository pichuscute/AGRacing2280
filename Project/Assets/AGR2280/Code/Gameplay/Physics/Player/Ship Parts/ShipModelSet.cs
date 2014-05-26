using UnityEngine;
using System.Collections;

public class ShipModelSet : MonoBehaviour {
	
	void Start () 
	{
		if (transform.parent.parent.parent.GetComponent<ShipController>())
		{
			transform.parent.parent.parent.GetComponent<ShipController>().ShipModel = this.gameObject;
		}
	}

}
