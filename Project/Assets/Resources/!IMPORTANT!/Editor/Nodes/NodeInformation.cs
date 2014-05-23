using UnityEngine;
using System.Collections;

public class NodeInformation : MonoBehaviour {

	public float trackWidth;

	void Update()
	{
		transform.localScale = new Vector3(trackWidth, transform.localScale.y, transform.localScale.z);
	}

	void OnGizmoDraw()
	{
		Gizmos.DrawLine(transform.TransformPoint(-trackWidth, 0, 0), transform.TransformPoint(-trackWidth, 100, 0));
		Gizmos.DrawLine(transform.TransformPoint(trackWidth, 0, 0), transform.TransformPoint(trackWidth, 100, 0));
	}
}
