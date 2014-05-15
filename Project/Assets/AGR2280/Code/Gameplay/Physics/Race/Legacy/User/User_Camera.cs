using UnityEngine;
using System.Collections;

public class User_Camera : MonoBehaviour {
	
	// This is the script for the chase cam
	
	public Transform target;
	public float distance;
	float origDistance;
	public float height;
	public float damping;
	float wantedDamp;
	float origDamp;
	int currentCam;
	float accelPitch;
	
	GameObject YRef;
	
	GameObject CockView;
	
	Vector3 wantedPos;
	
	float angleB4;
	float angleAfter;
	float extraY;
	float accelTimes;
	float distanceTurnAmount;
	float distanceTurnMax;
	
	float startFOV;
	float camFOV = 60;
	float zSmooth;
	
	float xDiff;
	float thisX;
	float xExtra;
    public GameObject CameraLook;
    float antiAway;

	// New Vars for sorting
	float accelerationDistance = 2.2f;

    // Vibration
    float camVibrationSeed;
    float camVibrationX;
    float camVibrationY;

    GameObject RaceMan;

	// Height
	float heightDamp;

	float boostDistance;

	float moveLookLength;

	bool highDamp;

	float dampABVel;
	float dampNormalVel;

	
	void Start () 
	{
		origDistance = distance;
		origDistance = 17.5f;
		origDamp = damping;
		CockView = GameObject.Find("Ship_Cockview");
		startFOV = GetComponent<Camera>().fieldOfView;

        RaceMan = GameObject.Find("RaceManager");
	}
	
	void Update()
	{
		if (Input.GetButtonDown("[KB] Camera") || Input.GetButtonDown("[PAD] Camera"))
		{
			currentCam += 1;
			if (currentCam > 2)
			{
				currentCam = 0;
			}
		}

		if (currentCam == 2)
		{
			transform.position = CockView.transform.position;
			transform.rotation = CockView.transform.rotation;
		}
		//Vector3 wantedPos = target.TransformPoint(0, height, -distance);
	}
	
	void FixedUpdate () 
	{
        if (!RaceMan.GetComponent<RaceMangement>().gamePaused)
        {
            if (currentCam == 0)
            {

				CameraPositionClose();
				CameraRotationClose();

            }

			if (currentCam == 1)
			{
				
				CameraPositionFar();
				CameraRotationFar();
				
			}
        }
	}

	void CameraPositionClose()
	{
		
		Vector3 xDistance = target.InverseTransformPoint(transform.position);
		xDistance.z = 0;
		xDistance.y = 0;
		float localXDist = xDistance.magnitude;
		
		
		float wantedInputRot = target.GetComponent<User_Ship>().turnAmount;
		float shipRot = target.GetComponent<User_Ship>().rotationAmount;
		
		float steerInput = Input.GetAxis("[KB] Steer");
		if (steerInput == 0)
        {
            steerInput = Input.GetAxis("[PAD] Steer");
        }

		if (target.GetComponent<User_Ship>().inputAirbrake !=0)
		{
			steerInput = 1;
		} 
		
		if (shipRot < 0)
		{
			shipRot = -shipRot;
		}


        bool isAB = false;
        if (Input.GetAxis("[KB] Airbrake") != 0 || Input.GetAxis("[PAD] Airbrake") != 0 || Input.GetAxis("[PAD] Analog Airbrake") != 0)
        {
            isAB = true;
        }

        if (shipRot > wantedInputRot && isAB)
        {
			dampNormalVel = 4;
			dampABVel = Mathf.Lerp (dampABVel, 2, Time.deltaTime);
            wantedDamp = 12 + localXDist;
            damping = Mathf.Lerp(damping, wantedDamp, Time.deltaTime * dampABVel);
        } else
        {
			dampABVel = 0;
			dampNormalVel = Mathf.Lerp (dampNormalVel , 2, Time.deltaTime);
            wantedDamp = 12 + localXDist * 5;
            damping = Mathf.Lerp(damping, wantedDamp, Time.deltaTime / dampNormalVel);
        }

		origDistance = 10.8f;
		height = 3.7f;
		
		distance = origDistance - target.InverseTransformDirection(target.rigidbody.velocity).z / (damping / (Time.deltaTime * (damping - accelerationDistance)));
		
		transform.parent = target.transform;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -(distance + boostDistance));
		transform.parent = null;
		
