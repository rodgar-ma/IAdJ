using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align
{

    protected Vector3 targetPosition;
    // Declara las variables que necesites para este SteeringBehaviour

    void Start()
    {
        this.nameSteering = "Face";
    }


    public override Steering GetSteering(Agent agent)
    {
        Vector3 direction;
        float orientation;
        Steering steer = new Steering();

        if(target != null)
        {
            targetPosition = target.Position;
        }

        // Calculamos la distancia al objetivo
        direction = targetPosition - agent.Position;

        // Comprobamos si la distancia es cero
        if (direction.magnitude == 0)
        {
            steer.angular = 0;
            steer.linear = Vector3.zero;
            return steer;
        }
        orientation = Mathf.Atan2(direction.x, direction.z);

        base.targetOrientation = agent.RadiansToDegrees(orientation);
        base.useDefaultTarget = false;

        return base.GetSteering(agent);
    }
}