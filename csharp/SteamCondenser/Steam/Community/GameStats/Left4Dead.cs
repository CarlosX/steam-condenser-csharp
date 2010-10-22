/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Xml;
using System.Collections.Generic;

namespace SteamCondenser.Steam.Community
{
	
	public class L4DMostRecentGame	
	{
		
		public string Difficulty { get; protected set; }
		public bool   Escaped    { get; protected set; }
		public string Movie      { get; protected set; }
		public string TimePlayed { get; protected set; }
		
		public L4DMostRecentGame(XmlElement data)
		{
			Difficulty = data.GetInnerText("difficulty");
			Escaped    = data.GetInnerText("bEscaped").Equals("1");
			Movie      = data.GetInnerText("movie");
			// TODO: check this out, maybe it's because i didn't played for a long time
			TimePlayed = data.GetInnerText("time");
		}
	}
	
	public class L4DFavourite
	{
		public string Campaign               { get; protected set; }
		public int    CampaignPercentage     { get; protected set; }
		public string Character              { get; protected set; }
		public int    CharacterPercentage    { get; protected set; }
		public string Level1Weapon           { get; protected set; }
		public int    Level1WeaponPercentage { get; protected set; }
		public string Level2Weapon           { get; protected set; }
		public int    Level2WeaponPercentage { get; protected set; }
		
		public L4DFavourite(XmlElement data)
		{
			Campaign               =           data.GetInnerText("campaign");
			CampaignPercentage     = int.Parse(data.GetInnerText("campaignpct"));
			Character              =           data.GetInnerText("character");
			CharacterPercentage    = int.Parse(data.GetInnerText("characterpct"));
			Level1Weapon           =           data.GetInnerText("weapon1");
			Level1WeaponPercentage = int.Parse(data.GetInnerText("weapon1pct"));
			Level2Weapon           =           data.GetInnerText("weapon2");
			Level2WeaponPercentage = int.Parse(data.GetInnerText("weapon2pct"));
			
			
		}
	}
	
	public class L4DSurvivalStats
	{
		public int   GoldMedals   { get; protected set; }
		public int   SolverMedals { get; protected set; }
		public int   BronzeMedals { get; protected set; }
		public int   RoundsPlayed { get; protected set; }
		public float BestTime     { get; protected set; }
		
		public L4DSurvivalStats(XmlElement data)
		{
			GoldMedals   =   int.Parse(data.GetInnerText("goldmedals"));
			SolverMedals =   int.Parse(data.GetInnerText("silvermedals"));
			BronzeMedals =   int.Parse(data.GetInnerText("bronzemedals"));
			RoundsPlayed =   int.Parse(data.GetInnerText("roundsplayed"));
			BestTime     = float.Parse(data.GetInnerText("besttime"));
		}
	}
	
	public class L4DTeamPlayStats
	{
		public int    Revived                 { get; protected set; }
		public string MostRevivedDifficulty   { get; protected set; }
		public float  AverageRevived          { get; protected set; }
		public float  AverageWasRevived       { get; protected set; }
		public int    Protected               { get; protected set; }
		public string MostProtectedDifficulty { get; protected set; }
		public float  AverageProtected        { get; protected set; }
		public float  AverageWasProtected     { get; protected set; }
		public int    FriendlyFireDamage      { get; protected set; } 
		// TODO: rename this long name
		public string MostFriendlyFireDamageDifficulty { get; protected set; }
		public float  AverageFriendlyFireDamage { get; protected set; }
		
		
		public L4DTeamPlayStats(XmlElement data)
		{
			Revived                 =   int.Parse(data.GetInnerText("revived"));
			MostRevivedDifficulty   =             data.GetInnerText("reviveddiff");
			AverageRevived          = float.Parse(data.GetInnerText("revivedavg"));
			AverageWasRevived       = float.Parse(data.GetInnerText("wasrevivedavg"));
			Protected               =   int.Parse(data.GetInnerText("protected"));
			MostProtectedDifficulty =             data.GetInnerText("protecteddiff");
			AverageProtected        = float.Parse(data.GetInnerText("protectedavg"));
			AverageWasProtected     = float.Parse(data.GetInnerText("wasprotectedavg"));
			FriendlyFireDamage      =   int.Parse(data.GetInnerText("ffdamage"));
			
			MostFriendlyFireDamageDifficulty =    data.GetInnerText("ffdamagediff");
			AverageFriendlyFireDamage=float.Parse(data.GetInnerText("ffdamageavg"));
		}
	}
	
