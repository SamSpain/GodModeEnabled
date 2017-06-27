using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour {
    // Class associated with pre-fab for 
    //Selection of Spawner for type
    public GameObject Spawner;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    
    }

    public void DeParent()
    { transform.parent = null; }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "FireSpawn" || col.gameObject.name == "FireSpawn(Clone)")
        {
            //Debug (
            Debug.Log("fire");
        }
      
        //If x hit fire Prefab
        if(col.gameObject.name == "FireMobilePrefab")
        {
            //Water puts out fire
            if (gameObject.name == "WaterSpawn" || gameObject.name == "WaterSpawn(Clone)")
            {
                Destroy(col.gameObject);
                GameObject wsl = GameObject.Find("ToolBelt/WaterSpawnLoc");
                wsl.GetComponent<ObjectElementSpawn>().spawn = 1;
            }

            //Agent Dies to fire
            if (gameObject.name == "Agent" || gameObject.name == "Agent(Clone)")
            {
                gameObject.GetComponent<AudioSource>().Play();
               Destroy(gameObject);
            }
                
        }

        if (col.gameObject.name == "BigOakPrefabs")
        {
            if (gameObject.name == "RockSpawn" || gameObject.name == "RockSpawn(Clone)")
            {
                //Play Bird Music on Collision
                GameObject tempCol = col.gameObject;
                tempCol.GetComponent<AudioSource>().Play();
                //--------------------------------------------------------
                GameObject rsl = GameObject.Find("ToolBelt/RockSpawnLoc");
                rsl.GetComponent<ObjectElementSpawn>().spawn = 1;

            }

            //Fire Impact on Tree / Destory Both
            //Replace Tree with fire... can you take out fire at the moment?
            if (gameObject.name == "FireSpawn" || gameObject.name == "FireSpawn(Clone)")
            {
                Vector3 TempVector = col.gameObject.transform.localPosition;
                Destroy(col.gameObject);
                GameObject FireMin = GameObject.Find("FireMobilePrefab");
                GameObject objectClone = (GameObject)Instantiate(FireMin, TempVector, Quaternion.identity);
                objectClone.transform.parent = null;
                objectClone.transform.name = FireMin.name;
            }

        }

        //Hit Tree with X
        if (col.gameObject.name == "TreePrefabs")
        {
            //Grass Impact on Tree Spawn Forest
            if (gameObject.name == "GrassSpawn" || gameObject.name == "GrassSpawn(Clone)")
            {
                Vector3 TempVector = col.gameObject.transform.localPosition;
                Destroy(col.gameObject);
                GameObject BigOak = GameObject.Find("BigOakPrefabs");
                GameObject objectClone = (GameObject)Instantiate(BigOak, TempVector, Quaternion.identity);
                objectClone.transform.parent = null;
                objectClone.transform.name = BigOak.name;
            }


            //Tree Impact on Tree
            if (gameObject.name == "TreeSpawn" || gameObject.name == "TreeSpawn(Clone)")
            {
                Vector3 TempVector = col.gameObject.transform.localPosition;
                Destroy(col.gameObject);
                GameObject BigOak = GameObject.Find("BigOakPrefabs");
                GameObject objectClone = (GameObject)Instantiate(BigOak, TempVector, Quaternion.identity);
                objectClone.transform.parent = null;
                objectClone.transform.name = BigOak.name;
            }

            //Fire Impact on Tree / Destory Both
            //Replace Tree with fire... can you take out fire at the moment?
            if (gameObject.name == "FireSpawn" || gameObject.name == "FireSpawn(Clone)")
            {
                Vector3 TempVector = col.gameObject.transform.localPosition;
                Destroy(col.gameObject);
                GameObject FireMin = GameObject.Find("FireMobilePrefab");
                GameObject objectClone = (GameObject)Instantiate(FireMin, TempVector, Quaternion.identity);
                objectClone.transform.parent = null;
                objectClone.transform.name = FireMin.name;
            }

            //Wood and Stone to Create Cave
            if (gameObject.name == "RockSpawn" || gameObject.name == "RockSpawn(Clone)")
            {
                Vector3 TempVector = col.gameObject.transform.localPosition;
                Destroy(col.gameObject);
                GameObject Cave = GameObject.Find("CavePrefab");
                GameObject objectClone = (GameObject)Instantiate(Cave, TempVector, Quaternion.identity);
                objectClone.transform.parent = null;
                objectClone.transform.name = "Cave";
            }



        }
        //Hit Terrian with X
        if (col.gameObject.name == "TerrainPrefab")
        {
            //Spawn instance of the object on collison
            GameObject objectClone = (GameObject)Instantiate(Spawner, transform.position, Quaternion.identity);
            objectClone.transform.parent = null;
            objectClone.transform.name = Spawner.name;

            if (gameObject.name == "TreeSpawn" || gameObject.name == "TreeSpawn(Clone)")
            { 
                GameObject tsl = GameObject.Find("ToolBelt/TreeSpawnLoc");
                tsl.GetComponent<ObjectElementSpawn>().spawn = 1;
            }

            if (gameObject.name == "WaterSpawn" || gameObject.name == "WaterSpawn(Clone)")
            {
                GameObject wsl = GameObject.Find("ToolBelt/WaterSpawnLoc");
                wsl.GetComponent<ObjectElementSpawn>().spawn = 1;
                
            }

            if (gameObject.name == "RockSpawn" || gameObject.name == "RockSpawn(Clone)")
            {
                GameObject rsl = GameObject.Find("ToolBelt/RockSpawnLoc");
                rsl.GetComponent<ObjectElementSpawn>().spawn = 1;
             

            }

            if (gameObject.name == "FireSpawn" || gameObject.name == "FireSpawn(Clone)")
            {
                GameObject fsl = GameObject.Find("ToolBelt/FireSpawnLoc");
                fsl.GetComponent<ObjectElementSpawn>().spawn = 1;
               
            }

            if (gameObject.name == "GrassSpawn" || gameObject.name == "GrassSpawn(Clone)")
            {
                GameObject gsl = GameObject.Find("ToolBelt/GrassSpawnLoc");
                gsl.GetComponent<ObjectElementSpawn>().spawn = 1;    

            }

            //Destory sphere on contact
            Destroy(gameObject);

        }
    }
}
