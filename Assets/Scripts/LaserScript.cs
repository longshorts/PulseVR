using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "target")
        {
            col.gameObject.GetComponent<TargetController>().kill();
        }

        if(col.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
