using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour
{

    public bool MouseLook = true;
    public string HorzAxis = "Horizontal";
    public string VertAxis = "Vertical";

    public float MaxSpeed = 5f;


    // Update is called once per frame
    void FixedUpdate()
    {
        float Horz = Input.GetAxis(HorzAxis);
        float Vert = Input.GetAxis(VertAxis);
        transform.position = new Vector3(transform.position.x + Horz * MaxSpeed, transform.position.y + Vert * MaxSpeed, transform.position.z);
    }
}
