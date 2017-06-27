using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardObject : MonoBehaviour {

    

    public void Update()
    {
        transform.LookAt(transform.position + transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
