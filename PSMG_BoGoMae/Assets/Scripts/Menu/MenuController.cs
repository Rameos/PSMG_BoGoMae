using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    private Ray ray;
    private RaycastHit hit;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Fluechtling")
                {
                    Application.LoadLevel("Refugee_Testlevel");
                }
                if (hit.transform.name == "Drohne")
                {
                    Application.LoadLevel("Drone_Testlevel");
                }
                if (hit.transform.name == "Quit")
                {
                    Application.Quit();
                }
            }
        }
    }
}
