using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

	public Material alive;
	public Material dead;

	bool hit;
    
    public bool linearMovement;
    public Vector3 movStart;
    public Vector3 movEnd;
    public float movTime;

	// Use this for initialization
	void Start () {
		hit = false;
		GetComponent<Renderer> ().material = alive;
	}
	
	// Update is called once per frame
	void Update () {
        if (linearMovement)
        {
            moveLinear();
        }
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

    public bool isHit()
    {
        return hit;
    }
}
