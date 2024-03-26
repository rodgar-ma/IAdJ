using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : Seek
{
    public GameObject pathObject;
    public List<Vector3> path;
    public int waypoint = -1;

    void Start()
    {
        this.nameSteering = "PathFollowing";
        if (pathObject != null)
        {
            Transform pathTransform = pathObject.GetComponent<Transform>();

            for (int i = 0; i < pathTransform.childCount; i++)
            {
                Transform point = pathTransform.GetChild(i);
                path.Add(point.transform.position);
            }
        }

        waypoint = 0;
    }


    public override Steering GetSteering(Agent agent)
    {

        // Comprobamos si hay objetivo
        if(waypoint == -1)
        {
            return null;
        }


        // Predecimos futura localizacion
        // Vector3 futurePos = agent.Position + agent.Velocity * maxPrediction;

        // Comprobamos la posicion del currentPath
        targetPosition = path[waypoint];
        base.targetPosition = getParam(agent, targetPosition);
        base.useDefaultTarget = false;

        // Delegamos en Seek
        return base.GetSteering(agent);
    }

    public override void NewTarget(Vector3 newTarget)
    {
        // Si nos llega un nuevo waypoin lo añadimos al final de la lista
        path.Add(newTarget);
    }

    private Vector3 getParam(Agent agent, Vector3 targetPosition)
    {

        // Comprobamos si ya hemos llegado a la posicion del objetivo actual
        if (Vector3.Distance(agent.Position, targetPosition) <= 0.1f)
        {
            // Si hemos llegado paramos y vamos a por el siguiente
            agent.Velocity = Vector3.zero;
            // Si no hemos llegado al ultimo vamos al siguiente waypoint
            if(waypoint < (path.Count - 1))
            {
                waypoint++;
                targetPosition = path[waypoint];
            }
        }
        return targetPosition;
    }

}
