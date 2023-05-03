using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    private Replay replay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetReplay(string instruction)
    {
        replay = new Replay(instruction);
    }

    public void AddStep(
        float moveInputAction,
        float turnInputAction,
        Vector3 agentPosition,
        Vector3 agentRotation,
        Vector3 agentVelocity,
        Vector3 agentAngularVelocity,
        Vector3 redBallPosition,
        Vector3 blueBallPosition,
        Vector3 greenBallPosition,
        Vector3 grayAreaPosition,
        Vector3 orangeAreaPosition,
        Vector3 whiteAreaPosition)
    {
        replay.AddStep(moveInputAction, turnInputAction,
            agentPosition, agentRotation, agentVelocity,
            agentAngularVelocity, redBallPosition, blueBallPosition,
            greenBallPosition, grayAreaPosition, orangeAreaPosition,
            whiteAreaPosition);
    }

    public void SaveReplay(string fileName)
    {
        string json = JsonUtility.ToJson(replay);
        string path = Application.dataPath + "/data/";
        string file = path + fileName + ".json";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        System.IO.File.WriteAllText(file, json);
    }
}
