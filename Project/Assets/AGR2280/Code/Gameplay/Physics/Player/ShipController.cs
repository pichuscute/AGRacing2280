using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	// This enumeration is set when we setup the race
	public RaceInformation.RaceSpeed speedClass;

	// This enumeration is used to determine whether the ship is being controlled manually or being controller
	// by AI
	public enum Controller { Player, AI, AutoPilot }
	public Controller currentController;
	public string editorController;

	// This enumeration is used to check what weapon the player currently has
	public RaceInformation.Pickup currentPickup;

	// Ship type
	public RaceInformation.ShipTypes thisShip;

	// This is used to add an extra value to the FOV when boosting
	public int cameraBoostFOV;

	// Internal Camera
	public int cameraInternalFOV;
	public float cameraInternalLength;

	// Backward Camera
	public int cameraBackwardFOV;
	public float cameraBackwardLength;

	// Far Camera
	public int cameraFarFOV;
	public float cameraFarLookAtLength;
	public float cameraFarLookAtHeight;
	public float cameraFarHeight;
	public float cameraFarLength;
	public float cameraFarSpring;

	// Close Camera
	public int cameraCloseFOV;
	public float cameraCloseLookAtLength;
	public float cameraCloseLookAtHeight;
	public float cameraCloseHeight;
	public float cameraCloseLength;
	public float cameraCloseSpring;

	// Ship Stats
	public float shipShield;
	public float shipEasyShield;

	public float shipheight;
	public float shipWidth;
	public float shipLength;
	public float weightDist;

	public int shipDisplaySpeed;
	public int shipDisplayThrust;
	public int shipDisplayHandling;
	public int shipDisplayShield;

	// Ship Final Config
	public float shipEngineAccelCap;
	public float shipEngineMaxSpeed;
	public float shipEngineGain;
	public float shipEngineFalloff;

	public float shipBrakeAmount;
	public float shipBrakeGain;
	public float shipBrakeFalloff;

	public float shipTurnMax;
	public float shipTurnGain;
	public float shipTurnFalloff;

	public float shipAirbrakeAmount;
	public float shipAirbrakeDrag;
	public float shipAirbrakeGain;
	public float shipAirbrakeFalloff;
	public float shipAirbrakeTurn;
	public float shipAirbrakeGrip;
	public float shipAirbrakeSideShiftAmount;

	public float shipAntiGravGripAir;
	public float shipAntiGravGripGround;
	public float shipAntiGravLandingRebound;
	public float shipAntiGravRebound;
	public float shipAntiGravReboundJumpTime;
	public float shipAntiGravRideHeight;

	public float shipPhysicsFlightGravity;
	public float shipPhysicsMass;
	public float shipPhysicsNormalGravity;
	public float shipPhysicsTrackGravity;

	public float shipPitchAir;
	public float shipPitchGround;
	public float shipPitchDamp;
	public float shipPitchAntiGravHeightAjust;

	// Ship Venom Config
	public float shipVenomEngineAccelCap;
	public float shipVenomEngineMaxSpeed;
	public float shipVenomEngineGain;
	public float shipVenomEngineFalloff;
	
	public float shipVenomBrakeAmount;
	public float shipVenomBrakeGain;
	public float shipVenomBrakeFalloff;
	
	public float shipVenomTurnMax;
	public float shipVenomTurnGain;
	public float shipVenomTurnFalloff;
	
	public float shipVenomAirbrakeAmount;
	public float shipVenomAirbrakeDrag;
	public float shipVenomAirbrakeGain;
	public float shipVenomAirbrakeFalloff;
	public float shipVenomAirbrakeTurn;
	public float shipVenomAirbrakeGrip;
	public float shipVenomAirbrakeSideShiftAmount;
	
	public float shipVenomAntiGravGripAir;
	public float shipVenomAntiGravGripGround;
	public float shipVenomAntiGravLandingRebound;
	public float shipVenomAntiGravRebound;
	public float shipVenomAntiGravReboundJumpTime;
	public float shipVenomAntiGravRideHeight;
	
	public float shipVenomPhysicsFlightGravity;
	public float shipVenomPhysicsMass;
	public float shipVenomPhysicsNormalGravity;
	public float shipVenomPhysicsTrackGravity;
	
	public float shipVenomPitchAir;
	public float shipVenomPitchGround;
	public float shipVenomPitchDamp;
	public float shipVenomPitchAntiGravHeightAjust;

	// Ship Flash Config
	public float shipFlashEngineAccelCap;
	public float shipFlashEngineMaxSpeed;
	public float shipFlashEngineGain;
	public float shipFlashEngineFalloff;
	
	public float shipFlashBrakeAmount;
	public float shipFlashBrakeGain;
	public float shipFlashBrakeFalloff;
	
	public float shipFlashTurnMax;
	public float shipFlashTurnGain;
	public float shipFlashTurnFalloff;
	
	public float shipFlashAirbrakeAmount;
	public float shipFlashAirbrakeDrag;
	public float shipFlashAirbrakeGain;
	public float shipFlashAirbrakeFalloff;
	public float shipFlashAirbrakeTurn;
	public float shipFlashAirbrakeGrip;
	public float shipFlashAirbrakeSideShiftAmount;
	
	public float shipFlashAntiGravGripAir;
	public float shipFlashAntiGravGripGround;
	public float shipFlashAntiGravLandingRebound;
	public float shipFlashAntiGravRebound;
	public float shipFlashAntiGravReboundJumpTime;
	public float shipFlashAntiGravRideHeight;
	
	public float shipFlashPhysicsFlightGravity;
	public float shipFlashPhysicsMass;
	public float shipFlashPhysicsNormalGravity;
	public float shipFlashPhysicsTrackGravity;
	
	public float shipFlashPitchAir;
	public float shipFlashPitchGround;
	public float shipFlashPitchDamp;
	public float shipFlashPitchAntiGravHeightAjust;

	// Ship Rapier Config
	public float shipRapierEngineAccelCap;
	public float shipRapierEngineMaxSpeed;
	public float shipRapierEngineGain;
	public float shipRapierEngineFalloff;
	
	public float shipRapierBrakeAmount;
	public float shipRapierBrakeGain;
	public float shipRapierBrakeFalloff;
	
	public float shipRapierTurnMax;
	public float shipRapierTurnGain;
	public float shipRapierTurnFalloff;
	
	public float shipRapierAirbrakeAmount;
	public float shipRapierAirbrakeDrag;
	public float shipRapierAirbrakeGain;
	public float shipRapierAirbrakeFalloff;
	public float shipRapierAirbrakeTurn;
	public float shipRapierAirbrakeGrip;
	public float shipRapierAirbrakeSideShiftAmount;
	
	public float shipRapierAntiGravGripAir;
	public float shipRapierAntiGravGripGround;
	public float shipRapierAntiGravLandingRebound;
	public float shipRapierAntiGravRebound;
	public float shipRapierAntiGravReboundJumpTime;
	public float shipRapierAntiGravRideHeight;
	
	public float shipRapierPhysicsFlightGravity;
	public float shipRapierPhysicsMass;
	public float shipRapierPhysicsNormalGravity;
	public float shipRapierPhysicsTrackGravity;
	
	public float shipRapierPitchAir;
	public float shipRapierPitchGround;
	public float shipRapierPitchDamp;
	public float shipRapierPitchAntiGravHeightAjust;

	// Ship Phantom Config
	public float shipPhantomEngineAccelCap;
	public float shipPhantomEngineMaxSpeed;
	public float shipPhantomEngineGain;
	public float shipPhantomEngineFalloff;
	
	public float shipPhantomBrakeAmount;
	public float shipPhantomBrakeGain;
	public float shipPhantomBrakeFalloff;
	
	public float shipPhantomTurnMax;
	public float shipPhantomTurnGain;
	public float shipPhantomTurnFalloff;
	
	public float shipPhantomAirbrakeAmount;
	public float shipPhantomAirbrakeDrag;
	public float shipPhantomAirbrakeGain;
	public float shipPhantomAirbrakeFalloff;
	public float shipPhantomAirbrakeTurn;
	public float shipPhantomAirbrakeGrip;
	public float shipPhantomAirbrakeSideShiftAmount;
	
	public float shipPhantomAntiGravGripAir;
	public float shipPhantomAntiGravGripGround;
	public float shipPhantomAntiGravLandingRebound;
	public float shipPhantomAntiGravRebound;
	public float shipPhantomAntiGravReboundJumpTime;
	public float shipPhantomAntiGravRideHeight;
	
	public float shipPhantomPhysicsFlightGravity;
	public float shipPhantomPhysicsMass;
	public float shipPhantomPhysicsNormalGravity;
	public float shipPhantomPhysicsTrackGravity;
	
	public float shipPhantomPitchAir;
	public float shipPhantomPitchGround;
	public float shipPhantomPitchDamp;
	public float shipPhantomPitchAntiGravHeightAjust;

	// Ship Hovering
	float RaycastOffset = 5.5f;
	Vector3 RaycastFrontPos;
	Vector3 RaycastBackPos;
	Vector3 RayCastDirection;

	public bool onMagStrip;
	public float RaycastFrontDistance;
	public float RaycastBackDistance;

	public float shipGravity;
	public float shipFallingSlowdown;

	public bool isGrounded;

	float shipFrontHoverDamping;
	float shipBackHoverDamping;

	float shipBaseHover;
	float shipGravityHover;
	float stopForce;
	float hoverRotToSpeed;
	float hoverRotNowSpeed;

	float backGravity;

	// Ship Acceleration
	public float shipThrust;
	public float shipAccel;
	public bool isThrusting;
	public float deaccelAmount;

	// Boost
	public float shipBoostAmount;
	public float shipBoostTimer;
	public float shipMaxEngineWithBoost;
	float cameraBoostEffect;

	// Ship Rotation
	public float normalForce;
	public float airBrakeForce;
	public float rotationForce;
	float shipAirbrakeTurningSpring;
	float shipAirbrakeTurningVelocity;
	float shipAirbrakeSteeringSpring;
	float shipAirBrakeVelocityAmount;
	float shipRotationGainVelocity;
	float shipRotationGainSpring;
	float shipRotationFalloffVelocity;
	float shipRotationFalloffSpring;

	float shipAirbrakeAccelLossVelocity;
	float shipAirBrakeAccelLossVelocityDamper;

	Quaternion wantedTrackRot;

	float shipAirbrakeFalloffSpring;
	float shipAirbrakeFalloffVelocity;

	float groundAngularDrag;

	// Pitch
	public float currentPitch;
	public float shipAnglePitchHelp;

	// Banking
	public float shipCurrentBank;
	float shipMaxBank = 40;
	float shipAirBrakeBankExtra = 10;
	public float ShipActualAirBrakeBankExtra;
	public float shipBankSpeed;
	float shipReturnBankSpeed;
	float shipActualBank;

	// Wobble
	float shipWobbleAmount;
	float shipWobbleSpeed;
	float shipWobbleTime;
	public float shipCurrentWobble;
	bool shipReachedTerminalVelocity;

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

	// Inputs
	public float inputSteer;
	public float inputPitch;
	public float inputAirbrake;
	public bool inputIsBraking;
	bool BRLeft;
	bool BRRight;

	// Objects
	GameObject CameraObject;
	public GameObject ModelGroup;

	public GameObject Audio_BoostPad;
	public GameObject Audio_Boost;
	public GameObject Audio_ABLeft;
	public GameObject Audio_ABRight;
	public GameObject Audio_Ignite;
	public GameObject Audio_Roll;
	public GameObject Audio_FloorHit;
	public GameObject Audio_WallScrape;

	public GameObject EngineLight;
	public GameObject ScrapeParticles;

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
	

	void Start () 
	{
		// Create Camera
		if (GameObject.Find("Main Camera"))
		{
			CameraObject = GameObject.Find("Main Camera").gameObject;
			CameraObject.name = "Client Camera";
			CameraObject.AddComponent<ShipChaseCam>();
			CameraObject.GetComponent<ShipChaseCam>().targetShip = this.gameObject;
		}

		SetupValues();

	}

	void Update()
	{
		if (currentController == Controller.Player)
		{
			GetPlayerInput();
		}

		SideShiftInput ();
	}
	void FixedUpdate () 
	{
		ShipGravity();
		ShipTurning();
		ShipAcceleration();
		ShipSideShift();
		BarrelRoll();
		ShipExtra();
	}

	void GetPlayerInput()
	{
		// Get Steering Input

		isThrusting = false;
		inputIsBraking = false;
		BRLeft = false;
		BRRight = false;

		inputSteer = Input.GetAxis("[KB] Steer");
		if (inputSteer == 0)
		{
			inputSteer = Input.GetAxis("[PAD] Steer");
		}

		// Barrel Roll
		if (inputSteer < 0)
		{
			BRLeft = true;
		}
		
		if (inputSteer> 0)
		{
			BRRight = true;
		}

		// Get Airbrake Input
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
			inputIsBraking = true;
			inputAirbrake = 0;
		}
		
		if (Input.GetButton("[PAD] Brake Left") && Input.GetButton("[PAD] Brake Right"))
		{
			inputIsBraking= true;
			inputAirbrake = 0;
		}

		// Acceleration
		if (Input.GetButton("[KB] Thruster") || Input.GetButton("[PAD] Thruster"))
		{
			isThrusting = true;
		}

		// Pitching 
		inputPitch = Input.GetAxis("[KB] Pitch");
		if (inputPitch == 0)
		{
			inputPitch = Input.GetAxis("[PAD] Pitch");
		}

		// Airbrake sound
		if (-inputAirbrake < 0)
		{
			if (!Audio_ABLeft.GetComponent<AudioSource>().isPlaying)
			{
				Audio_ABLeft.GetComponent<AudioSource>().Play();
			}
		} else if (!inputIsBraking)
		{
			if (Audio_ABLeft.GetComponent<AudioSource>().isPlaying)
			{
				Audio_ABLeft.GetComponent<AudioSource>().Stop();
			}
		}

		if (-inputAirbrake > 0)
		{
			if (!Audio_ABRight.GetComponent<AudioSource>().isPlaying)
			{
				Audio_ABRight.GetComponent<AudioSource>().Play();
			}
		} else if (!inputIsBraking)
		{
			if (Audio_ABRight.GetComponent<AudioSource>().isPlaying)
			{
				Audio_ABRight.GetComponent<AudioSource>().Stop();
			}
		}

		if (inputIsBraking)
		{
			if (!Audio_ABLeft.GetComponent<AudioSource>().isPlaying)
			{
				Audio_ABLeft.GetComponent<AudioSource>().Play();
			}

			if (!Audio_ABRight.GetComponent<AudioSource>().isPlaying)
			{
				Audio_ABRight.GetComponent<AudioSource>().Play();
			}
		}
	}

	void SideShiftInput()
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

	void ShipGravity()
	{
		//Pitching
		if (inputPitch !=0)
		{
			if (isGrounded)
			{
				currentPitch = Mathf.Lerp(currentPitch, inputPitch * shipPitchGround, Time.fixedDeltaTime * 5);
				//transform.rotation = Quaternion.Euler(transform.eulerAngles.x + currentPitch, transform.eulerAngles.y, transform.eulerAngles.z);
			} else
			{
				currentPitch = Mathf.Lerp(currentPitch, inputPitch * shipPitchAir, Time.fixedDeltaTime * 5);
			}

		} else 
		{
			currentPitch = Mathf.Lerp(currentPitch, 0, Time.fixedDeltaTime * 5);
		}

		// Reset Grounded Bool
		isGrounded = false;

		// Update Raycast positions
		if (weightDist > 0)
		{
			RaycastFrontPos = transform.TransformPoint(0,0, RaycastOffset + weightDist);
		} else 
		{
			RaycastFrontPos = transform.TransformPoint(0,0, RaycastOffset);
		}
		if (weightDist < 0)
		{
			RaycastBackPos = transform.TransformPoint(0,0, -(RaycastOffset + weightDist));
		} else 
		{
			RaycastBackPos = transform.TransformPoint(0,0, -RaycastOffset);
		}

		// First raycast
		RaycastHit frontHit;
		if (Physics.Raycast(RaycastFrontPos, -Vector3.up, out frontHit))
		{
			RaycastFrontDistance = frontHit.distance;
			if (frontHit.distance < shipAntiGravRideHeight)
			{
				isGrounded = true;
				shipFrontHoverDamping = Mathf.Lerp(shipFrontHoverDamping, 10, Time.deltaTime * shipAntiGravLandingRebound);
				shipAnglePitchHelp = 0;
				rigidbody.angularDrag = groundAngularDrag;

				transform.rotation = Quaternion.Euler(transform.eulerAngles.x + currentPitch, transform.eulerAngles.y, transform.eulerAngles.z);

				// Rotate to track normals
				if (frontHit.collider.gameObject.layer != LayerMask.NameToLayer("Track_Wall"))
				{
					//wantedTrackRot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, frontHit.normal), frontHit.normal), Time.deltaTime * 12);
					//transform.rotation = Quaternion.Slerp(transform.rotation, wantedTrackRot, Time.deltaTime * 10);

					wantedTrackRot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, frontHit.normal), frontHit.normal), Time.deltaTime * hoverRotToSpeed);
					transform.rotation = Quaternion.Slerp(transform.rotation, wantedTrackRot, Time.deltaTime * hoverRotNowSpeed);
				}


				// Apply Force
				//rigidbody.AddForceAtPosition(new Vector3(0, hoverForce, 0), RaycastFrontPos);
				float hoverDistance = shipAntiGravRideHeight;
				float hoverForce = hoverDistance - frontHit.distance;
				
				float springCost = hoverForce * shipBaseHover;
				Vector3 spring = RaycastFrontPos - frontHit.point;
				float length = spring.magnitude;
				float displacement = length - (shipAntiGravRideHeight);
				
				Vector3 springN = spring / length;
				Vector3 restoreForce = springN*(displacement*springCost);
				rigidbody.AddForceAtPosition(new Vector3(0, -restoreForce.y, 0), RaycastFrontPos);
				print (restoreForce.y);

				// Complete stop
				
				if (RaycastFrontDistance < shipAntiGravRideHeight)
				{
					rigidbody.angularVelocity = new Vector3(0, 0, 0);
					if (shipReachedTerminalVelocity)
					{
						// Wobble
						shipWobbleAmount = 2f;
						shipWobbleSpeed = 6;
						shipWobbleTime = 0;
						Audio_FloorHit.GetComponent<AudioSource>().Play();
						rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
						rigidbody.AddForceAtPosition(-Vector3.up * 500, RaycastFrontPos);
						rigidbody.AddForce(Vector3.up * 80);
						shipReachedTerminalVelocity = false;
					}
				}

			} else
			{	
				rigidbody.angularDrag = Mathf.Lerp(rigidbody.angularDrag, 100, Time.deltaTime * 5);
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
				shipFrontHoverDamping = 20;
			}

			// Pads
			if (frontHit.distance < shipAntiGravRideHeight + 3)
			{
				if (frontHit.collider.gameObject.layer == LayerMask.NameToLayer("Speed"))
				{
					shipBoostTimer = 3;
					shipBoostAmount = 500;
					Audio_BoostPad.GetComponent<AudioSource>().Play();
				}
			}
		} else
		{
			rigidbody.angularDrag = Mathf.Lerp(rigidbody.angularDrag, 100, Time.deltaTime * 5);
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
			shipFrontHoverDamping = 20;
			isGrounded = false;
		}

		RaycastHit backHit;
		if (Physics.Raycast(RaycastBackPos, -Vector3.up, out backHit))
		{
			backGravity = 0;
			RaycastFrontDistance = backHit.distance;
			if (backHit.distance < shipAntiGravRideHeight)
			{
				// Complete stop

				if (RaycastFrontDistance < shipAntiGravRideHeight)
				{
					rigidbody.angularVelocity = new Vector3(0, 0, 0);

				}
				
				// Apply Force
				//rigidbody.AddForceAtPosition(new Vector3(0, hoverForce, 0), RaycastBackPos);
				float hoverDistance = shipAntiGravRideHeight;
				float hoverForce = hoverDistance - backHit.distance;

				float springCost = hoverForce * shipBaseHover;
				Vector3 spring = RaycastBackPos - backHit.point;
				float length = spring.magnitude;
				float displacement = length - (shipAntiGravRideHeight);
				
				Vector3 springN = spring / length;
				Vector3 restoreForce = springN*(displacement*springCost);
				rigidbody.AddForceAtPosition(new Vector3(0, -restoreForce.y, 0), RaycastBackPos);
			} else 
			{
				shipBackHoverDamping = 20;
			}
		} else
		{
			shipBackHoverDamping = 20;
		}

		if (isGrounded)
		{
			shipGravity = Mathf.Lerp(shipGravity, shipPhysicsTrackGravity, Time.deltaTime * shipPhysicsNormalGravity);
			shipFallingSlowdown = 0;
		} else 
		{
			shipFallingSlowdown = Mathf.Lerp( shipFallingSlowdown, 1.5f, Time.deltaTime * 8);
			shipGravity = Mathf.Lerp(shipGravity, shipPhysicsFlightGravity * (rigidbody.drag + shipPhysicsMass), Time.deltaTime * shipFallingSlowdown);

			float shipAngle = 360 - transform.localEulerAngles.x;
			if (shipAngle < 90 && (RaycastFrontDistance > shipAntiGravRideHeight + 3))
			{
				backGravity = Mathf.Lerp(backGravity, shipGravity / 10, Time.deltaTime * 1.8f);
				rigidbody.AddForceAtPosition(new Vector3(0,-backGravity,0), RaycastBackPos);
			} else 
			{
				backGravity = 0;
			}
		}

		// Terminal Velocity Check
		if (shipGravity > (((shipPhysicsFlightGravity * (rigidbody.drag + shipPhysicsMass)) / 4) * 2.8f))
		{
			shipReachedTerminalVelocity = true;
		}

		// Apply Gravity
		rigidbody.AddForce(new Vector3(0,-shipGravity,0));

		// Test Turbo

		if (Input.GetButtonDown("[PAD] Fire") || Input.GetButtonDown("[KB] Fire"))
		{
			shipBoostTimer = 7;
			shipBoostAmount = 800;
			Audio_Boost.GetComponent<AudioSource>().Play();
		}

		// Wobble
		shipWobbleTime += Time.deltaTime;
		shipWobbleSpeed -= Time.deltaTime / 2;
		shipWobbleSpeed = Mathf.Clamp (shipWobbleSpeed, 0, 10);
		shipWobbleAmount -= Time.deltaTime / 2;
		shipWobbleAmount = Mathf.Clamp (shipWobbleAmount, 0, 10);
		
		shipCurrentWobble = Mathf.Sin (shipWobbleTime * shipWobbleSpeed) * shipWobbleAmount;
	}

	void ShipTurning()
	{
		// Airbrake Velocity
		shipAirBrakeVelocityAmount = shipThrust / (shipEngineMaxSpeed * (shipAirbrakeTurn / 25));
		if (inputSteer != 0)
		{
			shipRotationFalloffSpring = 300;
			shipRotationGainSpring = Mathf.Lerp(shipRotationGainSpring, 130, Time.deltaTime * (shipTurnGain / 80));
			if (isGrounded)
			{
				normalForce = Mathf.Lerp(normalForce, inputSteer * shipTurnMax, Time.deltaTime * (shipTurnGain / shipRotationGainSpring));
			} else 
			{
				normalForce = Mathf.Lerp(normalForce, (inputSteer * shipTurnMax) * 2, Time.deltaTime * (shipTurnGain / shipRotationGainSpring) / 5);
			}

		} else 
		{
			shipRotationGainSpring = 400;
			shipRotationFalloffSpring = Mathf.Lerp(shipRotationFalloffSpring, 50 , Time.deltaTime * (shipTurnFalloff / 200));
			//normalForce = Mathf.Lerp(normalForce, 0, Time.deltaTime * shipRotationFalloffVelocity);
			normalForce = Mathf.Lerp(normalForce, 0, Time.deltaTime * (shipTurnFalloff / shipRotationFalloffSpring));
		}
		if (inputAirbrake != 0)
		{
			shipAirbrakeFalloffVelocity = 0;
			shipAirbrakeFalloffSpring = 0;
		

			if (inputSteer < 0)
			{
				ShipActualAirBrakeBankExtra = Mathf.Lerp (ShipActualAirBrakeBankExtra, inputAirbrake * shipAirBrakeBankExtra, Time.deltaTime * 10);
			} else
			{
				ShipActualAirBrakeBankExtra = Mathf.Lerp (ShipActualAirBrakeBankExtra, -inputAirbrake * shipAirBrakeBankExtra, Time.deltaTime * 10);
			}

			if (inputSteer != 0)
			{
				shipAirbrakeSteeringSpring = Mathf.Lerp (shipAirbrakeSteeringSpring, 50, Time.deltaTime * (shipAirbrakeDrag / 50));
				if (isGrounded)
				{
					airBrakeForce = Mathf.Lerp(airBrakeForce, -inputAirbrake * shipAirBrakeVelocityAmount, Time.deltaTime * (shipAirbrakeGain / shipAirbrakeSteeringSpring));
				} else
				{
					airBrakeForce = Mathf.Lerp(airBrakeForce, (-inputAirbrake * shipAirBrakeVelocityAmount) * 1.4f, Time.deltaTime * (shipAirbrakeGain / shipAirbrakeSteeringSpring));
				}
				shipAirbrakeTurningSpring = 0;
			} else 
			{
				shipAirbrakeSteeringSpring = 300;
				shipAirbrakeTurningSpring = Mathf.Lerp (shipAirbrakeTurningSpring, (shipAirbrakeGain / 100), Time.deltaTime * (shipAirbrakeGain / 100));
				if (isGrounded)
				{
					airBrakeForce = Mathf.Lerp(airBrakeForce, -inputAirbrake * (shipAirBrakeVelocityAmount / 2), Time.deltaTime * shipAirbrakeTurningSpring );
				} else
				{
					airBrakeForce = Mathf.Lerp(airBrakeForce, -inputAirbrake * (shipAirBrakeVelocityAmount / 2) * 1.4f, Time.deltaTime * shipAirbrakeTurningSpring);
				}
			}

			// Apply airbrake drag
			if (isGrounded)
			{
				//rigidbody.AddForceAtPosition(transform.right * inputAirbrake * (shipAirBrakeVelocityAmount * shipAirbrakeDrag), transform.transform.TransformPoint(0,0, -200));
				//transform.RotateAround(RaycastFrontPos, Vector3.up, (airBrakeForce / 65));
			} else 
			{
				//rigidbody.AddForceAtPosition(transform.right * inputAirbrake * ((shipAirBrakeVelocityAmount * shipAirbrakeDrag * 2)), transform.transform.TransformPoint(0,0, -200));
				//transform.RotateAround(RaycastFrontPos, Vector3.up, ((airBrakeForce * 1.4f / 65)));
			}

		} else
		{
			shipAirbrakeSteeringSpring = 400;
			shipAirbrakeTurningVelocity = 0;
			shipAirbrakeTurningSpring = 0;
			ShipActualAirBrakeBankExtra = 0;
			shipAirbrakeFalloffVelocity = Mathf.Lerp(shipAirbrakeFalloffVelocity, 10, Time.deltaTime * 6);
			shipAirbrakeFalloffSpring = Mathf.Lerp(shipAirbrakeFalloffVelocity, shipAirbrakeFalloff / 20, Time.deltaTime * shipAirbrakeFalloffVelocity);
			airBrakeForce = Mathf.Lerp(airBrakeForce, 0, Time.deltaTime * shipAirbrakeFalloffSpring);

		}

		rotationForce = normalForce + airBrakeForce;

		// Banking
		if (inputSteer > 0)
		{
			shipReturnBankSpeed = 0;
			if (shipCurrentBank > 0 ||shipCurrentBank == 0)
			{
				if (shipCurrentBank > 15)
				{
					shipBankSpeed = Mathf.Lerp(shipBankSpeed, 5, Time.fixedDeltaTime * 5);
				} else 
				{
					shipBankSpeed = Mathf.Lerp(shipBankSpeed, 3, Time.fixedDeltaTime * 5);
				}
			}
			else
			{
				shipBankSpeed = Mathf.Lerp(shipBankSpeed, 1.2f, Time.fixedDeltaTime * 5);
			}
		}
		
		if (inputSteer < 0)
		{
			shipReturnBankSpeed = 0;
			if (shipCurrentBank < 0 || shipCurrentBank == 0)
			{
				
				if (shipCurrentBank < -15)
				{
					shipBankSpeed = Mathf.Lerp(shipBankSpeed, 5, Time.fixedDeltaTime * 5);
				} else 
				{
					shipBankSpeed = Mathf.Lerp(shipBankSpeed, 3, Time.fixedDeltaTime * 5);
				}
			}
			else
			{
				shipBankSpeed = Mathf.Lerp(shipBankSpeed, 1.2f, Time.fixedDeltaTime * 5);
			}
		}
		
		if (inputSteer == 0)
		{
			shipBankSpeed = 0;
			if (shipCurrentBank < 0)
			{
				if (shipCurrentBank < -25)
				{
					shipReturnBankSpeed = Mathf.Lerp(shipReturnBankSpeed, 4f, Time.fixedDeltaTime * 2);
				} else 
				{
					shipReturnBankSpeed = Mathf.Lerp(shipReturnBankSpeed, 2.5f, Time.fixedDeltaTime * 3);
				}
			} else
			{
				if (shipCurrentBank > 25)
				{
					shipReturnBankSpeed = Mathf.Lerp(shipReturnBankSpeed, 4f, Time.fixedDeltaTime * 2);
				} else 
				{
					shipReturnBankSpeed = Mathf.Lerp(shipReturnBankSpeed, 2.5f, Time.fixedDeltaTime * 3);
				}
			}
			
			shipCurrentBank = Mathf.Lerp(shipCurrentBank, 0 + ShipActualAirBrakeBankExtra, Time.fixedDeltaTime * shipReturnBankSpeed);
		} else
		{
			shipCurrentBank = Mathf.Lerp(shipCurrentBank, inputSteer * (shipMaxBank + ShipActualAirBrakeBankExtra), Time.deltaTime * shipBankSpeed);
		}

		// Apply Rotatons
		ModelGroup.transform.localRotation = Quaternion.Euler(ModelGroup.transform.localEulerAngles.x, ModelGroup.transform.localEulerAngles.y, -shipCurrentBank + rollState + shipCurrentWobble);
		transform.Rotate(Vector3.up * rotationForce);
	}

	void ShipAcceleration()
	{
		// Boosting
		if (shipBoostTimer > 0)
		{
			shipBoostTimer -= Time.fixedDeltaTime * 10;
		}

		// Clamp Boost so it can't go below 0
		if (shipBoostTimer < 0)
		{
			shipBoostTimer = 0;
		}
		
		if (inputIsBraking)
		{
			isThrusting = false;
		}

		// Lerp Ships max engine speed back to the max
		if (shipBoostTimer > 1) 
		{
			shipMaxEngineWithBoost = shipEngineMaxSpeed + shipBoostAmount;
		} else 
		{
			shipMaxEngineWithBoost = Mathf.MoveTowards(shipMaxEngineWithBoost, shipEngineMaxSpeed, Time.deltaTime * shipEngineFalloff);
		}
		// Camera Boosting Changes
		if (isThrusting)
		{
			if (shipBoostTimer > 0)
			{
				shipAccel = shipEngineAccelCap;
			} else 
			{
				shipAccel = Mathf.Lerp(shipAccel, shipEngineAccelCap, Time.deltaTime * ((shipEngineGain / 100) / 2));
			}

			if (shipThrust < 12)
			{
				shipAccel = Mathf.Lerp(shipAccel, shipEngineAccelCap, Time.deltaTime * (shipEngineGain / 100));
			}
			shipThrust += shipAccel;
			if (shipThrust > shipMaxEngineWithBoost)
			{
				shipThrust = shipMaxEngineWithBoost;
			}
		} else
		{
			// There is a lot of stuff happening here. The reason for it is that the drag in Unity is set to 5 so 
			//  the ship will very quickly grind to a halt, so I have to create my own artifical anti-drag
			if (shipBoostTimer > 0)
			{
				shipAccel = shipEngineAccelCap;
			} else 
			{
				shipAccel = 0;
			}
			if (!inputIsBraking)
			{
				if (shipThrust < 150)
				{
					if (shipThrust < 100)
					{
						shipThrust = Mathf.MoveTowards(shipThrust, 0, Time.deltaTime * 10);
					} else
					{
						shipThrust = Mathf.MoveTowards(shipThrust, 0, Time.deltaTime * 20);
					}
				} else 
				{
					if (shipThrust > (shipEngineMaxSpeed / 2))
					{
						shipThrust -= 8f;
					} else 
					{
						shipThrust -= 2f;
					}
				}
			}else 
			{
				shipThrust -= 25;
			}
			
		}
		
		if (shipThrust > ((shipEngineMaxSpeed) / 2))
		{
			if (inputAirbrake != 0)
			{
				if (isGrounded)
				{
					shipAirBrakeAccelLossVelocityDamper = Mathf.Lerp (shipAirBrakeAccelLossVelocityDamper, shipAntiGravGripAir * 1.2f, Time.deltaTime * 2);
				} else 
				{
					shipAirBrakeAccelLossVelocityDamper = Mathf.Lerp (shipAirBrakeAccelLossVelocityDamper, shipAntiGravGripGround * rigidbody.drag, Time.deltaTime * shipAirbrakeGrip);
				}
				shipAirbrakeAccelLossVelocity = Mathf.Lerp (shipAirbrakeAccelLossVelocity, 1, Time.deltaTime * shipAirBrakeAccelLossVelocityDamper);
				deaccelAmount = (rotationForce / shipAirbrakeAccelLossVelocity) * (shipAccel);
			} else 
			{
				shipAirBrakeAccelLossVelocityDamper = 0;
				shipAirbrakeAccelLossVelocity = 4;
				deaccelAmount = (rotationForce * (shipTurnMax * 0.2f)) * (shipAccel);
			}
			
			if (deaccelAmount < 0)
			{
				deaccelAmount = -deaccelAmount;
			}
			shipThrust -= deaccelAmount;
		}
		
		
		if (shipThrust < 0)
		{
			shipThrust = 0;
		}
		
		if (shipThrust > 0 || shipBoostTimer > 0)
		{
			if (shipBoostTimer > 0)
			{
				if (shipThrust < shipBoostAmount)
				{
					shipThrust = shipBoostAmount;
				}
			}
			rigidbody.AddRelativeForce(Vector3.forward * shipThrust);
		}

		/*
		if (shipBoostAmount > 0) 
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
		*/
	}

	void ShipSideShift()
	{
		
		if (hasSideShiftLeft)
		{
			if (sideShiftTimer < 10)
			{
				rigidbody.AddRelativeForce(transform.InverseTransformDirection(-transform.right) * shipAirbrakeSideShiftAmount);
			}
		}
		
		if (hasSideShiftRight)
		{
			if (sideShiftTimer < 10)
			{
				
				rigidbody.AddRelativeForce(transform.InverseTransformDirection(transform.right) * shipAirbrakeSideShiftAmount);
			}
		}
	}

	void ShipExtra()
	{
		if (shipAccel < 0.1f)
		{
			if (Input.GetButtonDown("[KB] Thruster") || Input.GetButtonDown("[PAD] Thruster"))
			{
				Audio_Ignite.GetComponent<AudioSource>().Play();
			}
		}

		// Engine Light Intensity;
		if (shipBoostTimer > 0)
		{
			EngineLight.GetComponent<Light>().intensity = Mathf.Lerp(EngineLight.GetComponent<Light>().intensity,7, Time.deltaTime * 12);
		} else 
		{
			EngineLight.GetComponent<Light>().intensity = Mathf.Lerp(EngineLight.GetComponent<Light>().intensity,1, Time.deltaTime * 5);
		}

	}

	void BarrelRoll()
	{
		if (!isGrounded) // || respawnTimer > 1
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
							//Integrity -= 15;
							rollSuccess = true;
							LeftRollOne = false;
							LeftRollTwo = false;
						}
					}
				}
			}
			
			if (rollSuccess)
			{
				if (rollState < 350)
				{
					rollStateSpeed = 8;
				}
				if (rollState < 320)
				{
					rollStateSpeed = 12;
				}
				if (rollState < 300)
				{
					rollStateSpeed = 19;
				}
				if (rollState < 90)
				{
					rollStateSpeed = 14;
				}
				if (rollState < 25)
				{
					rollStateSpeed = 7;
				}
				if (rollState < 8)
				{
					rollStateSpeed = 4;
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
				
				shipBoostTimer = 4;
				shipBoostAmount = 600;
				Audio_Boost.GetComponent<AudioSource>().Play();
				rollState = 0;
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

	void OnCollisionEnter(Collision other)
	{
		ScrapeParticles.transform.parent = null;
		Audio_WallScrape.GetComponent<AudioSource>().Play();
		ScrapeParticles.GetComponent<ParticleSystem>().enableEmission = true;
		ScrapeParticles.transform.position = other.contacts[0].point;

	}

	void OnCollisionStay(Collision other)
	{
		ScrapeParticles.transform.position = other.contacts[0].point;
		ScrapeParticles.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
	}

	void OnCollisionExit()
	{
		Audio_WallScrape.GetComponent<AudioSource>().Stop();
		ScrapeParticles.GetComponent<ParticleSystem>().enableEmission = false;

	}

	void SetupValues()
	{

		if (speedClass == RaceInformation.RaceSpeed.Venom)
		{
			shipEngineAccelCap = shipVenomEngineAccelCap;
			shipEngineMaxSpeed = shipVenomEngineMaxSpeed;
			shipEngineGain = shipVenomEngineGain;
			shipEngineFalloff = shipVenomEngineFalloff;
			
			shipBrakeAmount = shipVenomBrakeAmount;
			shipBrakeGain = shipVenomBrakeGain;
			shipBrakeFalloff = shipVenomBrakeFalloff;
			
			shipTurnMax = shipVenomTurnMax;
			shipTurnGain = shipVenomTurnGain;
			shipTurnFalloff = shipVenomTurnFalloff;
			
			shipAirbrakeAmount = shipVenomAirbrakeAmount;
			shipAirbrakeDrag = shipVenomAirbrakeDrag;
			shipAirbrakeGain = shipVenomAirbrakeGain;
			shipAirbrakeFalloff = shipVenomAirbrakeFalloff;
			shipAirbrakeTurn = shipVenomAirbrakeTurn;
			shipAirbrakeGrip = shipVenomAirbrakeGrip;
			shipAirbrakeSideShiftAmount = shipVenomAirbrakeSideShiftAmount;
			
			shipAntiGravGripAir = shipVenomAntiGravGripAir;
			shipAntiGravGripGround = shipVenomAntiGravGripGround;
			shipAntiGravLandingRebound = shipVenomAntiGravLandingRebound;
			shipAntiGravRebound = shipVenomAntiGravRebound;
			shipAntiGravReboundJumpTime = shipVenomAntiGravReboundJumpTime;
			shipAntiGravRideHeight = shipVenomAntiGravRideHeight;
			
			shipPhysicsFlightGravity = shipVenomPhysicsFlightGravity;
			shipPhysicsMass = shipVenomPhysicsMass;
			shipPhysicsNormalGravity = shipVenomPhysicsNormalGravity;
			shipPhysicsTrackGravity = shipVenomPhysicsTrackGravity;
			
			shipPitchAir = shipVenomPitchAir;
			shipPitchGround = shipVenomPitchGround;
			shipPitchDamp = shipVenomPitchDamp;
			shipPitchAntiGravHeightAjust = shipVenomPitchAntiGravHeightAjust;
		}

		if (speedClass == RaceInformation.RaceSpeed.Flash)
		{
			shipEngineAccelCap = shipFlashEngineAccelCap;
			shipEngineMaxSpeed = shipFlashEngineMaxSpeed;
			shipEngineGain = shipFlashEngineGain;
			shipEngineFalloff = shipFlashEngineFalloff;
			
			shipBrakeAmount = shipFlashBrakeAmount;
			shipBrakeGain = shipFlashBrakeGain;
			shipBrakeFalloff = shipFlashBrakeFalloff;
			
			shipTurnMax = shipFlashTurnMax;
			shipTurnGain = shipFlashTurnGain;
			shipTurnFalloff = shipFlashTurnFalloff;
			
			shipAirbrakeAmount = shipFlashAirbrakeAmount;
			shipAirbrakeDrag = shipFlashAirbrakeDrag;
			shipAirbrakeGain = shipFlashAirbrakeGain;
			shipAirbrakeFalloff = shipFlashAirbrakeFalloff;
			shipAirbrakeTurn = shipFlashAirbrakeTurn;
			shipAirbrakeGrip = shipFlashAirbrakeGrip;
			shipAirbrakeSideShiftAmount = shipFlashAirbrakeSideShiftAmount;
			
			shipAntiGravGripAir = shipFlashAntiGravGripAir;
			shipAntiGravGripGround = shipFlashAntiGravGripGround;
			shipAntiGravLandingRebound = shipFlashAntiGravLandingRebound;
			shipAntiGravRebound = shipFlashAntiGravRebound;
			shipAntiGravReboundJumpTime = shipFlashAntiGravReboundJumpTime;
			shipAntiGravRideHeight = shipFlashAntiGravRideHeight;
			
			shipPhysicsFlightGravity = shipFlashPhysicsFlightGravity;
			shipPhysicsMass = shipFlashPhysicsMass;
			shipPhysicsNormalGravity = shipFlashPhysicsNormalGravity;
			shipPhysicsTrackGravity = shipFlashPhysicsTrackGravity;
			
			shipPitchAir = shipFlashPitchAir;
			shipPitchGround = shipFlashPitchGround;
			shipPitchDamp = shipFlashPitchDamp;
			shipPitchAntiGravHeightAjust = shipFlashPitchAntiGravHeightAjust;
		}

		if (speedClass == RaceInformation.RaceSpeed.Rapier)
		{
			shipEngineAccelCap = shipRapierEngineAccelCap;
			shipEngineMaxSpeed = shipRapierEngineMaxSpeed;
			shipEngineGain = shipRapierEngineGain;
			shipEngineFalloff = shipRapierEngineFalloff;
			
			shipBrakeAmount = shipRapierBrakeAmount;
			shipBrakeGain = shipRapierBrakeGain;
			shipBrakeFalloff = shipRapierBrakeFalloff;
			
			shipTurnMax = shipRapierTurnMax;
			shipTurnGain = shipRapierTurnGain;
			shipTurnFalloff = shipRapierTurnFalloff;
			
			shipAirbrakeAmount = shipRapierAirbrakeAmount;
			shipAirbrakeDrag = shipRapierAirbrakeDrag;
			shipAirbrakeGain = shipRapierAirbrakeGain;
			shipAirbrakeFalloff = shipRapierAirbrakeFalloff;
			shipAirbrakeTurn = shipRapierAirbrakeTurn;
			shipAirbrakeGrip = shipRapierAirbrakeGrip;
			shipAirbrakeSideShiftAmount = shipRapierAirbrakeSideShiftAmount;
			
			shipAntiGravGripAir = shipRapierAntiGravGripAir;
			shipAntiGravGripGround = shipRapierAntiGravGripGround;
			shipAntiGravLandingRebound = shipRapierAntiGravLandingRebound;
			shipAntiGravRebound = shipRapierAntiGravRebound;
			shipAntiGravReboundJumpTime = shipRapierAntiGravReboundJumpTime;
			shipAntiGravRideHeight = shipRapierAntiGravRideHeight;
			
			shipPhysicsFlightGravity = shipRapierPhysicsFlightGravity;
			shipPhysicsMass = shipRapierPhysicsMass;
			shipPhysicsNormalGravity = shipRapierPhysicsNormalGravity;
			shipPhysicsTrackGravity = shipRapierPhysicsTrackGravity;
			
			shipPitchAir = shipRapierPitchAir;
			shipPitchGround = shipRapierPitchGround;
			shipPitchDamp = shipRapierPitchDamp;
			shipPitchAntiGravHeightAjust = shipRapierPitchAntiGravHeightAjust;
		}

		if (speedClass == RaceInformation.RaceSpeed.Phantom)
		{
			shipEngineAccelCap = shipPhantomEngineAccelCap;
			shipEngineMaxSpeed = shipPhantomEngineMaxSpeed;
			shipEngineGain = shipPhantomEngineGain;
			shipEngineFalloff = shipPhantomEngineFalloff;
			
			shipBrakeAmount = shipPhantomBrakeAmount;
			shipBrakeGain = shipPhantomBrakeGain;
			shipBrakeFalloff = shipPhantomBrakeFalloff;
			
			shipTurnMax = shipPhantomTurnMax;
			shipTurnGain = shipPhantomTurnGain;
			shipTurnFalloff = shipPhantomTurnFalloff;
			
			shipAirbrakeAmount = shipPhantomAirbrakeAmount;
			shipAirbrakeDrag = shipPhantomAirbrakeDrag;
			shipAirbrakeGain = shipPhantomAirbrakeGain;
			shipAirbrakeFalloff = shipPhantomAirbrakeFalloff;
			shipAirbrakeTurn = shipPhantomAirbrakeTurn;
			shipAirbrakeGrip = shipPhantomAirbrakeGrip;
			shipAirbrakeSideShiftAmount = shipPhantomAirbrakeSideShiftAmount;
			
			shipAntiGravGripAir = shipPhantomAntiGravGripAir;
			shipAntiGravGripGround = shipPhantomAntiGravGripGround;
			shipAntiGravLandingRebound = shipPhantomAntiGravLandingRebound;
			shipAntiGravRebound = shipPhantomAntiGravRebound;
			shipAntiGravReboundJumpTime = shipPhantomAntiGravReboundJumpTime;
			shipAntiGravRideHeight = shipPhantomAntiGravRideHeight;
			
			shipPhysicsFlightGravity = shipPhantomPhysicsFlightGravity;
			shipPhysicsMass = shipPhantomPhysicsMass;
			shipPhysicsNormalGravity = shipPhantomPhysicsNormalGravity;
			shipPhysicsTrackGravity = shipPhantomPhysicsTrackGravity;
			
			shipPitchAir = shipPhantomPitchAir;
			shipPitchGround = shipPhantomPitchGround;
			shipPitchDamp = shipPhantomPitchDamp;
			shipPitchAntiGravHeightAjust = shipPhantomPitchAntiGravHeightAjust;
		}

		if (thisShip == RaceInformation.ShipTypes.Agility)
		{
			shipBaseHover = 100;
			shipGravityHover = 100;
			stopForce = 1000;

			hoverRotToSpeed = 20;
			hoverRotNowSpeed = 18;

			groundAngularDrag = 5;
		}

		if (thisShip == RaceInformation.ShipTypes.Speed)
		{
			shipBaseHover = 75;
			shipGravityHover = 1;
			stopForce = 800;

			hoverRotToSpeed = 16;
			hoverRotNowSpeed = 18;

			groundAngularDrag = 1;
		}

		if (thisShip == RaceInformation.ShipTypes.Fighter)
		{
			shipBaseHover = 45;
			shipGravityHover = 10;
			stopForce = 500;

			hoverRotToSpeed = 20;
			hoverRotNowSpeed = 18;

			groundAngularDrag = 5;
		}

	}
}
