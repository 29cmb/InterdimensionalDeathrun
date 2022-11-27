using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPrecice : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera Cam;
    public GameObject Player;
    
    void Start()
    {
        Debug.DrawLine(Player.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistanceFromItem = Vector3.Distance(Player.transform.position, transform.position);
        if(playerDistanceFromItem < 1.15f)
        {
            Cam.transform.parent = Player.transform;
        }
    }
}