	public class L4DVersusStats
	{
		public int    GamesPlayed               { get; protected set; }
		public int    GamesCompleted            { get; protected set; }
		public int    FinalesSurvived           { get; protected set; }
		public float  FinalesSurvivedPercentage { get; protected set; }
		public int    Points                    { get; protected set; }
		public string MostPointsInfected        { get; protected set; }
		public int    GamesWon                  { get; protected set; }
		public int    GamesLost                 { get; protected set; }
		public int    HighestSurvivorScore      { get; protected set; }
		
		public L4DVersusStats(XmlElement data)
		{
			GamesPlayed = int.Parse(data.GetInnerText("gamesplayed"));
			GamesCompleted = int.Parse(data.GetInnerText("gamescompleted"));
			FinalesSurvived = int.Parse(data.GetInnerText("finales"));
			FinalesSurvivedPercentage = (float)FinalesSurvived/GamesPlayed;
			Points = int.Parse(data.GetInnerText("points"));
			MostPointsInfected = data.GetInnerText("pointsas");
			GamesWon = int.Parse(data.GetInnerText("gameswon"));
			GamesLost = int.Parse(data.GetInnerText("gameslost"));
			HighestSurvivorScore = int.Parse(data.GetInnerText("survivorscore"));
			                     
		}
	}
	
	public abstract class AbstractL4DStats : GameStats
	{
		
		public L4DMostRecentGame MostRecentGame { get; protected set; }
		public L4DFavourite      Favourite      { get; protected set; }
		public L4DSurvivalStats  SurvivalStats  { get; protected set; }
		public L4DTeamPlayStats  TeamPlayStats  { get; protected set; }
		public L4DVersusStats    VersusStats    { get; protected set; }
		
		// life time stats
		
		public int    FinalesSurvived    { get; protected set; }
		public int    GamesPlayed        { get; protected set; }
		public int    InfectedKilled     { get; protected set; }
		public float  KillsPerHour       { get; protected set; }
		public float  AverageKitsShared  { get; protected set; }
		public float  AverageKitsUsed    { get; protected set; }
		public float  AveragePillsShared { get; protected set; }
		public float  AveragePillsUsed   { get; protected set; }
		public string TimePlayed         { get; protected set; }
		
		public float FinalesSurvivedPercentage { get; protected set; }
		
		
		public AbstractL4DStats(string steamid, string gamename)
			: base(steamid, gamename)
		{
			FetchData();
		}
		
		public AbstractL4DStats(long steamid, string gamename)
			: base(steamid, gamename)
		{
			FetchData();
		}
		
		protected new void FetchData()
		{
			if (IsPublic)
			{
				var stats = doc.GetXmlElement("stats");
				
				// TODO: check if node is empty (== null)
				MostRecentGame   = new L4DMostRecentGame(stats.GetXmlElement("mostrecentgame"));
				Favourite        = new      L4DFavourite(stats.GetXmlElement("favorites"));
				SurvivalStats    = new  L4DSurvivalStats(stats.GetXmlElement("survival"));
				TeamPlayStats    = new  L4DTeamPlayStats(stats.GetXmlElement("teamplay"));
				VersusStats      = new    L4DVersusStats(stats.GetXmlElement("versus"));
				
				// lifetime stats, O RLY?
				var lifeTime = stats.GetXmlElement("lifetime");
				FinalesSurvived = int.Parse(lifeTime.GetInnerText("finales"));
				GamesPlayed = int.Parse(lifeTime.GetInnerText("gamesplayed"));
				InfectedKilled = int.Parse(lifeTime.GetInnerText("infectedkilled"));
				KillsPerHour = float.Parse(lifeTime.GetInnerText("killsperhour"));
				AverageKitsShared = float.Parse(lifeTime.GetInnerText("kitsshared"));
				AverageKitsUsed = float.Parse(lifeTime.GetInnerText("kitsused"));
				AveragePillsShared = float.Parse(lifeTime.GetInnerText("pillsshared"));
				AveragePillsUsed = float.Parse(lifeTime.GetInnerText("pillsused"));
				TimePlayed = lifeTime.GetInnerText("timeplayed");
				
				FinalesSurvivedPercentage = (float)FinalesSurvived/GamesPlayed;	
			}
		}
		
	}

