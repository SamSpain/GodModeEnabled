using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBeltUpdate : MonoBehaviour {
    public GameObject trackingCamera;
    public float yRotation;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
     
        transform.localPosition = trackingCamera.transform.localPosition;
        transform.localEulerAngles = trackingCamera.transform.localEulerAngles;

        Vector3 TempRot = transform.localEulerAngles;
        TempRot.x = 0;
        TempRot.z = 0;
        //TempRot.y = 0;
        transform.localEulerAngles = TempRot;

        Vector3 TempPos = transform.localPosition;
        TempPos.y = transform.localPosition.y - 0.5f;
        //TempPos.z = transform.localPosition.z - 0.3f;

        transform.localPosition = TempPos;

    }
}
