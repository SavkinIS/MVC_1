using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.rotation = Camera.main.transform.rotation;
    }

}
