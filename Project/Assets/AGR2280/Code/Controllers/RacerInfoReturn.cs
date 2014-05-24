using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class RacerInfoReturn : MonoBehaviour {

	public int thisRacerID;
	public int thisCurrentGate;

	public int thisRacerLap;

	public int thisLapMS;
	public int thisLapS;
	public int thisLapM;

	public int bestLapMS;
	public int bestLapS;
	public int bestLapM;

	public int overalMS;
	public int overalS;
	public int overalM;

	public int lastLapMS;
	public int lastLapS;
	public int lastLapM;

	public bool isClient;

	public Transform nextAPObject;

	public GUIStyle UISkin;
	public GUIStyle UIFont;

	bool calculatedLap = false;

	bool displayBest;
	float bestBoxHeight;

	public int[] lapMS;
	public int[] lapS;
	public int[] lapM;

	bool thisRacerFinished;

	float resultsWindowHeight;

	public Texture waterMark;
	string ResultsTitle = "RESULTS - PRESS S TO SAVE";

	void Start () 
	{
		if (thisRacerID > 8)
		{
			thisRacerID = 8;
		}

		// Zero out times
		thisRacerLap = 0;
		thisLapMS = 0;
		thisLapS = 0;
		thisLapM = 0;

		bestLapMS = 999;
		bestLapS = 999;
		bestLapM = 999;

		overalMS = 0;
		overalS = 0;
		overalM = 0;

		InvokeRepeating("RaceCount", 0, 0.01f);

		// Set number of laps to record (5 for trial)
		lapMS = new int[99];
		lapS = new int[99];
		lapM = new int[99];
	}
	

	void Update () 
	{
		// ONLY FOR TIME TRIAL BUILD
		if (thisRacerLap > 5)
		{
			GetComponent<ShipController>().currentController = ShipController.Controller.AutoPilot;
			thisRacerFinished = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.collider.gameObject.tag == "Gate")
		{
			GetComponent<ShipController>().respawnPosition = other.collider.gameObject.transform.position;
			GetComponent<ShipController>().respawnRotation = Quaternion.Euler(0, other.collider.gameObject.transform.eulerAngles.y, 0);

			string nodeName = other.collider.name;
			string nodeID = nodeName.Remove(0, 9).ToString();
			thisCurrentGate = int.Parse(nodeID);
			if (GameObject.Find("RaceGate_" + (thisCurrentGate + 4)))
			{
				nextAPObject = GameObject.Find("RaceGate_" + (thisCurrentGate + 4)).transform.Find("thisGateAPHelper");
				
			} else 
			{
				nextAPObject = GameObject.Find("RaceGate_" + 0).transform.Find("thisGateAPHelper");
			}

			// Current Lap
			if (thisCurrentGate == 2)
			{
				calculatedLap = false;
			}
		}

		// Pass gate
		if ((other.collider.gameObject.name == "RaceGate_0") && !calculatedLap)
		{
			
			if (thisRacerLap > 1)
			{
				lastLapMS = thisLapMS;
				lastLapS = thisLapS;
				lastLapM = thisLapM;
			}
			
			if (thisRacerLap > 2)
			{
				displayBest = true;
				bool updateBest = false;
				
				if (thisLapMS < bestLapMS)
				{
					updateBest = true;
					if (thisLapS < bestLapS)
					{
						updateBest = true;
						if (thisLapM < bestLapM)
						{
							updateBest = true;
						}
					}
				} else 
				{
					if (thisLapS < bestLapS)
					{
						updateBest = true;
						if (thisLapM < bestLapM)
						{
							updateBest = true;
						}
					} else 
					{
						if (thisLapM < bestLapM)
						{
							updateBest = true;
						}
					}
				}
				
				if (updateBest)
				{
					bestLapMS = thisLapMS;
					bestLapS = thisLapS;
					bestLapM = thisLapM;
				}
			}

			
			// Set Current Lap
			lapMS[thisRacerLap] = thisLapMS;
			lapS[thisRacerLap] = thisLapS;
			lapM[thisRacerLap] = thisLapM;

			thisLapMS = 0;
			thisLapS = 0;
			thisLapM = 0;
			thisRacerLap++;

			calculatedLap = true;
		}
	}

	void RaceCount()
	{
		if (thisCurrentGate > 0)
		{
			thisLapMS++;
			if (thisLapMS > 100)
			{
				thisLapS++;
				thisLapMS = 0;
			}

			if (thisLapS > 60)
			{
				thisLapM ++;
				thisLapS = 0;
			}
		}
	}

	void OnGUI()
	{
		// Draw Watermark
		if (isClient)
		{
			GUI.DrawTexture(new Rect(Screen.width - 128, 0, 128, 128), waterMark);
		}

		if (isClient && !thisRacerFinished)
		{

			Vector2 windowDim = new Vector2(Screen.width, Screen.height);

			// Draw Lap Seconds
			GUI.Box(new Rect(0, 0, (windowDim.x / 2 ) * 0.2f, (windowDim.y / 2) * 0.08f),"", UISkin);

			string milisecond = thisLapMS.ToString();
			if (thisLapMS < 10)
			{
				milisecond = ("0" + thisLapMS);
			}

			if (thisLapMS > 99)
			{
				milisecond = ("99");
			}

			GUI.Label(new Rect((windowDim.x * 0.01f), windowDim.y * 0.003f, 500, 500), "This: " + milisecond + ":");

			string second = thisLapS.ToString();
			if (thisLapS < 10)
			{
				second = ("0" + thisLapS);
			}

			GUI.Label(new Rect((windowDim.x * 0.05f), windowDim.y * 0.003f, 500, 500), second + ":");

			string minute = thisLapM.ToString();
			if (thisLapM < 10)
			{
				minute = ("0" + thisLapM);
			}

			GUI.Label(new Rect((windowDim.x * 0.065f), windowDim.y * 0.003f, 500, 500), minute);

			// Draw Best Lap Seconds

			if (displayBest)
			{
				bestBoxHeight = Mathf.Lerp(bestBoxHeight, 0.4f, Time.deltaTime * 0.2f);
			} else
			{
				bestBoxHeight = 0;
			}

			GUI.BeginGroup(new Rect(0,(windowDim.y / 2) * 0.08f, (windowDim.x / 2 ) * 0.18f, (windowDim.y / 2) * bestBoxHeight));
				GUI.Box(new Rect(0,0, (windowDim.x / 2 ) * 0.3f, (windowDim.y / 2) * 0.1f),"", UISkin);
				
				string Bmilisecond = bestLapMS.ToString();
				if (bestLapMS < 10)
				{
					Bmilisecond = ("0" + bestLapMS);
				}
				
				if (bestLapMS > 99)
				{
					Bmilisecond = ("99");
				}
				
				GUI.Label(new Rect((windowDim.x * 0.01f), windowDim.y * 0.01f, 500, 500), "Best: " + Bmilisecond + ":");
				
				string Bsecond = bestLapS.ToString();
				if (bestLapS < 10)
				{
					Bsecond = ("0" + bestLapS);
				}
				
				GUI.Label(new Rect((windowDim.x * 0.05f), windowDim.y * 0.01f, 500, 500), Bsecond + ":");
				
				string Bminute = bestLapM.ToString();
				if (bestLapM < 10)
				{
					Bminute = ("0" + bestLapM);
				}
				
				GUI.Label(new Rect((windowDim.x * 0.065f), windowDim.y * 0.01f, 500, 500), Bminute);
			GUI.EndGroup();
		}

		if (isClient && thisRacerFinished)
		{
			Vector2 windowDim = new Vector2(Screen.width, Screen.height);

			resultsWindowHeight = Mathf.Lerp(resultsWindowHeight, windowDim.y / 2, Time.deltaTime * 2);

			// Draw Results Screen

			GUI.BeginGroup(new Rect(windowDim.x / 4, windowDim.y / 4, windowDim.x / 2, resultsWindowHeight));

			GUI.color = new Color(1,1,1, 0.8f);

			// Main Box
			Texture2D tempTexture = UISkin.hover.background;

			UISkin.hover.background= null;
			GUI.Box(new Rect(0, 0, windowDim.x / 2, windowDim.y / 2), "" ,UISkin);
			//------------

			GUI.color = new Color(1,1,1, 1);

			// Times Box
			GUI.BeginGroup(new Rect(0, windowDim.y / 12, windowDim.x / 5, windowDim.y / 3));
				UIFont.fontSize = 24;
				UIFont.alignment = TextAnchor.MiddleLeft;

				GUI.Box(new Rect(0, 0, windowDim.x / 5, windowDim.y / 3), "" ,UISkin);
				GUI.Label(new Rect(5, 16, 100,40), "Lap 1 - " + lapMS[1] + ":" + lapS[1] + ":" + lapM[1], UIFont);
				GUI.Label(new Rect(5, 48, 100,40), "Lap 2 - " + lapMS[2] + ":" + lapS[2] + ":" + lapM[2], UIFont);
				GUI.Label(new Rect(5, 80, 100,40), "Lap 3 - " + lapMS[3] + ":" + lapS[3] + ":" + lapM[3], UIFont);
				GUI.Label(new Rect(5, 112, 100,40), "Lap 4 - " + lapMS[4] + ":" + lapS[4] + ":" + lapM[4], UIFont);
				GUI.Label(new Rect(5, 144, 100,40), "Lap 5 - " + lapMS[5] + ":" + lapS[5] + ":" + lapM[5], UIFont);
			GUI.EndGroup();
			//------------



			// Results Text
			GUI.color = new Color(1,1,1,1);
			UIFont.normal.textColor = new Color(0.1f,0.1f,0.1f,1);
			UIFont.fontSize = 32;
			UIFont.alignment = TextAnchor.UpperCenter;
			
			GUI.Label(new Rect(windowDim.x / 4 - 50, 0, 100, 100),ResultsTitle, UIFont);
			//------------

			// Buttons
			GUI.BeginGroup(new Rect(windowDim.x / 4, windowDim.y / 12, windowDim.x / 5, windowDim.y / 3));
			UISkin.hover.background = tempTexture;
			UISkin.alignment = TextAnchor.MiddleCenter;
				if (GUI.Button(new Rect(0,0, 200, 30), "Race Again!", UISkin))
				{
					Application.LoadLevel(0);
				}
			GUI.EndGroup();
			//------------

			GUI.EndGroup();

			// Hotkeys
			if (Input.GetKeyDown(KeyCode.S))
			{
				string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
				if (!Directory.Exists(documentsFolder + "/AGR2280/Screenshots/"))
				{
					Directory.CreateDirectory(documentsFolder + "/AGR2280/Screenshots/");
				}
				string path = documentsFolder + "/AGR2280/Screenshots/";

				Application.CaptureScreenshot(path + "Screenshot" + DateTime.Today.Day.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Year.ToString() + ".png");
				ResultsTitle = "RESULTS - SCREENSHOT SAVED!";
			}



		}
	}
}
