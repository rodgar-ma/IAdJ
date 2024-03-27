using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    public float wanderOffset = 10f;
    public float wanderRadius = 5f;
    public float wanderRate = 45f;

    // Start is called before the first frame update
    void Start()
    {
        this.nameSteering = "Wander";
    }

    public override Steering GetSteering(Agent agent)
    {
        Steering steer = new Steering();
        // Rango de movimiento de Wander (wanderRate)
        targetPosition = calcularWander(agent);


        // Calculamos la rotacion con Face
        steer = base.GetSteering(agent);
        // Calculamos movimiento en la direccion
        steer.linear = agent.MaxAcceleration * agent.AngleToVector(agent.Heading());
        // Devolvemos el Steering
        return steer;
    }

    private Vector3 calcularWander(Agent agent)
    {

        // Rango de movimiento de Wander (wanderRate)
        float wanderOrientation = Random.Range(-1f, 1f) * wanderRate;

        // Calculamos la orientacion
        float targetOrientation = wanderOrientation + agent.Heading();

        // Calculamos la posicion del target
        Vector3 targetPosition = agent.Position + wanderOffset * agent.AngleToVector(agent.Heading());
        targetPosition += wanderRadius * agent.AngleToVector(targetOrientation);

        // Devolvemos la posicion de target de Wander
        return targetPosition;
    }

}

