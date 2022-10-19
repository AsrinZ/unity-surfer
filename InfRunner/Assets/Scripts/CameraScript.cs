using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    PlayerMovement playerMovement;
    public Vector3 offset;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        //transform.position = new Vector3(0, target.position.y + offset.y, target.position.z + offset.z);
    }

    public void setAliveTrue()
    {
        playerMovement.alive = true;
    }
}
