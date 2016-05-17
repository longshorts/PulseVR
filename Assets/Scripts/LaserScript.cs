using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    Vector3 oldPosition;

	// Use this for initialization
	void Start () {
        oldPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if (oldPosition != Vector3.zero)
        {
            Ray ray = new Ray(transform.position, oldPosition - transform.position);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject obj = hitInfo.collider.gameObject;
                if (obj.CompareTag("target"))
                {
                    if (obj.GetComponent<TargetController>().alive)
                    {
                        obj.GetComponent<TargetController>().kill();
                        Destroy(gameObject);
                    }
                }
                else if (obj.gameObject.tag != "Player")
                {
                    Destroy(gameObject);
                }
            }
        }

        oldPosition = transform.position;
         
	}

    void OnTriggerEnter(Collider col)
    {
        bool kill = true;

        if (col.gameObject.tag == "target")
        {
            col.gameObject.GetComponent<TargetController>().kill();
        }

        if(col.gameObject.tag != "Player")
        {
            Ray ray = new Ray(transform.position, oldPosition - transform.position);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject obj = hitInfo.collider.gameObject;
                if (obj.CompareTag("target"))
                {
                    if (obj.GetComponent<TargetController>().alive)
                    {
                        obj.GetComponent<TargetController>().kill();
                        kill = true;
                    }
                    else
                    {
                        kill = false;
                    }
                    
                }
            }
            if(kill) Destroy(gameObject);
        }
    }
}
