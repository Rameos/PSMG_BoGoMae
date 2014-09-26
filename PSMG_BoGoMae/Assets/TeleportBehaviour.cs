using UnityEngine;
using System.Collections;

public class TeleportBehaviour : MonoBehaviour
{
    public Texture2D noWeaponTexture;
    private float gazeSpeed = 2f;
    private bool showNoWeaponText = false;
    private bool inShooting = false;
    private bool onTeleporter = false;
    private Rect gazeTexturePosition = new Rect(0f, 0f, Screen.width, 250f);
    public Texture2D crosshair;
    public Texture2D gazeTexture;
    private Vector3 refugeeTeleportPositionTo;
    private GameObject refugee;
    private Camera camera;
    private float xAxisWithLimit;
    private float yAxisWithLimit;
    private GazeInputFromAOI gazeInput;
    private float widthAOI;
    private float heightAOI;
    private AOI AOI_Top;


    // Use this for initialization
    void Start()
    {
        GameeventManager.onEnableShootHandler += reactOnEnableShoot;
        GameeventManager.onDisableShootHandler += reactOnDisableShoot;
    }

    private void reactOnDisableShoot()
    {
        inShooting = false;
        showNoWeaponText = false;
    }

    private void reactOnEnableShoot()
    {
        inShooting = true;
    }

    void OnGUI()
    {
        if (showNoWeaponText && Network.isClient)
        {
            GUI.Label(new Rect((Screen.width / 2) - 256f, (Screen.height / 2)- 256f, 512f, 512f), noWeaponTexture);
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (onTeleporter && !inShooting)
        {
            
            if (Network.isClient)
            {
                GameeventManager.onTeleporterField();
                refugee.transform.FindChild("Main Camera").gameObject.SetActive(false);
                refugee.transform.FindChild("TopDownCamera").gameObject.SetActive(true);
                camera = refugee.transform.FindChild("TopDownCamera").camera;
                camera.transform.position = new Vector3(camera.transform.position.x + gazeInput.getCameraXPositionWithGaze() * gazeSpeed, camera.transform.position.y, camera.transform.position.z + gazeInput.getCameraYPositionWithGaze() * gazeSpeed);
                lookForTeleportPosition();
                teleport();

            }
        }

    }



    private float cameraPositionFromGaze()
    {
        Vector3 actualEyePosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        float speed = 0;

        //Top
        if (AOI_Top.volume.Contains(actualEyePosition))
        {
            speed++;
        }

        return speed;
    }


    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.tag == Tags.REFUGEE)
        {
            if (inShooting)
            {
                showNoWeaponText = true;
            }
            else
            {
                refugee = GameObject.FindGameObjectWithTag("Refugee").gameObject;
                gazeInput = refugee.gameObject.GetComponent<GazeInputFromAOI>();
                if (Network.isClient)
                {
                    refugee.transform.FindChild("TopDownCamera").gameObject.SetActive(true);
                    camera = refugee.transform.FindChild("TopDownCamera").gameObject.camera;
                    camera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    onTeleporter = true;
                    calculateAOI();

                }
            }
        }
        else
        {
            onTeleporter = false;
        }
    }

    private void calculateAOI()
    {
        Rect topVolume = new Rect(0, 0, Screen.width, 250f);
        Vector3 topStart = Vector3.zero;
        Vector3 topEnd = new Vector3(Screen.width, 0, 0);
        AOI_Top = new AOI(topVolume, topStart, topEnd);
    }

    void OnTriggerExit(Collider collider)
    {
        showNoWeaponText = false;
        if (inShooting)
        {

        }
        else
        {
            GameeventManager.onTeleportLeft();
            if (Network.isClient)
            {
                refugee.transform.FindChild("TopDownCamera").gameObject.SetActive(false);
                refugee.transform.FindChild("Main Camera").gameObject.SetActive(true);
                refugee.GetComponent<CameraController>().playSoundIfEnabled();
            }
        }
        if (collider.gameObject.tag == Tags.REFUGEE)
        {
            onTeleporter = false;
        }

    }

    private void lookForTeleportPosition()
    {
        RaycastHit hit;
        Vector3 refugeeTeleportPositionFrom = refugee.transform.position;
        Ray ray = camera.ScreenPointToRay(camera.transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 teleportPosition = camera.transform.position;
        }

        refugeeTeleportPositionTo = new Vector3(camera.transform.position.x, 2f, camera.transform.position.z);

    }

    private void teleport()
    {

        if (Input.GetButtonUp("Teleport"))
        {
            if (Network.isClient)
            {
            refugee.transform.position = refugeeTeleportPositionTo;
            refugee.transform.FindChild("Main Camera").gameObject.SetActive(true);
            refugee.transform.FindChild("TopDownCamera").gameObject.SetActive(false);
            camera = refugee.transform.FindChild("Main Camera").camera;
            refugee.GetComponent<CameraController>().playSoundIfEnabled();
            }

        }
    }

    private Vector3 getRefugeeTeleportPositionTo()
    {
        Vector3 actualEyePosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        return actualEyePosition;
    }



}
