using UnityEngine;

public class MouseFolow : MonoBehaviour
{
    [SerializeField] float distance;
    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
         Physics.Raycast(ray, out RaycastHit hit);

        var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y);
        var v = Camera.main.ScreenToWorldPoint(mousePos);
        transform.GetComponent<Rigidbody>().MovePosition( new Vector3(Mathf.FloorToInt(hit.point.x), transform.localScale.y/2, Mathf.FloorToInt(hit.point.z)));

        if (Input.GetMouseButtonDown(0))
        {
            if (transform.TryGetComponent<BuildingView>(out BuildingView building)) building.Placed();
        }
    }
}