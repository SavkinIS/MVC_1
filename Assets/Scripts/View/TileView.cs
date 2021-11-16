using UnityEngine;

public class TileView : MonoBehaviour
{
    [Tooltip("материал при свободном тайле")]
    [SerializeField] Material materialNormal;
    [Tooltip("Материал при занятом тайле")]
    [SerializeField] Material materialOccupated;
    [Tooltip("Renderer текущего объекта")]
    [SerializeField] Renderer renderer;

    /// <summary>
    /// занят ли тайл
    /// </summary>
    bool isOccupated;


    /// <summary>
    /// Вернёт свободен ли тайл
    /// </summary>
    public bool GetOccupated => isOccupated;

    /// <summary>
    /// Установит состояние занятоти тайла и изменит на соответствующий цвет
    /// </summary>
    /// <param name="flag"></param>
    public void SetOccupated(bool flag)
    {
        isOccupated = flag;
        if (isOccupated)renderer.material = materialOccupated;
        else renderer.material = materialNormal;
    }    
}
