using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    private Grid grid;
    public GameObject prefab1;
    public GameObject prefab2;
    private GameObject inst_obj;
    //public GameObject showingObj;
    [SerializeField] GameObject activePrefab;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        activePrefab = prefab1;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            inst_obj.transform.Rotate(0, 90, 0);
            //Vector3 eulerAngles = inst_obj.transform.rotation.eulerAngles;
            //Debug.Log("transform.rotation angles x: " + eulerAngles.x + " y: " + eulerAngles.y + " z: " + eulerAngles.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("call GetMouseButtonDown");

            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                //---------------------------------
                //kosil, prinuditelno govorim 4to pol eto 0
                hitInfo.point = new Vector3(hitInfo.point.x, 0f, hitInfo.point.z);
                //---------------------------------
                PlaceCubeNear(hitInfo.point);
            }

        }
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


    void PlaceCubeNear(Vector3 clickPoint)
    {
        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        /*        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = finalPosition;
                cube.GetComponent<Renderer>().material.color = Color.blue;*/
        //Debug.Log(finalPosition);
        Instantiate(inst_obj, finalPosition, inst_obj.transform.rotation);
    }
    //TODO peredelat na spisok  
     void SetPrefab1()
        {
        activePrefab=prefab1;
        SetCursorObj(activePrefab);
    }
    void SetPrefab2()
        {
        activePrefab=prefab2;
        SetCursorObj(activePrefab);
    }

    void SetCursorObj(GameObject obj)
    {
        Destroy(inst_obj);
        inst_obj = Instantiate(obj, transform.position, Quaternion.identity);
    }

}