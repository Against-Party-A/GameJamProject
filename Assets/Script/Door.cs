using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private bool canOpen;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("Player") == 0)
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0)
        {
            canOpen = false;
        }
    }

    private void Update()
    {
        if(canOpen)
        {
            if (Input.GetKeyDown(KeyCode.F))
                OpenDoor();
        }
    }

    public void OpenDoor()
    {
        anim.SetTrigger("DoorOpen");
        Debug.Log("Open");
        StartCoroutine(CloseDoor());
    }

    public IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(2.0f);
        anim.SetTrigger("DoorClose");
    }
}
