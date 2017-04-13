using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gizmo_Controller handles any interactions to the objects in the editor, and
/// stores that info in a singleton script called Gizmo_Manager.
/// It also uses the stored information that Gizmo_Manager has to edit, add, and
/// delete objects placed.
/// </summary>
public class Gizmo_Controller : MonoBehaviour
{
    //singleton
    public static Gizmo_Controller Instance = null;
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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Adds a selection
        if (Input.GetMouseButtonDown(0))
        {
            Transform hitInfo = CastRay();
            if (hitInfo)
            {
                if (hitInfo.tag == "Cell")
                {
                    //Check if shift is not being held for adding more selections
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        //Clears the list of selections if the user does not want to select multiple objects.
                        Gizmo_Manager.Instance.ClearSelections();
                    }
                    //Check if the objects was not already selected
                    if(!hitInfo.GetComponent<Cell>().isSelected)
                    {
                        hitInfo.GetComponent<Cell>().isSelected = true;
                        Gizmo_Manager.Instance.ShowTool(true);
                        Gizmo_Manager.Instance.AddSelection(hitInfo);
                    }
                }
            }
        }

        //Clears all selections
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Gizmo_Manager.Instance.ShowTool(false);
            Gizmo_Manager.Instance.ClearSelections();
        }

        //Set gizmo type to position
        if(Input.GetKeyDown(KeyCode.W))
        {
            Gizmo_Manager.Instance.gizmo_Type = GizmoTypes.Position;
        }

        //Set gizmo type to rotation
        if (Input.GetKeyDown(KeyCode.E))
        {
            Gizmo_Manager.Instance.gizmo_Type = GizmoTypes.Rotation;
        }

        //Set gizmo type to scale
        if (Input.GetKeyDown(KeyCode.R))
        {
            Gizmo_Manager.Instance.gizmo_Type = GizmoTypes.Scale;
        }

        //Spawn a object
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateObj();
        }
    }

    //Creates a new prop parented to a cell prefab
    void CreateObj()
    {
        GameObject getPrefab = Resources.Load("Prefabs/SpriteObj") as GameObject;
        GameObject getCellPrefab = Resources.Load("Prefabs/Cell") as GameObject;
        GameObject newObj = Instantiate(getPrefab, transform.position, Quaternion.identity);
        GameObject newCell = Instantiate(getCellPrefab, transform.position, Quaternion.identity);
        newObj.transform.parent = newCell.transform;
        newCell.GetComponent<Cell>().ConnectChild();
    }

    // Casts a ray relative to the mouse's postion and camera position
    Transform CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo, Mathf.Infinity);
        return (hitInfo.collider != null) ? hitInfo.collider.transform : null;
    }
}