using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

	public Material alive;
	public Material dead;

	bool hit;
    bool triggerNextSequence;
    
    [Header("Linear Movement")]
    public bool linearMovement;
    public Vector3 movStart;
    public Vector3 movEnd;
    public float movTime;

    [Header("Clay Pidgeon")]
    public bool clayPidgeon;
    public Vector3 direction;
    public float force;

    private Rigidbody rb;
    private Vector3 initPosition;

	// Use this for initialization
	void Start () {
		hit = false;
        triggerNextSequence = false;
		GetComponent<Renderer> ().material = alive;
        rb = GetComponent<Rigidbody>();
        initPosition = transform.position;

        if (clayPidgeon) doClayPidgeon();
	}
	
	// Update is called once per frame
	void Update () {
        if (linearMovement)
        {
            moveLinear();
        }
	}

    void FixedUpdate()
    {

    }

	//Will return false if already dead
	public bool kill(){
		if (!hit) {
			GetComponent<Renderer> ().material = dead;
			hit = true;
			return true;
		}
		return false;
	}

    private void moveLinear()
    {
        transform.localPosition = Vector3.Lerp(movStart, movEnd,
            Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / movTime, 1f)));
    }

    private void doClayPidgeon()
    {
        if (!rb)
        {
            Debug.LogError("Rigidbody needs to be set for target");
            return;
        }

        rb.AddForce(direction * force);
    }

    void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;

        if (!hit && clayPidgeon)
        {
            transform.position = initPosition;
            rb.AddForce(direction * force);
        }

        if (hit && collision.transform.tag != "laser")
            triggerNextSequence = true;
        
    }

    public bool isHit()
    {
        return hit;
    }

    public bool isTriggerNextSequence()
    {
        return triggerNextSequence;
    }
}