		if (target.GetComponent<User_Ship>().boostTimer > 0)
		{
			boostDistance = Mathf.Lerp(boostDistance, 0.5f, Time.deltaTime * 5);
		} else 
		{
			boostDistance = Mathf.Lerp(boostDistance, 0, Time.deltaTime * 5);
		}
		wantedPos = target.TransformPoint(0, height + (boostDistance / 10), -(distance + boostDistance));
		transform.position = Vector3.Slerp(transform.position, new Vector3(wantedPos.x, transform.position.y, transform.position.z), Time.deltaTime * (damping));
		if (target.GetComponent<User_Ship>().publicDistance < target.GetComponent<User_Ship>().hoverHeight)
		{
			heightDamp = Mathf.Lerp(heightDamp, 100, Time.deltaTime * 2);
		} else
		{
			heightDamp = Mathf.Lerp(heightDamp, 20, Time.deltaTime * 10);
		}
		transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, wantedPos.y, transform.position.z), Time.deltaTime * heightDamp);
		transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, transform.position.y, wantedPos.z), Time.deltaTime * damping);
		
		if (Input.GetButton("[KB] Thruster") || Input.GetButton("[PAD] Thruster"))
		{
			if (target.GetComponent<User_Ship>().boostTimer > 0)
			{
				moveLookLength = Mathf.Lerp (moveLookLength, 5, Time.deltaTime * 15);
			} else
			{
				moveLookLength = Mathf.Lerp (moveLookLength, 2, Time.deltaTime * 15);
			}
		} else
		{
			if (target.GetComponent<User_Ship>().boostTimer > 0)
			{
				moveLookLength = Mathf.Lerp (moveLookLength, 5, Time.deltaTime * 15);
			} else 
			{
				moveLookLength = Mathf.Lerp (moveLookLength, 0, Time.deltaTime * 15);
			}
		}
	}
	
	void CameraRotationClose()
	{
		CameraLook.transform.position = transform.position;
		Vector3 lookAtPoint = target.TransformPoint(0, target.GetComponent<User_Ship>().cameraLookAtHeight, target.GetComponent<User_Ship>().cameraLookAtLength + moveLookLength);
		CameraLook.transform.LookAt(lookAtPoint);
		Quaternion WantedRot = CameraLook.transform.rotation;
		

		float lerpAdd = target.GetComponent<User_Ship>().rotationAmount * 100;
		if (lerpAdd < 0)
		{
			lerpAdd = -lerpAdd;
		}

        if (target.GetComponent<User_Ship>().frontGrounded)
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.eulerAngles.x, WantedRot.eulerAngles.y, target.eulerAngles.z + target.GetComponent<User_Ship>().currentWobble), Quaternion.Euler(WantedRot.eulerAngles.x, WantedRot.eulerAngles.y, target.eulerAngles.z + target.GetComponent<User_Ship>().currentWobble), Time.deltaTime * (24 + lerpAdd));
        } else
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.eulerAngles.x, WantedRot.eulerAngles.y, target.eulerAngles.z + target.GetComponent<User_Ship>().currentWobble), Quaternion.Euler(WantedRot.eulerAngles.x, WantedRot.eulerAngles.y, target.eulerAngles.z + target.GetComponent<User_Ship>().currentWobble), Time.deltaTime * 10000);
        }

	}
	
	public void Vibrate()
	{
		camVibrationSeed = Random.Range(-0.05f, 0.05f);
        camVibrationX = camVibrationSeed;
        camVibrationY = camVibrationSeed;
        transform.position = new Vector3(transform.position.x + camVibrationX, transform.position.y + camVibrationY, transform.position.z);

    }

	void OnGUI()
	{
		GUI.BeginGroup (new Rect (0, Screen.height / 2 - 300, 500, 800));
		GUI.Label (new Rect (0, 5, 500, 20), "Camera Stuff");
		GUI.Label (new Rect (0, 20, 500, 20), "PosXYZ: " + transform.position.x.ToString() + " " + transform.position.y.ToString() + " " + transform.position.z.ToString());
		GUI.Label (new Rect (0, 35, 500, 20), "EulerXYZ: " + transform.eulerAngles.x.ToString() + " " + transform.eulerAngles.y.ToString() + " " + transform.eulerAngles.z.ToString());
		GUI.Label (new Rect (0, 50, 500, 20), "WantedPos (XYZ): " + wantedPos.x.ToString() + " " + wantedPos.y.ToString() + " " + wantedPos.z.ToString());
		GUI.Label (new Rect (0, 65, 500, 20), "Distance: " + distance.ToString());
		GUI.Label (new Rect (0, 80, 500, 20), "Damping: " + damping.ToString());
		GUI.Label (new Rect (0, 95, 500, 20), "Height Damping: " + heightDamp.ToString());
		GUI.Label (new Rect (0, 110, 500, 20), "Height: " + height.ToString());
		GUI.EndGroup();
	}

	void CameraPositionFar()
	{
		
        Vector3 xDistance = target.InverseTransformPoint(transform.position);
        xDistance.z = 0;
        xDistance.y = 0;
        float localXDist = xDistance.magnitude;
        
        
        float wantedInputRot = target.GetComponent<User_Ship>().turnAmount;
        float shipRot = target.GetComponent<User_Ship>().rotationAmount;
        
        float steerInput = Input.GetAxis("[KB] Steer");
        if (steerInput == 0)
        {
            steerInput = Input.GetAxis("[PAD] Steer");
        }

        if (target.GetComponent<User_Ship>().inputAirbrake !=0)
        {
            steerInput = 1;
        } 
        
        if (shipRot < 0)
        {
            shipRot = -shipRot;
        }
        
        
        bool isAB = false;
        if (Input.GetAxis("[KB] Airbrake") != 0 || Input.GetAxis("[PAD] Airbrake") != 0 || Input.GetAxis("[PAD] Analog Airbrake") != 0)
        {
            isAB = true;
        }
        
        if (shipRot > wantedInputRot && isAB)
        {
            dampNormalVel = 4;
            dampABVel = Mathf.Lerp (dampABVel, 2, Time.deltaTime);
            wantedDamp = 10 + localXDist;
            damping = Mathf.Lerp(damping, wantedDamp, Time.deltaTime * dampABVel);
        } else
        {
            dampABVel = 0;
            dampNormalVel = Mathf.Lerp (dampNormalVel , 2, Time.deltaTime);
            wantedDamp = 10 + localXDist * 5;
            damping = Mathf.Lerp(damping, wantedDamp, Time.deltaTime / dampNormalVel);
        }

        origDistance = 14f;
        height = 4f;
        
        distance = origDistance - target.InverseTransformDirection(target.rigidbody.velocity).z / (damping / (Time.deltaTime * (damping - accelerationDistance)));
        
        transform.parent = target.transform;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -(distance + boostDistance));
        transform.parent = null;
        
        if (target.GetComponent<User_Ship>().boostTimer > 0)
        {
            boostDistance = Mathf.Lerp(boostDistance, 0.5f, Time.deltaTime * 5);
        } else 
        {
            boostDistance = Mathf.Lerp(boostDistance, 0, Time.deltaTime * 5);
        }
        wantedPos = target.TransformPoint(0, height + (boostDistance / 10), -(distance + boostDistance));
        transform.position = Vector3.Slerp(transform.position, new Vector3(wantedPos.x, transform.position.y, transform.position.z), Time.deltaTime * (damping));
        if (target.GetComponent<User_Ship>().publicDistance < target.GetComponent<User_Ship>().hoverHeight)
        {
            heightDamp = Mathf.Lerp(heightDamp, 100, Time.deltaTime * 2);
        } else
        {
            heightDamp = Mathf.Lerp(heightDamp, 20, Time.deltaTime * 10);
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, wantedPos.y, transform.position.z), Time.deltaTime * heightDamp);
        transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, transform.position.y, wantedPos.z), Time.deltaTime * damping);
        
        if (Input.GetButton("[KB] Thruster") || Input.GetButton("[PAD] Thruster"))
        {
            if (target.GetComponent<User_Ship>().boostTimer > 0)
            {
                moveLookLength = Mathf.Lerp (moveLookLength, 5, Time.deltaTime * 15);
            } else
            {
                moveLookLength = Mathf.Lerp (moveLookLength, 2, Time.deltaTime * 15);
            }
        } else
        {
            if (target.GetComponent<User_Ship>().boostTimer > 0)
            {
                moveLookLength = Mathf.Lerp (moveLookLength, 5, Time.deltaTime * 15);
            } else 
            {
                moveLookLength = Mathf.Lerp (moveLookLength, 0, Time.deltaTime * 15);
            }
        }
	}
	
	void CameraRotationFar()
	{
		CameraLook.transform.position = transform.position;
		Vector3 lookAtPoint = target.TransformPoint(0, 0, target.GetComponent<User_Ship>().cameraLookAtLength + moveLookLength);
		CameraLook.transform.LookAt(lookAtPoint);
		Quaternion WantedRot = CameraLook.transform.rotation;
		
		
		float lerpAdd = target.GetComponent<User_Ship>().rotationAmount * 100;
		if (lerpAdd < 0)
		{
			lerpAdd = -lerpAdd;
		}

		if (target.GetComponent<User_Ship>().frontGrounded)
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.eulerAngles.x, WantedRot.eulerAngles.y, target.eulerAngles.z + target.GetComponent<User_Ship>().currentWobble), Quaternion.Euler(WantedRot.eulerAngles.x, WantedRot.eulerAngles.y, target.eulerAngles.z + target.GetComponent<User_Ship>().currentWobble), Time.deltaTime * (12 + lerpAdd));
        } else
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.eulerAngles.x, WantedRot.eulerAngles.y, target.eulerAngles.z + target.GetComponent<User_Ship>().currentWobble), Quaternion.Euler(WantedRot.eulerAngles.x, WantedRot.eulerAngles.y, target.eulerAngles.z + target.GetComponent<User_Ship>().currentWobble), Time.deltaTime * 10000);
        }

		
	}
}
