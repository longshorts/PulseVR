using UnityEngine;
using System.Collections;

public class Sight : MonoBehaviour {
	public bool holo = false, angled = false;
	public Texture2D [] reticle = new Texture2D[7];
	public Color [] colours = new Color[7];
	
	private Material glassMat;
	public float scale = 10f, brightness = 1.0f, trans = 0.5f, distance = 9.9f, angle = 60f;
	private float rot_x = 0f, rot_y = 0f, rot_z = 0f;
	private int retIndex = 6, retColIndex = 1, glassColIndex = 6;
	
	public void Start() {
		glassMat = transform.GetChild(0).GetComponent<MeshRenderer>().material;
	}
	
	public void OnGUI() {
		GUI.Label(new Rect(25,0,200,25),"Reticle Scale");
		scale = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), scale, 0.0f, 100.0f);
		
		GUI.Label(new Rect(25,50,200,25),"Reticle Brightness");
		brightness = GUI.HorizontalSlider(new Rect(25, 75, 100, 30), brightness, 0f, 1f);
		
		GUI.Label(new Rect(25,100,200,25),"Glass Transparency");
		trans = GUI.HorizontalSlider(new Rect(25, 125, 100, 30), trans, 0f, 1f);
		
		if (GUI.Button(new Rect(25,150,200,25), "Cycle Reticle")) {
			retIndex = (retIndex + 1)%7;
		}
		
		if (GUI.Button(new Rect(25,175,200,25), "Cycle Reticle Colour")) {
			retColIndex = (retColIndex + 1)%7;
		}
		
		if (GUI.Button(new Rect(25,200,200,25), "Cycle Glass Colour")) {
			glassColIndex = (glassColIndex + 1)%7;
		}
		
		GUI.Label(new Rect(25,225,200,25),"X Rotation");
		rot_x = GUI.HorizontalSlider(new Rect(25, 250, 100, 30), rot_x, 0f, 360.0f);
		
		GUI.Label(new Rect(25,275,200,25),"Y Rotation");
		rot_y = GUI.HorizontalSlider(new Rect(25, 300, 100, 30), rot_y, 0f, 360.0f);
		
		GUI.Label(new Rect(25,325,200,25),"Z Rotation");
		rot_z = GUI.HorizontalSlider(new Rect(25, 350, 100, 30), rot_z, 0f, 360.0f);
		
		if (holo) {
			GUI.Label(new Rect(25,375,200,25),"Reticle Distance");
			distance = GUI.HorizontalSlider(new Rect(25, 400, 100, 30), distance, 0f, 100.0f);
		} else if (angled) {
			GUI.Label (new Rect(25,375,200,25),"Sight Angle");
			angle = GUI.HorizontalSlider(new Rect(25, 400, 100, 30), angle, 0f, 90.0f);
		}
	}
	
	public void Update() {
		glassMat.SetFloat("_uvScale",scale);
		glassMat.SetFloat("_reticleBright",brightness);
		glassMat.SetFloat("_glassTrans",trans);
		if (holo) {
			glassMat.SetFloat("_distance",distance);
		}
		if (angled) {
			glassMat.SetFloat("_angle",angle);
			transform.rotation = Quaternion.Euler(rot_x,rot_y,rot_z) * Quaternion.Euler(new Vector3(270 + angle,0,0));
		} else {
			transform.rotation = Quaternion.Euler(rot_x,rot_y,rot_z);
		}
		glassMat.SetTexture("_reticleTex",reticle[retIndex]);
		glassMat.SetColor("_glassColour",colours[glassColIndex]);
		glassMat.SetColor("_reticleColour",colours[retColIndex]);
	}
}
