using UnityEngine;
using System.Collections;

public class RaceMangement : MonoBehaviour {

	// Use this for initialization
	public bool raceStarted = false;
	bool camIntro;
	bool countDown;
	int countDownTimer;
	bool countDownState01;
	bool countDownState02;
	bool countDownState03;
	bool countDownFinished;

    public GameObject PlayerStartRing1;
    public GameObject PlayerStartRing2;
    public GameObject PlayerStartRing3;
    public GameObject PlayerStartRing4;
    public GameObject PlayerStartText;
    float textOffset;
    public Color RingPrepare;
    public Color RingGo;
    Color currentColor;
    float fadeTimer;

    public GameObject CountDownSound;
    public GameObject CountDownOne;
    public GameObject CountDownTwo;
    public GameObject CountDownThree;

    float startBoostTimer;
    public bool hasBoosted;
    public bool boostOpertunity = true;

    bool countDownFinish;

	public int GateCount;

    public bool gamePaused;
    int pausedState;

    // Pause menu assetts
    public GUIStyle pauseSkin;
    public Texture2D PauseScreenBackground;
    public Texture2D Menu_CloseButton;

	void Start () 
	{
		InvokeRepeating ("CountDown", 0, 1);	
        countDown = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (!countDownFinish)
        {
            if (fadeTimer < 50)
            {
                PlayerStartRing1.renderer.material.color = Color.Lerp(PlayerStartRing1.renderer.material.color, currentColor, Time.deltaTime * 5);
                PlayerStartRing2.renderer.material.color = Color.Lerp(PlayerStartRing2.renderer.material.color, currentColor, Time.deltaTime * 2);
                PlayerStartRing3.renderer.material.color = Color.Lerp(PlayerStartRing3.renderer.material.color, currentColor, Time.deltaTime);
                PlayerStartRing4.renderer.material.color = Color.Lerp(PlayerStartRing4.renderer.material.color, currentColor, Time.deltaTime / 2);
            }
            if (raceStarted)
            {
                if (fadeTimer > 1)
                {
                    PlayerStartRing1.renderer.enabled = false;
                    PlayerStartRing2.renderer.enabled = false;
                    PlayerStartRing3.renderer.enabled = false;
                    PlayerStartRing4.renderer.enabled = false;
                    PlayerStartText.renderer.enabled = false;
                    countDownFinish = true;
                }
            }
        }
        textOffset = Mathf.Lerp(textOffset, 0, Time.deltaTime * 5);
        PlayerStartText.GetComponent<TextMesh>().offsetZ = textOffset;

        if (countDownFinished)
        {
            if (boostOpertunity)
            {
                startBoostTimer += Time.time;
                if (startBoostTimer < 50)
                {
                    if (Input.GetButtonDown("[KB] Thruster") || Input.GetButtonDown("[PAD] Thruster"))
                    {
                        hasBoosted = true;
                        boostOpertunity = false;
                    }
                } else 
                {
                    hasBoosted = false;
                    boostOpertunity = false;
                }
            } else 
            {
                hasBoosted = false;
            }
        }

        // Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausedState++;
            if (pausedState > 1)
            {
                pausedState = 0;
            }

            if (pausedState == 0)
            {
                gamePaused = false;
            }

            if (pausedState == 1)
            {
                gamePaused = true;
            }
        }
       
	}

	void CountDown()
	{
		countDownTimer ++;
        if (countDown)
        {
            if (countDownTimer > 1)
            {
                if (!countDownState01)
                {
                    currentColor = RingPrepare;
                    PlayerStartText.GetComponent<TextMesh>().text = "3";
                    textOffset = -160;
                    CountDownSound.GetComponent<AudioSource>().Play();
                    CountDownThree.GetComponent<AudioSource>().Play();
                }
                countDownState01 = true;
            }

            if (countDownTimer > 2)
            {
                if (!countDownState02)
                {
                    PlayerStartText.GetComponent<TextMesh>().text = "2";
                    textOffset = -160;
                    CountDownSound.GetComponent<AudioSource>().Play();
                    CountDownTwo.GetComponent<AudioSource>().Play();
                }
                countDownState02 = true;

            }

            if (countDownTimer > 3)
            {
                if (!countDownState03)
                {
                    PlayerStartText.GetComponent<TextMesh>().text = "GET READY";
                    textOffset = -160;
                    CountDownSound.GetComponent<AudioSource>().Play();
                    CountDownOne.GetComponent<AudioSource>().Play();
                }
                countDownState03 = true;
            }

            if (countDownTimer > 4)
            {
                currentColor = RingGo;
                countDownFinished = true;
                PlayerStartText.GetComponent<TextMesh>().text = "GO!";
                textOffset = -160;
                CountDownSound.GetComponent<AudioSource>().pitch = 2;
                CountDownSound.GetComponent<AudioSource>().Play();

            }

            if (countDownFinished)
            {
                GameObject.Find ("Music").GetComponent<AudioSource>().Play();
                countDownState03 = false;
                raceStarted = true;
                countDown = false;
            }
        }
        if (raceStarted)
        {
            if (!countDownFinish)
            {
                fadeTimer++;
            }
        }
	}

    void OnGUI()
    {
        if (gamePaused)
        {
            float xPos = Screen.width;
            float yPos = Screen.height;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), PauseScreenBackground);

            if (GUI.Button(new Rect(0.81f*xPos, 0.19f*yPos, 0.032f*yPos, 0.032f*yPos), "X", pauseSkin))
            {
                gamePaused = false;
            }

            if (GUI.Button(new Rect(xPos / 2 - (0.32f*yPos / 2), 0.69f*yPos, 0.32f*yPos, 0.064f*yPos), "Quit to Menu", pauseSkin))
            {
                Application.LoadLevel(1);
            }
        }
    }
}
