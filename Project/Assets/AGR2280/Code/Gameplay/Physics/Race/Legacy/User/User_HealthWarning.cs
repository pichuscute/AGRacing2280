using UnityEngine;
using System.Collections;

public class User_HealthWarning : MonoBehaviour {

	// Use this for initialization
	float timer;
    float timerReset;
    bool playSound;
	public GameObject Ship;
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Ship.GetComponent<User_Ship>().Integrity < 26)
        {
            playSound = true;
            timerReset = 0.5f;
            if (Ship.GetComponent<User_Ship>().Integrity < 11)
            {
                timerReset = 0.3f;
                if (Ship.GetComponent<User_Ship>().Integrity < 6)
                {
                    timerReset = 0.1f;
                }
            }
        } else
        {
            playSound = false;
        }

        timer += Time.deltaTime;
        if (timer > timerReset)
        {
            if (playSound)
            {
                GetComponent<AudioSource>().Play();
            }
            timer = 0;
        }

	}
}
