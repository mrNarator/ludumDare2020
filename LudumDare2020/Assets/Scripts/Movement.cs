using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Movement : MonoBehaviour
{
    public float MovementCooldown = 0.25f;
    public float Speed = 1;
    private float remainingMoveCooldown = 0.0f;

    Rigidbody rb;
    Sentient sentient;
    Stack<MovementType> actionList = new Stack<MovementType>();

    public enum MovementType { 
        Forward,
        Backward,
        Left,
        Right,
    }

    public bool CanReceiveInput => (remainingMoveCooldown <= 0 && rb.velocity.magnitude <= 0.01) &&
        sentient == null ? true : sentient.Alive();

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sentient = GetComponent<Sentient>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        remainingMoveCooldown -= Time.deltaTime;

        if (CanReceiveInput && actionList.Count >0)
        {
            bool proccessed = false;
            switch(actionList.Pop())
            {
                case MovementType.Forward:
                    {
                        Forward();
                        proccessed = true;
                        break;
                    }
                case MovementType.Backward:
                    {
                        Backward();
                        proccessed = true;
                        break;
                    }
                case MovementType.Left:
                    {
                        Right();
                        proccessed = true;
                        break;
                    }
                case MovementType.Right:
                    {
                        Left();
                        proccessed = true;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            if (proccessed)
            {
                remainingMoveCooldown = MovementCooldown; 
                actionList.Clear();
                sentient?.ChangeFood(-SettingsProvider.Instance.Global.MovementFoodCost);
            }
        }
        else
        {
            actionList.Clear();
        }
    }

    public void RegisterAction(MovementType type)
    {
        actionList.Push(type);   
    }

    void Forward()
    {
        Vector3 dir = new Vector3(0.0f, 0.25f, 0.5f);
        Move(dir * Speed);
    }

    void Backward()
    {
        Vector3 dir = new Vector3(0.0f, 0.25f, -0.5f);
        Move(dir * Speed);
    }
    
    void Right()
    {
        Vector3 dir = new Vector3(0.5f, 0.25f, 0.0f);
        Move(dir * Speed);
    }

    void Left()
    {
        Vector3 dir = new Vector3(-0.5f, 0.25f, 0.0f);
        Move(dir * Speed);
    }

    void Move(Vector3 vector)
    {
        rb.AddForce(vector, ForceMode.VelocityChange);
    }
}
