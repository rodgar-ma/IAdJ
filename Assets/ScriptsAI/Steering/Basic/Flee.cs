using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehaviour
{

    // Declara las variables que necesites para este SteeringBehaviour
    public Vector3 targetPosition;

    void Start()
    {
        this.nameSteering = "Flee";
    }


    public override Steering GetSteering(Agent agent)
    {
        // Comprobamos que haya un target
        if (useDefaultTarget)
        {
            targetPosition = target.Position;
        }

        Steering steer = new Steering();

        // Calcula el steering.
        Vector3 fleeVelocity = agent.Position - targetPosition;

        // Calcular movimiento lineal
        steer.linear = fleeVelocity.normalized * agent.MaxAcceleration;

        // Calcular rotacion
        steer.angular = 0;

        // Retornamos el resultado final.
        return steer;
    }

}