using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWhereYouGoing : Align
{
    // Declara las variables que necesites para este SteeringBehaviour

    void Start()
    {
        this.nameSteering = "LookWhereYouGoing";
    }


    public override Steering GetSteering(Agent agent)
    {
        float orientation;

        // Comprobamos si la velocida es cero
        if (agent.Velocity.magnitude == 0)
        {
            return null;
        }

        // Si se esta moviendo calculamos el objetivo en base a la velocidad
        orientation = Mathf.Atan2(agent.Velocity.x, agent.Velocity.z);

        base.useDefaultTarget = false;
        base.targetOrientation = agent.RadiansToDegrees(orientation);

        return base.GetSteering(agent);
    }
}