using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using SteamCondenser.Steam.Packets;
using SteamCondenser.Steam.Packets.RCON;
using SteamCondenser.Steam.Sockets;

namespace SteamCondenser.Steam.Servers
{
	public class SourceServer : GameServer
	{
		protected RCONSocket rconSocket;
		protected int rconRequestId;
		
		public SourceServer(string ipAddress)
			: this(ipAddress, 27015)
		{
		}
		
		public SourceServer(string ipAddress, int portNumber)
			: this(IPAddress.Parse(ipAddress), portNumber)
		{
		}
		
		public SourceServer(IPAddress ipAddress)
			: this(ipAddress, 27015)
		{
		}
		
		public SourceServer(IPAddress ipAddress, int portNumber)
			: base(portNumber)
		{
			this.querySocket = new SourceServerQuerySocket(ipAddress, portNumber);
			this.rconSocket = new RCONSocket(ipAddress, portNumber);
		}
		
		public bool RconAuth(string password)
		{
			this.rconRequestId = (new Random()).Next();
			this.rconSocket.Send(new RCONAuthRequestPacket(this.rconRequestId, password));
			RCONAuthResponsePacket reply = this.rconSocket.GetReply() as RCONAuthResponsePacket;
			return (reply.RequestId == this.rconRequestId);
		}
		
		public string RconExec(string command)
		{
			this.rconSocket.Send(new RCONExecRequestPacket(this.rconRequestId, command));
			List<RCONExecResponsePacket> responsePackets=  new List<RCONExecResponsePacket>();
			RCONPacket responsePacket;
			
			try {
				while (true) {
					responsePacket = (RCONPacket)this.rconSocket.GetReply();
					if (responsePacket is RCONAuthResponsePacket)
					{
						throw new RCONNoAuthException();
					}
					responsePackets.Add((RCONExecResponsePacket)responsePacket);
				}
			} catch {
				if (responsePackets.Count == 0) throw new Exception();
			}
			
			string response = "";
			return "";
		}
		
	}
}
/*
namespace SteamCondenser.Steam.Servers
{
	public class SourceGameServer : GameServer
	{
		public SourceGameServer(IPAddress ipAddress, int port)
			: base(ipAddress, port)
		{
			this.querySocket = new SourceServerQuerySocket(ipAddress, port);
		}

		public override void UpdateServerInfo()
		{
			this.SendRequest(new SteamPacket(SteamPacketTypes.A2S_INFO, Encoding.ASCII.GetBytes("Source Engine Query")));

			SteamPacket packet = this.querySocket.GetReply();
			SourceServerInfoResponsePacket replyPacket = (SourceServerInfoResponsePacket)packet;

			this.serverInfo = replyPacket.ServerInfo;

			if(this.serverInfo.Port > 0)
			{
				this.gamePort = this.serverInfo.Port;
			}
			else if(this.serverInfo.ServerType != ServerTypes.Proxy)
			{
				this.gamePort = this.port;
			}

			if(this.serverInfo.SpectatorPort > 0)
			{
				this.spectatorPort = this.serverInfo.SpectatorPort;
			}
			else if(this.serverInfo.ServerType == ServerTypes.Proxy)
			{
				this.spectatorPort = this.port;
			}
		}

		public override void UpdateServerRules()
		{
			this.SendRequest(new SteamPacket(SteamPacketTypes.A2S_RULES));

			SteamPacket packet = this.querySocket.GetReply();

			if(packet.PacketType == SteamPacketTypes.S2C_CHALLENGE)
			{
				int challengeID = ((ChallengeResponsePacket)packet).ChallengeID;

				this.SendRequest(new SteamPacket(SteamPacketTypes.A2S_RULES, BitConverter.GetBytes(challengeID)));

				packet = this.querySocket.GetReply();
				ServerRulesResponsePacket replyPacket = (ServerRulesResponsePacket)packet;
				this.serverRules = replyPacket.ServerRules;
			}
		}

		public override void UpdatePlayerList()
		{
			this.playerList = new List<SteamPlayer>();

			if(this.serverInfo != null && this.serverInfo.ServerType != ServerTypes.Proxy)
			{
				this.SendRequest(new SteamPacket(SteamPacketTypes.A2S_PLAYER));

				SteamPacket packet = this.querySocket.GetReply();

				if(packet.PacketType == SteamPacketTypes.S2C_CHALLENGE)
				{
					int challengeID = ((ChallengeResponsePacket)packet).ChallengeID;

					this.SendRequest(new SteamPacket(SteamPacketTypes.A2S_PLAYER, BitConverter.GetBytes(challengeID)));

					packet = this.querySocket.GetReply();
					PlayersResponsePacket replyPacket = (PlayersResponsePacket)packet;
					this.playerList = replyPacket.PlayerList;

				}
			}
		}

		public override void UpdatePing()
		{
			DateTime pingStart = DateTime.Now;
			double currentPing = 0.0;

			this.SendRequest(new SteamPacket(SteamPacketTypes.A2A_PING));

			SteamPacket packet = this.querySocket.GetReply();

			if(packet.PacketType == SteamPacketTypes.A2A_ACK)
			{
				// Update ping
				TimeSpan pingTime = DateTime.Now.Subtract(pingStart);
				currentPing = pingTime.TotalMilliseconds;

				this.pingAverage = ((this.pingAverage * this.pingCount) + currentPing) / (this.pingCount + 1);
				this.ping = currentPing;
				this.pingCount++;
			}
		}
	}
}
*/