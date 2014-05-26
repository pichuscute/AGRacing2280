using UnityEngine;
using System.Collections;

public class ShipChaseCam : MonoBehaviour {

	// Camera Information
	//  These values are used to detect which camera type we want
	public enum CameraType { Close, Far, Internal, Backward }
	public CameraType currentCameraType;

	int currentCameraMode;
	int lastCameraMode;
	public GameObject targetShip;

	float currentCameraSpring;
	float currentCameraCalculatedSpring;
	float currentCameraWantedCalculatedSpring;
	float currentCameraLookAtHeight;
	float currentCameraLookAtLength;
	float currentCameraLength;
	float currentCameraInterpolatedLength;
	float currentCameraHeight;
	float currentCameraFOV;

	float cameraSpringNormalVelocity;
	float cameraSpringAirbrakeVelocity;
	float cameraWantedSpring;

	float cameraBoostDistance;
	float cameraHeightSpring;
	float cameraMoveLength;
	float cameraBaseLookSpring;

	Vector3 wantedPosition;

	float calculatedLocalX;
	float xLocalDistance;

	float xLocalLength;

	float shipTurningCalc;
	float lookAtHeightMove;
	GameObject RaceController;

	void Start()
	{
		RaceController = GameObject.Find("RaceManager");
	}
	
	void Update () 
	{
		if (RaceController.GetComponent<RaceController>().raceProgress != global::RaceController.RaceState.Overview)
		{
			CameraInputAndUpdate();
			if (currentCameraMode == 2 || currentCameraMode == 3)
			{
				InternalCam();
			}
		} else 
		{
			transform.parent = GameObject.Find("TrackOverviewAnim").transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.Euler(0,0,0);
		}
	}

	void FixedUpdate()
	{
		if (RaceController.GetComponent<RaceController>().raceProgress != global::RaceController.RaceState.Overview)
		{
			if (currentCameraMode == 0 || currentCameraMode == 1)
			{
				ChaseCam();
			}
		}
	}

