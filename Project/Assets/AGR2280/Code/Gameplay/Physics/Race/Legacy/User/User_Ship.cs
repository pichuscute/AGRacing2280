using UnityEngine;
using System.Collections;

public class User_Ship : MonoBehaviour {

	// Race Speed
	public enum SpeedClass
	{
        Venom = 0,
        Flash = 1,
        Rapier = 2,
        Phantom = 3
	};

    public SpeedClass RaceSpeed;

    // Gravity Values
    float airGravity;
    float normalGravity;
    float trackGravity;

    float fallSpeed;

    // Inputs
    bool isThrusting;
    float steerInput;
    public float inputAirbrake;
    float bankingInputAirbrake;
    public bool isBraking;
    bool BRLeft;
    bool BRRight;

    // Hover Stats
    public float hoverHeight;
    public float baseHoverForce;
    float frontHoverForce;
    float frontGroundHeight;
    float frontGroundHeightInverse;
	public float publicDistance;

    float backHoverForce;
    float backGroundHeight;
    float backGroundHeightInverse;

    public bool frontGrounded;
    bool backGrounded;
    bool isGrounded;
    bool isHalfGrounded;
    public bool cameraLockY;
	float hoverRebound;

	float backGravity;

	// Wobble
	float wobbleAmount;
	float wobbleSpeed;
	float wobbleTime;
	public float currentWobble;
	bool reachedTerminalVelocity;

	// Acceleration
	public float accelCap;
	public float maxEngineTorque;
	public float engineFalloff;
	public float engineGain;
	public float boostExtra;
	public float acceleration;
	float currentAccel;
	float decaccel;
    public float deaccelAmount;

    // Turning
    public float turnAmount;
    public float actualTurnAmount;
    public float airBrakeAmount;
    public float actualAirbrakeAmount;
    public float rotationAmount;
    public float currentBank;
    float maxBank = 40;
    float airBrakeExtra = 10;
    float actualAirBrakeExtra;
    public float bankSpeed;
    float returnBankSpeed;
    float actualBank;
	float turnVelocity;

	float airbrakeAccelLossVelocity;
	float airBrakeALVV;

    float turnVelocityLoss;
    float turnVelocityLossLoss;
    float turnVelocityLossLossLoss;
	public float turnFallOff;
	float turnVelocityFallOff;

    // Sideshifting
    public float sideShiftTimer;
    public float sideShiftPressTimer;
    public bool sideShiftLeft;
    public bool sideShiftRight;
    bool canSideShiftLeft = true;
    bool canSideShiftRight = true;
    public bool hasSideShiftLeft;
    public bool hasSideShiftRight;
    public float sideShiftCooldown;

    // GameObjects
    public GameObject FrontRayPos;
    public GameObject BackRayPos;
    public GameObject ShipModelGroup;

    public GameObject Audio_Roll;
    public GameObject Audio_Boost;
    public GameObject Audio_Engine;
    public GameObject Audio_Ignition;
    public GameObject Audio_Airbrake;
	public GameObject Audio_Wind;
	public GameObject Audio_WallScrape;

    public GameObject AnimationHolder;
    public GameObject ShipFlare;
    public GameObject EngineFlare;

    public GameObject BoostFlareLeft;
    public GameObject BoostFlareRight;
    GameObject RaceMan;
    float boostFlareSpeed;

    public GameObject EngineLight;
    float engineLightIntensity;

    public GameObject NeededPrefab;
    public GameObject DirtFloor;

	// Camera Settings
	public float cameraLookAtHeight;
	public float cameraLookAtLength;

    // Pitch
    float shipAnglePitchHelp;
    float inputPitch;
    float PitchMax = 0.6f;
    public float currentPitch;
    float shipAngleChangeSpeed;

    // Boost
    public float boostAmount;
    public float boostTimer;
    float boostExtraTurn;
    float cameraBoostEffect;
	float cameraCromatic;

    // Barrel Roll
    public bool rollSuccess;
    float rollState;
    float rollStateSpeed;
    public bool LeftRollOne;
    public bool LeftRollTwo;
    public bool LeftRollFinal;
    public int rollTimerOne;
    public int rollTimerTwo;
    public bool RightRollOne;
    public bool RightRollTwo;
    public bool RightRollFinal;
    bool hasRolled;

    // Camera Settings
    public float cameraFarNormalFOV = 60;
    public float cameraFarBoostFOV = 40;
    public float cameraCloseNormalFOV = 60;
    public float cameraCloseBoostFOV = 40;
    public float currentCameraFOV;

    public float cameraCloseDamping = 12;
    public float cameraFarDamping = 10;

	public float cameraRespawnOverlay;

    // Sound and Visuals
    float enginePitch;

    // Collision
    Vector3 CollisionDirection;
    float collideDrag; // The ship has a drag of 5 to stop it from floating around when turning so we need to simulate our own drag
    Vector3 DirectionLine;
    float CollisionAngle;
    float FinalAngle;
    Vector3 CollisionSideCheck;
    bool shipColliding;
    Vector3 collisionSpeed;
    Vector3 SpeedBeforeCollision;
    Vector3 ShipRigidbodyVelocity;
    Vector3[] hitPoint;
    float CollisionPointsNo;
    
    float[] CollisionDelays;
    int ColDelay;
    string shipCollisionLayer;

	Vector3 BoostDirection;
	bool directionalBoost;

	public Vector3 shipWeightDrag;
	public Vector3 cameraXFocusDrag;

    // Race Manager
    bool hasStarted;

	// Respawn
	public Vector3 respawnPos;
	public Quaternion respawnRot;

	float respawnTimer;
	bool onTrack;

	public bool isRaycast;

    // Visual
    float engineFlareSpeed;

    // Health
    public int Integrity = 100;

    Quaternion wantedTrackRot;

