using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector
{
    private Vector3 position;
    private Vector3 normal;
    private bool isColliding = false;

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    public Vector3 Normal
    {
        get { return normal; }
        set { normal = value; }
    }

    public bool IsColliding
    {
        get { return isColliding; }
        set { isColliding = value; }
    }

    public void getCollision(Vector3 charPosition, Vector3 bigote, float lookahead)
    {
        isColliding = false;

        Ray rayo = new Ray(charPosition, bigote);
        RaycastHit hit;
        if (Physics.Raycast(rayo, out hit, lookahead))
        {
            // Comprobamos que ha sido con una pared
            if (hit.collider.CompareTag("Wall"))
            {
                position = hit.point;
                normal = hit.normal;
                isColliding = true;
            }
        }
    }

}
