using UnityEngine;
using System.Collections;

public class RaceInformation : MonoBehaviour {

	public enum RaceSpeed { Venom, Flash, Rapier, Phantom }
	public enum Pickup { Cannon, Rockets, Missle, Plasma, Quake, Leech, Mines, Bomb, AutoPilot, Shield, Turbo, Disruptor, HoverStop, Cloak, FalseWall, Blackout }
	public enum ShipTypes { Speed, Agility, Fighter };
	public enum RacerType { Player, AI};

	public enum Event { Race, Battle, PointRace, Multi, SurvivalBoost, SurvivalVelocity, Zone, BoostZone, RandZone, Trial, BoostTrial, SpeedLap};
	public struct currentTournament
	{
		public int playerCount;
		public Event[] currentEvent;
	}
}
