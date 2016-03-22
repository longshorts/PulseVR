using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour {

    public Rigidbody laser;
    public GameObject bulletSpawn;

    [SerializeField] private float speed = 10;

    private Camera camera;
    

	// Use this for initialization
	void Start () {
        camera = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray1 = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
		RaycastHit hitInfo;

        Ray ray2 = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward * 40);

		if(Input.GetButtonDown("Fire1")){

            fireLaser();

            //Simple raycast code
			/*if(Physics.Raycast (ray, out hitInfo)){
				GameObject obj = hitInfo.collider.gameObject;
				if(obj.CompareTag("target")){
					obj.GetComponent<TargetController>().kill ();
				}
			}*/
		}

		Debug.DrawRay (Camera.main.transform.position, ray1.direction * 40, Color.yellow);
        Debug.DrawRay(bulletSpawn.transform.position, ray2.direction* 40, Color.cyan);
	}

    public void fireLaser()
    {
        Rigidbody instantiatedLaser = Instantiate(laser, bulletSpawn.transform.position, bulletSpawn.transform.rotation) as Rigidbody;
        if (GetComponent<Collider>())
            Physics.IgnoreCollision(instantiatedLaser.GetComponent<Collider>(), GetComponent<Collider>());
        //instantiatedLaser.velocity = transform.TransformDirection(ray.direction * speed);
        instantiatedLaser.AddForce(bulletSpawn.transform.forward * speed);
    }
}
