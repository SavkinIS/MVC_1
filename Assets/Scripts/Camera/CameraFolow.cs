using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    void Start()
    {
        transform.LookAt(Camera.main.transform.position);
    }

}
