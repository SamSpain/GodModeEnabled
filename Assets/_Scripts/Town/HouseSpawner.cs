using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{

    public GameObject house;
    public bool built = false;
    public bool building = false;
    public Vector3 target;
    public float distance;


    

    public void Build()
    {
        building = true;
        target = house.transform.position;
        target = new Vector3(house.transform.position.x, house.transform.position.y + 3, house.transform.position.z);
    }
    public void Update()
    {
        if(building)
        {
            distance = Vector3.Distance(target, house.transform.position);
            if (distance > 0.5f)
            {
                house.transform.Translate(Vector3.up * Time.deltaTime);
                

                
            }
            else
            {
                building = false;
                built = true;
                house.AddComponent<Rigidbody>();
                house.AddComponent<BoxCollider>();
                house.GetComponent<BoxCollider>().size = new Vector3(0.1f, 0.0001f, 0.1f);
            }
            
        }
    }
}
