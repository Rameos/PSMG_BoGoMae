using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

    string registerGameName = "HideAndSeekTestServer";
    string serverNameVisibleToPlayer = "Hide&Seek Server";
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
        if (Network.isClient || Network.isServer)
        {
            return;
        }
        if (GUI.Button(new Rect(25f, 25f, 150f, 30f), "Start new server!"))
        {
            StartServer();
        }
        if (GUI.Button(new Rect(25f, 65f, 150f, 30f), "Refresh server list!"))
        {
            StartCoroutine("RefreshHostList");
        }

        if (hostData != null)
        {
            for (int i = 0; i < hostData.Length; i++) 
            {
                if (GUI.Button(new Rect(Screen.width / 2, 65f + (30f * i), 300f, 30f), hostData[i].gameName))
                {
                    Debug.Log("connect to server");
                    Network.Connect(hostData[i]);
                }
            }
        }
    }
}
