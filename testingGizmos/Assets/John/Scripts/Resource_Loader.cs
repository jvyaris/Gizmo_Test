using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Loader : MonoBehaviour
{
    public Object[] listOfObjects = null;
    public bool[] buttonList;
    private string[] objNames;
    
    void Start()
    {
        //Load all prefabs
        listOfObjects = Resources.LoadAll("Props/");

        //fill obj names for resource toolbar
        objNames = new string[listOfObjects.Length];
        for (int i = 0; i < listOfObjects.Length; i++)
        {
            objNames[i] = listOfObjects[i].name;
        }

        buttonList = new bool[listOfObjects.Length];
        //for(int i = 0; i < listOfObjects.Length; i++)
        //{
        //    buttonList[i] = false;
        //}
    }

    void OnGUI()
    {
        //UI window for selecting props/platforms
        GUI.Window(0, new Rect(0, 0, 500, 100), ResourcesWindow, "Resources");
    }

    void ResourcesWindow(int id)
    {
        for (int i = 0; i < listOfObjects.Length; i++)
        {
            buttonList[i] = GUI.Button(new Rect(30 * i, 20, 10, 10), objNames[i]);
            if (buttonList[i])
            {
                CreateNewObj(i);
            }
        }
    }

    //Creates a new gameobject and parents it to a cell
    void CreateNewObj(int index)
    {
        GameObject getCellPrefab = Resources.Load("Prefabs/Cell") as GameObject;
        GameObject newObj = Instantiate((GameObject)listOfObjects[index], transform.position, Quaternion.identity);
        GameObject newCell = Instantiate(getCellPrefab, transform.position, Quaternion.identity);
        newObj.transform.parent = newCell.transform;
        newCell.GetComponent<Cell>().ConnectChild();
    }
}
