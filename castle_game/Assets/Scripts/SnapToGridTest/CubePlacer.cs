using System.Collections.Generic;
using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    public bool delObj;

    private Grid grid;

    private GameObject inst_obj;

    public List<GameObject> myListObjects = new List<GameObject>();

    [SerializeField] GameObject activePrefab;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();

    }
    void SetDelObj(bool status)
    {
        delObj = status;
        Destroy(inst_obj);
    }
    void delObject()
    {
        if (delObj && Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {
                Destroy(hitInfo.collider.gameObject);
            }
        }
    }

    void RotateInstObj()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            inst_obj.transform.Rotate(0, 90, 0);
            //Vector3 eulerAngles = inst_obj.transform.rotation.eulerAngles;
            //Debug.Log("transform.rotation angles x: " + eulerAngles.x + " y: " + eulerAngles.y + " z: " + eulerAngles.z);
        }     if (Input.GetKeyDown(KeyCode.D))
        {
            inst_obj.transform.Rotate(0, -90, 0);
        }
    }
    void SetObj() {
        if (Input.GetMouseButtonDown(0) && !delObj)
        {
            //Debug.Log("call GetMouseButtonDown");

            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                //---------------------------------
                //kostil, prinuditelno govorim 4to pol eto 0
                hitInfo.point = new Vector3(hitInfo.point.x, 0f, hitInfo.point.z);
                //---------------------------------
                PlaceCubeNear(hitInfo.point);

            }

        }
    }
    void FollowMouse()
    {
        RaycastHit[] hits;
        Ray rayMouse;

        rayMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(rayMouse);
        int i = 0;
        while (i < hits.Length)
        {
            RaycastHit hit = hits[i];
            //Debug.Log(hit.collider.gameObject.name);
            i++;

            if (hit.collider.gameObject.name == "Plane")
            {
                inst_obj.transform.position = new Vector3(hit.point.x, 0.1f, hit.point.z);
            }
        }
    }
    private void Update()
    {
        delObject();
        RotateInstObj();
        SetObj();
        FollowMouse();
     }


    void PlaceCubeNear(Vector3 clickPoint)
    {
        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        /*        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = finalPosition;
                cube.GetComponent<Renderer>().material.color = Color.blue;*/
        //Debug.Log(finalPosition);
        Instantiate(inst_obj, finalPosition, inst_obj.transform.rotation);
    }
    public void SetPrefab(int i)
        {
        delObj = false;
        Destroy(inst_obj);
        activePrefab = myListObjects[i].gameObject;
        inst_obj = Instantiate(activePrefab, transform.position, Quaternion.identity);
    }    



}