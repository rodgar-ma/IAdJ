using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : SteeringBehaviour
{

    // Declara las variables que necesites para este SteeringBehaviour
    public float timeToTarget = 0.001f;
    protected Vector3 targetPosition;


    void Start()
    {
        this.nameSteering = "Arrive";
    }


    public override Steering GetSteering(Agent agent)
    {
        Steering steer = new Steering();
        float steerSpeed;
        Vector3 desiredVelocity;

        if (useDefaultTarget)
        {
            targetPosition = target.Position;
        }

        // Calcula el steering.
        Vector3 direction = targetPosition - agent.Position;

        // Calculamos distancia con el objetivo
        float distance = direction.magnitude;
        

        // Comprobamos si estamos dentro del radio
        if(distance < agent.InteriorRadius)
        {
            steer.linear = Vector3.zero;
        }
        else
        {
            // Calcular movimiento lineal
            // steer.linear = arriveVelocity / timeToTarget;

            // Si estamos fuera del radio slow
            if (distance > agent.ArrivalRadius)
            { 
                steerSpeed = agent.MaxSpeed;
            }
            // Si no calculamos la velocidad escalada
            else
            {
                steerSpeed = agent.MaxSpeed * distance / agent.ArrivalRadius;
            }

            // 

            // Combinamos direccion y velocidad
            desiredVelocity = direction.normalized * steerSpeed;

            // Acceleration tries to get to the target velocity
            steer.linear = (desiredVelocity - target.Velocity) / timeToTarget;


            // Comprobamos que no sobrepasemos la velocidad maxima
            if (steer.linear.magnitude > agent.MaxAcceleration)
            {
                steer.linear = steer.linear.normalized * agent.MaxAcceleration;
            }
        }



        // Calcular rotacion
        steer.angular = 0;

        // Retornamos el resultado final.
        return steer;
    }
}