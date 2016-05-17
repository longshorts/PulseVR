using UnityEngine;
using UnityEngine.VR;
using System.Collections;
using System.Collections.Generic;

public class ShootController : MonoBehaviour {

    public Rigidbody laser;
    public GameObject bulletSpawn;

    [SerializeField] private float speed = 10;

    private Camera camera;

    private Vector3 leftEye;
    private Vector3 rightEye;

    private TargetManager tm;
    /*private Material m_material;
    private List<Vector3> m_cameraPositions;
    private int m_currentEye;*/

    /*public void OnEnable()
    {
        const string shaderName = "Transparent/ReflexVR";

        if (null == m_material)
            m_material = GameObject.Find("scope_lense").GetComponent<Renderer>().material;

        if (null == m_material)
            Debug.LogError("Can't locate required shader \"" + shaderName + "\"\n");

        if (null == m_cameraPositions || m_cameraPositions.Count != 2)
        {
            m_cameraPositions = new List<Vector3>();
            m_cameraPositions.Add(Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.LeftEye)) * InputTracking.GetLocalPosition(VRNode.LeftEye));
            m_cameraPositions.Add(Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.RightEye)) * InputTracking.GetLocalPosition(VRNode.RightEye));
        }
    }*/

	// Use this for initialization
	void Start () {
        camera = GetComponentInChildren<Camera>();
        tm = GameObject.Find("TargetManager").GetComponent<TargetManager>(); ;

        //leftEye = Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.LeftEye)) * InputTracking.GetLocalPosition(VRNode.LeftEye);
        //rightEye = Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.RightEye)) * InputTracking.GetLocalPosition(VRNode.RightEye);
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

		//Debug.DrawRay (Camera.main.transform.position, ray1.direction * 40, Color.yellow);
        //Debug.DrawRay(bulletSpawn.transform.position, ray2.direction* 40, Color.cyan);
	}

    /*void FixedUpdate()
    {
        Ray ray1 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;

        Ray ray2 = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward * 40);

        Debug.DrawRay(Camera.main.transform.position, ray1.direction * 40, Color.yellow);
        Debug.DrawRay(bulletSpawn.transform.position, ray2.direction * 40, Color.cyan);
    }*/

    public void fireLaser()
    {
        Rigidbody instantiatedLaser = Instantiate(laser, bulletSpawn.transform.position, bulletSpawn.transform.rotation) as Rigidbody;
        if (GetComponent<Collider>())
            Physics.IgnoreCollision(instantiatedLaser.GetComponent<Collider>(), GetComponent<Collider>());
        //instantiatedLaser.velocity = transform.TransformDirection(ray.direction * speed);
        instantiatedLaser.AddForce(bulletSpawn.transform.forward * speed);

        tm.addShotToRound();
    }

    /*/// <summary>
    ///  called twice-per frame (once per eye)
    /// </summary>
    void OnPreRender()
    {
        if (!VRDevice.isPresent) return;

        Shader.SetGlobalVector("TestVector", m_cameraPositions[m_currentEye]);
        Shader.SetGlobalInt("RenderingEye", m_currentEye);

        m_currentEye = 1 - m_currentEye;
        Debug.Log(
            "OnRenderImage(\"" + name + "\")"
            + ((m_currentEye == 0) ? " LEFT \n" : " RIGHT \n")
            )
            ;
    }

    /// <summary>
    ///  called twice-per frame (once per eye)
    /// </summary>
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!VRDevice.isPresent) return;
        Debug.Log(
            "OnRenderImage(\"" + name + "\")"
            + ((m_currentEye == 0) ? " LEFT \n" : " RIGHT \n")
            )
            ;


        Graphics.Blit(source, destination, m_material);
    }*/
}
