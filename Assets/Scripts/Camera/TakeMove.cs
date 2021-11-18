using UnityEngine;


/// <summary>
/// dragging the Map
/// </summary>
public class TakeMove : MonoBehaviour
{
    Vector3 mousePos;
    GameObject changePosObject;

    bool canUse = false;
    void Update()
    {

        if (canUse && Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            MoveCity();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            canUse = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (changePosObject != null)
            {
                MoveCity();
            }
        }
        else
        {
            mousePos = Vector3.zero;
            canUse = false;
        }

    }

    /// <summary>
    /// Перемещение города
    /// </summary>
    private void MoveCity()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
        var hits = Physics.RaycastAll(ray);
        if (Input.GetMouseButton(0))
        {
            foreach (var hit in hits)
            {
                if ((hit.transform.TryGetComponent(out CityView city)))
                {
                    city.transform.position = new Vector3(Mathf.FloorToInt(hit.point.x), 0, Mathf.FloorToInt(hit.point.z));
                    changePosObject = city.transform.gameObject;
                }
            }
        }
    }
}
