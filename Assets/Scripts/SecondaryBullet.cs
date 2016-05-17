using UnityEngine;
using System.Collections;

public class SecondaryBullet : MonoBehaviour {

    Transform bulletSpawn;
    Transform gun;
    public bool AK = true;

    Vector3 temp;

	// Use this for initialization
	void Start () {
        //bulletSpawn = GameObject.Find("bulletSpawn").transform;
        if (AK) gun = GameObject.Find("AK-74u").transform;
	}
	
	// Update is called once per frame
	void Update () {
        temp = gun.position;

        transform.position = temp;// + new Vector3(0.043f, 0.197f, -1.917f);

        transform.rotation = gun.rotation;
        if (AK) { transform.rotation *= Quaternion.Euler(0, 180f, 0); }
	}
}
