using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{

    // Declara las variables que necesites para este SteeringBehaviour
    protected Vector3 targetPosition = new Vector3();
   
    
    void Start()
    {
        this.nameSteering = "Seek";
    }


    public override Steering GetSteering(Agent agent)
    {

        // Creamos el output
        Steering steer = new Steering();

        // Calculamos la posicion del target
        if (useDefaultTarget)
        {
            targetPosition = target.Position;
        }

        // Obtenemos la direccion al target
        steer.linear = targetPosition - agent.Position;

        // Aceleramos en la direccion
        steer.linear.Normalize();
        steer.linear *= agent.MaxAcceleration;

        // Output
        steer.angular = 0;
        return steer;
       
    }

    public virtual void NewTarget(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
        useDefaultTarget = false;
    }

}