using UnityEngine;
using System.IO;
using System.Collections;

public class TargetManager : MonoBehaviour {

    public GameObject[] startTarget;
    public GameObject[] wave1;
    public GameObject[] wave2;
    public GameObject[] wave3;

    private int roundWait = 3;
    private int sequenceIndex = 0;
    private bool changingRounds = false;

    public int shotsFired = 0;
    public float wave1Time = 0f;
    public int wave1Shots = 0;
    public float wave2Time = 0f;
    public int wave2Shots = 0;
    public float wave3Time = 0f;
    public int wave3Shots = 0;

    public string testType;
    public string fileName = "LOG";

    enum Stage { Start, Wave1, Wave2, Wave3, Done };
    Stage myStage;

	// Use this for initialization
	void Start () {
        //Hide mouse
        Cursor.visible = false;

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

        if (Input.GetKey("escape"))
            Application.Quit();

        if(!changingRounds)
        switch (myStage)
        {
            case Stage.Start:
                WaveStage(startTarget);
                break;
            case Stage.Wave1:
                WaveStage(wave1);
                wave1Time += Time.deltaTime;
                break;
            case Stage.Wave2:
                WaveStage(wave2);
                wave2Time += Time.deltaTime;
                break;
            case Stage.Wave3:
                SequenceStage(wave3);
                wave3Time += Time.deltaTime;
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
        ChangeRound();
    }

    void SequenceStage(GameObject[] wave)
    {
        wave[sequenceIndex].SetActive(true);
        for (int i = 0; i < wave.Length; i++)
        {
            if(i != sequenceIndex) wave[i].SetActive(false);
        }

        if (wave[sequenceIndex].GetComponent<TargetController>().isTriggerNextSequence())
        {
            sequenceIndex++;
            //If all targets have been hit
            if (sequenceIndex >= wave.Length)
            {
                sequenceIndex = 0;
                ChangeRound();
            }
        }


        
    }

    void ChangeRound()
    {
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
                doDone();
                break;
            default:
                break;
        }

        sequenceIndex = 0;
    }

    IEnumerator RoundTransition(GameObject[] oldWave, GameObject[] newWave)
    {
        changingRounds = true;
        yield return new WaitForSeconds(roundWait);
        for (int i = 0; i < newWave.Length; i++)
        {
            newWave[i].SetActive(true);
        }
        for (int i = 0; i < oldWave.Length; i++)
        {
            oldWave[i].SetActive(false);
        }
        changingRounds = false;
    }

    public void addShotToRound()
    {
        shotsFired++;

        switch (myStage)
        {
            case Stage.Start:

                break;
            case Stage.Wave1:
                wave1Shots++;
                break;
            case Stage.Wave2:
                wave2Shots++;
                break;
            case Stage.Wave3:
                wave3Shots++;
                break;
            default:
                break;
        }
    }

    void doDone()
    {
        int fileID = 0;
        string fullFileName = fileName + "-" + testType + "-";

        while (File.Exists(fullFileName + fileID + ".txt"))
        {
            fileID++;
        }

        StreamWriter sr = File.CreateText(fullFileName + fileID + ".txt");
        sr.WriteLine(testType + "-" + fileID);
        sr.WriteLine("Shots Fired: {0}", shotsFired);
        sr.WriteLine("Wave 1 Time: {0}", wave1Time);
        sr.WriteLine("Wave 1 Shots: {0}", wave1Shots);
        sr.WriteLine("Wave 2 Time: {0}", wave2Time);
        sr.WriteLine("Wave 2 Shots: {0}", wave2Shots);
        sr.WriteLine("Wave 3 Time: {0}", wave3Time);
        sr.WriteLine("Wave 3 Shots: {0}", wave3Shots);
        sr.Close();
    }
}
