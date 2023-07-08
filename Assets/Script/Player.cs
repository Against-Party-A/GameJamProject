using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private Rigidbody rig;
    private Quaternion target;

    private Vector3 input;
    private bool isFirstToBedroom = true;

    public float speed;
    public float rotateSpeed;


    protected override void Awake()
    {
        base.Awake();
        rig = this.GetComponent<Rigidbody>();
        target = this.transform.rotation;
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        rig.MovePosition(this.transform.position + input * speed * Time.deltaTime);
        if (input.x > 0 && input.z == 0)
        {
            target = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else if (input.x < 0 && input.z == 0)
        {
            target = Quaternion.Euler(new Vector3(0, -90, 0));
        }
        else if (input.z > 0 && input.x == 0)
        {
            target = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (input.z < 0 && input.x == 0)
        {
            target = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (input.x > 0 && input.z > 0)
        {
            target = Quaternion.Euler(new Vector3(0, 45, 0));
        }
        else if (input.x > 0 && input.z < 0)
        {
            target = Quaternion.Euler(new Vector3(0, 135, 0));
        }
        else if(input.x < 0 && input.z > 0)
        {
            target = Quaternion.Euler(new Vector3(0, -45, 0));
        }
        else if(input.x < 0 && input.z < 0)
        {
            target = Quaternion.Euler(new Vector3(0, 225, 0));
        }
        TurnAround(target);
    }

    private void TurnAround(Quaternion target)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotateSpeed);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.CompareTo("BedRoom") == 0)
        {
            if(isFirstToBedroom)
            {
                UIManager.Instance.StartAngerTiming();
                isFirstToBedroom = false;
            }
            else
            {
                UIManager.Instance.StartRelieveTiming();
                isFirstToBedroom = true;
            }
        }
    }
}
