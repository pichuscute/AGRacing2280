using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ShipController))]
public class AGRTools_ShipInspector : Editor {
	

    public enum EditorController { Player, AI, AutoPilot }
    public EditorController editorCurrentController;

    public Vector2 CameraVarScroll;

    bool dependancyOpen;
    bool VenomSettings;
    Vector2 VenomSettingsScroll;

    bool FlashSettings;
    Vector2 FlashSettingsScroll;

    bool RapierSettings;
    Vector2 RapierSettingsScroll;

    bool PhantomSettings;
    Vector2 PhantomSettingsScroll;
	public override void OnInspectorGUI()
	{
		ShipController classTarget = (ShipController)target;

		classTarget.speedClass = (RaceInformation.RaceSpeed)EditorGUILayout.EnumPopup("Unloaded Speed Class:", classTarget.speedClass);

        editorCurrentController = (EditorController)EditorGUILayout.EnumPopup("Unloaded Ship Controller:", editorCurrentController);
        classTarget.editorController = editorCurrentController.ToString();

		classTarget.thisShip = (RaceInformation.ShipTypes)EditorGUILayout.EnumPopup("Ship Type:", classTarget.thisShip);

        dependancyOpen = EditorGUILayout.Foldout(dependancyOpen, "Dependancies");
        if (dependancyOpen)
        {
            GUILayout.Label("Camera Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
            CameraVarScroll = EditorGUILayout.BeginScrollView(CameraVarScroll, GUILayout.Height(150));

            GUILayout.Label("Generic", EditorStyles.largeLabel);
            classTarget.cameraBoostFOV = EditorGUILayout.IntField(new GUIContent("Boost FOV:","How much to increase the FOV when boosting"), classTarget.cameraBoostFOV);

            GUILayout.Label("Internal Camera", EditorStyles.largeLabel);
            classTarget.cameraInternalFOV = EditorGUILayout.IntField(new GUIContent("Field of View:","How much you can see in degrees"), classTarget.cameraInternalFOV);
            classTarget.cameraInternalLength = EditorGUILayout.FloatField(new GUIContent("Length:", "The Z offset of the camera from the centre of the craft"), classTarget.cameraInternalLength);

            GUILayout.Label("Backwards Camera", EditorStyles.largeLabel);
            classTarget.cameraBackwardFOV = EditorGUILayout.IntField(new GUIContent("Field of View:","How much you can see in degrees"), classTarget.cameraBackwardFOV);
            classTarget.cameraBackwardLength = EditorGUILayout.FloatField(new GUIContent("Length:", "The Z offset of the camera from the centre of the craft"), classTarget.cameraBackwardLength);

            GUILayout.Label("Far Camera", EditorStyles.largeLabel);
            classTarget.cameraFarFOV = EditorGUILayout.IntField(new GUIContent("Field of View:","How much you can see in degrees"), classTarget.cameraFarFOV);
            classTarget.cameraFarLength = EditorGUILayout.FloatField(new GUIContent("Offset Length:", "The distance to position the camera behind the ship"), classTarget.cameraFarLength);
            classTarget.cameraFarHeight = EditorGUILayout.FloatField(new GUIContent("Offset Height:", "The height to position the camera above the ship"), classTarget.cameraFarHeight);
            classTarget.cameraFarLookAtLength = EditorGUILayout.FloatField(new GUIContent("Lookat Length:", "The z offset from the ship the camera will look at"), classTarget.cameraFarLookAtLength);
            classTarget.cameraFarLookAtHeight = EditorGUILayout.FloatField(new GUIContent("Lookat Height:", "The y offset from the ship the camera will look at"), classTarget.cameraFarLookAtHeight);
            classTarget.cameraFarSpring = EditorGUILayout.FloatField(new GUIContent("Spring:", "The damping of the camera"), classTarget.cameraFarSpring);

            GUILayout.Label("Close Camera", EditorStyles.largeLabel);
            classTarget.cameraCloseFOV = EditorGUILayout.IntField(new GUIContent("Field of View:","How much you can see in degrees"), classTarget.cameraCloseFOV);
            classTarget.cameraCloseLength = EditorGUILayout.FloatField(new GUIContent("Offset Length:", "The distance to position the camera behind the ship"), classTarget.cameraCloseLength);
            classTarget.cameraCloseHeight = EditorGUILayout.FloatField(new GUIContent("Offset Height:", "The height to position the camera above the ship"), classTarget.cameraCloseHeight);
            classTarget.cameraCloseLookAtLength = EditorGUILayout.FloatField(new GUIContent("Lookat Length:", "The z offset from the ship the camera will look at"), classTarget.cameraCloseLookAtLength);
            classTarget.cameraCloseLookAtHeight = EditorGUILayout.FloatField(new GUIContent("Lookat Height:", "The y offset from the ship the camera will look at"), classTarget.cameraCloseLookAtHeight);
            classTarget.cameraCloseSpring = EditorGUILayout.FloatField(new GUIContent("Spring:", "The damping of the camera"), classTarget.cameraCloseSpring);
        
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.Label("Ship Settings", EditorStyles.boldLabel);

			classTarget.weightDist = EditorGUILayout.FloatField(new GUIContent("Weight Distribution:", "The balance of the hover casts"), classTarget.weightDist);

			classTarget.Audio_BoostPad = (GameObject)EditorGUILayout.ObjectField("Boost Pad Audio",classTarget.Audio_BoostPad, typeof(GameObject), true);
			classTarget.Audio_ABLeft = (GameObject)EditorGUILayout.ObjectField("Airbrake Left Audio",classTarget.Audio_ABLeft, typeof(GameObject), true);
			classTarget.Audio_ABRight = (GameObject)EditorGUILayout.ObjectField("Airbrake Right Audio",classTarget.Audio_ABRight, typeof(GameObject), true);
			classTarget.Audio_Ignite = (GameObject)EditorGUILayout.ObjectField("Ignition Audio",classTarget.Audio_Ignite, typeof(GameObject), true);
			classTarget.Audio_Roll = (GameObject)EditorGUILayout.ObjectField("BR Audio",classTarget.Audio_Roll, typeof(GameObject), true);
			classTarget.Audio_Boost = (GameObject)EditorGUILayout.ObjectField("Turbo Audio",classTarget.Audio_Boost, typeof(GameObject), true);
			classTarget.Audio_FloorHit = (GameObject)EditorGUILayout.ObjectField("Floor Hit Audio",classTarget.Audio_FloorHit, typeof(GameObject), true);
			classTarget.Audio_WallScrape = (GameObject)EditorGUILayout.ObjectField("Wall Scrape Audio",classTarget.Audio_WallScrape, typeof(GameObject), true);

			EditorGUILayout.Separator();

			classTarget.EngineLight = (GameObject)EditorGUILayout.ObjectField("Engine Light",classTarget.EngineLight, typeof(GameObject), true);
			classTarget.ScrapeParticles = (GameObject)EditorGUILayout.ObjectField("Scrape Particles",classTarget.ScrapeParticles, typeof(GameObject), true);

            VenomSettings = EditorGUILayout.Foldout(VenomSettings, "Venom Settings");
            if (VenomSettings)
            {
                EditorGUILayout.BeginVertical();
                VenomSettingsScroll = EditorGUILayout.BeginScrollView(VenomSettingsScroll, GUILayout.Height(150));

                GUILayout.Label("Engine", EditorStyles.largeLabel);
                classTarget.shipVenomEngineAccelCap = EditorGUILayout.FloatField(new GUIContent("Engine Accel Cap:", "The maximum value the thrust acceleration can reach"), classTarget.shipVenomEngineAccelCap);
                classTarget.shipVenomEngineMaxSpeed = EditorGUILayout.FloatField(new GUIContent("Engine Max Speed:", "The ships top speed"), classTarget.shipVenomEngineMaxSpeed);
                classTarget.shipVenomEngineGain = EditorGUILayout.FloatField(new GUIContent("Engine Gain:", "The speed that the acceleration will increase"), classTarget.shipVenomEngineGain);
                classTarget.shipVenomEngineFalloff = EditorGUILayout.FloatField(new GUIContent("Engine Falloff:", "The base speed that the ship will loose speed"), classTarget.shipVenomEngineFalloff);

                GUILayout.Label("Brake", EditorStyles.largeLabel);
                classTarget.shipVenomBrakeAmount = EditorGUILayout.FloatField(new GUIContent("Brake Amount:", "How fast the brakes should slow down the craft"), classTarget.shipVenomBrakeAmount);
                classTarget.shipVenomBrakeGain = EditorGUILayout.FloatField(new GUIContent("Brake Gain:", "The speed it takes the brakes to reach their max"), classTarget.shipVenomBrakeGain);
                classTarget.shipVenomBrakeFalloff = EditorGUILayout.FloatField(new GUIContent("Brake Falloff:", "The speed it takes the brake force to stop"), classTarget.shipVenomBrakeFalloff);

                GUILayout.Label("Turn", EditorStyles.largeLabel);
                classTarget.shipVenomTurnMax = EditorGUILayout.FloatField(new GUIContent("Turn Amount:", "The max angle per frame the ship should turn"), classTarget.shipVenomTurnMax);
                classTarget.shipVenomTurnGain = EditorGUILayout.FloatField(new GUIContent("Turn Gain:", "How fast the ship reaches it's max APS"), classTarget.shipVenomTurnGain);
                classTarget.shipVenomTurnFalloff = EditorGUILayout.FloatField(new GUIContent("Turn Falloff:","How fast the ship looses speed (higher values causes a springy effect)"), classTarget.shipVenomTurnFalloff);

                GUILayout.Label("Airbrakes", EditorStyles.largeLabel);
                classTarget.shipVenomAirbrakeAmount = EditorGUILayout.FloatField(new GUIContent("Airbrake Amount:", "The base force of the airbrakes"), classTarget.shipVenomAirbrakeAmount);
                classTarget.shipVenomAirbrakeDrag = EditorGUILayout.FloatField(new GUIContent("Airbrake Drag:", "Airbrake dampening"), classTarget.shipVenomAirbrakeDrag);
                classTarget.shipVenomAirbrakeGain = EditorGUILayout.FloatField(new GUIContent("Airbrake Gain:", "The speed at which the airbrakes reach their max force"), classTarget.shipVenomAirbrakeGain);
                classTarget.shipVenomAirbrakeFalloff = EditorGUILayout.FloatField(new GUIContent("Airbrake Falloff:", "The speed at which the airbrakes zero back out"), classTarget.shipVenomAirbrakeFalloff);
                classTarget.shipVenomAirbrakeTurn = EditorGUILayout.FloatField(new GUIContent("Airbrake Turn:", "This variable needs a description"), classTarget.shipVenomAirbrakeTurn);
                classTarget.shipVenomAirbrakeGrip = EditorGUILayout.FloatField(new GUIContent("Airbrake Slidegrip:", "The dampening of sideshifting"), classTarget.shipVenomAirbrakeGrip);
                classTarget.shipVenomAirbrakeSideShiftAmount = EditorGUILayout.FloatField(new GUIContent("Airbrake Sideshift:", "The force of sideshifts"), classTarget.shipVenomAirbrakeSideShiftAmount);
                
                GUILayout.Label("Anti-Gravity", EditorStyles.largeLabel);
                classTarget.shipVenomAntiGravGripAir = EditorGUILayout.FloatField(new GUIContent("Air grip:", "How sticky the ship is when in the air"), classTarget.shipVenomAntiGravGripAir);
                classTarget.shipVenomAntiGravGripGround = EditorGUILayout.FloatField(new GUIContent("Ground grip:", "How sticky the ship is when on the ground"), classTarget.shipVenomAntiGravGripGround);
                classTarget.shipVenomAntiGravLandingRebound = EditorGUILayout.FloatField(new GUIContent("Landing Rebound:", "Hover dampening"), classTarget.shipVenomAntiGravLandingRebound);
                classTarget.shipVenomAntiGravRebound = EditorGUILayout.FloatField(new GUIContent("Rebound:", "Hover dampening if falling from a height"), classTarget.shipVenomAntiGravRebound);
                classTarget.shipVenomAntiGravReboundJumpTime = EditorGUILayout.FloatField(new GUIContent("Rebound Jump Time:", "Springyness of the hover"), classTarget.shipVenomAntiGravReboundJumpTime);
                classTarget.shipVenomAntiGravRideHeight = EditorGUILayout.FloatField(new GUIContent("Ride Height:", "How far off the ground (in units) to hover"), classTarget.shipVenomAntiGravRideHeight);

                GUILayout.Label("Physics", EditorStyles.largeLabel);
                classTarget.shipVenomPhysicsFlightGravity = EditorGUILayout.FloatField(new GUIContent("Flight Gravity:", "The gravity when off track"), classTarget.shipVenomPhysicsFlightGravity);
                classTarget.shipVenomPhysicsTrackGravity = EditorGUILayout.FloatField(new GUIContent("Track Gravity:", "The gravity when on track"), classTarget.shipVenomPhysicsTrackGravity);
				classTarget.shipVenomPhysicsNormalGravity = EditorGUILayout.FloatField(new GUIContent("Normal Gravity:", "The speed which gravity changes"), classTarget.shipVenomPhysicsNormalGravity);
                classTarget.shipVenomPhysicsMass = EditorGUILayout.FloatField(new GUIContent("Mass:", "This should really be kept at 1"), classTarget.shipVenomPhysicsMass);
                
                GUILayout.Label("Pitch", EditorStyles.largeLabel);
                classTarget.shipVenomPitchAir = EditorGUILayout.FloatField(new GUIContent("Air Pitch:", "How much to pitch the nose if in the air"), classTarget.shipVenomPitchAir);
                classTarget.shipVenomPitchGround = EditorGUILayout.FloatField(new GUIContent("Ground Pitch:", "How much to pitch the nose if on the ground"), classTarget.shipVenomPitchGround);
                classTarget.shipVenomPitchDamp = EditorGUILayout.FloatField(new GUIContent("Pitch Damp:", "Pitch dampening"), classTarget.shipVenomPitchGround);
                classTarget.shipVenomPitchAntiGravHeightAjust = EditorGUILayout.FloatField(new GUIContent("Height Ajust:", "Should ideally be left at 1"), classTarget.shipVenomPitchAntiGravHeightAjust);

                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndVertical();
            }
            FlashSettings = EditorGUILayout.Foldout(FlashSettings, "Flash Settings");
                if (FlashSettings)
                {
                    EditorGUILayout.BeginVertical();
                    FlashSettingsScroll = EditorGUILayout.BeginScrollView(FlashSettingsScroll, GUILayout.Height(150));
                    
                    GUILayout.Label("Engine", EditorStyles.largeLabel);
                    classTarget.shipFlashEngineAccelCap = EditorGUILayout.FloatField(new GUIContent("Engine Accel Cap:", "The maximum value the thrust acceleration can reach"), classTarget.shipFlashEngineAccelCap);
                    classTarget.shipFlashEngineMaxSpeed = EditorGUILayout.FloatField(new GUIContent("Engine Max Speed:", "The ships top speed"), classTarget.shipFlashEngineMaxSpeed);
                    classTarget.shipFlashEngineGain = EditorGUILayout.FloatField(new GUIContent("Engine Gain:", "The speed that the acceleration will increase"), classTarget.shipFlashEngineGain);
                    classTarget.shipFlashEngineFalloff = EditorGUILayout.FloatField(new GUIContent("Engine Falloff:", "The base speed that the ship will loose speed"), classTarget.shipFlashEngineFalloff);
                    
                    GUILayout.Label("Brake", EditorStyles.largeLabel);
                    classTarget.shipFlashBrakeAmount = EditorGUILayout.FloatField(new GUIContent("Brake Amount:", "How fast the brakes should slow down the craft"), classTarget.shipFlashBrakeAmount);
                    classTarget.shipFlashBrakeGain = EditorGUILayout.FloatField(new GUIContent("Brake Gain:", "The speed it takes the brakes to reach their max"), classTarget.shipFlashBrakeGain);
                    classTarget.shipFlashBrakeFalloff = EditorGUILayout.FloatField(new GUIContent("Brake Falloff:", "The speed it takes the brake force to stop"), classTarget.shipFlashBrakeFalloff);
                    
                    GUILayout.Label("Turn", EditorStyles.largeLabel);
                    classTarget.shipFlashTurnMax = EditorGUILayout.FloatField(new GUIContent("Turn Amount:", "The max angle per frame the ship should turn"), classTarget.shipFlashTurnMax);
                    classTarget.shipFlashTurnGain = EditorGUILayout.FloatField(new GUIContent("Turn Gain:", "How fast the ship reaches it's max APS"), classTarget.shipFlashTurnGain);
                    classTarget.shipFlashTurnFalloff = EditorGUILayout.FloatField(new GUIContent("Turn Falloff:","How fast the ship looses speed (higher values causes a springy effect)"), classTarget.shipFlashTurnFalloff);
                    
                    GUILayout.Label("Airbrakes", EditorStyles.largeLabel);
                    classTarget.shipFlashAirbrakeAmount = EditorGUILayout.FloatField(new GUIContent("Airbrake Amount:", "The base force of the airbrakes"), classTarget.shipFlashAirbrakeAmount);
                    classTarget.shipFlashAirbrakeDrag = EditorGUILayout.FloatField(new GUIContent("Airbrake Drag:", "Airbrake dampening"), classTarget.shipFlashAirbrakeDrag);
                    classTarget.shipFlashAirbrakeGain = EditorGUILayout.FloatField(new GUIContent("Airbrake Gain:", "The speed at which the airbrakes reach their max force"), classTarget.shipFlashAirbrakeGain);
                    classTarget.shipFlashAirbrakeFalloff = EditorGUILayout.FloatField(new GUIContent("Airbrake Falloff:", "The speed at which the airbrakes zero back out"), classTarget.shipFlashAirbrakeFalloff);
                    classTarget.shipFlashAirbrakeTurn = EditorGUILayout.FloatField(new GUIContent("Airbrake Turn:", "This variable needs a description"), classTarget.shipFlashAirbrakeTurn);
                    classTarget.shipFlashAirbrakeGrip = EditorGUILayout.FloatField(new GUIContent("Airbrake Slidegrip:", "The dampening of sideshifting"), classTarget.shipFlashAirbrakeGrip);
                    classTarget.shipFlashAirbrakeSideShiftAmount = EditorGUILayout.FloatField(new GUIContent("Airbrake Sideshift:", "The force of sideshifts"), classTarget.shipFlashAirbrakeSideShiftAmount);
                    
                    GUILayout.Label("Anti-Gravity", EditorStyles.largeLabel);
                    classTarget.shipFlashAntiGravGripAir = EditorGUILayout.FloatField(new GUIContent("Air grip:", "How sticky the ship is when in the air"), classTarget.shipFlashAntiGravGripAir);
                    classTarget.shipFlashAntiGravGripGround = EditorGUILayout.FloatField(new GUIContent("Ground grip:", "How sticky the ship is when on the ground"), classTarget.shipFlashAntiGravGripGround);
                    classTarget.shipFlashAntiGravLandingRebound = EditorGUILayout.FloatField(new GUIContent("Landing Rebound:", "Hover dampening"), classTarget.shipFlashAntiGravLandingRebound);
                    classTarget.shipFlashAntiGravRebound = EditorGUILayout.FloatField(new GUIContent("Rebound:", "Hover dampening if falling from a height"), classTarget.shipFlashAntiGravRebound);
                    classTarget.shipFlashAntiGravReboundJumpTime = EditorGUILayout.FloatField(new GUIContent("Rebound Jump Time:", "Springyness of the hover"), classTarget.shipFlashAntiGravReboundJumpTime);
                    classTarget.shipFlashAntiGravRideHeight = EditorGUILayout.FloatField(new GUIContent("Ride Height:", "How far off the ground (in units) to hover"), classTarget.shipFlashAntiGravRideHeight);
                    
                    GUILayout.Label("Physics", EditorStyles.largeLabel);
                    classTarget.shipFlashPhysicsFlightGravity = EditorGUILayout.FloatField(new GUIContent("Flight Gravity:", "The gravity when off track"), classTarget.shipFlashPhysicsFlightGravity);
                    classTarget.shipFlashPhysicsTrackGravity = EditorGUILayout.FloatField(new GUIContent("Track Gravity:", "The gravity when on track"), classTarget.shipFlashPhysicsTrackGravity);
				classTarget.shipFlashPhysicsNormalGravity = EditorGUILayout.FloatField(new GUIContent("Normal Gravity:", "The speed which gravity changes"), classTarget.shipFlashPhysicsNormalGravity);
                    classTarget.shipFlashPhysicsMass = EditorGUILayout.FloatField(new GUIContent("Mass:", "This should really be kept at 1"), classTarget.shipFlashPhysicsMass);
                    
                    GUILayout.Label("Pitch", EditorStyles.largeLabel);
                    classTarget.shipFlashPitchAir = EditorGUILayout.FloatField(new GUIContent("Air Pitch:", "How much to pitch the nose if in the air"), classTarget.shipFlashPitchAir);
                    classTarget.shipFlashPitchGround = EditorGUILayout.FloatField(new GUIContent("Ground Pitch:", "How much to pitch the nose if on the ground"), classTarget.shipFlashPitchGround);
                    classTarget.shipFlashPitchDamp = EditorGUILayout.FloatField(new GUIContent("Pitch Damp:", "Pitch dampening"), classTarget.shipFlashPitchGround);
                    classTarget.shipFlashPitchAntiGravHeightAjust = EditorGUILayout.FloatField(new GUIContent("Height Ajust:", "Should ideally be left at 1"), classTarget.shipFlashPitchAntiGravHeightAjust);
                    
                    EditorGUILayout.EndScrollView();
                    EditorGUILayout.EndVertical();
            }
        
            RapierSettings = EditorGUILayout.Foldout(RapierSettings, "Rapier Settings");
            if (RapierSettings)
            {
                EditorGUILayout.BeginVertical();
                RapierSettingsScroll = EditorGUILayout.BeginScrollView(RapierSettingsScroll, GUILayout.Height(150));
                
                GUILayout.Label("Engine", EditorStyles.largeLabel);
                classTarget.shipRapierEngineAccelCap = EditorGUILayout.FloatField(new GUIContent("Engine Accel Cap:", "The maximum value the thrust acceleration can reach"), classTarget.shipRapierEngineAccelCap);
                classTarget.shipRapierEngineMaxSpeed = EditorGUILayout.FloatField(new GUIContent("Engine Max Speed:", "The ships top speed"), classTarget.shipRapierEngineMaxSpeed);
                classTarget.shipRapierEngineGain = EditorGUILayout.FloatField(new GUIContent("Engine Gain:", "The speed that the acceleration will increase"), classTarget.shipRapierEngineGain);
                classTarget.shipRapierEngineFalloff = EditorGUILayout.FloatField(new GUIContent("Engine Falloff:", "The base speed that the ship will loose speed"), classTarget.shipRapierEngineFalloff);
                
                GUILayout.Label("Brake", EditorStyles.largeLabel);
                classTarget.shipRapierBrakeAmount = EditorGUILayout.FloatField(new GUIContent("Brake Amount:", "How fast the brakes should slow down the craft"), classTarget.shipRapierBrakeAmount);
                classTarget.shipRapierBrakeGain = EditorGUILayout.FloatField(new GUIContent("Brake Gain:", "The speed it takes the brakes to reach their max"), classTarget.shipRapierBrakeGain);
                classTarget.shipRapierBrakeFalloff = EditorGUILayout.FloatField(new GUIContent("Brake Falloff:", "The speed it takes the brake force to stop"), classTarget.shipRapierBrakeFalloff);
                
                GUILayout.Label("Turn", EditorStyles.largeLabel);
                classTarget.shipRapierTurnMax = EditorGUILayout.FloatField(new GUIContent("Turn Amount:", "The max angle per frame the ship should turn"), classTarget.shipRapierTurnMax);
                classTarget.shipRapierTurnGain = EditorGUILayout.FloatField(new GUIContent("Turn Gain:", "How fast the ship reaches it's max APS"), classTarget.shipRapierTurnGain);
                classTarget.shipRapierTurnFalloff = EditorGUILayout.FloatField(new GUIContent("Turn Falloff:","How fast the ship looses speed (higher values causes a springy effect)"), classTarget.shipRapierTurnFalloff);
                
                GUILayout.Label("Airbrakes", EditorStyles.largeLabel);
                classTarget.shipRapierAirbrakeAmount = EditorGUILayout.FloatField(new GUIContent("Airbrake Amount:", "The base force of the airbrakes"), classTarget.shipRapierAirbrakeAmount);
                classTarget.shipRapierAirbrakeDrag = EditorGUILayout.FloatField(new GUIContent("Airbrake Drag:", "Airbrake dampening"), classTarget.shipRapierAirbrakeDrag);
                classTarget.shipRapierAirbrakeGain = EditorGUILayout.FloatField(new GUIContent("Airbrake Gain:", "The speed at which the airbrakes reach their max force"), classTarget.shipRapierAirbrakeGain);
                classTarget.shipRapierAirbrakeFalloff = EditorGUILayout.FloatField(new GUIContent("Airbrake Falloff:", "The speed at which the airbrakes zero back out"), classTarget.shipRapierAirbrakeFalloff);
                classTarget.shipRapierAirbrakeTurn = EditorGUILayout.FloatField(new GUIContent("Airbrake Turn:", "This variable needs a description"), classTarget.shipRapierAirbrakeTurn);
                classTarget.shipRapierAirbrakeGrip = EditorGUILayout.FloatField(new GUIContent("Airbrake Slidegrip:", "The dampening of sideshifting"), classTarget.shipRapierAirbrakeGrip);
                classTarget.shipRapierAirbrakeSideShiftAmount = EditorGUILayout.FloatField(new GUIContent("Airbrake Sideshift:", "The force of sideshifts"), classTarget.shipRapierAirbrakeSideShiftAmount);
                
                GUILayout.Label("Anti-Gravity", EditorStyles.largeLabel);
                classTarget.shipRapierAntiGravGripAir = EditorGUILayout.FloatField(new GUIContent("Air grip:", "How sticky the ship is when in the air"), classTarget.shipRapierAntiGravGripAir);
                classTarget.shipRapierAntiGravGripGround = EditorGUILayout.FloatField(new GUIContent("Ground grip:", "How sticky the ship is when on the ground"), classTarget.shipRapierAntiGravGripGround);
                classTarget.shipRapierAntiGravLandingRebound = EditorGUILayout.FloatField(new GUIContent("Landing Rebound:", "Hover dampening"), classTarget.shipRapierAntiGravLandingRebound);
                classTarget.shipRapierAntiGravRebound = EditorGUILayout.FloatField(new GUIContent("Rebound:", "Hover dampening if falling from a height"), classTarget.shipRapierAntiGravRebound);
                classTarget.shipRapierAntiGravReboundJumpTime = EditorGUILayout.FloatField(new GUIContent("Rebound Jump Time:", "Springyness of the hover"), classTarget.shipRapierAntiGravReboundJumpTime);
                classTarget.shipRapierAntiGravRideHeight = EditorGUILayout.FloatField(new GUIContent("Ride Height:", "How far off the ground (in units) to hover"), classTarget.shipRapierAntiGravRideHeight);
                
                GUILayout.Label("Physics", EditorStyles.largeLabel);
                classTarget.shipRapierPhysicsFlightGravity = EditorGUILayout.FloatField(new GUIContent("Flight Gravity:", "The gravity when off track"), classTarget.shipRapierPhysicsFlightGravity);
                classTarget.shipRapierPhysicsTrackGravity = EditorGUILayout.FloatField(new GUIContent("Track Gravity:", "The gravity when on track"), classTarget.shipRapierPhysicsTrackGravity);
				classTarget.shipRapierPhysicsNormalGravity = EditorGUILayout.FloatField(new GUIContent("Normal Gravity:", "The speed which gravity changes"), classTarget.shipRapierPhysicsNormalGravity);
                classTarget.shipRapierPhysicsMass = EditorGUILayout.FloatField(new GUIContent("Mass:", "This should really be kept at 1"), classTarget.shipRapierPhysicsMass);
                
                GUILayout.Label("Pitch", EditorStyles.largeLabel);
                classTarget.shipRapierPitchAir = EditorGUILayout.FloatField(new GUIContent("Air Pitch:", "How much to pitch the nose if in the air"), classTarget.shipRapierPitchAir);
                classTarget.shipRapierPitchGround = EditorGUILayout.FloatField(new GUIContent("Ground Pitch:", "How much to pitch the nose if on the ground"), classTarget.shipRapierPitchGround);
                classTarget.shipRapierPitchDamp = EditorGUILayout.FloatField(new GUIContent("Pitch Damp:", "Pitch dampening"), classTarget.shipRapierPitchGround);
                classTarget.shipRapierPitchAntiGravHeightAjust = EditorGUILayout.FloatField(new GUIContent("Height Ajust:", "Should ideally be left at 1"), classTarget.shipRapierPitchAntiGravHeightAjust);
                
                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndVertical();
            }

            PhantomSettings = EditorGUILayout.Foldout(PhantomSettings, "Phantom Settings");
            if (PhantomSettings)
            {
                EditorGUILayout.BeginVertical();
                PhantomSettingsScroll = EditorGUILayout.BeginScrollView(PhantomSettingsScroll, GUILayout.Height(150));
                
                GUILayout.Label("Engine", EditorStyles.largeLabel);
                classTarget.shipPhantomEngineAccelCap = EditorGUILayout.FloatField(new GUIContent("Engine Accel Cap:", "The maximum value the thrust acceleration can reach"), classTarget.shipPhantomEngineAccelCap);
                classTarget.shipPhantomEngineMaxSpeed = EditorGUILayout.FloatField(new GUIContent("Engine Max Speed:", "The ships top speed"), classTarget.shipPhantomEngineMaxSpeed);
                classTarget.shipPhantomEngineGain = EditorGUILayout.FloatField(new GUIContent("Engine Gain:", "The speed that the acceleration will increase"), classTarget.shipPhantomEngineGain);
                classTarget.shipPhantomEngineFalloff = EditorGUILayout.FloatField(new GUIContent("Engine Falloff:", "The base speed that the ship will loose speed"), classTarget.shipPhantomEngineFalloff);
                
                GUILayout.Label("Brake", EditorStyles.largeLabel);
                classTarget.shipPhantomBrakeAmount = EditorGUILayout.FloatField(new GUIContent("Brake Amount:", "How fast the brakes should slow down the craft"), classTarget.shipPhantomBrakeAmount);
                classTarget.shipPhantomBrakeGain = EditorGUILayout.FloatField(new GUIContent("Brake Gain:", "The speed it takes the brakes to reach their max"), classTarget.shipPhantomBrakeGain);
                classTarget.shipPhantomBrakeFalloff = EditorGUILayout.FloatField(new GUIContent("Brake Falloff:", "The speed it takes the brake force to stop"), classTarget.shipPhantomBrakeFalloff);
                
                GUILayout.Label("Turn", EditorStyles.largeLabel);
                classTarget.shipPhantomTurnMax = EditorGUILayout.FloatField(new GUIContent("Turn Amount:", "The max angle per frame the ship should turn"), classTarget.shipPhantomTurnMax);
                classTarget.shipPhantomTurnGain = EditorGUILayout.FloatField(new GUIContent("Turn Gain:", "How fast the ship reaches it's max APS"), classTarget.shipPhantomTurnGain);
                classTarget.shipPhantomTurnFalloff = EditorGUILayout.FloatField(new GUIContent("Turn Falloff:","How fast the ship looses speed (higher values causes a springy effect)"), classTarget.shipPhantomTurnFalloff);
                
                GUILayout.Label("Airbrakes", EditorStyles.largeLabel);
                classTarget.shipPhantomAirbrakeAmount = EditorGUILayout.FloatField(new GUIContent("Airbrake Amount:", "The base force of the airbrakes"), classTarget.shipPhantomAirbrakeAmount);
                classTarget.shipPhantomAirbrakeDrag = EditorGUILayout.FloatField(new GUIContent("Airbrake Drag:", "Airbrake dampening"), classTarget.shipPhantomAirbrakeDrag);
                classTarget.shipPhantomAirbrakeGain = EditorGUILayout.FloatField(new GUIContent("Airbrake Gain:", "The speed at which the airbrakes reach their max force"), classTarget.shipPhantomAirbrakeGain);
                classTarget.shipPhantomAirbrakeFalloff = EditorGUILayout.FloatField(new GUIContent("Airbrake Falloff:", "The speed at which the airbrakes zero back out"), classTarget.shipPhantomAirbrakeFalloff);
                classTarget.shipPhantomAirbrakeTurn = EditorGUILayout.FloatField(new GUIContent("Airbrake Turn:", "This variable needs a description"), classTarget.shipPhantomAirbrakeTurn);
                classTarget.shipPhantomAirbrakeGrip = EditorGUILayout.FloatField(new GUIContent("Airbrake Slidegrip:", "The dampening of sideshifting"), classTarget.shipPhantomAirbrakeGrip);
                classTarget.shipPhantomAirbrakeSideShiftAmount = EditorGUILayout.FloatField(new GUIContent("Airbrake Sideshift:", "The force of sideshifts"), classTarget.shipPhantomAirbrakeSideShiftAmount);
                
                GUILayout.Label("Anti-Gravity", EditorStyles.largeLabel);
                classTarget.shipPhantomAntiGravGripAir = EditorGUILayout.FloatField(new GUIContent("Air grip:", "How sticky the ship is when in the air"), classTarget.shipPhantomAntiGravGripAir);
                classTarget.shipPhantomAntiGravGripGround = EditorGUILayout.FloatField(new GUIContent("Ground grip:", "How sticky the ship is when on the ground"), classTarget.shipPhantomAntiGravGripGround);
                classTarget.shipPhantomAntiGravLandingRebound = EditorGUILayout.FloatField(new GUIContent("Landing Rebound:", "Hover dampening"), classTarget.shipPhantomAntiGravLandingRebound);
                classTarget.shipPhantomAntiGravRebound = EditorGUILayout.FloatField(new GUIContent("Rebound:", "Hover dampening if falling from a height"), classTarget.shipPhantomAntiGravRebound);
                classTarget.shipPhantomAntiGravReboundJumpTime = EditorGUILayout.FloatField(new GUIContent("Rebound Jump Time:", "Springyness of the hover"), classTarget.shipPhantomAntiGravReboundJumpTime);
                classTarget.shipPhantomAntiGravRideHeight = EditorGUILayout.FloatField(new GUIContent("Ride Height:", "How far off the ground (in units) to hover"), classTarget.shipPhantomAntiGravRideHeight);
                
                GUILayout.Label("Physics", EditorStyles.largeLabel);
                classTarget.shipPhantomPhysicsFlightGravity = EditorGUILayout.FloatField(new GUIContent("Flight Gravity:", "The gravity when off track"), classTarget.shipPhantomPhysicsFlightGravity);
                classTarget.shipPhantomPhysicsTrackGravity = EditorGUILayout.FloatField(new GUIContent("Track Gravity:", "The gravity when on track"), classTarget.shipPhantomPhysicsTrackGravity);
				classTarget.shipPhantomPhysicsNormalGravity = EditorGUILayout.FloatField(new GUIContent("Normal Gravity:", "The speed which gravity changes"), classTarget.shipPhantomPhysicsNormalGravity);
                classTarget.shipPhantomPhysicsMass = EditorGUILayout.FloatField(new GUIContent("Mass:", "This should really be kept at 1"), classTarget.shipPhantomPhysicsMass);
                
                GUILayout.Label("Pitch", EditorStyles.largeLabel);
                classTarget.shipPhantomPitchAir = EditorGUILayout.FloatField(new GUIContent("Air Pitch:", "How much to pitch the nose if in the air"), classTarget.shipPhantomPitchAir);
                classTarget.shipPhantomPitchGround = EditorGUILayout.FloatField(new GUIContent("Ground Pitch:", "How much to pitch the nose if on the ground"), classTarget.shipPhantomPitchGround);
                classTarget.shipPhantomPitchDamp = EditorGUILayout.FloatField(new GUIContent("Pitch Damp:", "Pitch dampening"), classTarget.shipPhantomPitchGround);
                classTarget.shipPhantomPitchAntiGravHeightAjust = EditorGUILayout.FloatField(new GUIContent("Height Ajust:", "Should ideally be left at 1"), classTarget.shipPhantomPitchAntiGravHeightAjust);
                
                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndVertical();
            }
        }
    }
}
