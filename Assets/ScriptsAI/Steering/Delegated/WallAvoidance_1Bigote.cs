using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAvoidance_1Bigote : Seek
{
    //Distancia minima a la pared
    public float avoidDistance;
    //Longitud del bigote
    public float lookahead;


    // Start is called before the first frame update
    void Start()
    {
        this.nameSteering = "WallAvoidance";
        useDefaultTarget = false;
    }

    public override Steering GetSteering(Agent agent)
    {

        // 1. Calcular el target pa delegar a seek
        if (target == null)
        {
            return null;
        }

        base.targetPosition = target.Position;
        base.useDefaultTarget = false;

        // 2. Calcular colision
        // Creamos el bigote
        Vector3 bigote = new Vector3();
        bigote = agent.Velocity;
        bigote.Normalize();

        CollisionDetector col = new CollisionDetector();
        col.getCollision(agent.Position, bigote, lookahead);

        // Si hay colision se calcula un nuevo target
        if(col.IsColliding)
        {
            base.targetPosition = col.Position + col.Normal * avoidDistance;
        }

        return base.GetSteering(agent);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * lookahead);
    }
}
