using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class DodgeGoal : Agent
{
    [SerializeField] private Transform targetTransform;

    BufferSensorComponent sensorBuffer;

    public void Start()
    {
        sensorBuffer = GetComponent<BufferSensorComponent>();
    }
    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;
        SetReward(+1f);
    }
    public override void CollectObservations(VectorSensor sensor)
    {

        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
        var SpawnOvni = transform.parent.GetComponentsInChildren<OVNI>();
        //System.Array.Sort(SpawnOvni, (a, b) => (Vector3.Distance(a.transform.position, transform.position)).CompareTo(Vector3.Distance(b.transform.position, transform.position)));
        int Spawns = 0;
        foreach (OVNI b in SpawnOvni)
        {
            if (Spawns >= 20)
            {
                break;
            }
            float[] SpawnObservations = new float[]{
                (b.transform.position.x - transform.position.x) / 5f,
                (b.transform.position.y - transform.position.y) / 5f,
                b.transform.forward.x,
                b.transform.forward.y
            };
            Spawns += 1;
            sensorBuffer.AppendObservation(SpawnObservations);
        };

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];


        float moveSpeed = 6f;
        transform.position += new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = Input.GetAxisRaw("Horizontal");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Parede>(out Parede Parede))
        {
            SetReward(-1f);
            EndEpisode();
        }


        if (other.TryGetComponent<OVNI>(out OVNI OVNI))
        {
            SetReward(-1f);
            EndEpisode();
        }

    }

}
