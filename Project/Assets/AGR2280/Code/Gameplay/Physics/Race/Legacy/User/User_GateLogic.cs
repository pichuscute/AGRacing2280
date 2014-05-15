using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class User_GateLogic : MonoBehaviour {

	// Use this for initialization#
	GameObject RaceManager;
	int currentGate;
	int currentLap;
	bool hasPassedHalfGate;
	bool hasPassedLastGate;

    int bestMilisecond = 9999;
    int bestSecond = 9999;
    int bestMinute = 9999;

    int thisMilisecond;
    int thisSecond;
    int thisMinute;

    bool timerStarted;
    bool timesCheckIsMore;
    bool timesMinuteMore;
    bool timesSecondMore;

    bool timesMinuteSame;
    bool timesSecondSame;

    public Texture2D AgressiveHud;
	public Texture2D LapBox;

    public GUIStyle hudBlueBox;
	public GUIStyle hudText;

    public float DebugGUIScaleX;
    public float DebugGUIScaleY;

    string trackName = "test";

	void Start () 
	{
		RaceManager = GameObject.Find ("RaceManager");
        InvokeRepeating("Lap_Times", 0, 0.01f);


        // Get best time
        
        string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
        string filename = trackName + "_SpeedLapBest.AGRTime";
        string path = documentsFolder + "/AGR2280/Times/" + trackName + "/" + filename;
        if (File.Exists(path))
        {
            StreamReader readRecord = new StreamReader(path);
            readRecord.ReadLine();
            bestMinute = int.Parse(readRecord.ReadLine());
            readRecord.ReadLine();
            bestSecond = int.Parse(readRecord.ReadLine());
            readRecord.ReadLine();
            bestMilisecond = int.Parse(readRecord.ReadLine());
            readRecord.Close();
        }
        print(bestMilisecond);
        

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter(Collider Collide)
	{
		if (Collide.gameObject.tag == "Gate") 
		{
			string gateID = Collide.collider.gameObject.name.Remove(0, 9);
			int gateExtractID = int.Parse(gateID);
			currentGate = gateExtractID;

			GetComponent<User_Ship>().respawnPos = Collide.gameObject.transform.position;
			GetComponent<User_Ship>().respawnRot = Collide.gameObject.transform.rotation;
		}

        if (currentGate == Mathf.RoundToInt(RaceManager.GetComponent<RaceMangement>().GateCount / 2))
        {
            hasPassedHalfGate = true;
        }
        if (currentGate == RaceManager.GetComponent<RaceMangement>().GateCount)
        {
            hasPassedLastGate = true;
        }

		if (currentGate == 1) 
		{
            if (hasPassedHalfGate && hasPassedLastGate)
            {
                if (timerStarted)
                {
                    calculateTimes();
                }
                currentLap ++;
            }
            if (currentLap == 0)
            {
                timerStarted = true;
                currentLap ++;
            }
            hasPassedHalfGate = false;
            hasPassedLastGate = false;

		}
	}

	void OnGUI()
	{
        GUI.Label(new Rect(2, 20, 100, 100), "Current:" + thisMilisecond + "/" + thisSecond + "/" + thisMinute);
        GUI.Label(new Rect(2, 45, 100, 100), "Best:" + bestMilisecond + "/" + bestSecond + "/" + bestMinute);

        float xPos = Screen.width;
        float yPos = Screen.height;

        GUI.DrawTexture(new Rect(Screen.width / 2 - ((AgressiveHud.width / 1600f) *xPos) / 2, 0, (AgressiveHud.width / 1600f) *xPos, (AgressiveHud.height / 700f) * yPos), AgressiveHud);

        //GUI.Box(new Rect(890, 108, 16, -95), "", hudBlueBox);
        GUI.Box(new Rect(0.456f*xPos, 0.155f*yPos, 0.010f*xPos, (GetComponent<User_Ship>().Integrity / 100f) * (-0.132f * yPos)), GetComponent<User_Ship>().Integrity.ToString(), hudBlueBox);
		float rightBoxHeight = (GetComponent<User_Ship>().acceleration + GetComponent<User_Ship>().boostAmount) / 2000;
		rightBoxHeight = Mathf.Clamp (rightBoxHeight,0, 1);
		GUI.Box(new Rect(0.5395f*xPos, 0.155f*yPos, 0.010f*xPos, (rightBoxHeight) * (-0.132f * yPos)),"KM/H", hudBlueBox);

		GUI.DrawTexture(new Rect(((AgressiveHud.width / 5500f) *xPos) / 2, 0, (AgressiveHud.width / 5500f) *xPos, (AgressiveHud.height / 800f) * yPos), LapBox);
		GUI.TextField (new Rect (0.06f * xPos, 0.01f * yPos, 0.06f * xPos, 0.01f * yPos), "Lap", hudText);
		GUI.TextField (new Rect (0.06f * xPos, 0.05f * yPos, 0.06f * xPos, 0.05f * yPos), currentLap.ToString() + "/" + "99", hudText);
	}
    
    void Lap_Times()
    {
        if (timerStarted)
        {
            thisMilisecond++;
            if (thisMilisecond > 100)
            {
                thisSecond++;
                thisMilisecond = 0;
            }
            if (thisSecond > 60)
            {
                thisMinute++;
                thisSecond = 0;
            }
        }
    }

    void calculateTimes()
    {
        timesCheckIsMore = false;
        timesMinuteMore = false;
        timesSecondMore = false;
        timesMinuteSame = false;
        timesSecondSame = false;    

		if (currentLap > 1)
        {
            if (thisMilisecond < bestMilisecond)
            {
                timesCheckIsMore = true;
                bestMilisecond = thisMilisecond;
            }

            if (thisSecond < bestSecond)
            {
				timesCheckIsMore = true;
				bestSecond = thisSecond;
            }

            if (thisMinute < bestMinute)
            {
				timesCheckIsMore = true;
				bestMinute = thisMinute;
            }

        }

		if (timesCheckIsMore)
        {
            // Get Track Name
			
			string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
			string path = documentsFolder + "/AGR2280/Times/" + trackName + "/";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			StreamWriter createRecord = new StreamWriter(path + trackName + "_SpeedLapBest.AGRTime");
			createRecord.WriteLine("[Minute]");
			createRecord.WriteLine(bestMinute);
            createRecord.WriteLine("[Second]");
            createRecord.WriteLine(bestSecond);
            createRecord.WriteLine("[Milisecond]");
            createRecord.WriteLine(bestMilisecond);
            createRecord.Close();
                                 
			
		}
		
		thisMinute = 0;
        thisSecond = 0;
        thisMilisecond = 0;
    }
}
