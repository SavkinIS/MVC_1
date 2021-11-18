using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [Tooltip("Renderer текущего объекта")]
    [SerializeField] Renderer renderer;
    [Tooltip("Материал объекта в нормальном состоянии")]
    [SerializeField] Material materialnNormal;
    [Tooltip("Материал объекта в состоянии  невозможного размещения")]
    [SerializeField] Material materialError;
    [Tooltip("Материал объекта в режиме редактора, когда объект размещен")]
    [SerializeField] Material materialEditor;
    [Tooltip("BoxCollider текущего обхъекта")]
    [SerializeField] BoxCollider boxCollider;
    [Tooltip("Текст вывода мощи здания")]
    [SerializeField] TMP_Text buildingPower;
    [Tooltip("Компонент привязки к курсору")]
    [SerializeField] MouseFolow mouseFolow;

    int power;
    CityManager cityManager;
    /// <summary>
    /// Размещено ли здание
    /// </summary>
    bool isPlaced;
    /// <summary>
    /// tiles that the object occupies
    /// </summary>
    List<TileView> tiles = new List<TileView>();
    List<TileView> tilesAll = new List<TileView>();
    /// <summary>
    /// logical area
    /// </summary>
    int area;

    public bool IsPlaced { get => isPlaced; }

    void Start()
    {
        area = Area();
        cityManager = FindObjectOfType<CityManager>();
        boxCollider.size = new Vector3(1, 1, 1);
        tiles = new List<TileView>();
    }


    /// <summary>
    /// Sets the size of the object
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void SetScaleу(float x, float y, float z)
    {
        transform.localScale = new Vector3(x, y, z);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TileView>(out TileView tile))
        {
            if (isPlaced) tile.SetOccupated(true);
            else AddToArea(tile);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<TileView>(out TileView tile))
        {
            RemoveToArea(tile);
        }
    }

    /// <summary>
    /// Building placement
    /// </summary>
    public void Placed()
    {
        if (area == tiles.Count) TakePlaceOn?.Invoke();
    }

    /// <summary>
    /// Add a tile to the area of the object
    /// </summary>
    /// <param name="tile"></param>
    public void AddToArea(TileView tile)
    {
        if (!tile.GetOccupated)
        {
            tiles.Add(tile);
        }

        tilesAll.Add(tile);
        ComparesArea();
    }
    /// <summary>
    /// Remove tile from the area of the object
    /// </summary>
    /// <param name="tile"></param>

    public void RemoveToArea(TileView tile)
    {
        tiles.Remove(tile);
        tilesAll.Remove(tile);
        ComparesArea();
    }

    /// <summary>
    /// Compares the actual and logical area
    /// </summary>
    private void ComparesArea()
    {
        if (area == tiles.Count && tilesAll.Count == area)
        {
            renderer.material = materialnNormal;
        }
        else
        {
            renderer.material = materialError;
        }
    }

    /// <summary>
    /// logical area
    /// </summary>
    /// <returns></returns>
    int Area()
    {
        return Mathf.FloorToInt((transform.localScale.x / ReadConfigure.TileWidht()) * (transform.localScale.z) / ReadConfigure.TileWidht());
    }


    /// <summary>
    /// Will change the placement status
    /// </summary>
    /// <param name="value"></param>
    public void SetPlaced(bool value)
    {

        if (area == tiles.Count && !isPlaced && tilesAll.Count == area)
        {
            isPlaced = value;
            cityManager.EditorChangeOn += () => renderer.material = materialEditor;
            cityManager.EditorChangeOff += () => renderer.material = materialnNormal;
            cityManager.EditClose();
            mouseFolow.enabled = false;
            boxCollider.size = PercentVector();
        }

    }


    /// <summary>
    /// Returns a vector proportional to the occupied territory
    /// </summary>
    /// <returns></returns>
    Vector3 PercentVector()
    {
        Vector3 vector = new Vector3(1f + Percent(transform.localScale.x), 1, 1f + Percent(transform.localScale.z));
        return vector;
    }

    /// <summary>
    /// part of tile occupy
    /// </summary>
    /// <param name="vectorPoint"></param>
    /// <returns></returns>
    float Percent(float vectorPoint)
    {
        var i = (1 / (vectorPoint / ReadConfigure.TileWidht())) * 2;
        return i;
    }

    /// <summary>
    /// Destroys the object and unsubscribes from events
    /// </summary>
    public void DestroyBuild()
    {
        cityManager.EditorChangeOn -= () => renderer.material = materialEditor;
        cityManager.EditorChangeOff -= () => renderer.material = materialnNormal;
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Sets the capacity of the building
    /// </summary>
    /// <param name="power"></param>
    public void SetPower(int power)
    {
        buildingPower.text = power.ToString();
        this.power = power;
    }


    public delegate void TakePlace();
    /// <summary>
    /// Notifies that the building has been installed
    /// </summary>
    public event TakePlace TakePlaceOn;


   

}
