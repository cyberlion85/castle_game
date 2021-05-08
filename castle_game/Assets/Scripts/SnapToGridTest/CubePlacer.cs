using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    private Grid grid;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
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
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = finalPosition;
        cube.GetComponent<Renderer>().material.color = Color.blue;

        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }
}