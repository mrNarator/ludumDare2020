using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotator : MonoBehaviour
{
    public float rotationSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime); 
    }
}
