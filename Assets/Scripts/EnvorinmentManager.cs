using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnvorinmentManager : MonoBehaviour
{
    public Controller controller;
    public SpaceManager spaceManager;
    public GoalManager goalManager;
    public Recorder recorder;
    public TextMeshProUGUI textMeshPro;
    private float timeSinceLastStep = 0f;

    /*
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (goalManager.func(goalManager.targetObject, goalManager.areaObject))
        {
            recorder.AddStep(controller.moveInput, controller.turnInput,
                spaceManager.agent.transform.position, spaceManager.agent.transform.rotation.eulerAngles,
                controller.carRigidbody.velocity, controller.carRigidbody.angularVelocity,
                spaceManager.redBall.transform.position, spaceManager.blueBall.transform.position,
                spaceManager.greenBall.transform.position, spaceManager.grayArea.transform.position,
                spaceManager.orangeArea.transform.position, spaceManager.whiteArea.transform.position);

            recorder.SaveReplay(GenerateName());
            Reset();
        }

        if (EndGame())
        {
            Reset();
        }
    }

    void FixedUpdate()
    {
        timeSinceLastStep += Time.fixedDeltaTime;

        if (timeSinceLastStep >= 0.04f) // 1 / 50 = 0.02
        {
            timeSinceLastStep -= 0.04f;


            recorder.AddStep(controller.moveInput, controller.turnInput,
                spaceManager.agent.transform.position, spaceManager.agent.transform.rotation.eulerAngles,
                controller.carRigidbody.velocity, controller.carRigidbody.angularVelocity,
                spaceManager.redBall.transform.position, spaceManager.blueBall.transform.position,
                spaceManager.greenBall.transform.position, spaceManager.grayArea.transform.position,
                spaceManager.orangeArea.transform.position, spaceManager.whiteArea.transform.position);
        }
    }

    private string GenerateName()
    {
        string date = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string replayName = "Replay_" + date;

        return replayName;
    }

    private bool EndGame()
    {
        bool terminated = false;

        if (OutSidePlane(spaceManager.agent))
        {
            return true;
        }

        if (OutSidePlane(spaceManager.redBall))
        {
            return true;
        }

        if (OutSidePlane(spaceManager.blueBall))
        {
            return true;
        }

        if (OutSidePlane(spaceManager.greenBall))
        {
            return true;
        }

        return terminated;
    }*/

    public bool OutSidePlane(GameObject gameObject)
    {
        if (Mathf.Abs(gameObject.transform.localPosition.x) > 10 || Mathf.Abs(gameObject.transform.localPosition.z) > 10)
        {
            return true;
        }

        return false;
    }

    public void Reset()
    {
        spaceManager.Reset();
        goalManager.GenerateTask();
        textMeshPro.SetText(goalManager.instruction);
        recorder.ResetReplay(goalManager.instruction);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }

}
