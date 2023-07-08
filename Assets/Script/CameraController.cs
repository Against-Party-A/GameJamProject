using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 difference;

    public Transform target;

    private void Awake()
    {
        mainCamera = this.GetComponent<Camera>();
    }

    private void Start()
    {
        difference = this.transform.position - target.transform.position;
    }

    private void Update()
    {
        this.transform.position = target.transform.position + difference;
    }
}