	void CameraInputAndUpdate()
	{
		if (Input.GetButton("[KB] LookBehind") || Input.GetButton("[PAD] LookBehind"))
		{
			currentCameraMode = 3;
		} else 
		{
			if (currentCameraMode == 3)
			{
				currentCameraMode = lastCameraMode;
			}
			if (Input.GetButtonDown("[KB] Camera") || Input.GetButtonDown("[PAD] Camera"))
			{
				currentCameraMode ++;
				if (currentCameraMode > 2)
				{
					currentCameraMode = 0;
				}
				lastCameraMode = currentCameraMode;
			}
		}

		// Apply currentCameraMode to enum
		if (currentCameraMode == 0)
		{
			currentCameraType = CameraType.Close;
			currentCameraFOV = targetShip.GetComponent<ShipController>().cameraCloseFOV;
			currentCameraHeight = targetShip.GetComponent<ShipController>().cameraCloseHeight;
			currentCameraLength = targetShip.GetComponent<ShipController>().cameraCloseLength;
			currentCameraLookAtHeight = targetShip.GetComponent<ShipController>().cameraCloseLookAtHeight;
			currentCameraLookAtLength = targetShip.GetComponent<ShipController>().cameraCloseLookAtLength;
			currentCameraSpring = targetShip.GetComponent<ShipController>().cameraCloseSpring;
			cameraBaseLookSpring = 12;
		}
		if (currentCameraMode == 1)
		{
			currentCameraType = CameraType.Far;
			currentCameraFOV = targetShip.GetComponent<ShipController>().cameraFarFOV;
			currentCameraHeight = targetShip.GetComponent<ShipController>().cameraFarHeight;
			currentCameraLength = targetShip.GetComponent<ShipController>().cameraFarLength;
			currentCameraLookAtHeight = targetShip.GetComponent<ShipController>().cameraFarLookAtHeight;
			currentCameraLookAtLength = targetShip.GetComponent<ShipController>().cameraFarLookAtLength;
			currentCameraSpring = targetShip.GetComponent<ShipController>().cameraFarSpring;
			cameraBaseLookSpring = 12;
		}
		if (currentCameraMode == 2)
		{
			currentCameraType = CameraType.Internal;
			currentCameraFOV = targetShip.GetComponent<ShipController>().cameraInternalFOV;
			currentCameraLength = targetShip.GetComponent<ShipController>().cameraInternalLength;
			currentCameraHeight = 0;
		}
		if (currentCameraMode == 3)
		{
			currentCameraType = CameraType.Backward;
			currentCameraFOV = targetShip.GetComponent<ShipController>().cameraBackwardFOV;
			currentCameraLength = -targetShip.GetComponent<ShipController>().cameraBackwardLength;
			currentCameraHeight = 0;
		}

		// Set FOV
		if (targetShip.GetComponent<ShipController>().shipBoostTimer > 1)
		{
			GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, currentCameraFOV + targetShip.GetComponent<ShipController>().cameraBoostFOV, Time.deltaTime * 5);
			GetComponent<Vignetting>().chromaticAberration = Mathf.Lerp(GetComponent<Vignetting>().chromaticAberration, targetShip.GetComponent<ShipController>().shipBoostAmount, Time.deltaTime / 10);
			GetComponent<Vignetting>().chromaticAberration = Mathf.Clamp(GetComponent<Vignetting>().chromaticAberration, 0, 35);
		} else
		{
			GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, currentCameraFOV, Time.deltaTime * 5);
			GetComponent<Vignetting>().chromaticAberration = Mathf.Lerp(GetComponent<Vignetting>().chromaticAberration, 0, Time.deltaTime * 5);
		}
	}

	void ChaseCam()
	{
		// Get Local X distance
		Vector3 LocalDistance = targetShip.transform.InverseTransformPoint(transform.position);
		LocalDistance.y = 0;
		LocalDistance.z = 0;
		xLocalDistance = LocalDistance.magnitude;

		// Get Inputs
		float tightCameraLimit = targetShip.GetComponent<ShipController>().shipTurnMax;
		float targetShipCurrentRot = targetShip.GetComponent<ShipController>().rotationForce;
		float inputSteer = Input.GetAxis("[KB] Steer");
		if (inputSteer == 0)
		{
			inputSteer = Input.GetAxis("[PAD] Steer");
		}

		// Max out steering if airbraking
		if (targetShip.GetComponent<ShipController>().inputAirbrake != 0)
		{
			inputSteer = 0;
		}

		// Inverse steering if under 0
		if (targetShipCurrentRot < 0)
		{
			targetShipCurrentRot = -targetShipCurrentRot;
		}

		bool isAB = false;
		if (Input.GetAxis("[KB] Airbrake") != 0 || Input.GetAxis("[PAD] Airbrake") != 0 || Input.GetAxis("[PAD] Analog Airbrake") != 0)
		{
			isAB = true;
		}

		// Work out the amount of spring we want
		shipTurningCalc = Mathf.Lerp(shipTurningCalc, targetShipCurrentRot, Time.deltaTime * currentCameraSpring / 6);

		if (targetShipCurrentRot > tightCameraLimit || isAB)
		{
			cameraSpringNormalVelocity = 4;
			cameraSpringAirbrakeVelocity = Mathf.Lerp (cameraSpringAirbrakeVelocity, 2, Time.deltaTime);
			currentCameraWantedCalculatedSpring = (currentCameraSpring + (xLocalDistance * shipTurningCalc)) - (shipTurningCalc * 3);
			calculatedLocalX = Mathf.Lerp(calculatedLocalX, xLocalDistance, Time.deltaTime * (currentCameraSpring / (targetShip.GetComponent<ShipController>().shipAirbrakeTurn * 100)));
			currentCameraCalculatedSpring = Mathf.Lerp(currentCameraCalculatedSpring, currentCameraWantedCalculatedSpring, Time.deltaTime * cameraSpringAirbrakeVelocity);

			xLocalLength = Mathf.Lerp(xLocalLength, xLocalDistance / 25, Time.deltaTime * 5);
		} else
		{
			cameraSpringAirbrakeVelocity = 0;
			cameraSpringNormalVelocity = Mathf.Lerp (cameraSpringNormalVelocity , 2, Time.deltaTime);
			currentCameraWantedCalculatedSpring = (currentCameraSpring + (xLocalDistance * shipTurningCalc)  * currentCameraSpring) - (shipTurningCalc * 3);
			calculatedLocalX = Mathf.Lerp(calculatedLocalX, xLocalDistance, Time.deltaTime * (currentCameraSpring / 200));
			currentCameraCalculatedSpring = Mathf.Lerp(currentCameraCalculatedSpring, currentCameraWantedCalculatedSpring, Time.deltaTime / cameraSpringNormalVelocity);

			xLocalLength = Mathf.Lerp(xLocalLength, 0, Time.deltaTime * 5);
		}

		// Distance
		currentCameraInterpolatedLength = currentCameraLength - targetShip.transform.InverseTransformDirection(targetShip.rigidbody.velocity).z / (currentCameraCalculatedSpring / (Time.deltaTime * (currentCameraCalculatedSpring - 2.2f))) - xLocalLength;

		// Briefly parent camera to help with distance
		transform.parent = targetShip.transform;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -(currentCameraInterpolatedLength + cameraBoostDistance));
		transform.parent = null;

		// Change Boost distance if boosting
		if (targetShip.GetComponent<ShipController>().shipBoostTimer > 0)
		{
			cameraBoostDistance = Mathf.Lerp(cameraBoostDistance, 0.5f, Time.deltaTime * 5);
		} else 
		{
			cameraBoostDistance = Mathf.Lerp(cameraBoostDistance, 0, Time.deltaTime * 5);
		}

		// Get the position the camera should be at then lerp to it
		wantedPosition = targetShip.transform.TransformPoint(0, currentCameraHeight + (cameraBoostDistance / 10), -(currentCameraInterpolatedLength + cameraBoostDistance));
		transform.position = Vector3.Slerp(transform.position, new Vector3(wantedPosition.x, transform.position.y, transform.position.z), Time.deltaTime * (currentCameraCalculatedSpring));
		if (targetShip.GetComponent<ShipController>().RaycastFrontDistance < targetShip.GetComponent<ShipController>().shipAntiGravRideHeight)
		{
			cameraHeightSpring = Mathf.Lerp(cameraHeightSpring, 100, Time.deltaTime * 2);
		} else
		{
			cameraHeightSpring = Mathf.Lerp(cameraHeightSpring, 20, Time.deltaTime * 10);
		}
		transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, wantedPosition.y, transform.position.z), Time.deltaTime * cameraHeightSpring);
		transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, transform.position.y, wantedPosition.z), Time.deltaTime * currentCameraCalculatedSpring);

		// Boosting length increase
		if (targetShip.GetComponent<ShipController>().shipBoostTimer > 0)
		{
			cameraMoveLength = Mathf.Lerp (cameraMoveLength, 5, Time.deltaTime * 15);
		} else
		{
			cameraMoveLength = Mathf.Lerp (cameraMoveLength, 2, Time.deltaTime * 15);
		}

		Vector3 lookAtPosition = targetShip.transform.TransformPoint(0, currentCameraLookAtHeight, currentCameraLookAtLength + cameraMoveLength);

		float lookAtHeightMoveVel = targetShip.rigidbody.velocity.y;
		if (lookAtHeightMoveVel != 0)
		{
			lookAtHeightMoveVel /= currentCameraSpring;
		}
		if (lookAtHeightMoveVel < 0)
		{
			lookAtHeightMoveVel = -lookAtHeightMoveVel;
		}

		lookAtHeightMove = Mathf.Lerp (lookAtHeightMove, lookAtPosition.y, Time.deltaTime * (currentCameraSpring + lookAtHeightMoveVel));


		Quaternion WantedRot = Quaternion.LookRotation(new Vector3(lookAtPosition.x, lookAtHeightMove, lookAtPosition.z) - transform.position);

		float lerpAdd = targetShip.GetComponent<ShipController>().rotationForce * 4;
		if (lerpAdd < 0)
		{
			lerpAdd = -lerpAdd;
		}
		transform.rotation = Quaternion.Euler(WantedRot.eulerAngles.x, WantedRot.eulerAngles.y, targetShip.transform.eulerAngles.z + targetShip.GetComponent<ShipController>().shipCurrentWobble);

	}

	void InternalCam()
	{
		transform.position = targetShip.transform.TransformPoint(0,0, currentCameraLength);
		Vector3 targetShipRotation = targetShip.GetComponent<ShipController>().ModelGroup.transform.eulerAngles;
		if (currentCameraMode == 2)
		{
			transform.rotation = Quaternion.Euler(targetShipRotation.x, targetShipRotation.y, targetShipRotation.z);
		} else 
		{
			transform.rotation = Quaternion.Euler(targetShipRotation.x, targetShipRotation.y + 180, -targetShipRotation.z);
		}
	}
}
