using UnityEngine;
using System;
using System.IO;
using System.Collections;

using FMOD;

public class MusicManager : MonoBehaviour {

	// Vars
	string MusicDirectory;
	string fileFormat = ".mp3";

	int songToPlay;

	bool isPlaying;

	private FMOD.System system = null;
	private FMOD.Sound PlayingMusic = null;
	private FMOD.Channel channel = null;

	GameObject Target;
    GameObject RaceManager;

	float FilterHP;

	DSPConnection highPassFilter;

	DSP musicHighPass;

    bool isMusicPlaying;
	bool firstSongStarted;

    FMOD.RESULT result;

	void Start()
	{
		result = FMOD.Factory.System_Create (ref system);
        firstSongStarted = false;

		Target = GameObject.Find ("Plr_ShipController");
        RaceManager = GameObject.Find("RaceManager");
	}


	void Update() 
	{
		FMOD.RESULT result;
		if (firstSongStarted)
        {
            result = channel.isPlaying(ref isMusicPlaying);
        }

		if (RaceManager.GetComponent<RaceMangement>().raceStarted && !isMusicPlaying)
        {
            PlayNewSong();
            
        }
		if (isMusicPlaying)
        {
            HighPass();
        }

        if (RaceManager.GetComponent<RaceMangement>().gamePaused && firstSongStarted)
        {
            channel.setPaused(true);
        } else
        {
            if (firstSongStarted)
            {
                channel.setPaused(false);
            }
        }
	}
    
	void HighPass()
	{
		if (Target.GetComponent<User_Ship>().publicDistance > Target.GetComponent<User_Ship>().hoverHeight + 5) 
		{
			FilterHP = Mathf.Lerp (FilterHP, 2000, Time.deltaTime * 5);
		} else {
			FilterHP = Mathf.Lerp (FilterHP, 0, Time.deltaTime * 5);
		}
		musicHighPass.setParameter (0, FilterHP);

	}

    void PlayNewSong()
    {


        string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
        string path = documentsFolder + "/AGR2280/Custom Music/";
        string[] availableFiles = Directory.GetFiles(path, "*.mp3");
        
        // Pick song to stream
        songToPlay = UnityEngine.Random.Range(0, availableFiles.Length);

		channel = null;
		PlayingMusic = null;
        result = system.init (1, FMOD.INITFLAGS.NORMAL, (IntPtr)null);
        result = system.createSound (availableFiles [songToPlay], FMOD.MODE.CREATESTREAM, ref PlayingMusic);
        result = system.playSound (FMOD.CHANNELINDEX.FREE, PlayingMusic, false, ref channel);
        result = system.createDSPByType (FMOD.DSP_TYPE.HIGHPASS, ref musicHighPass);
        result = channel.addDSP (musicHighPass, ref highPassFilter);
        firstSongStarted = true;

    }
    
	void OnApplicationQuit()
	{
		channel.stop();
		system.release ();
		channel = null;
		PlayingMusic = null;
		system = null;
	}
}
