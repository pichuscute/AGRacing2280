using UnityEngine;
using System.Collections;

public class AirbrakeRotation : MonoBehaviour {

	public float maxAngle;

    public float ABGain;
    float gainVelocity;
    public float ABFallOff;
    float fallOffVelocity;

	public enum airBrakeSide
	{
        left,
        right
	};

    public airBrakeSide shipAirBrakeSide;

    float airBrakeInput;

    float currentAngle;

    bool isBraking;

	
	// Update is called once per frame
	void FixedUpdate () 
    {
        airBrakeInput = Input.GetAxis("[KB] Airbrake");
        if (airBrakeInput == 0)
        {
            airBrakeInput = Input.GetAxis("[PAD] Airbrake");
        }

        if (airBrakeInput == 0)
        {
            airBrakeInput = Input.GetAxis("[PAD] Analog Airbrake");
        }

		if ((Input.GetAxis("[KB] Brake Left") > 0 && Input.GetAxis("[KB] Brake Right") > 0)|| (Input.GetAxis("[PAD] Brake Left") > 0 && Input.GetAxis("[PAD] Brake Right") > 0))
        {
            isBraking = true;
        } else
        {
            isBraking = false;
        }

        if (isBraking)
        {
            gainVelocity = Mathf.Lerp(gainVelocity, ABGain, Time.fixedDeltaTime * (ABGain / 100));
            fallOffVelocity = 0;
            currentAngle = Mathf.MoveTowards(currentAngle, maxAngle, Time.fixedDeltaTime * gainVelocity);
        } else
        {
            if (shipAirBrakeSide == airBrakeSide.left)
            {
                if (-airBrakeInput < 0)
                {
                    fallOffVelocity = 0;
                    gainVelocity = Mathf.Lerp(gainVelocity, ABGain, Time.fixedDeltaTime * (ABGain / 100));
                    currentAngle = Mathf.MoveTowards(currentAngle, airBrakeInput * maxAngle, Time.fixedDeltaTime * gainVelocity);
                } else
                {
                    gainVelocity = 0;
                    fallOffVelocity = Mathf.Lerp (fallOffVelocity, ABFallOff, Time.fixedDeltaTime * (ABFallOff / (ABGain / 10)));
                    currentAngle = Mathf.MoveTowards(currentAngle, 0, Time.fixedDeltaTime * fallOffVelocity);
                }
            }

            if (shipAirBrakeSide == airBrakeSide.right)
            {
                if (-airBrakeInput > 0)
                {
                    fallOffVelocity = 0;
                    gainVelocity = Mathf.Lerp(gainVelocity, ABGain, Time.fixedDeltaTime * (ABGain / 100));
                    currentAngle = Mathf.MoveTowards(currentAngle, airBrakeInput * -maxAngle, Time.fixedDeltaTime * gainVelocity);
                } else
                {
                    gainVelocity = 0;
                    fallOffVelocity = Mathf.Lerp (fallOffVelocity, ABFallOff, Time.fixedDeltaTime * (ABFallOff / (ABGain / 10)));
                    currentAngle = Mathf.MoveTowards(currentAngle, 0, Time.fixedDeltaTime * fallOffVelocity);
                }
            }
        }

        transform.localRotation = Quaternion.Euler(currentAngle, 0, 0);
	}
}
