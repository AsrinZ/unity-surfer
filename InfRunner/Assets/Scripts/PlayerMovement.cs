using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;
    public float horizontalSpeed = 5f;
    public float jumpForce = 5f;
    private float laneDist = 4;
    private int lane = 1;
    public bool isOnFloor = true;
    public Transform pivot;
    public Quaternion rotation;
    private CapsuleCollider playerCollider;
    public Color phaseColor;
    public bool DynamicFov = false;
    public float boostSpeed = 5f;
    public GameObject surfboard;
    Vector3 forwardMovementVector, horizontalMovementVector;
    Vector3 tempSpeed;
    public Camera cam;
    Light boardLight;
    [SerializeField][Range(0f, 1f)] float lerpTime;
    private GameManager gameManager;
    public bool alive = false;
    public bool leftMovementInput = false;
    public bool rightMovementInput = false;
    private float ScreenWidth;
    AdManager adManager;

    private void Start()
    {
        ScreenWidth = Screen.width;
        gameManager = FindObjectOfType<GameManager>();
        forwardMovementVector = new Vector3(0f, 0f, speed);
        horizontalMovementVector = new Vector3(horizontalSpeed, 0f, 0f);
        playerCollider = GetComponent<CapsuleCollider>();
        boardLight = surfboard.GetComponent<Light>();
        adManager = FindObjectOfType<AdManager>();  
       
    }


    void Update()
    {
        /*
        if (Input.GetKey("w"))
        {
            transform.position += forwardMovementVector;
        
        }
        */
        if (!alive) return;
        DynamicFOV();
        transform.position += forwardMovementVector;
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).position.x > ScreenWidth / 2 && transform.position.x < 4)
            {
                //Debug.Log("Touch right");
                rightMovementInput = true;
                transform.position += horizontalMovementVector;

            }
            else if(Input.GetTouch(0).position.x <= ScreenWidth / 2 && transform.position.x  > -4)
            {
                //Debug.Log("Touch left");
                leftMovementInput = true;
                transform.position -= horizontalMovementVector;
            }
            else
            {
                rightMovementInput = false;
                leftMovementInput = false;
            }
        }
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tempSpeed = forwardMovementVector;
            forwardMovementVector *= boostSpeed;
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            forwardMovementVector = tempSpeed;
            
        }
        
        if (Input.GetKey("s") &&  transform.position.z > 30)
        {
            transform.position -= forwardMovementVector;
          
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Hit an obstacle!");
        }
    }

    void DynamicFOV()
    {
        if(Input.GetKey(KeyCode.Space) && DynamicFov == true)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 80, 5f * Time.deltaTime);
            boardLight.intensity = Mathf.Lerp(boardLight.intensity, 150, 10f * Time.deltaTime);

        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, 5f * Time.deltaTime);
            boardLight.intensity = Mathf.Lerp(boardLight.intensity, 1, 10f * Time.deltaTime);
        }
    }

    public void Die()
    {
        alive = false;
        adManager.LoadAd();
        //Restart the scene
        gameManager.Restart();
    }
    
}
