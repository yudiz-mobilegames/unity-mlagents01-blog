using System;
using UnityEngine.UI;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class CarAgent : Agent
{
    #region
    private Rigidbody rb;
    public float speed = 20f;
    public GameObject m_area;
    public Text Score_text; 
    int score = 0;
   

    public event Action onReset;
    #endregion
    public override void Initialize()
    {
        //This method is used to perform one-time initialization or set up of the Agent instance
        rb = GetComponent<Rigidbody>();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //This Method is used to collect Vector Observations of the agnet for the step
        sensor.AddObservation(transform.position.x);
    }
    public override void Heuristic(float[] actionsOut)
    {
        //To choose an action for this agent using a custom hesuristic
        actionsOut[0] = Input.GetAxis("Horizontal");
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        //to specify agent behavior at every step, based on the provided action.
        MoveAction(vectorAction);
    }

    public override void OnEpisodeBegin()
    {
        //to set up an Agent instance at the beginning of an episode.
        transform.position = new Vector3(-1f, 0.1f, 0f) ;
        rb.velocity = Vector3.zero;
       
        Reset();
    }

    private void Reset()
    {
        onReset?.Invoke();
    }

    private void OnCollisionEnter(Collision obj)
    {
        if(obj.gameObject.CompareTag("vehicle"))
        {
            score = 0;
            Score_text.text = score.ToString(); 
            AddReward(-1f);
            EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("goal"))
        {
            AddReward(1f);
            score += 1;
            Score_text.text = score.ToString();
        }
    }

    void MoveAction(float[] act)
    {
        Vector3 controlSignal = Vector3.zero;
        var side = Mathf.Clamp(act[0], -1f, 1f);
     
        if (side > 0f && transform.localPosition.x < -0.5f)
        {
            controlSignal.x = 1f;
        }
        if (side < 0 && transform.localPosition.x > 0.5f)
        {
            controlSignal.x = -1f;
        }
        rb.AddForce(controlSignal * speed);

        if (transform.position.x - m_area.transform.position.x > 1.3f ||
           transform.position.x - m_area.transform.position.x < -1.3f)
        {
            rb.velocity = Vector3.zero;
        }
    }
 
}
