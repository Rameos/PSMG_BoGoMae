using UnityEngine;
using System.Collections;

class AOI
{
    public Rect volume { get; set; }
    public Vector3 startPoint { get; set; }
    public Vector3 endPoint { get; set; }


    public AOI(Rect volume, Vector3 startPoint, Vector3 endPoint)
    {

        this.volume = volume;
        this.startPoint = startPoint;
        this.endPoint = endPoint;
    }

    
}
