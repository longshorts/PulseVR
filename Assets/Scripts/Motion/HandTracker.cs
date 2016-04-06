using UnityEngine;
using System.Collections;

public class HandTracker : MonoBehaviour {

    public SixenseHands m_hand;
    public SixenseInput.Controller m_controller = null;
    public Vector3 m_baseOffset;
    public Quaternion m_baseRotation;
    public GameObject resetTarget;

    public Vector3 m_resetPosition;
    float m_fLastTriggerVal;
    Vector3 m_initialPosition;
    Quaternion m_initialRotation;


    protected void Start()
    {
        m_initialRotation = transform.localRotation;
        m_initialPosition = transform.localPosition;
        m_baseOffset = new Vector3(0, 0, 0);
    }


    protected void Update()
    {
        if (m_controller == null)
        {
            m_controller = SixenseInput.GetController(m_hand);
        }
    }


    public Quaternion InitialRotation
    {
        get { return m_initialRotation; }
        set { m_initialRotation = value; }
    }

    public Vector3 InitialPosition
    {
        get { return m_initialPosition; }
        set { m_initialPosition = value; }
    }
}
