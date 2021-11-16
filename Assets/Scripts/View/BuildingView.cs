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
    [SerializeField] Transform textPos;
    [SerializeField] int fontSize;

    int power;
    CityManager cityManager;
    /// <summary>
    /// Размещено ли здание
    /// </summary>
    bool isPlaced;
    /// <summary>
    /// тайлы которыйе занимает объект
    /// </summary>
    List<TileView> tiles = new List<TileView>();
    /// <summary>
    /// логическая площадь
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
    /// Устанавливает размер объекта
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
    /// Размещение здания
    /// </summary>
    public void Placed()
    {
        if (Squear() == tiles.Count) TakePlaceOn?.Invoke();
    }

    /// <summary>
    /// Добавить тайл к площади объекта
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
    /// Убрать тайл из площади объекта
    /// </summary>
    /// <param name="tile"></param>

    public void RemoveToSquer(TileView tile)
    {
        tiles.Remove(tile);
        EqualSquear();
    }

    /// <summary>
    /// Сравнивает фактическую и логическую площать
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
    /// логическая площадь
    /// </summary>
    /// <returns></returns>
    int Squear()
    {
        return Mathf.FloorToInt((transform.localScale.x / ReadConfigure.TileWidht()) * (transform.localScale.z) / ReadConfigure.TileWidht());
    }


    /// <summary>
    /// Изменят состояние размещения
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
    /// Уничтожает объект и отписывается от событий
    /// </summary>
    public void DestroyBuild()
    {
        cityManager.EditorChangeOn -= () => renderer.material = materialEditor;
        cityManager.EditorChangeOff -= () => renderer.material = materialnNormal;
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Устанавливвает мощность здания
    /// </summary>
    /// <param name="power"></param>
    public void SetPower(int power)
    {
        //buildingPower.text = power.ToString();
        this.power = power;
    }


    public delegate void TakePlace();
    /// <summary>
    /// Сообщает что здание установленно
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
