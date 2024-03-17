using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : SteeringBehaviour
{

    // Declara las variables que necesites para este SteeringBehaviour
    public float timeToTarget = 0.25f;
    protected float targetOrientation;
    
    void Start()
    {
        this.nameSteering = "Align";
    }


    public override Steering GetSteering(Agent agent)
    {
        Steering steer = new Steering();
        float rotation, steerRotation, steerAngularAcc;

        // Calculamos la orientacion del target
        if (useDefaultTarget)
        {
            targetOrientation = target.Heading();
        }
        rotation = agent.GetMiniminAngleTo(targetOrientation);


        // Calcula el steering.
        steer.linear = Vector3.zero;

        // Comprobamos si estamos mirando en la direccion correcta
        if(Mathf.Abs(rotation) < agent.InteriorAngle)
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