using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	private const string typeName = "PSMGHideAndSeek";
	private const string gameName = "HideAndSeekRoom";
	
	private bool isRefreshingHostList = false;
	private HostData[] hostList;
	
	//public GameObject playerPrefab;
	
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}
	
	private void StartServer()
	{
		Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	
	void OnServerInitialized()
	{
		SpawnPlayer(Config.INSTANTIATE_DRONE, new Vector3(0, 850, 0));
	}
	
	
	void Update()
	{
		if (isRefreshingHostList && MasterServer.PollHostList().Length > 0)
		{
			isRefreshingHostList = false;
			hostList = MasterServer.PollHostList();
		}
	}
	
	private void RefreshHostList()
	{
		if (!isRefreshingHostList)
		{
			isRefreshingHostList = true;
			MasterServer.RequestHostList(typeName);
		}
	}
	
	
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		SpawnPlayer(Config.INSTANTIATE_REFUGEE, new Vector3(0, 1, 0));
	}
	
	
	private void SpawnPlayer(string player, Vector3 position)
	{
		Network.Instantiate(Resources.Load(player), position, Quaternion.identity, 0);
	}
}