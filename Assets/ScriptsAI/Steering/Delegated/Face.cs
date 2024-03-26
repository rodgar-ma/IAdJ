using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align
{

    // Declara las variables que necesites para este SteeringBehaviour

    void Start()
    {
        this.nameSteering = "Face";
    }


    public override Steering GetSteering(Agent agent)
    {
        Vector3 direction;
        float orientation;

        // Calculamos la distancia al objetivo
        direction = target.Position - agent.Position;

        // Comprobamos si la distancia es cero
        if (direction.magnitude == 0)
        {
            return null;
        }
        orientation = Mathf.Atan2(direction.x, direction.z);

        base.targetOrientation = target.RadiansToDegrees(orientation);
        base.useDefaultTarget = false;

        return base.GetSteering(agent);
    }
}