using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo_Handler : MonoBehaviour
{
    public GameObject position_Cap;
    public GameObject rotation_Cap;
    public GameObject scale_Cap;

    public GizmoAxis gizmo_Axis;
    public Transform center;
    public Vector3 mouse_StartPos;
    public Vector3 mouse_CurDir;
    public Vector3 camCheck;
    public float mouse_Dist = 0f;
    public float mouse_Dot = 0f;
    public float maxMove = 0f;
    public float curMove = 0f;
    public float numOfUnits = 0f;


    void Update()
    {
        camCheck = Camera.main.transform.position - center.position;
        camCheck.Normalize();
    }

    void OnMouseDown()
    {
        //Set mouse's start position
        mouse_StartPos = Input.mousePosition;
    }
    void OnMouseDrag()
    {
        //Normalize the mouse relative to it's initial position when clicked. To get it's direction.
        mouse_CurDir = Input.mousePosition - mouse_StartPos;
        mouse_CurDir.Normalize();

        bool isMoving = 
            (Mathf.Abs(Input.GetAxis("Mouse X") + Input.GetAxis("Mouse Y")) * Time.deltaTime > 0f) 
            ? true : false;

        if (isMoving)
        {
            curMove += 1f;
        }

        switch (Gizmo_Manager.Instance.gizmo_Type)
        {
            case GizmoTypes.Position:
                {
                    switch (gizmo_Axis)
                    {
                        case GizmoAxis.X:
                            {
                                //This is to check, whether the mouse is moving 
                                //along the X axis's forward direction 
                                mouse_Dot = Vector3.Dot(mouse_CurDir, center.transform.right);

                                //Checks if the camera is behind the object, then flip dot product value
                                if (camCheck.z > 0f)
                                {
                                    mouse_Dot = -mouse_Dot;
                                }
                                
                                //Increment the object's x position
                                if (mouse_Dot > 0f)
                                {
                                    if(curMove >= maxMove)
                                    {
                                        Gizmo_Manager.Instance.MoveSelections(numOfUnits, 0f, 0f);
                                        curMove = 0f;
                                    }
                                    mouse_StartPos = Input.mousePosition;
                                }
                                //Decrement the object's x position
                                else if (mouse_Dot < 0f)
                                {
                                    if (curMove >= maxMove)
                                    {
                                        Gizmo_Manager.Instance.MoveSelections(-numOfUnits, 0f, 0f);
                                        curMove = 0f;
                                    }
                                    mouse_StartPos = Input.mousePosition;
                                }
                                break;
                            }
                        case GizmoAxis.Y:
                            {
                                //This is to check, whether the mouse is moving 
                                //along the X axis's forward direction 
                                mouse_Dot = Vector3.Dot(mouse_CurDir, center.transform.up);
                                
                                //Increment the object's x position
                                if (mouse_Dot > 0f)
                                {
                                    if (curMove >= maxMove)
                                    {
                                        Gizmo_Manager.Instance.MoveSelections(0f, numOfUnits, 0f);
                                        curMove = 0f;
                                    }
                                    mouse_StartPos = Input.mousePosition;
                                }
                                //Decrement the object's x position
                                else if (mouse_Dot < 0f)
                                {
                                    if (curMove >= maxMove)
                                    {
                                        Gizmo_Manager.Instance.MoveSelections(0f, -numOfUnits, 0f);
                                        curMove = 0f;
                                    }
                                    mouse_StartPos = Input.mousePosition;
                                }
                                break;
                            }
                        case GizmoAxis.Z:
                            {
                                //This is to check, whether the mouse is moving 
                                //along the X axis's forward direction 
                                mouse_Dot = Vector3.Dot(mouse_CurDir, center.transform.up + center.transform.right);

                                //Checks if the camera is behind the object, then flip dot product value
                                if (camCheck.x < 0f)
                                {
                                    mouse_Dot = -mouse_Dot;
                                }

                                //Increment the object's x position
                                if (mouse_Dot > 0f)
                                {
                                    if (curMove >= maxMove)
                                    {
                                        Gizmo_Manager.Instance.MoveSelections(0f, 0f, numOfUnits);
                                        curMove = 0f;
                                    }
                                    mouse_StartPos = Input.mousePosition;
                                }
                                //Decrement the object's x position
                                else if (mouse_Dot < 0f)
                                {
                                    if (curMove >= maxMove)
                                    {
                                        Gizmo_Manager.Instance.MoveSelections(0f, 0f, -numOfUnits);
                                        curMove = 0f;
                                    }
                                    mouse_StartPos = Input.mousePosition;
                                }
                                break;
                            }
                    }
                    break;
                }
            case GizmoTypes.Rotation:
                {
                    switch (gizmo_Axis)
                    {
                        case GizmoAxis.X:
                            {
                                break;
                            }
                        case GizmoAxis.Y:
                            {
                                break;
                            }
                        case GizmoAxis.Z:
                            {
                                break;
                            }
                    }
                    break;
                }
            case GizmoTypes.Scale:
                {
                    switch (gizmo_Axis)
                    {
                        case GizmoAxis.X:
                            {
                                break;
                            }
                        case GizmoAxis.Y:
                            {
                                break;
                            }
                        case GizmoAxis.Z:
                            {
                                break;
                            }
                    }
                    break;
                }
        }
    }
}