	void Start () 
	{
	    // Ship stats according to race speed
        if (RaceSpeed == SpeedClass.Venom)
        {
			airGravity = 110 * 6;
			normalGravity = 5 * 4;
            trackGravity = 80;
        }

        if (RaceSpeed == SpeedClass.Flash)
        {
			airGravity = 110 * 6;
			normalGravity = 5 * 4;
			trackGravity = 80;
		}
		
		if (RaceSpeed == SpeedClass.Rapier)
        {
			airGravity = 115 * 6;
			normalGravity = 5 * 4;
			trackGravity = 85;
		}
		
		if (RaceSpeed == SpeedClass.Phantom)
        {
			airGravity = 115 * 6;
			normalGravity = 5 * 4;
			trackGravity = 85;
		}

        // Collisions
        hitPoint = new Vector3[3];
        CollisionDelays = new float[6];

        // Needed Gameobjects
        DirtFloor = GameObject.Find("Plr_DirtFloor");

		// Setup Respawn
		respawnRot = transform.rotation;
		respawnPos = transform.position;
        RaceMan = GameObject.Find("RaceManager");
        InvokeRepeating("Timed_Stuff", 0, 0.3f);
	}

    void Update()
    {
        Func_Input();
		Func_SideShiftInput ();

        hasStarted = GameObject.Find("RaceManager").GetComponent<RaceMangement>().raceStarted;

        // Boost Start
        if (GameObject.Find("RaceManager").GetComponent<RaceMangement>().hasBoosted)
        {
            boostAmount = 700;
            boostTimer = 10;
            acceleration = maxEngineTorque / 2;
            Audio_Boost.GetComponent<AudioSource>().Play();
        }

		rigidbody.centerOfMass = new Vector3 (0, 0, 0.09f);

        // Clamp Integrity
        Integrity = Mathf.Clamp(Integrity, 0, 100);
    }

	void FixedUpdate () 
	{
        if (!RaceMan.GetComponent<RaceMangement>().gamePaused)
        {
            if (!Audio_Engine.GetComponent<AudioSource>().isPlaying)
            {
                Audio_Engine.GetComponent<AudioSource>().Play();
            }
            rigidbody.isKinematic = false;
            Func_Hover();
            if (hasStarted)
            {
                Func_Accel();
                Func_Angles();
                Func_SideShift();
                if (Integrity > 20)
                {
                    Func_BarrelRoll();
                }
                Func_SoundVisual();
                Func_Collisions();
            }
        } else
        {
            ShipFlare.GetComponent<TrailRenderer>().time = 0;
            rigidbody.isKinematic = true;
            Audio_Engine.GetComponent<AudioSource>().Stop();
        }
		//rigidbody.AddForce (-Vector3.up * normalGravity);
		publicDistance = frontGroundHeight;
	}
    
    void Func_Input()
    {
        // Resets
        isThrusting = false;
        isBraking = false;
        BRLeft = false;
        BRRight = false;

        if (Input.GetButton("[KB] Thruster") || Input.GetButton("[PAD] Thruster"))
        {
            isThrusting = true;
        }

        steerInput = Input.GetAxis("[KB] Steer");
        if (steerInput == 0)
        {
            steerInput = Input.GetAxis("[PAD] Steer");
        }

        inputAirbrake = Input.GetAxis("[KB] Airbrake");
        if (inputAirbrake == 0)
        {
            inputAirbrake = Input.GetAxis("[PAD] Airbrake");
        }

        if (inputAirbrake == 0)
        {
            inputAirbrake = Input.GetAxis("[PAD] Analog Airbrake");
        }

        // Braking
        if (Input.GetButton("[KB] Brake Left") && Input.GetButton("[KB] Brake Right"))
        {
            isBraking = true;
            inputAirbrake = 0;
        }

        if (Input.GetButton("[PAD] Brake Left") && Input.GetButton("[PAD] Brake Right"))
        {
            isBraking = true;
            inputAirbrake = 0;
        }

        inputPitch = Input.GetAxis("[KB] Pitch");
        if (inputPitch == 0)
        {
            inputPitch = Input.GetAxis("[PAD] Pitch");
        }

        // Barrel Roll
        if (steerInput < 0)
        {
            BRLeft = true;
        }

        if (steerInput > 0)
        {
            BRRight = true;
        }

    }

	void Func_SideShiftInput()
	{
		sideShiftCooldown -= Time.fixedDeltaTime * 8;
		if (sideShiftCooldown < 0)
		{
			if (Input.GetButtonDown("[KB] Brake Left") || Input.GetButtonDown("[PAD] Brake Left"))
			{
				if (!sideShiftLeft && !sideShiftRight && canSideShiftLeft)
				{
					sideShiftTimer = 0;
					sideShiftPressTimer = 0;
					sideShiftLeft = true;
				}
			}
			
			if (sideShiftLeft)
			{
				sideShiftTimer = 0;
				sideShiftRight = false;
				sideShiftLeft = true;
				sideShiftPressTimer += Time.fixedDeltaTime * 30;
				if (sideShiftPressTimer > 0.6f && sideShiftPressTimer < 12)
				{
					if (Input.GetButtonDown("[KB] Brake Left") || Input.GetButtonDown("[PAD] Brake Left"))
					{
						hasSideShiftRight = false;
						hasSideShiftLeft = true;
						sideShiftLeft = false;
					}
				}
				if (sideShiftPressTimer > 8)
				{
					hasSideShiftRight = false;
					hasSideShiftLeft = false;
					sideShiftRight = false;
					sideShiftLeft = false;
				}
			}
			
			if (hasSideShiftLeft)
			{
				canSideShiftLeft = false;
				hasSideShiftRight = false;
				sideShiftRight = false;
				sideShiftTimer += Time.fixedDeltaTime * 50;
				if (sideShiftTimer > 10)
				{
					sideShiftRight = false;
					sideShiftCooldown = 10;
					canSideShiftLeft = true;
					hasSideShiftLeft = false;
				}
			}
			
			if (Input.GetButtonDown("[KB] Brake Right") || Input.GetButtonDown("[PAD] Brake Right"))
			{
				if (!sideShiftLeft && !sideShiftRight && canSideShiftRight)
				{
					sideShiftTimer = 0;
					sideShiftPressTimer = 0;
					sideShiftRight = true;
				}
			}
			
			if (sideShiftRight)
			{
				sideShiftTimer = 0;
				sideShiftLeft = false;
				sideShiftRight = true;
				sideShiftPressTimer += Time.fixedDeltaTime * 30;
				if (sideShiftPressTimer > 0.6f && sideShiftPressTimer < 12)
				{
					if (Input.GetButtonDown("[KB] Brake Right") || Input.GetButtonDown("[PAD] Brake Right"))
					{
						hasSideShiftLeft = false;
						hasSideShiftRight = true;
						sideShiftRight = false;
					}
				}
				if (sideShiftPressTimer > 8)
				{
					hasSideShiftLeft = false;
					hasSideShiftRight = false;
					sideShiftLeft = false;
					sideShiftRight = false;
				}
			}
			
			if (hasSideShiftRight)
			{
				canSideShiftRight = false;
				hasSideShiftLeft = false;
				sideShiftLeft = false;
				sideShiftTimer += Time.fixedDeltaTime * 50;
				if (sideShiftTimer > 10)
				{
					sideShiftRight = false;
					sideShiftCooldown = 10;
					canSideShiftRight = true;
					hasSideShiftRight = false;
				}
			}
		}
		else
		{
			sideShiftLeft = false;
			sideShiftRight = false;
			hasSideShiftLeft = false;
			hasSideShiftRight = false;
		}
	}
	
