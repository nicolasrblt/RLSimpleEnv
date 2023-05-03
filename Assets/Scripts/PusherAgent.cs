using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class PusherAgent : Agent
{
    Rigidbody rBody;
    EnvorinmentManager environmentManager;
    float baseAgentBallDist;
    float baseBallTargetDist;
    float minAgentBallDist;
    float minBallTargetDist;
    int step;

    public GameObject target;
    public GameObject ball;
    public GameObject manager;
    public float speed = 10.0f; // forward movement speed
    public float turnSpeed = 50.0f; // turning speed
    public int maxStepEpisode = 5000;

    void Start () {
        rBody = GetComponent<Rigidbody>();
        environmentManager = manager.GetComponent<EnvorinmentManager>();
        environmentManager.goalManager.func = environmentManager.goalManager.PushInFunction;
        environmentManager.goalManager.areaObject = target;
        environmentManager.goalManager.targetObject = ball;
    }

    public override void OnEpisodeBegin()
    {
       // If the Agent fell, zero its momentum
        environmentManager.spaceManager.Reset(this.environmentManager.OutSidePlane(this.gameObject));//transform.localPosition.y < 0);
        step = 0;
        baseAgentBallDist = Vector3.Distance(this.transform.localPosition, this.ball.transform.localPosition);
        baseBallTargetDist = Vector3.Distance(target.transform.localPosition, this.ball.transform.localPosition);
        minAgentBallDist = baseAgentBallDist;
        minBallTargetDist = baseBallTargetDist;
    }

    public override void CollectObservations(VectorSensor sensor)
    {

        // Target ball and agent positions
//        sensor.AddObservation(target.transform.localPosition);
//        sensor.AddObservation(ball.transform.localPosition);
        sensor.AddObservation(this.transform.InverseTransformPoint(target.transform.position)); // target pos / agent
        sensor.AddObservation(this.transform.InverseTransformPoint(ball.transform.position));   // ball pos / agent
        sensor.AddObservation(this.transform.localPosition);                                    // agent pos / training area

        // Agent (and ball) velocity
        //sensor.AddObservation(rBody.velocity.x);
        //sensor.AddObservation(rBody.velocity.z);
        sensor.AddObservation(ball.GetComponent<Rigidbody>().velocity.x);
        sensor.AddObservation(ball.GetComponent<Rigidbody>().velocity.z);

        // Agent rotation
        sensor.AddObservation(this.transform.localRotation.y);

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

        // Actions, size = 2
        // move the car forward or backward
        Vector3 movement = transform.forward * actionBuffers.ContinuousActions[0] * speed;

        rBody.AddForce(movement/*, ForceMode.VelocityChange*/);

        // turn the car left or right
        Quaternion turn = Quaternion.Euler(0, actionBuffers.ContinuousActions[1] * turnSpeed * Time.fixedDeltaTime, 0);
        rBody.MoveRotation(rBody.rotation * turn);


        step++;

        // Rewards
        float distanceToTarget = Vector3.Distance(ball.transform.localPosition, target.transform.localPosition);

        minAgentBallDist = Mathf.Min(minAgentBallDist, Vector3.Distance(this.transform.localPosition, this.ball.transform.localPosition));
        minBallTargetDist = Mathf.Min(minBallTargetDist, Vector3.Distance(target.transform.localPosition, this.ball.transform.localPosition));

        

        if (environmentManager.goalManager.IsSuccess())
        {
            EndEpisode(10);
        }
        else if (this.environmentManager.OutSidePlane(this.gameObject) || this.environmentManager.OutSidePlane(this.ball))
        {
            EndEpisode(-3);
        }
        else if (step >= maxStepEpisode) {
            EndEpisode(0);
        }
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
    }

    public void EndEpisode(float reward) {
            AddReward(reward);                                      // reward depending on the outcome
            AddReward(-0.1f*(step/maxStepEpisode));                 // reward finishing quickly <=> -0.1/maxStepEpisode each step
            AddReward(5*(1-minAgentBallDist/baseAgentBallDist));    // reward getting to the ball
            AddReward(5*(1-minBallTargetDist/baseBallTargetDist));  // reward getting the ball to the target
            EndEpisode();
    }
}
