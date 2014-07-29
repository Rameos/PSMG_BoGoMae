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
    private bool isAOIVisualisationOn = true;

    private AOI AOI_Left;
    private AOI AOI_Right;
    private float offSetRightAOI;

    /// <summary>
    /// Zeichnet die AOI
    /// </summary>
    void OnGUI()
    {
        if (isAOIVisualisationOn)
        {
            GUI.DrawTexture(AOI_Left.volume, texture_LeftAOI);
            GUI.DrawTexture(AOI_Right.volume, texture_RightAOI);
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


    /// <summary>
    /// Überprüft, ob der Blick in einer AOI gelandet ist und berechnet die Rotationsgeschwindigkeit
    /// </summary>
    public float checkGazeInput()
    {
        Vector3 actualEyePosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        float speed = 0;

        //Links
        if (AOI_Left.volume.Contains(actualEyePosition))
        {
            speed = -(Mathf.Abs((AOI_Left.volume.width - actualEyePosition.x) / AOI_Left.volume.width));
        }

            //Rechts
        else if (AOI_Right.volume.Contains(actualEyePosition))
        {
            speed = Mathf.Abs(((AOI_Right.volume.width - actualEyePosition.x) + offSetRightAOI) / AOI_Right.volume.width);
        }

        return speed;
    }
}

