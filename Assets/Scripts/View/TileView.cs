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
    /// is tile Occupated
    /// </summary>
    bool isOccupated;


    /// <summary>
    /// Will return whether the tile is free
    /// </summary>
    public bool GetOccupated => isOccupated;

    /// <summary>
    /// Set the tile's busy state and change to the appropriate color
    /// </summary>
    /// <param name="flag"></param>
    public void SetOccupated(bool flag)
    {
        isOccupated = flag;
        if (isOccupated)renderer.material = materialOccupated;
        else renderer.material = materialNormal;
    }    
}
