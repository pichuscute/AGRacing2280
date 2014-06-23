using UnityEngine;
using System.Collections;

public class ServerHandler : MonoBehaviour {


	public string[] PlayerName;
	public int[] hostPlayer;

	public string serverName;
	public string serverPassword;
	public bool serverHasPassword;

	public string serverIP;

	public int maxRacers;

	public bool isSinglePlayer;

	public void StartServer(string name, string password, int racerCount, bool usingPassword)
	{
		serverName = name;
		serverPassword = password;
		maxRacers = racerCount;
		serverHasPassword = usingPassword;

		Network.InitializeServer(maxRacers, 25565, false);
		MasterServer.RegisterHost("Set to race event later", serverName, "");
	}
}
