using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkObjects : MonoBehaviour
{

    public GameObject markPrefab;
    public List<GameObject> nPCs;

    // Start is called before the first frame update
    void Start()
    {
        nPCs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                // Comprobamos que sea seleccionable
                string str = hit.transform.gameObject.tag;
                if(str.Equals("NPC"))
                {
                    SelectNPC(hit.transform.gameObject);
                }
                if (str.Equals("Terrain"))
                {
                    TargetUpdate(hit.point);
                }
            }
        }
    }

    private void SelectNPC(GameObject thing)
    {

        // Si el npc no estaba seleccionado lo seleccionamos
        if (nPCs.Contains(thing))
        {
            nPCs.Remove(thing);
            UnMark(thing);
        }
        else
        {
            nPCs.Add(thing);
            Mark(thing);
        }

        
    }

    private void Mark(GameObject thing)
    {
        GameObject marker = Instantiate(markPrefab, thing.transform);
        marker.transform.localPosition = Vector3.up * 1;
        marker.name = "Mark";
        marker.tag = "Mark";
    }

    private void UnMark(GameObject thing)
    {
        GameObject marker = thing.transform.Find("Mark").gameObject;
        Destroy(marker);
    }

    private void TargetUpdate(Vector3 point)
    {
        Vector3 newTarget = new Vector3(point.x, point.y + 1, point.z);

        foreach (var npc in nPCs)
        {
            npc.SendMessage("NewTarget", newTarget);
        }
    }
}
