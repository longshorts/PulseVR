using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour {

    public GameObject[] startTarget;
    public GameObject[] wave1;
    public GameObject[] wave2;
    public GameObject[] wave3;

    private int roundWait = 3;

    enum Stage { Start, Wave1, Wave2, Wave3, Done };
    Stage myStage;

	// Use this for initialization
	void Start () {
        startTarget[0].SetActive(true);
        
        //Setup waves
        for (int i = 0; i < wave1.Length; i++)
        {
            wave1[i].SetActive(false);
        }
        for (int i = 0; i < wave2.Length; i++)
        {
            wave2[i].SetActive(false);
        }
        for (int i = 0; i < wave3.Length; i++)
        {
            wave3[i].SetActive(false);
        }

        myStage = Stage.Start;
	}
	
	// Update is called once per frame
	void Update () {
        switch (myStage)
        {
            case Stage.Start:
                WaveStage(startTarget);
                break;
            case Stage.Wave1:
                WaveStage(wave1);
                break;
            case Stage.Wave2:
                WaveStage(wave2);
                break;
            case Stage.Wave3:
                WaveStage(wave3);
                break;
            default:
                break;
        }
	}

    void WaveStage(GameObject[] wave) 
    {
        for (int i = 0; i < wave.Length; i++)
        {
            if (!wave[i].GetComponent<TargetController>().isHit())
                return; //Return as all targets have not yet been hit
        }

        //If all targets have been hit proceed
        switch (myStage)
        {
            case Stage.Start:
                StartCoroutine(RoundTransition(startTarget, wave1));
                myStage = Stage.Wave1;
                break;
            case Stage.Wave1:
                StartCoroutine(RoundTransition(wave1, wave2));
                myStage = Stage.Wave2;
                break;
            case Stage.Wave2:
                StartCoroutine(RoundTransition(wave2, wave3));
                myStage = Stage.Wave3;
                break;
            case Stage.Wave3:
                print("Rounds finished!");
                myStage = Stage.Done;
                break;
            default:
                break;
        }
    }

    IEnumerator RoundTransition(GameObject[] oldWave, GameObject[] newWave)
    {
        yield return new WaitForSeconds(roundWait);
        for (int i = 0; i < newWave.Length; i++)
        {
            newWave[i].SetActive(true);
        }
        for (int i = 0; i < oldWave.Length; i++)
        {
            oldWave[i].SetActive(false);
        }
    }
}
