using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOrbitObject : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 CameraOffset;

    public bool RotateAroundPlayer = true;

    public float RotationSpeed = 10f;

    public GameObject Board;

    public bool MouseHold = false;
    void Start()
    {
        CameraOffset = this.transform.position - Board.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //CameraOffset = this.transform.position - Board.transform.position;

        if (RotateAroundPlayer)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);

            CameraOffset = camTurnAngle * CameraOffset;
        }

        Vector3 newPos = Board.transform.position + CameraOffset;

        this.transform.position = Vector3.Slerp(transform.position, newPos, 5f);


        if (RotateAroundPlayer)
        {
            this.transform.LookAt(Board.transform);
        }


        if( MouseHold == true)
        {
            RotateAroundPlayer = true;
        }
        else if(MouseHold == false)
        {
            RotateAroundPlayer = false;
        }
        if (Input.GetMouseButtonDown(1)){
            MouseHold = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            MouseHold = false;
        }
    }
}
