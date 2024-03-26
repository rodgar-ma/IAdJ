using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    public float wanderOffset = 10f;
    public float wanderRadius = 5f;
    public float wanderRate = 45f;
    private float timeToWander = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.nameSteering = "Wander";
    }

    public override Steering GetSteering(Agent agent)
    {
        Steering steer = new Steering();
        // Rango de movimiento de Wander (wanderRate)

        if( timeToWander <= 5f)
        {
            timeToWander += Time.deltaTime;
        }
        else
        {
            agent.Velocity = Vector3.zero;
            targetPosition = calcularWander(agent);
            timeToWander = 0f;
        }

        // Calculamos la rotacion con Face
        steer = base.GetSteering(agent);
        // Calculamos movimiento en la direccion
        steer.linear = agent.MaxAcceleration * agent.AngleToVector(agent.Orientation);
        // Devolvemos el Steering
        return steer;
    }

    private Vector3 calcularWander(Agent agent)
    {

        // Rango de movimiento de Wander (wanderRate)
        float wanderOrientation = Random.Range(-1f, 1f) * wanderRate;

        // Calculamos la orientacion
        float targetOrientation = wanderOrientation + agent.Orientation;

        // Calculamos la posicion del target
        Vector3 targetPosition = agent.Position + wanderOffset * agent.AngleToVector(agent.Orientation);
        targetPosition += wanderRadius * agent.AngleToVector(targetOrientation);

        // Devolvemos la posicion de target de Wander
        return targetPosition;
    }

}

