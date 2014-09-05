using UnityEngine;
using System.Collections;

public class GazeInputFromAOI : MonoBehaviour
{


    [SerializeField]
    private Texture2D texture_LeftAOI;
    [SerializeField]
    private Texture2D texture_RightAOI;
    [SerializeField]
    private float widthAOI;
    [SerializeField]
    private Texture2D texture_TopAOI;
    [SerializeField]
    private Texture2D texture_DownAOI;
    [SerializeField]
    private float heightAOI;

    [SerializeField]
    private bool isAOIVisualisationOn = true;

    private AOI AOI_Top;
    private AOI AOI_Down;
    private AOI AOI_Left;
    private AOI AOI_Right;
    private float offSetDownAOI, offSetRightAOI;

    /// <summary>
    /// Zeichnet die AOI
    /// </summary>
    void OnGUI()
    {
        if (isAOIVisualisationOn)
        {
            GUI.DrawTexture(AOI_Top.volume, texture_LeftAOI);
            GUI.DrawTexture(AOI_Down.volume, texture_RightAOI);

        }
    }

    /// <summary>
    /// Berechnet zu Beginn der App die fläche der AOI
    /// </summary>
    void Start()
    {
        calculateAOI();
    }

    /// <summary>
    /// Upadatet die AOIs während der nutzung (Nützlich fürs Debugging)
    /// </summary>
    void Update()
    {
        calculateAOI();
    }

    /// <summary>
    ///  Definiert zwei AOI Flächen; Links und Rechts für die Rotation, die Später übergeben werden soll 
    /// </summary>
    private void calculateAOI()
    {
        setAOIsTopAndDown();
        setAOIsLeftAndRight();
         
    }

    private void setAOIsLeftAndRight()
    {
        Rect leftVolume = new Rect(0, 0, Screen.width * widthAOI, Screen.height);
        Vector3 leftStart = Vector3.zero;
        Vector3 leftEnd = new Vector3(Screen.width * widthAOI, 0, 0);
        AOI_Left = new AOI(leftVolume, leftStart, leftEnd);

        Rect rightVolume = new Rect(Screen.width - Screen.width * widthAOI, 0, Screen.width * widthAOI, Screen.height);
        Vector3 rightEnd = new Vector3(Screen.width, 0, 0);
        Vector3 rightStart = new Vector3(Screen.width - Screen.width * widthAOI, 0, 0);
        AOI_Right = new AOI(rightVolume, rightStart, rightEnd);

        offSetRightAOI = AOI_Right.startPoint.x - AOI_Left.endPoint.x;
    }

    private void setAOIsTopAndDown()
    {
        Rect topVolume = new Rect(0, 0, Screen.width, Screen.height * heightAOI);
        Vector3 topStart = new Vector3(0, 0, 0);
        Vector3 topEnd = new Vector3(Screen.width, Screen.height * 0.5f, 0);
        AOI_Top = new AOI(topVolume, topStart, topEnd);

        Rect downVolume = new Rect(0, Screen.height * 0.5f, Screen.width, Screen.height - Screen.height * heightAOI);
        Vector3 downEnd = new Vector3(Screen.width, Screen.height, 0);
        Vector3 downStart = new Vector3(0, Screen.height - Screen.height * 0.5f, 0);
        AOI_Down = new AOI(downVolume, downStart, downEnd);

        offSetDownAOI = AOI_Down.startPoint.x - AOI_Top.endPoint.x;
    }


    /// <summary>
    /// Überprüft, ob der Blick in einer AOI gelandet ist und berechnet die Rotationsgeschwindigkeit
    /// </summary>
    public float gazeRotationSpeedXAxis()
    {
        Vector3 actualEyePosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        float speed = 0;

        //Top
        if (AOI_Top.volume.Contains(actualEyePosition))
        {
            speed = -(Mathf.Abs((AOI_Top.volume.width - actualEyePosition.x) / AOI_Top.volume.width));
        }

            //Down
        else if (AOI_Down.volume.Contains(actualEyePosition))
        {
            speed = Mathf.Abs(((AOI_Down.volume.width - actualEyePosition.x) + offSetDownAOI) / AOI_Down.volume.width);
        }



        return speed;
    }
    public float gazeRotationSpeedYAxis()
    {
        Vector3 actualEyePosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        float speed = 0;

        //Left
        if (AOI_Left.volume.Contains(actualEyePosition))
        {
            speed = -(Mathf.Abs((AOI_Left.volume.width - actualEyePosition.x) / AOI_Left.volume.width));
        }

        //Right
        else if (AOI_Right.volume.Contains(actualEyePosition))
        {
            speed = Mathf.Abs(((AOI_Right.volume.width - actualEyePosition.x) + offSetRightAOI) / AOI_Right.volume.width);
        }

        return speed;
    }
}

