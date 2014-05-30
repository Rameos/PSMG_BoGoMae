using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float sensitivity = 30;
    public float cameraHeight = 3;

    public float minXAngle = -90;
    public float maxXAngle = 90;
    private float xAngle;
    private float yAngle;

    public float minDistance = 3;
    public float maxDistance = 15;
    private float currentDistance = 10;
    private float targetDistance;

    public float correctionSpeed = 10;
    public float zoomSpeed = 2;

    private bool isHoming = true;
    private float homingSpeed = 3f;


	void Start () 
    {

        if (target == null)
        {
            this.enabled = false;
        }
        else
        {
            xAngle = target.eulerAngles.x;
            yAngle = target.eulerAngles.y;
        }

        targetDistance = currentDistance;
	}
	

    void LateUpdate() 
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float scroolWheel = Input.GetAxis("Mouse ScrollWheel");

        float forward = Input.GetAxis("Vertical");
        float strafe = Input.GetAxis("Strafe");

        if (forward != 0 || strafe !=0)
        {
            isHoming = true;
        }
        else if (isHoming)
        {
            isHoming = false;
        }

        float lastXAngle = xAngle;

        if (Input.GetMouseButtonDown(0))
        {
            isHoming = false;
            xAngle = mouseY * Time.deltaTime * sensitivity;
            yAngle = mouseY * Time.deltaTime * sensitivity;
        }

        if (isHoming)
        {
            yAngle = Mathf.LerpAngle(yAngle, target.eulerAngles.y, homingSpeed * Time.deltaTime);

            if (Mathf.Abs(yAngle - target.eulerAngles.y) < 0.1f)
                //reset, stop homing
            {
                isHoming = false;
                yAngle = target.eulerAngles.y;
            }
        }

        float distanceCorrection = 0;
        float cameraRadius = 0.5f; 

        // check for ground collisions
        int layerMask = 1 << LayerMask.NameToLayer("Terrain");
        RaycastHit hit = new RaycastHit();
        Vector3 rayDirection = this.transform.position - target.position;
        if (Physics.SphereCast(target.position, cameraRadius, rayDirection, out hit, targetDistance, layerMask))
        {
            distanceCorrection = -targetDistance + hit.distance;
        }

        targetDistance = Mathf.Clamp(Mathf.SmoothStep(targetDistance, targetDistance - scroolWheel, Time.deltaTime * zoomSpeed), minDistance, maxDistance);
        currentDistance = Mathf.Clamp(targetDistance + distanceCorrection, minDistance, maxDistance);

        xAngle = ClampAngle(xAngle, minXAngle, maxXAngle);
        if (currentDistance == minDistance && distanceCorrection != 0 && (mouseX) < 0)
            // stop rotating
        {
            xAngle = lastXAngle;
        }

        this.transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);
        this.transform.position = target.position + this.transform.TransformDirection(new Vector3(0, cameraHeight, -currentDistance));
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if(angle < -360)
        {
            angle += 360;
        }
        if(angle > 360) 
        {
            angle -= 360;
        }
        return Mathf.Clamp (angle, min, max);
    }


}
