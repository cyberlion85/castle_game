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
        inst_obj = Instantiate(activePrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("call GetMouseButtonDown");

            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(transform.position, transform.forward * 1000, Color.yellow);


            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceCubeNear(hitInfo.point);
            }

        }

        RaycastHit[] hits;
        Ray rayMouse;

        rayMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.RaycastAll(rayMouse, out hits)) {
            if (hits.rigidbody.name == "Plane")
            {
                inst_obj.transform.position = new Vector3(hits.point.x, 0.1f, hits.point.z);
                //Debug.Log(hit.rigidbody.name);
            }
        }
        /* Vector3 mousePosition;
         mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         mousePosition = new Vector3(mousePosition.x, mousePosition.y, 1.0f);
         Debug.Log(mousePosition);
         inst_obj.transform.position = mousePosition;*/

    }


    private void PlaceCubeNear(Vector3 clickPoint)
    {
        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        /*        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = finalPosition;
                cube.GetComponent<Renderer>().material.color = Color.blue;*/

        Instantiate(activePrefab, finalPosition, Quaternion.identity);
    }
    public void SetPrefab1()
        {
        activePrefab=prefab1;
}    public void SetPrefab2()
        {
        activePrefab=prefab2;
}

    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = Camera.main.pixelHeight - currentEvent.mousePosition.y;

        point = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + Camera.main.pixelWidth + ":" + Camera.main.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }
}