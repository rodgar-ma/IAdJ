using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMark : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotar sobre el eje Y
        transform.Rotate(Vector3.up * Time.deltaTime * 1000, Space.World);
    }
}
