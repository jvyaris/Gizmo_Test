using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isSelected = false;

    private BoxCollider boxCollider;
    private GameObject childObj;
    private Sprite mySprite;
    
    public void ConnectChild()
    {
        childObj = transform.GetChild(0).gameObject;
        boxCollider = GetComponent<BoxCollider>();
        if (childObj)
        {
            mySprite = childObj.GetComponent<SpriteRenderer>().sprite;
            boxCollider.size = mySprite.bounds.size;
        }
    }
    void Update()
    {
        if(Gizmo_Manager.Instance.selections.Count > 0)
        {
            gameObject.layer = 2;
        }
        else
        {
            gameObject.layer = 0;
        }
    }
}