using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

    string registerGameName = "HideAndSeekTestServer";
    string serverNameVisibleToPlayer = "Laufendes Spiel betreten";
    string serverComment = "gameServer";
    int numberOfPlayers = 2;
    bool isRefreshing = false;
    float refreshRequestLenght = 3.0f;
    HostData[] hostData;

    public void StartServer() 
    {

        Network.InitializeServer(numberOfPlayers, 25002, false);
        MasterServer.RegisterHost(registerGameName, serverNameVisibleToPlayer, serverComment);
    }

    public void OnServerInitialized()
    {
        Debug.Log("Server initialized");
    }

    void OnMasterServerEvent(MasterServerEvent masterServerEvent)
    {
        if (masterServerEvent == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registration Successful!");
            SpawnPlayer();
        }
    }

    public IEnumerator RefreshHostList()
    {
        Debug.Log("Refreshing HostList");
        MasterServer.RequestHostList(registerGameName);
        float timeStarted = Time.time;
        float timeEnd = Time.time + refreshRequestLenght;

        while (Time.time < timeEnd)
        {
            hostData = MasterServer.PollHostList();
            yield return new WaitForEndOfFrame();
        }

        if (hostData == null || hostData.Length == 0)
        {
            Debug.Log("No active Server found.");
        }
        else {
            Debug.Log(hostData.Length + " habe been found.");
        }

    }


    public void OnGUI()
    {
        float buttonWidth = 200f;
        float buttonHeight = 30f;

        if (Network.isClient || Network.isServer)
        {
            return;
        }
        if (GUI.Button(new Rect(Screen.width/2 - buttonWidth/2, Screen.height/2 - buttonHeight/2, buttonWidth, buttonHeight), "Start new game as Drone"))
        {
            StartServer();
            Application.LoadLevel("NetworkTestScene");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - buttonHeight / 2 + buttonHeight*2, buttonWidth, buttonHeight), "Look for running game"))
        {
            StartCoroutine("RefreshHostList");
        }

        if (hostData != null)
        {
            for (int i = 0; i < hostData.Length; i++) 
            {
                if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - buttonHeight / 2 + buttonHeight * 4, buttonWidth, buttonHeight), hostData[i].gameName))
                {
                    Network.Connect(hostData[i]);
                    Application.LoadLevel("NetworkTestScene");
                    SpawnPlayer();

                }
            }
        }
    }

    private void SpawnPlayer() 
    {

        if(Network.isServer) {
            Vector3 spawnPositionDrone = new Vector3(0, 850, 0);
            InstantiatePlayer(Config.INSTANTIATE_DRONE, spawnPositionDrone);
            Debug.Log("Drone is Server: "+Network.isServer);
            GameeventManager.droneIsActive();
        }
        else if (Network.isClient)
        {
            Vector3 spawnPositionRefugee = new Vector3(0, 1, 0);
            InstantiatePlayer(Config.INSTANTIATE_REFUGEE, spawnPositionRefugee);
            Debug.Log("Refugee is Client: " + Network.isClient);
            GameeventManager.refugeeIsActive();
        }
    }

    

    private void InstantiatePlayer(string player, Vector3 vector3SpawnPosition)
    {
        Network.Instantiate(Resources.Load(player), vector3SpawnPosition, Quaternion.identity, 0);
    }
}
