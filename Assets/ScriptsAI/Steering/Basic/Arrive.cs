using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : SteeringBehaviour
{

    // Declara las variables que necesites para este SteeringBehaviour
    public float timeToTarget = 0.25f;
    protected Vector3 targetPosition;


    void Start()
    {
        this.nameSteering = "Arrive";
    }


    public override Steering GetSteering(Agent agent)
    {
        Steering steer = new Steering();
        float steerSpeed;

        // Calcula el steering.
        Vector3 arriveVelocity = target.Position - agent.Position;

        // Calculamos distancia con el objetivo
        float distance = arriveVelocity.magnitude;
        

        // Comprobamos si estamos dentro del radio
        if(distance < target.InteriorRadius)
        {
            return null;
        }
        else
        {
            // Calcular movimiento lineal
            // steer.linear = arriveVelocity / timeToTarget;

            // Si estamos fuera del radio slow
            if (distance > target.ArrivalRadius)
            {
                steerSpeed = agent.MaxSpeed;
            }
            // Si no calculamos la velocidad escalada
            else
            {
                steerSpeed = agent.MaxSpeed * distance / target.ArrivalRadius;
            }

            // 

            // Combinamos direccion y velocidad
            arriveVelocity = arriveVelocity.normalized * steerSpeed;

            // Acceleration tries to get to the target velocity
            steer.linear = arriveVelocity - target.Velocity;
            steer.linear /= timeToTarget;


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