using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Movement movement;
    Sentient sentient;
    PlayerHUD playerHUD;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        sentient = GetComponent<Sentient>();

        playerHUD = FindObjectOfType<PlayerHUD>();

        sentient.SetupAsPlayer();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!sentient.Alive())
        {
            //dont try this at home 
            playerHUD.DeathNotice(sentient);
            return;
        }

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

       
        playerHUD.UpdateScores(sentient);
    }
}
