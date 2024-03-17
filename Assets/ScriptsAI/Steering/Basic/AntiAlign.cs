using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAlign : SteeringBehaviour
{

    // Declara las variables que necesites para este SteeringBehaviour
    public float timeToTarget = 0.25f;
    protected float targetOrientation;
    void Start()
    {
        this.nameSteering = "AntiAlign";
    }


    public override Steering GetSteering(Agent agent)
    {
        if (useDefaultTarget)
        {
            targetOrientation = target.Heading();
        }

        Steering steer = new Steering();
        float steerRotation, steerAngularAcc;

        // Calcula el steering.
        steer.linear = Vector3.zero;

        float rotation = agent.GetMiniminAngleTo(targetOrientation + 180);

        // Comprobamos si estamos mirando en la direccion correcta
        if (Mathf.Abs(rotation) < target.InteriorAngle)
        {
            return null;
        }

        // Comprobamos si estamos fuera del angulo exterior
        if (Mathf.Abs(rotation) > agent.ExteriorAngle)
        {
            steerRotation = agent.MaxRotation;
        }
        // Si no calculamos una rotacion escalada
        else
        {
            steerRotation = agent.MaxRotation * Mathf.Abs(rotation) / agent.ExteriorAngle;
        }

        // Combinamos velocidad y direccion de la rotacion
        steerRotation *= rotation / Mathf.Abs(rotation);

        // Acceleration tries to get to the target rotation
        steer.angular = steerRotation - agent.Rotation;
        steer.angular /= timeToTarget;

        // Comprobamos que no excedamos la aceleración angular máxima
        steerAngularAcc = Mathf.Abs(steer.angular);
        if (steerAngularAcc > agent.MaxAngularAcc)
        {
            steer.angular /= steerAngularAcc;
            steer.angular *= agent.MaxAngularAcc;
        }
        // Retornamos el resultado final.
        return steer;
    }

}