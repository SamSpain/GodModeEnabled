using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectElementSpawn : MonoBehaviour {
    public int spawn;
    public GameObject ElementSpawner;
    private GameObject ToolBelt;

    private void Start()
    {
        ToolBelt = GameObject.Find("[CameraRig]/ToolBelt");
    }

    public void Spawn()
    { spawn = 1; }

	
	// Update is called once per frame
	void Update () {

    if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(0);
        }
        if (spawn == 1){
            // Spawn New Sphere
            GameObject objectClone = (GameObject)Instantiate(ElementSpawner, transform.position, Quaternion.identity,ToolBelt.transform);
            spawn = 0;
        };
		
	}
}
