using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * L. Daniel Hern�ndez. 2018. Copyleft
 * 
 * Una propuesta para dar �rdenes a un grupo de agentes sin formaci�n.
 * 
 * Recursos:
 * Los rayos de C�mara: https://docs.unity3d.com/es/current/Manual/CameraRays.html
 * "Percepci�n" mediante Physics.Raycast: https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
 * SendMessage to external functions: https://www.youtube.com/watch?v=4j-lh3C_w1Q
 * 
 * */

public class PropuestaOrdenarIrAUnLugar : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        // Damos una orden cuando levantemos el bot�n del rat�n.
        if (Input.GetMouseButtonUp(0))
        {

            // Comprobamos si el rat�n golpea a algo en el escenario.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {

                // Si lo que golpea es un punto del terreno entonces da la orden a todas las unidades NPC
                if (hitInfo.collider != null && hitInfo.collider.CompareTag("Terrain"))
                {
                    Vector3 newTarget = hitInfo.point;

                    GameObject[] listNPC = GameObject.FindGameObjectsWithTag("NPC");

                    /**
                     * Otra alternativa es recurrir a una lista p�blica de todas las unidades seleccionadas
                     *              public List<GameObject> selectedUnits;
                     * En este caso se cambiar�a "listNPC" por "selectedUnits"
                     * 
                     * Dicha lista pertenecer�a a una clase encarga de controlar eventos generales del juego,
                     * como por ejemplo la selecci�n de unidades. La ventaja de mantener una lista en tiempo
                     * de ejecuci�n es obvia: Si el n�mero de unidades es peque�o (p.e. dos) en relaci�n con
                     * el n�mero total de NPC (p.e. miles), pues no ser�a necesario que Unity busque en todos los
                     * objetos del escenario con la marca de haber sido seleccionado, 
                     * lo que facilita y agiliza algunas tareas. P.e. para realizar formaciones.
                     */

                    foreach (var npc in listNPC)
                    {
                        // Llama al m�todo denominado "NewTarget" en TODOS y cada uno de los MonoBehaviour de este game object (npc)
                        npc.SendMessage("NewTarget", newTarget);

                        // Se asume que cada NPC tiene varias componentes scripts (es decir, varios MonoBehaviour).
                        // En algunos de esos scripts est� la funci�n "NewTarget(Vector3 target)"
                        // Dicha funci�n contendr� las instrucciones necesarias para ir o no al nuevo destino.
                        // P.e. Dejar lo que est� haciendo y  disparar a target.
                        // P.e. Si no tengo vida suficiente huir de target.
                        // P.e. Si fui seleccionado en una acci�n anterio y estoy a la espera de nuevas �rdenes, entonces hacer un Arrive a target.

                        // Nota1: En el caso de que tu objeto tenga una estructura jer�rquica, 
                        // y se quiera invocar a NewTarget de todos sus hijos, deber�s usar BroadcastMessage.

                        // Nota 2: En el caso de que solo se tenga una funci�n "NewTarget" para cada NPC, entonces 
                        // puede ser m�s eficiente algo como:
                        //                  npc.GetComponent<ComponenteScriptConteniendoLaFuncion>().NewTarget(newTarget);
                        // que obtiene la componente del NPC que yo s� que contiene a la funci�n NewTarget(), y la invoca.
                    }
                }
            }
        }
    }
}