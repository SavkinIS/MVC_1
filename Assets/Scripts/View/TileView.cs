using UnityEngine;

public class TileView : MonoBehaviour
{
    [Tooltip("�������� ��� ��������� �����")]
    [SerializeField] Material materialNormal;
    [Tooltip("�������� ��� ������� �����")]
    [SerializeField] Material materialOccupated;
    [Tooltip("Renderer �������� �������")]
    [SerializeField] Renderer renderer;

    /// <summary>
    /// ����� �� ����
    /// </summary>
    bool isOccupated;


    /// <summary>
    /// ����� �������� �� ����
    /// </summary>
    public bool GetOccupated => isOccupated;

    /// <summary>
    /// ��������� ��������� �������� ����� � ������� �� ��������������� ����
    /// </summary>
    /// <param name="flag"></param>
    public void SetOccupated(bool flag)
    {
        isOccupated = flag;
        if (isOccupated)renderer.material = materialOccupated;
        else renderer.material = materialNormal;
    }    
}
