using UnityEngine;
using System.Collections;

public class ModelGroupSet : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if (transform.parent.GetComponent<ShipController>())
		{
			transform.parent.GetComponent<ShipController>().ModelGroup = this.gameObject;
		}
	}
}
