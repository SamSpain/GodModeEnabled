using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

    public GameObject rock;
    public bool built = false;
    public bool building = false;
    public Vector3 target;
    public float distance;

    public void Build()
    {
        building = true;
        target = rock.transform.position;
        target = new Vector3(rock.transform.position.x, rock.transform.position.y + 5, rock.transform.position.z);
    }
    public void Update()
    {
        if (building)
        {
            distance = Vector3.Distance(target, rock.transform.position);
            if (distance > 0.5f)
            {
                rock.transform.Translate(Vector3.up * Time.deltaTime);



            }
            else
            {
                building = false;
                built = true;
                rock.AddComponent<Rigidbody>();
                rock.AddComponent<BoxCollider>();
                rock.GetComponent<BoxCollider>().size = new Vector3(0.1f, 0.0001f, 0.1f);
            }

        }
    }


}
