using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to spawn objects on the ground
/// </summary>
public class PlaceholderSpawner : MonoBehaviour
{
    public GameObject canPrefab;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {


                GameObject canInstance = (GameObject)Instantiate(canPrefab, hit.point, Quaternion.identity);
                canInstance.transform.parent = null;
            }
        }

        if (Input.GetMouseButtonDown(1))
        { // if left button pressed...

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {


                GameObject canInstance = (GameObject)Instantiate(canPrefab, hit.point, Quaternion.identity);
                canInstance.transform.parent = null;
            }
        }
    }

}
