using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum GizmoTypes      { Position, Rotation, Scale }
public enum GizmoAxis       { X, Y, Z }

/// <summary>
/// Gizmo_Manager stores information of what is being interacted, states, and objects by the gizmo tool
/// </summary>
public class Gizmo_Manager : MonoBehaviour
{
    //singleton
    public static Gizmo_Manager Instance = null;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    //Get prefabs
    private GameObject tool_Prefab = null;
    //..
    
    public GameObject gizmo_Tool = null;
    public Gizmo_Handler axis_X;
    public Gizmo_Handler axis_Y;
    public Gizmo_Handler axis_Z;
    public List<Transform> selections = null;

    public GizmoTypes gizmo_Type;


    void Start()
    {
        //Make a new list
        selections = new List<Transform>();
    }

    // - Scales the gizmo tool relative to the camera position
    void Update()
    {
        //Scales the gizmo_tool based on the camera distance, and updates it's position
        if (selections.Count > 0)
        {
            UpdateCenter();
            float distance = Vector3.Distance(gizmo_Tool.transform.position, Camera.main.transform.position);
            gizmo_Tool.transform.localScale = new Vector3(distance * 0.1f, gizmo_Tool.transform.localScale.x, gizmo_Tool.transform.localScale.y);
        }
    }
    //Manage selections of the user
    public void AddSelection(Transform transform)
    {
        //set the layer to ignore raycast
        transform.gameObject.layer = 2;

        //Add the newly added selection
        selections.Add(transform);
    }   
    public void ClearSelections()
    {
        //Deselect all objects
        foreach(Transform obj in selections)
        {
            obj.gameObject.layer = 0;
            obj.GetComponent<Cell>().isSelected = false;
        }

        //Clear all objects from the list of selections
        selections.Clear();
    }

    //Manipulate the selections via gizmo tool
    public void MoveSelections(float x, float y, float z)
    {
        //Moves all the selections by # of units on each axis
        foreach(Transform obj in selections)
        {
            obj.Translate(x, y, z);
        }
    }
    public void RotateSelections(Vector3 units)
    {
        //Rotates all the selections by # of units on each axis
        foreach (Transform obj in selections)
        {
            obj.Rotate(units);
        }
    }
    public void ScaleSelections(Vector3 units)
    {
        //Scales all the selections by # of units on each axis
        foreach (Transform obj in selections)
        {
            obj.localScale += units;
        }
    }

    //Helper functions
    public void ShowTool(bool show)
    {
        //Check if the tool is loaded
        if (!gizmo_Tool)
        {
            print("Tool is not loaded");
        }

        gizmo_Tool.SetActive(show);
    }
    public void UpdateCenter()
    {
        if (selections.Count <= 0)
        {
            print("There are no selections listed");
            return;
        }
        //Check if there are multiple selections
        if (selections.Count > 1)
        {
            //Finds the center of all selected objects
            Vector3 sum = Vector3.zero;
            foreach (Transform pos in selections)
            {
                sum += pos.position;
            }
            Vector3 centerOfSelections = sum / selections.Count;
            //Set position of the tool to the center of the selections
            gizmo_Tool.transform.position = centerOfSelections;
        }
        else
        {
            //Snap to the first selection's position
            gizmo_Tool.transform.position = selections[0].position;
        }
    }
}