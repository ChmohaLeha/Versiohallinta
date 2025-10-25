using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject draggedBuilding;
    public GameManager gameManager;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private LayerMask baseArea;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if(draggedBuilding)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                draggedBuilding.transform.position = hit.point;
            }
            else
            {

            }
            if(Input.GetMouseButtonDown(0))
            {
                Bounds draggedBuildingBounds = draggedBuilding.GetComponent<BoxCollider>().bounds;
                Vector3 halfExtents = new Vector3(draggedBuildingBounds.extents.x, draggedBuildingBounds.extents.y, draggedBuildingBounds.extents.z);
                if (Physics.CheckBox(draggedBuilding.transform.position, halfExtents, Quaternion.identity, baseArea) == false)
                {
                    draggedBuilding.layer = 6;
                    draggedBuilding = null;
                }
                

                
            }
            if(Input.GetMouseButtonDown(1))
            {
                Destroy(draggedBuilding);
                draggedBuilding=null;
            }
        }
    }
}
