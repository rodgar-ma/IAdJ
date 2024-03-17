using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTraceScreen : MonoBehaviour
{

    private GameObject firstThing = null;
    private GameObject secondThing = null;
    private bool firstTime = true;

    MeshRenderer m_Render = null;
    Color m_OriginalColor = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Devuelve un rayo desde la camara hasta un punto en la pantalla
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Other: .Linecast() . BoxCast() .SphereCAst() .CapsuleCast()

        // Dibujar rayos
        if(Physics.Raycast(ray, out hit))
        {
            drawRay(ray, hit);
        }

    }

    void drawRay(Ray ray, RaycastHit hit)
    {
        // El hit no está en el plano
        string str = hit.transform.gameObject.tag;
        if(!(str.Equals("Terrain")))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            Debug.DrawLine(hit.point, hit.point + 20 * hit.normal, Color.blue);
        }

        // Cambiamos el color del objeto si es posible
        changeColor(hit);
    }

    void changeColor(RaycastHit hit)
    {
        string str = hit.transform.gameObject.tag;

        if(firstTime && (str.Equals("NPC"))){
            firstThing = hit.transform.gameObject;
            m_Render = firstThing.GetComponent<MeshRenderer>();
            m_OriginalColor = m_Render.material.color;
            m_Render.material.color = Color.gray;
            firstTime = false;
            return;
        }

        if (firstThing == null) return;

        secondThing = hit.transform.gameObject;
        if (firstThing == secondThing) return;

        m_Render.material.color = m_OriginalColor;

        firstTime = true;

    }

}
