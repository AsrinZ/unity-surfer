using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 100f;
    private float laneDist = 4;
    private int lane = 1;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("a press");
        }
        else if (Input.GetKeyDown("d"))
        {
            Debug.Log("d press");
        }
    }
}
