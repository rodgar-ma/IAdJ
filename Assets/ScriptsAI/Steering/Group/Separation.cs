using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : Seek
{
    // Start is called before the first frame update

    public List<Vector3> potentialTargets;      // Targets potenciales
    public float threshold;                     // Threshold para tomar accion
    public float decayCoefficient;              // k
    void Start()
    {
        this.nameSteering = "Separation";
    }

    // Update is called once per frame
    public override Steering GetSteering(Agent agent)
    {
        
        foreach(Vector3 target in potentialTargets)
        {
            // Creamos nuevo steering

            // Comprobar si el target está cerca
            Vector3 direction = target - agent.Position;
            float distance = direction.magnitude;

            if (distance < threshold)
            {
                // Calcular la fuerza de repulsion
                float strength = Mathf.Min(decayCoefficient / (distance * distance), agent.MaxAcceleration);

                // Añadir aceleración
                direction.Normalize();

            }
        }
        
        
        
        
        return base.GetSteering(agent);
    }
}