	void Func_Hover()
	{
		// Resets
		isGrounded = false;
		isHalfGrounded = false;
		
		Vector3 rayDir = Vector3.up;
		
		RaycastHit frontHit;
		if (Physics.Raycast(FrontRayPos.transform.position, -rayDir, out frontHit))
		{
			isRaycast = true;
			frontGroundHeight = frontHit.distance;
			frontGroundHeightInverse = frontGroundHeight - hoverHeight;

			float actualHoverForce = baseHoverForce;
			if (frontGroundHeight < hoverHeight)
			{
				if (frontGroundHeight < hoverHeight / 3)
				{
					rigidbody.angularVelocity = new Vector3(0, rigidbody.angularVelocity.y, 0);
					float hoverForceExtra = ((hoverHeight / 3) - frontGroundHeight) / (hoverHeight / 3);
					Vector3 hoverForceExtraForce = (transform.up * 500 * hoverForceExtra);
					rigidbody.AddForceAtPosition(hoverForceExtraForce, FrontRayPos.transform.position);
				}

				// Wobble
				if (frontGroundHeight < hoverHeight / 1.2f && reachedTerminalVelocity)
				{
					rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
					rigidbody.AddForce(Vector3.up * actualHoverForce * 20);
					wobbleAmount = 1.7f;
					wobbleSpeed = 5;
					wobbleTime = 0;
					reachedTerminalVelocity = false;
				}
				
				frontGrounded = true;
				if (frontHit.collider.gameObject.layer != LayerMask.NameToLayer("Track_Wall"))
				{
					wantedTrackRot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, frontHit.normal), frontHit.normal), Time.deltaTime * 12);
					transform.rotation = Quaternion.Slerp(transform.rotation, wantedTrackRot, Time.deltaTime * 10);
				}
				frontHoverForce = frontGroundHeight - (actualHoverForce + (fallSpeed / 8));
				frontHoverForce *= frontGroundHeightInverse;
				if (isBraking)
				{
					rigidbody.AddForceAtPosition(new Vector3(0, frontHoverForce, 0), FrontRayPos.transform.position);
				} else
				{
                	rigidbody.AddForceAtPosition(transform.up * frontHoverForce, FrontRayPos.transform.position);
				}
				shipWeightDrag = new Vector3(frontHit.normal.x, 0, frontHit.normal.z) * ((baseHoverForce * 3) + (acceleration / 6));
				//rigidbody.AddForceAtPosition(shipWeightDrag, FrontRayPos.transform.position);

            } else 
            {
				// Nose Dive
				float shipAngle = 360 - transform.localEulerAngles.x;
                shipAnglePitchHelp = Mathf.Lerp (shipAnglePitchHelp, currentPitch * 5, Time.deltaTime * 2);
                if (shipAnglePitchHelp < 0)
                {
                    shipAnglePitchHelp = 0;
                }

				if (shipAngle < 70 && shipAngle > 1)
				{
					transform.Rotate(new Vector3(1 * shipAngle, 0, 0) * Time.fixedDeltaTime * shipAnglePitchHelp);
				}

				
				float shipAngleFront = 0 - transform.localEulerAngles.x;
				if (shipAngleFront > -70 && shipAngleFront < 2)
				{
					transform.Rotate(new Vector3(1 * -shipAngleFront, 0, 0) * Time.fixedDeltaTime * shipAnglePitchHelp);
				}
                frontGrounded = false;
            }

        } else
        {
			// Nose Dive
			float shipAngle = 360 - transform.localEulerAngles.x;
            shipAnglePitchHelp = Mathf.Lerp (shipAnglePitchHelp, currentPitch * 5, Time.deltaTime * 2);
            if (shipAnglePitchHelp < 0)
            {
                shipAnglePitchHelp = 0;
            }
			if (shipAngle < 70 && shipAngle > 1)
			{
				transform.Rotate(new Vector3(1 * shipAngle, 0, 0) * Time.fixedDeltaTime  * shipAnglePitchHelp);
			}

            frontGrounded = false;
			isRaycast = false;
        }

        RaycastHit backHit;
        if (Physics.Raycast(BackRayPos.transform.position, -rayDir, out backHit))
        {
            backGroundHeight = backHit.distance;
            backGroundHeightInverse = backGroundHeight - hoverHeight;

            if (backGroundHeight < hoverHeight)
            {
				if (backGroundHeight< hoverHeight / 3)
				{
					//transform.position = new Vector3(transform.position.x, frontHit.point.y + hoverHeight / 3, transform.position.z);
					//rigidbody.AddForce(rayDir * 5, ForceMode.VelocityChange);
					rigidbody.angularVelocity = new Vector3(0, rigidbody.angularVelocity.y, 0);
					float hoverForceExtra = ((hoverHeight / 3) - backGroundHeight) / (hoverHeight / 3);
					Vector3 hoverForceExtraForce = (transform.up * 1500 * hoverForceExtra);
					rigidbody.AddForceAtPosition(hoverForceExtraForce, BackRayPos.transform.position);
				}

				backGrounded = true;
				//transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, backHit.normal), backHit.normal), Time.deltaTime);
                backHoverForce = frontGroundHeight - (baseHoverForce + (fallSpeed / 8));
				backHoverForce *= backGroundHeightInverse;
				rigidbody.AddForceAtPosition(new Vector3(0, backHoverForce, 0), BackRayPos.transform.position);
                
            } else 
            {
                backGrounded = false;
            }
            
        } else
        {
            backGrounded = false;
        }

        if (frontGrounded)
        {
			backGravity = 0;
			fallSpeed = Mathf.Lerp(fallSpeed, trackGravity, Time.deltaTime * 5);
			rigidbody.angularDrag = Mathf.MoveTowards(rigidbody.angularDrag, 1, Time.deltaTime * 20);

            if (currentPitch > 0)
            {
                rigidbody.AddForceAtPosition(-rayDir * (currentPitch * 50), FrontRayPos.transform.position);
            }

            if (currentPitch < 0)
            {
                rigidbody.AddForceAtPosition(-rayDir * (-currentPitch * 50), BackRayPos.transform.position);
            }
        } else
		{
            fallSpeed = Mathf.Lerp(fallSpeed, airGravity, Time.deltaTime * 1.8f);
			if (fallSpeed > airGravity - 150)
			{
				print (fallSpeed);
				reachedTerminalVelocity = true;
			}
			rigidbody.angularDrag = 10;
			float shipAngle = 360 - transform.localEulerAngles.x;
			if (shipAngle < 90 && (frontGroundHeight > hoverHeight + 3))
			{
				backGravity = Mathf.Lerp(backGravity, fallSpeed / 10, Time.deltaTime * 1.8f);
                rigidbody.angularDrag = 10 + backGravity;
				rigidbody.AddForceAtPosition(new Vector3(0,-backGravity,0), BackRayPos.transform.position);
			} else 
            {
				backGravity = 0;
			}

			// set lerp speed to 2 if ship falls too slowly, 1.8 is the default now
        }

        rigidbody.AddForce(-rayDir * fallSpeed);


        // Surface Floors
        if (frontGroundHeight < hoverHeight + 4)
        {
			if (isRaycast)
			{
	            if (frontHit.collider.gameObject.layer == LayerMask.NameToLayer("Track_DirtFloor"))
	            {
	                DirtFloor.GetComponent<ParticleSystem>().enableEmission = true;
	                DirtFloor.transform.position = new Vector3(transform.position.x, frontHit.point.y, transform.position.z);
	            } else
	            {
	                DirtFloor.GetComponent<ParticleSystem>().enableEmission = false;
	            }
    			// Boost Pads and Pickups
    			if (frontHit.collider.gameObject.layer == LayerMask.NameToLayer("Speed"))
    			{
    				boostTimer = 0;
    				boostAmount = 0;
    				boostTimer = 3;
    				boostAmount = 900;
    				Audio_Wind.GetComponent<AudioSource>().Play();
    				directionalBoost = true;
    				BoostDirection = frontHit.collider.gameObject.transform.right;
    			}
			}
        }

		if (!frontGrounded) 
		{
			if (!isRaycast)
			{
				respawnTimer += 1;
				if (respawnTimer > 60)
				{
					acceleration = 0;
					boostAmount = 0;
					boostTimer = 0;
					hasRolled = false;
					rollSuccess = false;
					respawnTimer = -1;
				}
			} else 
			{
				if (frontHit.collider.gameObject.tag != "TrackSegment")
				{
					respawnTimer += 1;
					if (respawnTimer > 60)
					{
						acceleration = 0;
						boostAmount = 0;
						boostTimer = 0;
						hasRolled = false;
						rollSuccess = false;
						respawnTimer = -1;
					}
				} else 
				{
					respawnTimer = 0;
				}
			}

			if (respawnTimer == -1)
			{
				transform.position = respawnPos;
				transform.rotation = respawnRot;
				cameraRespawnOverlay = 2;
				respawnTimer = 0;
			}
		}
	}
	
	void Func_Accel()
	{
		
		// Boosting
		if (boostTimer > 0)
        {
            boostTimer -= Time.fixedDeltaTime * 10;
            currentCameraFOV = Mathf.Lerp(currentCameraFOV, cameraCloseBoostFOV, Time.deltaTime * 7);
            cameraBoostEffect = Mathf.Lerp(cameraBoostEffect, 2, Time.deltaTime * 5);
			cameraCromatic = Mathf.Lerp (cameraCromatic, 30, Time.deltaTime * 12);
        } else
        {
            currentCameraFOV = Mathf.Lerp(currentCameraFOV, cameraCloseNormalFOV, Time.deltaTime * 7);
            cameraBoostEffect = Mathf.Lerp(cameraBoostEffect, 0, Time.deltaTime * 5);
			cameraCromatic = Mathf.Lerp (cameraCromatic, 0, Time.deltaTime * 12);
        }

		cameraRespawnOverlay = Mathf.Lerp (cameraRespawnOverlay, 0, Time.deltaTime * 5);
        
        if (boostTimer < 0)
        {
            boostTimer = 0;
        }
        
		if (boostTimer < 1) 
		{
			boostAmount -= 80;
		}
        if (boostAmount < 0)
        {
            boostAmount = 0;
        }

        if (isBraking)
        {
            isThrusting = false;
        }

        // Camera Boosting Changes
        Camera.main.fieldOfView = currentCameraFOV;
		Camera.main.GetComponent<Vignetting>().chromaticAberration = cameraCromatic;
		Camera.main.GetComponent<CameraMotionBlur> ().previewScale = new Vector3(0,0, cameraBoostEffect);
		Camera.main.GetComponent<ScreenOverlay> ().intensity = cameraRespawnOverlay;
		if (isThrusting)
        {
            currentAccel = Mathf.Lerp(currentAccel, accelCap, Time.deltaTime * 2);
			if (acceleration < 12)
			{
				currentAccel = Mathf.Lerp(currentAccel, accelCap, Time.deltaTime * 8);
			}
            acceleration += currentAccel;
            if (acceleration > maxEngineTorque)
            {
                acceleration = maxEngineTorque;
            }
        } else
        {
            currentAccel = 0;
            if (!isBraking)
            {
				if (acceleration < 150)
				{
                    if (acceleration < 100)
                    {
                        acceleration = Mathf.MoveTowards(acceleration, 0, Time.deltaTime * 10);
                    } else
                    {
					    acceleration = Mathf.MoveTowards(acceleration, 0, Time.deltaTime * 20);
                    }
				} else 
				{
					acceleration -= 2f;
				}
		    }else 
		    {
		        acceleration -= 25;
		    }

        }

		if (acceleration > ((maxEngineTorque) / 2))
		{
            if (isThrusting)
            {
				if (inputAirbrake != 0)
				{
					airBrakeALVV = Mathf.Lerp (airBrakeALVV, 3, Time.deltaTime * 3);
					airbrakeAccelLossVelocity = Mathf.Lerp (airbrakeAccelLossVelocity, 1, Time.deltaTime * airBrakeALVV);
					deaccelAmount = (rotationAmount / airbrakeAccelLossVelocity) * (currentAccel);
				} else 
				{
					airBrakeALVV = 0;
					airbrakeAccelLossVelocity = 4;
                	deaccelAmount = (rotationAmount / 2.3f) * (currentAccel);
				}

                if (deaccelAmount < 0)
                {
                    deaccelAmount = -deaccelAmount;
                }
    			acceleration -= deaccelAmount;
            }
		}


		if (acceleration < 0)
		{
			acceleration = 0;
		}

        if (acceleration > 0)
        {
            rigidbody.AddRelativeForce(Vector3.forward * acceleration);
        }

		if (boostAmount > 0) 
		{
			if (directionalBoost)
			{
				rigidbody.AddRelativeForce(Vector3.forward * boostAmount);
				//rigidbody.AddForce(BoostDirection * boostAmount);
			} else 
			{
				rigidbody.AddRelativeForce(Vector3.forward * boostAmount);
			}
		}

		// Airbrake sensitivity
		airBrakeAmount = (acceleration + (boostAmount / 10))/ 1000;
	}

    void Func_Angles()
    {

        // Airbrake acceleration
        float airBrakeAccel = acceleration / 100;
        airBrakeAccel = Mathf.Clamp(airBrakeAccel, 0, 1);
        bankingInputAirbrake = inputAirbrake;
        inputAirbrake = airBrakeAccel * inputAirbrake;

        if (inputAirbrake != 0)
        {
            turnVelocityLoss = 0;
            turnVelocityLossLoss = 0;
            turnVelocityLossLossLoss = 0;
            actualAirbrakeAmount = steerInput * (inputAirbrake * -airBrakeAmount);
        } else
        {
            actualAirbrakeAmount = 0;
        }

        if (steerInput != 0)
        {
            turnVelocityLoss = 0;
            turnVelocityLossLoss = 0;
            turnVelocityLossLossLoss = 0;
            actualAirBrakeExtra = steerInput * (-bankingInputAirbrake * airBrakeExtra);
            if (inputAirbrake != 0)
            {
				turnVelocity = Mathf.Lerp (turnVelocity, 3.5f, Time.deltaTime * 30);
                rotationAmount = Mathf.Lerp(rotationAmount, steerInput * (turnAmount + actualAirbrakeAmount), Time.deltaTime * turnVelocity);
            } else 
            {
				if ((steerInput < 0 && rotationAmount > 0) || (steerInput > 0 && rotationAmount < 0))
				{
					turnVelocity = Mathf.Lerp (turnVelocity, 2.8f, Time.deltaTime * 16);
				} else
				{
					turnVelocity = Mathf.Lerp (turnVelocity, 2.8f, Time.deltaTime * 8);
				}
                rotationAmount = Mathf.Lerp(rotationAmount, steerInput * turnAmount, Time.deltaTime * turnVelocity);
            }
        } else
        {

            actualAirBrakeExtra = -bankingInputAirbrake * airBrakeExtra;
            if (inputAirbrake != 0)
            {
				turnVelocity = Mathf.Lerp (turnVelocity, 2.8f, Time.deltaTime * 8);
				rotationAmount = Mathf.Lerp(rotationAmount, -inputAirbrake * (turnAmount / 1.2f), Time.deltaTime * turnVelocity);
            } else
            {
				turnVelocity = 0;
				turnVelocityFallOff = Mathf.Lerp(turnVelocityFallOff, 18, Time.deltaTime* turnFallOff);
				turnVelocityLossLoss = Mathf.Lerp(turnVelocityLossLoss, turnVelocityFallOff, Time.deltaTime * turnFallOff);
                turnVelocityLoss = Mathf.Lerp(turnVelocityLoss, turnFallOff, Time.deltaTime * turnVelocityLossLoss);
                rotationAmount = Mathf.Lerp(rotationAmount, 0, Time.deltaTime * turnVelocityLoss);
            }
        }
        
        //actualAirBrakeExtra = steerInput * (-inputAirbrake * airBrakeExtra);
        if (steerInput > 0)
        {
            returnBankSpeed = 0;
            if (currentBank > 0 || currentBank == 0)
            {
                if (currentBank > 15)
                {
                    bankSpeed = Mathf.Lerp(bankSpeed, 5, Time.fixedDeltaTime * 5);
                } else 
				{
					bankSpeed = Mathf.Lerp(bankSpeed, 3, Time.fixedDeltaTime * 5);
				}
            }
            else
            {
                bankSpeed = Mathf.Lerp(bankSpeed, 1.5f, Time.fixedDeltaTime * 5);
            }
        }
        
        if (steerInput < 0)
        {
            returnBankSpeed = 0;
            if (currentBank < 0 || currentBank == 0)
            {
                
                if (currentBank < -15)
                {
                    bankSpeed = Mathf.Lerp(bankSpeed, 5, Time.fixedDeltaTime * 5);
                } else 
				{
					bankSpeed = Mathf.Lerp(bankSpeed, 3, Time.fixedDeltaTime * 5);
				}
            }
            else
            {
                bankSpeed = Mathf.Lerp(bankSpeed, 1.5f, Time.fixedDeltaTime * 5);
            }
        }

        if (steerInput == 0)
        {
            bankSpeed = 0;
            if (currentBank < 0)
            {
                if (currentBank < -25)
                {
                    returnBankSpeed = Mathf.Lerp(returnBankSpeed, 2.2f, Time.fixedDeltaTime * 5);
                } else 
				{
					returnBankSpeed = Mathf.Lerp(returnBankSpeed, 6, Time.fixedDeltaTime * 3);
				}
            } else
            {
                if (currentBank > 25)
                {
                    returnBankSpeed = Mathf.Lerp(returnBankSpeed, 2.2f, Time.fixedDeltaTime * 5);
                } else 
				{
					returnBankSpeed = Mathf.Lerp(returnBankSpeed, 6, Time.fixedDeltaTime * 3);
				}
            }
            
            currentBank = Mathf.Lerp(currentBank, 0 + actualAirBrakeExtra, Time.fixedDeltaTime * returnBankSpeed);
        } else
        {
            currentBank = Mathf.Lerp(currentBank, steerInput * (maxBank + actualAirBrakeExtra), Time.deltaTime * bankSpeed);
        }
		print (returnBankSpeed);
        // Pitch


        //Pitching
        if (inputPitch !=0)
        {
            currentPitch = Mathf.Lerp(currentPitch, inputPitch * PitchMax, Time.fixedDeltaTime * 5);
        } else 
        {
            currentPitch = Mathf.Lerp(currentPitch, 0, Time.fixedDeltaTime * 5);
        }

        if (frontGroundHeight < hoverHeight)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x + currentPitch, transform.eulerAngles.y, transform.eulerAngles.z);
        }

		wobbleTime += Time.deltaTime;
		wobbleSpeed -= Time.deltaTime / 2;
		wobbleSpeed = Mathf.Clamp (wobbleSpeed, 0, 10);
		wobbleAmount -= Time.deltaTime / 2;
		wobbleAmount = Mathf.Clamp (wobbleAmount, 0, 10);

		currentWobble = Mathf.Sin (wobbleTime * wobbleSpeed) * wobbleAmount;

        ShipModelGroup.transform.localRotation = Quaternion.Euler(ShipModelGroup.transform.localEulerAngles.x, ShipModelGroup.transform.localEulerAngles.y, -currentBank + rollState + currentWobble);
        transform.Rotate(Vector3.up * rotationAmount);


    }

    void Func_SideShift()
    {

            if (hasSideShiftLeft)
			{
                if (sideShiftTimer < 10)
                {
                    rigidbody.AddRelativeForce(transform.InverseTransformDirection(-transform.right) * 450);
                }
            }
            
            if (hasSideShiftRight)
            {
                if (sideShiftTimer < 10)
                {

                rigidbody.AddRelativeForce(transform.InverseTransformDirection(transform.right) * 450);
            }
            }
    }

    void Func_BarrelRoll()
    {
        if (frontGroundHeight > hoverHeight) // || respawnTimer > 1
        {
            if (!hasRolled)
            {
                if (BRLeft)
                {
                    LeftRollOne = true;
                }
                
                if (LeftRollOne)
                {
                    rollTimerOne++;
                    if (rollTimerOne > 0.2f && rollTimerOne < 9)
                    {
                        if (BRRight)
                        {
                            LeftRollTwo = true;
                            LeftRollOne = false;
                        }
                        
                    }
                    else
                    {
                        LeftRollOne = false;
                    }
                }
                
                if (LeftRollTwo)
                {
                    rollTimerTwo++;
                    if (rollTimerTwo > 0.2f && rollTimerTwo < 9)
                    {
                        if (BRLeft)
                        {
                            Audio_Roll.GetComponent<AudioSource>().Play();
                            Integrity -= 15;
                            rollSuccess = true;
                            LeftRollOne = false;
                            LeftRollTwo = false;
                        }
                    }
                }
            }
            
            if (rollSuccess)
            {
                if (rollState < 310)
                {
                    rollStateSpeed = 17;
                }
                if (rollState < 50)
                {
                    rollStateSpeed = 12;
                }
				if (rollState < 25)
				{
					rollStateSpeed = 7;
				}
                if (rollStateSpeed > 310)
                {
                    rollStateSpeed = 7;
                    if (rollStateSpeed > 350)
                    {
                        rollStateSpeed = 2;
                    }
                }

                rollState += rollStateSpeed;
                if (rollState > 285)
                {
                    hasRolled = true;
                }
                if (rollState > 360)
                {
                    rollState = 360;
                }
            }
        }
        else
        {
            if (hasRolled)
            {
                boostTimer = 0;
                boostAmount = 0;

                boostTimer = 6;
                boostAmount = 600;
                Audio_Boost.GetComponent<AudioSource>().Play();
                rollState = 0;
				BoostDirection = transform.forward;
				directionalBoost = false;
                hasRolled = false;
            }
            else
            {
                hasRolled = false;
            }
            rollSuccess = false;
            LeftRollOne = false;
            LeftRollTwo = false;
            LeftRollFinal = false;
            rollTimerOne = 0;
            rollTimerTwo = 0;
            if (rollState > 0)
            {
                rollState -= 14;
            }
            if (rollState < 0)
            {
                rollState = 0;
            }
        }
    }

    void Func_SoundVisual()
    {
        // Engine
        if (enginePitch < 0.3f && (Input.GetButtonDown("[KB] Thruster") || Input.GetButtonDown("[PAD] Thruster")))
        {
            Audio_Ignition.GetComponent<AudioSource>().Play();
        }
        enginePitch = acceleration / maxEngineTorque;
        enginePitch = Mathf.Clamp(enginePitch, 0.2f, 1);
        if (isThrusting)
        {
            Audio_Engine.GetComponent<AudioSource>().volume = Mathf.Lerp(Audio_Engine.GetComponent<AudioSource>().volume, 1, Time.deltaTime * 5);
        }
        Audio_Engine.GetComponent<AudioSource>().pitch = enginePitch;
        if (enginePitch < 0.3f)
        {
            Audio_Engine.GetComponent<AudioSource>().volume = Mathf.Lerp(Audio_Engine.GetComponent<AudioSource>().volume, 0, Time.deltaTime);
        }

        // Animation
        if (acceleration < 250)
        {
            if (!AnimationHolder.GetComponent<Animation>().isPlaying)
            {
                AnimationHolder.GetComponent<Animation>().animation["ShipIdle"].speed = 1;
                AnimationHolder.GetComponent<Animation>().Play("ShipIdle");
            }
        }
        else
        {
            AnimationHolder.GetComponent<Animation>().animation["ShipIdle"].time = 0;
            AnimationHolder.GetComponent<Animation>().animation.Stop();
            AnimationHolder.transform.localPosition = Vector3.Lerp(AnimationHolder.transform.localPosition, new Vector3(0, 0, 0), Time.fixedDeltaTime * 10);
            AnimationHolder.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        // Flare state
        if (acceleration > 5)
        {
            ShipFlare.GetComponent<TrailRenderer>().time = 0.2f;
        } else
        {
            ShipFlare.GetComponent<TrailRenderer>().time = 0f;
        }

        // Engine Flare
        if (isThrusting)
        {
            engineFlareSpeed = Mathf.Lerp(engineFlareSpeed, 5 + boostAmount / 100, Time.deltaTime * 5);

        } else
        {
            engineFlareSpeed = Mathf.Lerp(engineFlareSpeed, 0.20f + boostAmount / 100, Time.deltaTime * 3);
        }
        EngineFlare.GetComponent<ParticleSystem>().startSpeed = engineFlareSpeed;

        if (boostAmount > 550)
        {
            boostFlareSpeed = 15;
        } else
        {
            boostFlareSpeed = Mathf.Lerp(boostFlareSpeed, 0, Time.deltaTime * 4);
        }

        BoostFlareLeft.GetComponent<ParticleSystem>().startSpeed = boostFlareSpeed;
        BoostFlareRight.GetComponent<ParticleSystem>().startSpeed = boostFlareSpeed;

        if (boostFlareSpeed < 0.1f)
        {
            BoostFlareLeft.GetComponent<ParticleSystem>().enableEmission = false;
            BoostFlareRight.GetComponent<ParticleSystem>().enableEmission = false;
        } else
        {
            BoostFlareLeft.GetComponent<ParticleSystem>().enableEmission = true;
            BoostFlareRight.GetComponent<ParticleSystem>().enableEmission = true;
        }

        if (boostTimer > 0 || Audio_Ignition.GetComponent<AudioSource>().isPlaying)
        {
            engineLightIntensity = 10;
        } else
        {
            engineLightIntensity = Mathf.Lerp (engineLightIntensity, 2, Time.deltaTime * 5);
        }

        EngineLight.GetComponent<Light>().intensity = engineLightIntensity;
    }

    void OnCollisionEnter(Collision collision)
    {
		rigidbody.angularVelocity = new Vector3 (0, 0, 0);
		Audio_WallScrape.GetComponent<AudioSource> ().Play();
        collideDrag = 30;
        //CollisionDirection = transform.position - collision.contacts[0].point;
        CollisionDirection = -collision.contacts[0].normal;
        // CollisionDirection.Normalize();
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Track_Floor"))
        {
            //Audio_Collide.GetComponent<AudioSource>().Play();
			rigidbody.AddForceAtPosition(transform.up * fallSpeed, collision.transform.position);
        }
        
        shipColliding = true;
        
    }
    
    void OnCollisionExit()
    {
		rigidbody.angularVelocity = new Vector3 (0, 0, 0);
		Audio_WallScrape.GetComponent<AudioSource> ().Stop();
        //Audio_Collide.GetComponent<AudioSource>().Stop();
        shipColliding = false;
        
    }
    void OnCollisionStay(Collision stayCollision)
    {
		rigidbody.angularVelocity = new Vector3 (0, 0, 0);
        //CollisionPointsNo = 
        shipCollisionLayer = null;
        if (stayCollision.gameObject.layer != LayerMask.NameToLayer("Track_Floor"))
        {
            CollisionPointsNo = stayCollision.contacts.GetLength(0);
            hitPoint[0] = stayCollision.contacts[0].point;
            if (CollisionPointsNo == 2)
            {
                hitPoint[1] = stayCollision.contacts[1].point;
            }
            if (CollisionPointsNo == 3)
            {
                hitPoint[2] = stayCollision.contacts[2].point;
            }
        }
        // Keep ship from hitting ground
        if (stayCollision.collider.gameObject.layer == LayerMask.NameToLayer("Track_Floor") || stayCollision.collider.gameObject.layer == LayerMask.NameToLayer("Track_DirtFloor"))
        {
            shipCollisionLayer = "Track_Floor";
            rigidbody.AddForce(new Vector3(0,200,0));
        }
        if (stayCollision.collider.gameObject.layer != LayerMask.NameToLayer("Track_Floor"))
        {
            if (acceleration > (maxEngineTorque / 2))
            {
                acceleration -= (currentAccel + 15);
            }
            Camera.main.GetComponent<User_Camera>().Vibrate();
        }
        
        if (stayCollision.collider.gameObject.layer == LayerMask.NameToLayer("Track_Wall"))
        {
            shipCollisionLayer = "Track_Wall";
        }
		rigidbody.angularVelocity = new Vector3(0, rigidbody.angularVelocity.y, 0);
    }
    
    void Func_Collisions()
    {

        ColDelay = 0;
        while (ColDelay < 6)
        {
            CollisionDelays[ColDelay] = CollisionDelays[ColDelay] + Time.fixedDeltaTime;
            ColDelay++;
        }
        if (!shipColliding)
        {
            SpeedBeforeCollision = new Vector3(transform.InverseTransformDirection(rigidbody.velocity).x, rigidbody.velocity.y, transform.InverseTransformDirection(rigidbody.velocity).z);
        }
        else
        {
            rigidbody.angularVelocity = new Vector3(0 ,rigidbody.angularVelocity.y, 0);
            ShipRigidbodyVelocity = new Vector3(transform.InverseTransformDirection(rigidbody.velocity).x, rigidbody.velocity.y, transform.InverseTransformDirection(rigidbody.velocity).z);
            if (CollisionPointsNo == 2)
            {
                CollisionSideCheck = transform.InverseTransformPoint(hitPoint[0] + hitPoint[1] / 2);
            }
            else
            {
                CollisionSideCheck = transform.InverseTransformPoint(hitPoint[0]);
            }
            
            if ((CollisionSideCheck.x > 0 && ShipRigidbodyVelocity.x > 0) || (CollisionSideCheck.x < 0 && ShipRigidbodyVelocity.x < 0))
            {
                ShipRigidbodyVelocity.x = 0;
                SpeedBeforeCollision.x = 0;
            }
            collisionSpeed = ShipRigidbodyVelocity - SpeedBeforeCollision;
            collisionSpeed = new Vector3(Mathf.Clamp(collisionSpeed.x, -999, 999), Mathf.Clamp(collisionSpeed.y, -999, 0), Mathf.Clamp(collisionSpeed.z, -999, 0));
            if (CollisionSideCheck.z > 0 && collisionSpeed.y < 0.1f &&  CollisionDelays[0] > 0.3f) //&&ismaglock
            {
                //rigidbody.AddForce(Vector3.up * 2000 * collisionSpeed.y * Time.fixedDeltaTime, ForceMode.Impulse);
                CollisionDelays[0] = 0;
            }
            if (CollisionSideCheck.x < 0 && CollisionSideCheck.z > 0 && shipCollisionLayer == "Track_Wall" && CollisionDelays[1] > 0.08f)
            {
                if (transform.InverseTransformDirection(rigidbody.velocity).x < 0)
                {
                    //transform.Rotate(new Vector3(0, Mathf.Abs(ShipRigidbodyVelocity.z), 0) * Time.fixedDeltaTime);
                }
                if (acceleration > (maxEngineTorque / 2))
                {
                    acceleration -= (currentAccel + 12);
                }
                
                CollisionDelays[1] = 0;
            }
            
            if (CollisionSideCheck.x > 0 && CollisionSideCheck.z > 0 && shipCollisionLayer == "Track_Wall" && CollisionDelays[2] > 0.08f)
            {
                if (transform.InverseTransformDirection(rigidbody.velocity).x < 0)
                {
                    //transform.Rotate(new Vector3(0, -Mathf.Abs(ShipRigidbodyVelocity.z), 0) * Time.fixedDeltaTime);
                }
                if (acceleration > (maxEngineTorque / 2))
                {
                    acceleration -= (currentAccel + 12);
                }
                
                CollisionDelays[2] = 0;
            }
            
            if (CollisionSideCheck.x < 0 && CollisionSideCheck.z < 0 && shipCollisionLayer == "Track_Wall" && CollisionDelays[3] > 0.1f)
            {
                if (acceleration > (maxEngineTorque / 2))
                {
                    acceleration -= (currentAccel + 12);
                }
                
                CollisionDelays[3] = 0;
            }
            
            if (CollisionSideCheck.x > 0 && CollisionSideCheck.z < 0 && shipCollisionLayer == "Track_Wall" && CollisionDelays[4] > 0.1f)
            {
                if (acceleration > (maxEngineTorque / 2))
                {
                    acceleration -= (currentAccel + 12);
                }
                
                CollisionDelays[4] = 0;
            }
            
            if (CollisionSideCheck.z > 0 && shipCollisionLayer == "Track_Wall" && 0.3f < Mathf.Clamp(1 - ShipRigidbodyVelocity.z / SpeedBeforeCollision.z, 0, 1) && CollisionDelays[5] > 0.5f)
            {
				rigidbody.angularVelocity = new Vector3 (0, 0, 0);
                float tempVelz = collisionSpeed.z;
                rigidbody.velocity = new Vector3(0, 0, 0);
                rigidbody.AddRelativeForce(new Vector3(0, 0, Mathf.Clamp(tempVelz / 2, -50, 0) * 200) * Time.deltaTime, ForceMode.Impulse);
                acceleration = 0;
                currentAccel = 0;
                boostAmount = 0;
                boostTimer = 0;
                if (acceleration > (maxEngineTorque / 2))
                {
                    acceleration -= (currentAccel + 32);
                }
                
                CollisionDelays[5] = 0;
            }
        }
    }

    void Timed_Stuff()
    {
        Integrity ++;
    }
}
