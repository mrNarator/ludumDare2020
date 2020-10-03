using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Movement movement;
    Sentient sentient;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        sentient = GetComponent<Sentient>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movement.RegisterAction(Movement.MovementType.Forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.RegisterAction(Movement.MovementType.Backward);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.RegisterAction(Movement.MovementType.Left);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement.RegisterAction(Movement.MovementType.Right);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            sentient.Interact();
        }
    }
}
