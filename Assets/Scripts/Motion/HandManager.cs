using UnityEngine;
using System.Collections;

public class HandManager : MonoBehaviour {

    HandTracker[] m_hands;
    GameObject rifle;
    GameObject rifleGhost;
    GameObject handTarget_Trigger;
    GameObject handTarget_Forgrip;

    public bool invert_180 = true;
    public bool AKRIFLE = true;

    public GameObject rightHandObject;
    public GameObject leftHandObject;

    HandTracker triggerHand;
    HandTracker forgripHand;

    Vector3 m_baseOffset;
    Vector3 triggerInitialPosition;
    Vector3 forgripInitialPosition;
    float m_sensitivity = 0.02f; // Sixense units are in mm
    bool m_bInitialized;


    // Use this for initialization
    void Start()
    {
        m_hands = GetComponentsInChildren<HandTracker>();
        rifle = GameObject.Find("MotionGun");
        rifleGhost = GameObject.Find("MotionGunGhost");
        handTarget_Forgrip = GameObject.Find("HandTarget_Forgrip");
        handTarget_Trigger = GameObject.Find("HandTarget_Trigger");
        triggerInitialPosition = GameObject.Find("HandTrackerRight").transform.localPosition;
        forgripInitialPosition = GameObject.Find("HandTrackerLeft").transform.localPosition;
    }


    // Update is called once per frame
    void Update()
    {
        bool bResetHandPosition = false;
        bool bGhostRifleEnabled = false;

        foreach (HandTracker hand in m_hands)
        {
            //Render ghost gun accordingly
            if (IsControllerActive(hand.m_controller) && hand.m_controller.GetButton(SixenseButtons.ONE))
            {
                bGhostRifleEnabled = true;
            }

            if (IsControllerActive(hand.m_controller) && hand.m_controller.GetButtonUp(SixenseButtons.ONE))
            {
                bResetHandPosition = true;
                triggerHand = hand;

                if (hand.m_controller.Hand.ToString().Equals("RIGHT"))
                {
                    rifle.transform.parent = rightHandObject.transform;
                }
                else
                {
                    rifle.transform.parent = leftHandObject.transform;
                }
                if (AKRIFLE) rifle.transform.localPosition = new Vector3(0.01000834f, -0.1583923f, 1.819998f);
                else rifle.transform.localPosition = new Vector3(-0.1059966f, 0.4159997f, 1.770007f);

                rifle.transform.rotation = rifle.transform.parent.rotation;
                rifle.transform.rotation *= Quaternion.Euler(0, 180f, 0);
                /*Vector3 rot = rifle.transform.parent.rotation.eulerAngles;
                rot = new Vector3(rot.x, rot.y + 180, rot.z);
                rifle.transform.rotation = Quaternion.Euler(rot);*/
            }

            //if (IsControllerActive(hand.m_controller) && hand.m_controller.Hand.ToString().Equals("RIGHT") && hand.m_controller.GetButtonDown(SixenseButtons.TRIGGER))
            if (IsControllerActive(hand.m_controller) && hand.Equals(triggerHand) && hand.m_controller.GetButtonDown(SixenseButtons.TRIGGER))
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
                if (!hand.Equals(triggerHand))
                    forgripHand = hand;

                hand.m_baseOffset = hand.m_controller.Position;
                hand.m_baseRotation = hand.m_controller.Rotation;

                /*if (hand.Equals(triggerHand))
                {
                    // Positon
                    hand.transform.position = handTarget_Trigger.transform.position;

                    //BROKEN
                    //hand.transform.position = handTarget_Trigger.transform.position;
                    //hand.InitialPosition = handTarget_Trigger.transform.position;

                    // Rotation
                    hand.transform.rotation = handTarget_Trigger.transform.rotation;

                    //BROKEN
                    //hand.transform.rotation = handTarget_Trigger.transform.rotation;
                    //hand.InitialRotation = handTarget_Trigger.transform.rotation;
                } else if(hand.Equals(forgripHand))
                {
                    // Positon
                    hand.transform.position = handTarget_Forgrip.transform.position;

                    //BROKEN
                    //hand.transform.position = handTarget_Forgrip.transform.position;
                    //hand.InitialPosition = handTarget_Forgrip.transform.position;

                    // Rotation
                    hand.transform.rotation = handTarget_Forgrip.transform.rotation;

                    //BROKEN
                    //hand.transform.rotation = handTarget_Forgrip.transform.rotation;
                    //hand.InitialRotation = handTarget_Forgrip.transform.rotation;
                }*/

                /*// Positon
                hand.m_baseOffset = hand.m_controller.Position;
                hand.transform.localPosition = hand.InitialPosition;
                //hand.transform.position = hand.resetTarget.transform.position;

                //hand.m_resetPosition = hand.resetTarget.transform.position;

                // Rotation
                hand.m_baseRotation = hand.m_controller.Rotation;
                hand.transform.localRotation = hand.InitialRotation;*/

            }

            //m_baseOffset /= 2;

            //m_baseOffset = Vector3.zero;
            
        }

        if (bGhostRifleEnabled)
        {
            rifleGhost.SetActive(true);
        }
        else
        {
            rifleGhost.SetActive(false);
        }
    }


    /** Updates hand position and rotation */
    void UpdateHand(HandTracker hand)
    {
        bool bControllerActive = IsControllerActive(hand.m_controller);

        if (bControllerActive)
        {
            if(hand.Equals(triggerHand))
                hand.transform.localPosition = (hand.m_controller.Position - hand.m_baseOffset) * m_sensitivity + triggerInitialPosition;
            else
                hand.transform.localPosition = (hand.m_controller.Position - hand.m_baseOffset) * m_sensitivity + forgripInitialPosition;
            //hand.transform.position = handTarget_Trigger.transform.position;
            //hand.transform.position = (hand.m_controller.Position - hand.m_baseOffset) * m_sensitivity + hand.InitialPosition;
            //hand.transform.localPosition = (hand.m_controller.Position - hand.m_baseOffset) * m_sensitivity + hand.InitialPosition;
            //hand.transform.position = (hand.m_controller.Position - hand.m_baseOffset) * m_sensitivity + hand.m_resetPosition;

            if (forgripHand.m_controller.GetButton(SixenseButtons.TRIGGER) && hand.Equals(triggerHand))
            {
                hand.transform.LookAt(forgripHand.transform);
            }
            else
            {
                hand.transform.localRotation = (hand.m_controller.Rotation * Quaternion.Inverse(hand.m_baseRotation));
            }
            //hand.transform.rotation = handTarget_Trigger.transform.rotation;

            
        }

        else
        {
            // use the inital position and orientation because the controller is not active
            hand.transform.position = hand.InitialPosition;
            hand.transform.rotation = hand.InitialRotation;
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
