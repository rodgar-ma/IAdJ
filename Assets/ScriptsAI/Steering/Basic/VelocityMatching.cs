using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatching : SteeringBehaviour
{

    // Declara las variables que necesites para este SteeringBehaviour
    public float timeToTarget = 0.1f;
    
    void Start()
    {
        this.nameSteering = "VelocityMatching";
    }


    public override Steering GetSteering(Agent agent)
    {
        Steering steer = new Steering();

        // Calcula el steering.
        steer.linear = target.Velocity - agent.Velocity;
        steer.linear /= timeToTarget;

        // Comprobamos si la aceleración es demasiada alta
        if(steer.linear.magnitude > agent.MaxSpeed)
        {
            steer.linear.Normalize();
            steer.linear *= agent.MaxSpeed;
        }

        steer.angular = 0;

        // Retornamos el resultado final.
        return steer;
    }
}