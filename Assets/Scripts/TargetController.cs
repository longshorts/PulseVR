using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

	public Material alive;
	public Material dead;

	bool isAlive;

	// Use this for initialization
	void Start () {
		isAlive = true;
		GetComponent<Renderer> ().material = alive;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Will return false if already dead
	public bool kill(){
		if (isAlive) {
			GetComponent<Renderer> ().material = dead;
			isAlive = false;
			return true;
		}
		return false;
	}
}
