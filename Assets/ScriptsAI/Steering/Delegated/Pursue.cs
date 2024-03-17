﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : Seek
{

    // Declara las variables que necesites para este SteeringBehaviour
    public float maxPrediction = 0.5f;   // Tiempo máximo de prediccion
    
    void Start()
    {
        this.nameSteering = "Pursue";
    }


    public override Steering GetSteering(Agent agent)
    {
        Vector3 direction;
        float distance, speed, prediction;

        // Calculamos la distancia al objetivo
        direction = target.Position - agent.Position;
        distance = direction.magnitude;

        // Calculamos la velocidad actual
        speed = agent.Velocity.magnitude;

        // Comprobamos si la velocidad es demasiado pequeña
        if(speed <= distance / maxPrediction)
        {
            prediction = maxPrediction; // La prediccion es la maxima    
        }
        // Si no calculamos la prediccion
        else
        {
            prediction = distance / speed;
        }

        // 
        base.targetPosition = target.Position + target.Velocity * prediction;
        base.useDefaultTarget = false;

        // Delegamos en seek
        return base.GetSteering(agent);
    }
}