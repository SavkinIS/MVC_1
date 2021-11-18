using UnityEngine;


/// <summary>
/// Camera Control
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("Скорость измнея высоты камеры")]
    [SerializeField] float speed;
    [Tooltip("Начальный угол камеры")]
    [SerializeField] Vector3 cameraEulerAngles;
    [SerializeField] CityManager cityManager;

    void Start()
    {
        Vector3 vect = cityManager.GetFirstTilePos();
        var mainCamera = Camera.main;
        mainCamera.transform.position = new Vector3(vect.x- ReadConfigure.TileWidht(), ReadConfigure.GetCity().widthTile , vect.z-ReadConfigure.TileWidht());
        mainCamera.transform.localEulerAngles = cameraEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        ScrolMouse();
    }

    void ScrolMouse()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, 0, scroll * speed, Space.Self);
    }
}
