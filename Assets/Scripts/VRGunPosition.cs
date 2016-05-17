using UnityEngine;
using UnityEngine.VR;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class VRGunPosition : MonoBehaviour {

    public Transform gun;
    public float XSensitivity = 0.05f;
    public float YSensitivity = 0.05f;

    public Transform bulletSpawn;

	// Use this for initialization
	void Start () {
        bulletSpawn = GameObject.Find("bulletSpawn").transform;
	}
	
	// Update is called once per frame
	void Update () {
        float xRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
        float yRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

        gun.localPosition += new Vector3(xRot, yRot, 0);

        //bulletSpawn.position = gun.position;
	}
}
