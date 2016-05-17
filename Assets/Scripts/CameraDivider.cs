using UnityEngine;
using UnityEngine.VR;
using System.Collections;
using System.Collections.Generic;

public class CameraDivider : MonoBehaviour {

    private TargetManager tm;
    private Material m_material;
    private List<Vector3> m_cameraPositions;
    private int m_currentEye;

    public void OnEnable()
    {
        const string shaderName = "Transparent/ReflexVR";
        Debug.Log("Start");

        if (null == m_material)
            m_material = GameObject.Find("scope_lense").GetComponent<Renderer>().material;

        if (null == m_material)
            Debug.LogError("Can't locate required shader \"" + shaderName + "\"\n");

        if (null == m_cameraPositions || m_cameraPositions.Count != 2)
        {
            m_cameraPositions = new List<Vector3>();
            m_cameraPositions.Add(transform.parent.TransformPoint(InputTracking.GetLocalPosition(VRNode.LeftEye)));
            m_cameraPositions.Add(transform.parent.TransformPoint(InputTracking.GetLocalPosition(VRNode.LeftEye)));
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        m_cameraPositions[0] = transform.parent.TransformPoint(InputTracking.GetLocalPosition(VRNode.LeftEye));
        m_cameraPositions[1] = transform.parent.TransformPoint(InputTracking.GetLocalPosition(VRNode.RightEye));

        Ray ray1 = new Ray(m_cameraPositions[0], transform.forward * 20);
        Ray ray2 = new Ray(m_cameraPositions[1], transform.forward * 20);

        Debug.DrawRay(m_cameraPositions[0], ray1.direction * 20, Color.red);
        Debug.DrawRay(m_cameraPositions[1], ray2.direction * 20, Color.red);
    }

    /// <summary>
    ///  called twice-per frame (once per eye)
    /// </summary>
    void OnPreRender()
    {
        if (!VRDevice.isPresent) return;

        Shader.SetGlobalVector("TestVector", m_cameraPositions[m_currentEye]);
        Shader.SetGlobalInt("RenderingEye", m_currentEye);

        m_currentEye = 1 - m_currentEye;
        /*Debug.Log(
            "OnRenderImage(\"" + name + "\")"
            + ((m_currentEye == 0) ? " LEFT \n" : " RIGHT \n")
            )
            ;*/
    }
}
