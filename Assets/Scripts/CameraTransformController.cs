using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//camera popsition controll
public class CameraTransformController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.TransportToRoom += HeightCheck;
    }

    void HeightCheck()
    {
        if (transform.position.y > 0.75f)
        {
            Debug.Log("Camera Offset Ascending" + transform.position.y);
            Vector3 newPosition = transform.position;
            newPosition.y = 0.7f;
            transform.position = newPosition;
        }
    }


    void Update()
    {
        //Debug.Log(transform.position.y);
        if(transform.position.y < 0.65f)
        {
            Debug.Log("Camera Offset Descending" + transform.position.y);
            Vector3 newPosition = transform.position;
            newPosition.y = 0.7f;
            transform.position = newPosition;
        }
    }

    
}
