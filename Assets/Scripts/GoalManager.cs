using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public GameObject agent;
    public GameObject redBall;
    public GameObject blueBall;
    public GameObject greenBall;

    public GameObject grayArea;
    public GameObject orangeArea;
    public GameObject whiteArea;

    public Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> areas = new Dictionary<string, GameObject>();
    public Dictionary<string, Func<GameObject, GameObject, bool>> functions = new Dictionary<string, Func<GameObject, GameObject, bool>>();

    public GameObject targetObject;
    public GameObject areaObject;
    public Func<GameObject, GameObject, bool> func;
    public string instruction;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("GoalManager Start");
        objects.Add("agent", agent);
        objects.Add("red ball", redBall);
        objects.Add("blue ball", blueBall);
        objects.Add("green ball", greenBall);

        areas.Add("gray area", grayArea);
        areas.Add("orange area", orangeArea);
        areas.Add("white area", whiteArea);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsSuccess() {
        return func(this.targetObject, this.areaObject);
    }

    public void GenerateTask()
    {
        List<string> objectKeys = new List<string>(objects.Keys);
        List<string> areaKeys = new List<string>(areas.Keys);

        string randomObjectKey = objectKeys[UnityEngine.Random.Range(0, objectKeys.Count)];
        string randomAreaKey = areaKeys[UnityEngine.Random.Range(0, areaKeys.Count)];
        targetObject = objects[randomObjectKey];
        areaObject = areas[randomAreaKey];

        if (randomObjectKey == "agent")
        {
            func = StopInFunction;
            instruction = "Stop the agent in " + randomAreaKey + " area";
        }
        else
        {
            int eps = UnityEngine.Random.Range(0, 2);
            if(eps == 0)
            {
                func = PushInFunction;
                instruction = "Push the " + randomObjectKey + " in " + randomAreaKey + " area";
            }
            else
            {
                func = PushOutFunction;
                targetObject.transform.position = areaObject.transform.position;
                instruction = "Push the " + randomObjectKey + " out " + randomAreaKey + " area";
            }
        }
    }

    public bool StopInFunction(GameObject gameObject, GameObject area)
    {
        bool terminate = false;

        float distance = Vector3.Distance(gameObject.transform.position, area.transform.position);
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        Vector3 speed = rigidbody.velocity;

        if(distance <= 0.5 && speed == Vector3.zero)
        {
            terminate = true;
        }

        return terminate;
    }

    public bool PushInFunction(GameObject gameObject, GameObject area)
    {
        bool terminate = false;

        float distance = Vector3.Distance(gameObject.transform.position, area.transform.position);
        
        if (distance <= 0.5)
        {
            terminate = true;
        }

        return terminate;
    }

    public bool PushOutFunction(GameObject gameObject, GameObject area)
    {
        bool terminate = false;

        float distance = Vector3.Distance(gameObject.transform.position, area.transform.position);

        if (distance > 1f)
        {
            terminate = true;
        }

        return terminate;
    }
}
