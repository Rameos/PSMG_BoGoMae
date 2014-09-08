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
        
		GameObject dronePlayer = SpawnPlayer(Config.INSTANTIATE_DRONE, new Vector3(0, 350, 0));
        dronePlayer.GetComponent<DroneController>().enabled = true;
        dronePlayer.transform.FindChild("DroneCamera").gameObject.SetActive(true);
        
        
        // droneplayer block auskommentieren, wenn man refugee testen möchte:

        /*
        GameObject refugeePlayer = SpawnPlayer(Config.INSTANTIATE_REFUGEE, new Vector3(-30, 2, 0));
        refugeePlayer.GetComponent<RefugeeFPShooting>().enabled = true;
        refugeePlayer.GetComponent<CameraController>().enabled = true;
        refugeePlayer.GetComponent<RefugeeMovement>().enabled = true;
        refugeePlayer.GetComponent<CharacterController>().enabled = true;
        refugeePlayer.transform.FindChild("Main Camera").gameObject.SetActive(true);
        */
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
		GameObject refugeePlayer = SpawnPlayer(Config.INSTANTIATE_REFUGEE, new Vector3(-30, 2, 0));
        refugeePlayer.GetComponent<RefugeeFPShooting>().enabled = true;
        refugeePlayer.GetComponent<CameraController>().enabled = true;
        refugeePlayer.GetComponent<RefugeeMovement>().enabled = true;
        refugeePlayer.GetComponent<CharacterController>().enabled = true;
        refugeePlayer.GetComponent<GazeInputFromAOI>().enabled = true;
        refugeePlayer.transform.FindChild("Main Camera").GetComponent<ChangeItemInput>().enabled = true;
        refugeePlayer.transform.FindChild("Main Camera").gameObject.SetActive(true);
	}
	
	
	private GameObject SpawnPlayer(string player, Vector3 position)
	{
        
		return (GameObject) Network.Instantiate(Resources.Load(player), position, Quaternion.identity, 0);
	}
}