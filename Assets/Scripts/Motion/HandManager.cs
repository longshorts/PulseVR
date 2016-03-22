using UnityEngine;
using System.Collections;

public class HandManager : MonoBehaviour {

    HandTracker[] m_hands;
    GameObject rifle;

    Vector3 m_baseOffset;
    float m_sensitivity = 0.05f; // Sixense units are in mm
    bool m_bInitialized;


    // Use this for initialization
    void Start()
    {
        m_hands = GetComponentsInChildren<HandTracker>();
        rifle = GameObject.Find("handRifle");
    }


    // Update is called once per frame
    void Update()
    {
        bool bResetHandPosition = false;

        foreach (HandTracker hand in m_hands)
        {
            if (IsControllerActive(hand.m_controller) && hand.m_controller.GetButtonDown(SixenseButtons.START))
            {
                bResetHandPosition = true;
            }

            if (IsControllerActive(hand.m_controller) && hand.m_controller.Hand.ToString().Equals("RIGHT") && hand.m_controller.GetButtonDown(SixenseButtons.TRIGGER))
            {
                if(rifle.GetComponent<ShootController>())
                    rifle.GetComponent<ShootController>().fireLaser();
            }

            if (m_bInitialized)
            {
                UpdateHand(hand);
            }
        }

        if (bResetHandPosition)
        {
            m_bInitialized = true;

            m_baseOffset = Vector3.zero;

            // Get the base offset assuming forward facing down the z axis of the base
            foreach (HandTracker hand in m_hands)
            {
                // Positon
                hand.m_baseOffset = hand.m_controller.Position;
                hand.transform.localPosition = hand.InitialPosition;
                //hand.transform.position = hand.resetTarget.transform.position;

                //hand.m_resetPosition = hand.resetTarget.transform.position;
                
                // Rotation
                hand.m_baseRotation = hand.m_controller.Rotation;
                hand.transform.localRotation = hand.InitialRotation;

            }

            m_baseOffset /= 2;

            //m_baseOffset = Vector3.zero;
        }
    }


    /** Updates hand position and rotation */
    void UpdateHand(HandTracker hand)
    {
        bool bControllerActive = IsControllerActive(hand.m_controller);

        if (bControllerActive)
        {
            hand.transform.localPosition = (hand.m_controller.Position - hand.m_baseOffset) * m_sensitivity + hand.InitialPosition;
            //hand.transform.position = (hand.m_controller.Position - hand.m_baseOffset) * m_sensitivity + hand.m_resetPosition;


            hand.transform.localRotation = (hand.m_controller.Rotation * Quaternion.Inverse(hand.m_baseRotation));
        }

        else
        {
            // use the inital position and orientation because the controller is not active
            hand.transform.localPosition = hand.InitialPosition;
            hand.transform.localRotation = hand.InitialRotation;
        }
    }


    void OnGUI()
    {
        if (!m_bInitialized)
        {
            GUI.Box(new Rect(Screen.width / 2 - 50, Screen.height - 40, 100, 30), "Press Start");
        }
    }


    /** returns true if a controller is enabled and not docked */
    bool IsControllerActive(SixenseInput.Controller controller)
    {
        return (controller != null && controller.Enabled && !controller.Docked);
    }
}
