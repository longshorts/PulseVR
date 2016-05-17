using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	private float rot_x, rot_y;

	public void Start() {
		rot_x = transform.rotation.x;
		rot_y = transform.rotation.y;
	}
	
	void Update () {
		rot_x = clamp360(rot_x - Input.GetAxis("Mouse Y"));
		rot_y = clamp360(rot_y + Input.GetAxis("Mouse X"));
		transform.rotation = Quaternion.Euler(rot_x,rot_y,0f);
		
		transform.position += (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * Time.deltaTime * 2f;
	}
	
	private float clamp360(float x) {
		return x > 360f ? x - 360f : x < 0f ? x + 360f : x;
	}
}
