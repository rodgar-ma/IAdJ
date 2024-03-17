using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkCharacter : MonoBehaviour
{
    public GameObject markPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                enableMark(hit.transform.gameObject);
            }
        }
    }

    private void mark(GameObject thing)
    {
        GameObject marker = null;

        // Si hay un hijo del objeto llamado "Mark"
        if (thing.transform.Find("Mark") != null) // Obtenemos referencia
        {
            marker = thing.transform.Find("Mark").gameObject;
        }

        // Si no hay ninguna referencia
        if (marker == null)
        {
            // Creamos una instancia del prefab
            marker = Instantiate(markPrefab, thing.transform);

            // Cambiamos la posicion relativa
            marker.transform.localPosition = Vector3.up * 1;

            // Le cambiamos el nombre
            marker.name = "Mark";
        }
        // Si había alguna referencia
        else
        {
            Destroy(marker);    // Destruimos la marca
        }
    }

    void enableMark(GameObject thing)
    {
        bool show = false;
        Transform marker = thing.transform.Find("Mark"); // Encontramos la marca
        show = marker.GetComponent<Renderer>().enabled;

        // Cambiamos el check activado
        marker.GetComponent<Renderer>().enabled = !show;
        marker.GetComponent<Rotation>().enabled = !show;
    }
}
