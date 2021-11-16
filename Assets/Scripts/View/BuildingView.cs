using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [Tooltip("Renderer �������� �������")]
    [SerializeField] Renderer renderer;
    [Tooltip("�������� ������� � ���������� ���������")]
    [SerializeField] Material materialnNormal;
    [Tooltip("�������� ������� � ���������  ������������ ����������")]
    [SerializeField] Material materialError;
    [Tooltip("�������� ������� � ������ ���������, ����� ������ ��������")]
    [SerializeField] Material materialEditor;
    [Tooltip("BoxCollider �������� ��������")]
    [SerializeField] BoxCollider boxCollider;
    [Tooltip("����� ������ ���� ������")]
    [SerializeField] TMP_Text buildingPower;
    [Tooltip("��������� �������� � �������")]
    [SerializeField] MouseFolow mouseFolow;
    [SerializeField] Transform textPos;
    [SerializeField] int fontSize;

    int power;
    CityManager cityManager;
    /// <summary>
    /// ��������� �� ������
    /// </summary>
    bool isPlaced;
    /// <summary>
    /// ����� �������� �������� ������
    /// </summary>
    List<TileView> tiles = new List<TileView>();
    /// <summary>
    /// ���������� �������
    /// </summary>
    int squear;
    private Vector2 posGUI;

    public bool IsPlaced { get => isPlaced; }

    void Start()
    {

        cityManager = FindObjectOfType<CityManager>();
        boxCollider.size = new Vector3(1, 1, 1);
        tiles = new List<TileView>();
    }


    private void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(textPos.transform.position);
        posGUI = new Vector2(screenPosition.x - 60f, Screen.height - screenPosition.y - 10f);
    }

    /// <summary>
    /// ������������� ������ �������
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void SetScale�(float x, float y, float z)
    {
        transform.localScale = new Vector3(x, y, z);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TileView>(out TileView tile))
        {
            if (isPlaced) tile.SetOccupated(true);
            else AddToSquer(tile);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<TileView>(out TileView tile))
        {
            RemoveToSquer(tile);
        }
    }

    /// <summary>
    /// ���������� ������
    /// </summary>
    public void Placed()
    {
        if (Squear() == tiles.Count) TakePlaceOn?.Invoke();
    }

    /// <summary>
    /// �������� ���� � ������� �������
    /// </summary>
    /// <param name="tile"></param>
    public void AddToSquer(TileView tile)
    {
        if (!tile.GetOccupated)
        {
            tiles.Add(tile);
            EqualSquear();
        }

    }
    /// <summary>
    /// ������ ���� �� ������� �������
    /// </summary>
    /// <param name="tile"></param>

    public void RemoveToSquer(TileView tile)
    {
        tiles.Remove(tile);
        EqualSquear();
    }

    /// <summary>
    /// ���������� ����������� � ���������� �������
    /// </summary>
    private void EqualSquear()
    {
        if (Squear() == tiles.Count)
        {
            renderer.material = materialnNormal;
        }
        else
        {
            renderer.material = materialError;
        }
    }

    /// <summary>
    /// ���������� �������
    /// </summary>
    /// <returns></returns>
    int Squear()
    {
        return Mathf.FloorToInt((transform.localScale.x / ReadConfigure.TileWidht()) * (transform.localScale.z) / ReadConfigure.TileWidht());
    }


    /// <summary>
    /// ������� ��������� ����������
    /// </summary>
    /// <param name="value"></param>
    public void SetPlaced(bool value)
    {

        if (Squear() == tiles.Count && !isPlaced)
        {
            isPlaced = value;
            cityManager.EditorChangeOn += () => renderer.material = materialEditor;
            cityManager.EditorChangeOff += () => renderer.material = materialnNormal;
            cityManager.EditClose();
            mouseFolow.enabled = false;
            boxCollider.size = new Vector3(1.1f, 1, 1.1f);
        }

    }
    /// <summary>
    /// ���������� ������ � ������������ �� �������
    /// </summary>
    public void DestroyBuild()
    {
        cityManager.EditorChangeOn -= () => renderer.material = materialEditor;
        cityManager.EditorChangeOff -= () => renderer.material = materialnNormal;
        Destroy(this.gameObject);
    }

    /// <summary>
    /// �������������� �������� ������
    /// </summary>
    /// <param name="power"></param>
    public void SetPower(int power)
    {
        //buildingPower.text = power.ToString();
        this.power = power;
    }


    public delegate void TakePlace();
    /// <summary>
    /// �������� ��� ������ ������������
    /// </summary>
    public event TakePlace TakePlaceOn;


    void OnGUI()
    {

        Rect rect = new Rect(posGUI.x, posGUI.y , 120f, 120f);
        var style = GUI.skin.label;
        style.fontSize = fontSize;
        
        GUIStyle label = new GUIStyle(style);
        label.alignment = TextAnchor.UpperCenter;

        GUI.Label(rect, power.ToString(), label);
    }
}
