using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//camera popsition controll
public class CameraTransformController : MonoBehaviour
{
    public Transform RoomSpawnPoint;
    private float _radius = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.TransportToRoom += HeightCheck;
    }

    void HeightCheck()
    {
        if (transform.position.y > 0.7f)
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

        float distance = Vector3.Distance(transform.position, RoomSpawnPoint.position);
        if (distance <= _radius)
        {
            HeightCheck();
        }
    }

    
}
