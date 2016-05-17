using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class VREyeTracker : MonoBehaviour {

    private Vector3 leftEye;
    private Vector3 rightEye;

    public void OnEnable()
    {
        /*const string shaderName = "Transparent/ReflexVR";

        if (null == m_material)
            m_material = new Material(Shader.Find(shaderName));

        if (null == m_material)
            Debug.LogError("Can't locate required shader \"" + shaderName + "\"\n");

        if (null == m_testColors || m_testColors.Count != 2)
        {
            m_testColors = new List<Color>();
            m_testColors.Add(new Color(1, 0, 0, 1));
            m_testColors.Add(new Color(0, 0, 1, 1));
        }*/
    }

	// Use this for initialization
	void Start () {
        leftEye = Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.LeftEye)) * InputTracking.GetLocalPosition(VRNode.LeftEye);
        rightEye = Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.RightEye)) * InputTracking.GetLocalPosition(VRNode.RightEye);
	}
	
	// Update is called once per frame
	void Update () {
        leftEye = Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.LeftEye)) * InputTracking.GetLocalPosition(VRNode.LeftEye);
        rightEye = Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.RightEye)) * InputTracking.GetLocalPosition(VRNode.RightEye);

	}
    /*
    /// <summary>
    ///  called twice-per frame (once per eye)
    /// </summary>
    void OnPreRender()
    {
        Shader.SetGlobalColor("TestColor", m_testColors[m_currentEye]);
        Shader.SetGlobalInt("RenderingEye", m_currentEye);

        m_currentEye = 1 - m_currentEye;
    }

    /// <summary>
    ///  called twice-per frame (once per eye)
    /// </summary>
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log(
            "OnRenderImage(\"" + name + "\")"
            + ((m_currentEye == 0) ? " LEFT \n" : " RIGHT \n")
            )
            ;


        Graphics.Blit(source, destination, m_material);
    }*/
}
