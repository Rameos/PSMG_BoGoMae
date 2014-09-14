using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	private const string typeName = "PSMGHideAndSeek_christoph";
	private const string gameName = "HideAndSeekRoom_christoph";
    private AudioSource sound;

    private AudioClip audio;
	
	private bool isRefreshingHostList = false;
	private HostData[] hostList;
    GameObject menuCamera;
	
	//public GameObject playerPrefab;

    void Start()
    {
       sound  = GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<AudioSource>();
       audio = (AudioClip)(Resources.Load("Menue"));
    }
	
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
            {
                sound.PlayOneShot(audio);
                StartServer();
            }


            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
            {
                sound.PlayOneShot(audio);
                RefreshHostList();

            }
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
                    if (GUI.Button(new Rect(100, 400 + (150 * i), 300, 100), hostList[i].gameName)) 
                    {
                        sound.PlayOneShot(audio);
						JoinServer(hostList[i]);
                    }
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

        //InstantiateDrone();
		
        InstantiateRefugee();
		
    }

    private void InstantiateRefugee()
    {
        GameObject refugeePlayer = SpawnPlayer(Config.INSTANTIATE_REFUGEE, new Vector3(-30, 2, 50));
        refugeePlayer.GetComponent<RefugeeFPShooting>().enabled = true;
        refugeePlayer.GetComponent<CameraController>().enabled = true;
        refugeePlayer.GetComponent<HasHealth>().enabled = true;
        refugeePlayer.GetComponent<RefugeeMovement>().enabled = true;
        refugeePlayer.GetComponent<CharacterController>().enabled = true;
        refugeePlayer.GetComponent<GazeInputFromAOI>().enabled = true;
        refugeePlayer.GetComponent<ChangeItemInput>().enabled = true; 
        //refugeePlayer.transform.FindChild("Main Camera").GetComponent<ChangeItemInput>().enabled = true;
		refugeePlayer.transform.FindChild("Main Camera").gameObject.SetActive(true);
		GameObject.Find ("Menu Camera").camera.gameObject.SetActive (false);
    }

    private void InstantiateDrone()
    {
        GameObject dronePlayer = SpawnPlayer(Config.INSTANTIATE_DRONE, new Vector3(-30, 500, 50));
        dronePlayer.GetComponent<DroneController>().enabled = true;
        dronePlayer.GetComponent<Energymanagement>().enabled = true;
        dronePlayer.GetComponent<DroneItemBehavior>().enabled = true;
        dronePlayer.GetComponent<HasHealth>().enabled = true;
        dronePlayer.transform.FindChild("DroneCamera").gameObject.SetActive(true);
        dronePlayer.GetComponent<DroneRocketAttack>().enabled = true;
		GameObject.Find ("Menu Camera").camera.gameObject.SetActive (false);
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
        InstantiateRefugee();
	}
	
	
	private GameObject SpawnPlayer(string player, Vector3 position)
	{
        
		return (GameObject) Network.Instantiate(Resources.Load(player), position, Quaternion.identity, 0);
	}
}