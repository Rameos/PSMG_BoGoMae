using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

    private float gameTime = 540f;
    private float showRefugeeTime;
    private string gameTimeString;
    private float minutes;
    private float seconds;
    private RefugeeMovement refugeeMovement;
    private int transmitterCounter;
    private string deadPlayer = null;
    private string winner = null;
    private Rect gameTimeGUIPosition = new Rect(Screen.width - 90, 50, 40, 30);
    private bool showMenu = false;
    private bool showRefugeeTrace = false;
    public Texture2D stopwatch;

    // Use this for initialization
    void Start()
    {
        GameeventManager.transmitterIsDestroydHandler += reactOnTransmitterDestroyd;
        GameeventManager.onGoalReachedHandler += reactOnGoalReached;
        GameeventManager.onDroneSetSlowTrapHandler += reactOnSetSlowTrap;
        GameeventManager.onShowEnemyHandler += reactOnShowEnemy;
        GameeventManager.onPlayerDiedHandler += reactOnPlayerDied;
        transmitterCounter = GameObject.FindGameObjectsWithTag("Transmitter").Length;
        Debug.Log(transmitterCounter);
    }

    // Update is called once per frame
    void Update()
    {
        if (Network.connections.Length == 1)
        {
            if (Network.isServer)
            {
                GameObject refugee = GameObject.FindGameObjectWithTag(Config.REFUGEE_TAG);
                refugee.transform.FindChild("Main Camera").gameObject.SetActive(false);
            }
            Countdown();
        }
        if (showRefugeeTrace)
        {
            showRefugeeTraceWhileEyesClosed();
        }
        ShowMenu();
        CheckPlayersAlive();
    }

    void CheckPlayersAlive()
    {
        if (deadPlayer != null)
        {
            if (deadPlayer == Config.REFUGEE_TAG)
            {
                networkView.RPC("QuitGame", RPCMode.All, "DroneWon");
            }
            else if (deadPlayer == Config.DRONE_TAG)
            {
                networkView.RPC("QuitGame", RPCMode.All, "RefugeeWon");
            }
        }
    }

    private void ShowMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showMenu = !showMenu;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Network.isClient)
                Application.LoadLevel("DroneWon");
            if (Network.isServer)
                Application.LoadLevel("RefugeeWon");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            showMenu = !showMenu;
        }

    }

    private void Countdown()
    {
        if (gameTime > 59)
        {
            minutes = gameTime / 60;
            seconds = gameTime % 60;
        }
        else
        {
            minutes = 0;
            seconds = gameTime % 60f;
        }

        if (seconds < 10)
        {
            minutes = (int)minutes;
            seconds = (int)seconds;
            gameTimeString = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            minutes = (int)minutes;
            seconds = (int)seconds;
            gameTimeString = minutes.ToString() + ":" + seconds.ToString();
        }
        gameTime -= Time.deltaTime;

    }

    private void reactOnGoalReached()
    {
        Debug.Log("Flüchtling erreicht Ziel, Sieg, hurra!");
        deadPlayer = "Drone";
    }

    private void reactOnTransmitterDestroyd()
    {
        Debug.Log("in react on transmitter destroyd");
        transmitterCounter--;
        if (transmitterCounter == 0)
        {

            Debug.Log("Refugee hat gewonnen | alle transmitter kaputt");
            networkView.RPC("CrashDrone", RPCMode.All, null);
        }
    }

    private void reactOnPlayerDied(GameObject gameObject)
    {
        Debug.Log("GameLogic.reactOnPlayerDied: " + gameObject.name);
        networkView.RPC("PlayerDied", RPCMode.All, gameObject.tag);
    }

    private void reactOnSetSlowTrap()
    {
        networkView.RPC("SlowDownRefugee", RPCMode.All, null);
    }

    private void reactOnShowEnemy()
    {
        //leuchtkegel in refugee script anzeigen
        Debug.Log("reactOnShowEnemy");
        networkView.RPC("ShowRefugeeTrace", RPCMode.All, null);
    }

    [RPC]
    public void CrashDrone()
    {
        GameObject drone = GameObject.FindGameObjectWithTag(Config.DRONE_TAG);
        drone.rigidbody.useGravity = true;
        drone.rigidbody.freezeRotation = false;
    }

    [RPC]
    public void PlayerDied(string gameOjectTag)
    {
        Debug.Log("RPC PlayerDied");
        if (gameOjectTag == Config.DRONE_TAG)
        {
            Debug.Log("1");
            deadPlayer = gameOjectTag;
            winner = "Refugee";
        }
        else if (gameOjectTag == Config.REFUGEE_TAG)
        {
            Debug.Log("2");
            deadPlayer = gameOjectTag;
            winner = "Drone";
        }
    }

    [RPC]
    public void ShowRefugeeTrace()
    {
        Debug.Log("ShowRefugeeTrace");
        showRefugeeTrace = true;
        showRefugeeTime = 20f;
        GameObject refugee = GameObject.FindGameObjectWithTag(Config.REFUGEE_TAG);
        refugee.GetComponent<RefugeeMovement>().showDiscoveredMessage = true;

        /*
        if (showRefugeeTrace) {
                // evtl nach kurzer zeit wieder deaktivieren
                refugee.transform.FindChild ("TraceLight").gameObject.SetActive (true);
                Debug.Log("show == true");
        } else {
                refugee.transform.FindChild ("TraceLight").gameObject.SetActive (false);
        }
      */
    }

    private void showRefugeeTraceWhileEyesClosed()
    {
        GameObject refugee = GameObject.FindGameObjectWithTag(Config.REFUGEE_TAG);
        if (ShowRefugeeTimeRemaining())
        {
            refugee.transform.FindChild("TraceLight").gameObject.SetActive(true);

        }
        else
        {
            refugee.transform.FindChild("TraceLight").gameObject.SetActive(false);
            showRefugeeTrace = false;
        }
        /*
        if (gazeModel.diamLeftEye == 0)
        {
            refugee.transform.FindChild("TraceLight").gameObject.SetActive(true);
            Debug.Log("sRTELEC if");

        }
        else
        {
            refugee.transform.FindChild("TraceLight").gameObject.SetActive(false);
            showRefugeeTrace = false;
            Debug.Log("sRTELEC else");
        }
        */

    }

    private bool ShowRefugeeTimeRemaining()
    {
        showRefugeeTime -= Time.deltaTime;
        if (showRefugeeTime <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    [RPC]
    public void SlowDownRefugee()
    {
        Debug.Log("RPC SlowDownRefugee");

        float speed = 1f;
        GameObject refugee = GameObject.FindGameObjectWithTag(Config.REFUGEE_TAG);
        refugee.GetComponent<RefugeeMovement>().setMovementSpeedTo(speed);

    }

    [RPC]
    public void QuitGame(string situation)
    {
        Application.LoadLevel(situation);
    }

    void OnGUI()
    {
        if (gameTime > 0)
        {
            GUIStyle font = new GUIStyle();
            font.fontSize = 20;
            font.fontStyle = FontStyle.Italic;
            font.normal.textColor = new Color32(50,50,50,150);
            GUI.Label(gameTimeGUIPosition," "+ gameTimeString, font);
            GUI.DrawTexture(new Rect(Screen.width-100, 10, 74, 90), stopwatch);

        }
        else
        {
            gameTimeString = "0:00";
            GUI.Box(gameTimeGUIPosition, "Zeit: " + gameTimeString);
            GUI.Label(new Rect(10, 10, 300, 20), "Time is over! Drone won the game, Refugee lost!");
            deadPlayer = "Refugee";
            networkView.RPC("QuitGame", RPCMode.All, "DroneWon");
        }

        if (showMenu)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 50), "B: Beenden"))
            {
                if (Network.isClient)
                    deadPlayer = "Refugee";
                if (Network.isServer)
                    deadPlayer = "Drone";
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 50), "F: Fortsetzen"))
            {
                showMenu = !showMenu;
            }

        }
    }
}
