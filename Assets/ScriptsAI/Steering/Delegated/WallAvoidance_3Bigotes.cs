using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAvoidance_3Bigotes : Seek
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
        // Creamos los bigotes
        Vector3 bigoteCentral = crearBigote(agent.Velocity, 0f);
        Vector3 bigoteIzq = crearBigote(agent.Velocity, -45f);
        Vector3 bigoteDer = crearBigote(agent.Velocity, 45f);


        CollisionDetector col = new CollisionDetector();
        col.getCollision(agent.Position, bigoteCentral, lookahead);

        // Si hay colision se calcula un nuevo target
        //ColisionCentral
        if (col.IsColliding)
        {
            base.targetPosition = col.Position + col.Normal * avoidDistance;
        }
        else
        {
            // Colision Derecha
            col.getCollision(agent.Position, bigoteDer, lookahead/4);
            if (col.IsColliding)
            {
                base.targetPosition = col.Position + col.Normal * avoidDistance;
            }
            else
            {
                // Colision Izquierda
                col.getCollision(agent.Position, bigoteDer, lookahead / 4);
                if (col.IsColliding)
                {
                    base.targetPosition = col.Position + col.Normal * avoidDistance;
                }
            }
        }

        return base.GetSteering(agent);
    }

    private Vector3 crearBigote(Vector3 direction, float orientation)
    {   
        // Creamos el bigote
        Vector3 bigote = new Vector3();
        bigote = direction;


        // Rotamos el bigote
        Quaternion rotation = Quaternion.AngleAxis(orientation, Vector3.up);
        bigote = rotation * bigote;

        
        bigote.Normalize();
        return bigote;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * lookahead);
    }
}

