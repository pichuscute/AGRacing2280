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
	float FilterLP = 25000;

	DSPConnection highPassFilter;
	DSPConnection lowPassFilter;

	DSP musicHighPass;
	DSP musicLowPass;

    bool isMusicPlaying;
	bool firstSongStarted;

	CHANNELINDEX music;

    FMOD.RESULT result;

	void Start()
	{
		result = FMOD.Factory.System_Create(ref system);
        firstSongStarted = false;

		Target = GameObject.Find ("PlayerShip");
	}


	void Update() 
	{
		system.update();
		if (firstSongStarted)
        {
			result = channel.isPlaying(ref isMusicPlaying);
			
		}

		if (!isMusicPlaying)
        {
            PlayNewSong();
            
        }
		if (isMusicPlaying)
        {
           
			if (Target.GetComponent<RacerInfoReturn>().thisRacerFinished)
			{
				FilterLP = Mathf.Lerp(FilterLP, 4000, Time.deltaTime * 3);
				musicLowPass.setParameter(0, FilterLP);
			} else
			{
				FilterLP = Mathf.Lerp(FilterLP, 25000, Time.deltaTime * 3);
				musicLowPass.setParameter(0, FilterLP);
				HighPass();
			}
        }

		/*
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
        */
	}
    
	void HighPass()
	{
		if (Target.GetComponent<ShipController>().isAR) 
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

		if (channel != null){channel = null;}
		PlayingMusic = null;

        result = system.init (1, FMOD.INITFLAGS.NORMAL, (IntPtr)null);
        result = system.createSound (availableFiles [songToPlay], FMOD.MODE.CREATESTREAM, ref PlayingMusic);
        result = system.playSound (music, PlayingMusic, false, ref channel);
        result = system.createDSPByType (FMOD.DSP_TYPE.HIGHPASS, ref musicHighPass);
		result = system.createDSPByType (FMOD.DSP_TYPE.LOWPASS, ref musicLowPass);
        result = channel.addDSP (musicHighPass, ref highPassFilter);
		result = channel.addDSP (musicLowPass, ref lowPassFilter);
        firstSongStarted = true;

    }
    
	void OnApplicationQuit()
	{
		PlayingMusic.release();
		channel.stop();
		system.close();
		system.release();
		channel = null;
		PlayingMusic = null;
		system = null;
	}

	public void StopMusic()
	{
		PlayingMusic.release();
		channel.stop();
		system.close();
		system.release();
		channel = null;
		PlayingMusic = null;
		system = null;
	}
}
