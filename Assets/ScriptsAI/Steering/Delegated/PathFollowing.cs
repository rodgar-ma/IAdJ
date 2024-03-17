using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : Seek
{

    public List<Vector3> path;
    public int waypoint = 0;
    public float maxPrediction = 0.5f;   // Tiempo máximo de prediccion

    void Start()
    {
        this.nameSteering = "PathFollowing";
        
    }


    public override Steering GetSteering(Agent agent)
    {

        // Comprobamos si hay objetivo
        if(target == null)
        {
            // Si hay una meta hay un camino a seguir se crea un target nuevo
            if(waypoint < path.Count)
            {
                GameObject newTarget = new GameObject("PFTarget");
                newTarget.AddComponent<Agent>();
                target = newTarget.GetComponent<Agent>();
                target.Position = path[waypoint];
            }
            else
            {
                return null;
            }
        }

        // Predecimos futura localizacion
        // Vector3 futurePos = agent.Position + agent.Velocity * maxPrediction;

        // Comprobamos la posicion del currentPath
        base.target = getParam(agent.Position, target);

        // Delegamos en Seek
        return base.GetSteering(agent);
    }

    public override void NewTarget(Vector3 newTarget)
    {
        // Si nos llega un nuevo waypoin lo añadimos al final de la lista
        path.Add(newTarget);
    }

    private Agent getParam(Vector3 position, Agent target)
    { 
        // Comprobamos si ya hemos llegado a la posicion del objetivo actual
        if (Vector3.Distance(position, target.Position) <= target.InteriorRadius)
        {
            // Si no hemos llegado al ultimo vamos al siguiente waypoint
            if(waypoint < (path.Count - 1))
            {
                waypoint++;
                target.Position = path[waypoint];
            }
            else
            {
                Destroy(target.gameObject);
                return null;
            }
        }
        return target;
    }

}
