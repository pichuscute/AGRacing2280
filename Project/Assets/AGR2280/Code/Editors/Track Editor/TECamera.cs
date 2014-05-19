using UnityEngine;
using System.Collections;

public class TECamera : MonoBehaviour {


	// Camera Rotation
	float cameraMouseRotX;
	float cameraRotY;
	
	// Mouse Input
	float cameraInputX;
	float cameraInputY;
	bool mbtnMoveCam;

	// Keyboard Input
	float horAxis;
	float verAxis;
	float upAxis;

	bool fastMove;


	void Start () 
	{
	
	}

	void Update () 
	{
		GetInput();
		if (mbtnMoveCam)
		{
			CameraMove();
		}
	}

	void GetInput()
	{
		mbtnMoveCam = false;

		cameraInputX = Mathf.Lerp(cameraInputX, Input.GetAxis("Mouse X") / 2, Time.deltaTime * 12);
		cameraInputY = Mathf.Lerp(cameraInputY, Input.GetAxis("Mouse Y") / 2, Time.deltaTime * 12);
		if (Input.GetMouseButton(1))
		{
			mbtnMoveCam = true;
		}

		// Default axis
		horAxis = 0;
		verAxis = 0;
		upAxis = 0;
		fastMove = false;
		if (Input.GetKey(KeyCode.W)) { verAxis = 1; }
		if (Input.GetKey(KeyCode.S)) { verAxis = -1; }
		if (Input.GetKey(KeyCode.A)) { horAxis = -1; }
		if (Input.GetKey(KeyCode.D)) { horAxis = 1; }
		if (Input.GetKey(KeyCode.Q)) { upAxis = -1; }
		if (Input.GetKey(KeyCode.E)) { upAxis = 1; }
		if (Input.GetKey(KeyCode.LeftShift)) { fastMove = true; }
	}

	void CameraMove()
	{
		// Rotation
		cameraMouseRotX -= cameraInputY;
		cameraMouseRotX = Mathf.Clamp(cameraMouseRotX ,-90, 90);

		transform.rotation = Quaternion.Euler (cameraMouseRotX, transform.eulerAngles.y + cameraInputX, 0);

		// Apply Movement
		if (fastMove)
		{
			rigidbody.AddRelativeForce(Vector3.forward * (verAxis * 300));
			rigidbody.AddRelativeForce(Vector3.right * (horAxis * 300));
			rigidbody.AddForce(new Vector3(0,(upAxis * 300),0));
		} else
		{
			rigidbody.AddRelativeForce(Vector3.forward * (verAxis * 150));
			rigidbody.AddRelativeForce(Vector3.right * (horAxis * 150));
			rigidbody.AddForce(new Vector3(0,(upAxis * 150),0));
		}

		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, 1000), transform.position.z);


	}
}