	public abstract class AbstractL4DWeapon : GameWeapon
	{
		// TODO: check the strings ows
		public string Accuracy           { get; protected set; }
		public string HeadShotPercentage { get; protected set; }
		public string KillPercentage     { get; protected set; }
		public string Name               { get; protected set; }
			
		public AbstractL4DWeapon(XmlElement data)
			: base(data)
		{
			Name  = data.Name;

			Shots              = int.Parse(data.GetInnerText("shots"));
			HeadShotPercentage =           data.GetInnerText("headshots");
			Accuracy           =           data.GetInnerText("accuracy");
		}
	}
	
	
	# region Left4Dead
	
	public class L4DMap
	{
		public enum Medals { None, Bronze, Silver, Gold };
		
		public static Medals MedalFrom(string medal)
		{
			switch (medal)
			{
			case "gold":
				return Medals.Gold;
			case "silver":
				return Medals.Silver;
			case "bronze":
				return Medals.Bronze;
			default:
				return Medals.None;
			}
		}
		
		public static Medals MedalFrom(int medal)
		{
			return (Medals)medal;
		}
		 
		public float  BestTime    { get; protected set; }
		public string ID          { get; protected set; }
		public Medals Medal       { get; protected set; }
		public int    TimesPlayed { get; protected set; }
		public string Name        { get; protected set; }
		
		public L4DMap(XmlElement data)
		{
			ID = data.Name;
			Name     =             data.GetInnerText("name");
			BestTime = float.Parse(data.GetInnerText("besttimeseconds"));
			Medal    =   MedalFrom(data.GetInnerText("medal"));
		}
	}
	
	public class L4DExplosive : GameWeapon
	{
		public static bool IsExplosive(string weaponname)
		{
			return ((weaponname == "molotov") || (weaponname == "pipes"));
		}
		public L4DExplosive(XmlElement data)
			: base(data)
		{
			ID = data.Name;
			Shots = int.Parse(data.GetInnerText("thrown"));
		}
		
		public float AverageKillsPerShot { get { return 1 / AverageShotsPerKill; } }
	}
	
	public class L4DWeapon : AbstractL4DWeapon
	{
		public L4DWeapon(XmlElement data)
			: base(data)
		{
			KillPercentage = data.GetInnerText("killpct");
		}
	}
	
	public class L4DStats : AbstractL4DStats
	{
		public L4DMap[] MapStats { get; protected set; }
		public GameWeapon[] WeaponStats { get; protected set; }
		
		public L4DStats(string steamid)
			: base(steamid, "l4d")
		{
			FetchData();
		}
		
		public L4DStats(long steamid)
			: base(steamid, "l4d")
		{
			FetchData();
		}
		
		protected new void FetchData()
		{
			if (IsPublic)
			{
				var stats = doc.GetXmlElement("stats");
				
				var survivalStats = stats.GetXmlElement("survival");
				List<L4DMap> mapList = new List<L4DMap>();
				foreach (XmlElement map in survivalStats.GetXmlElement("maps"))
				{
					mapList.Add(new L4DMap(map));
				}
				MapStats = mapList.ToArray();
				
				var weaponsStats = stats.GetXmlElement("weapons");
				List<GameWeapon> weaponList = new List<GameWeapon>();
				foreach (XmlElement weapon in weaponsStats)
				{
					if (L4DExplosive.IsExplosive(weapon.Name))
						weaponList.Add(new L4DExplosive(weapon));
					else
						weaponList.Add(new L4DWeapon(weapon));
				}
				WeaponStats = weaponList.ToArray();
			}
		}
	}
	
	#endregion
}