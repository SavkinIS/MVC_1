using Assets.Scripts.Controllers;
using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Мунеджер Города
/// </summary>
public class CityManager : MonoBehaviour
{
    [SerializeField] CityView cityView;
    [Tooltip("Плоскость территории города")]
    [SerializeField] Plane cityTerritory;
    [Tooltip("Плоскость окружения")]
    [SerializeField] Plane planeEnviroment;
    [Tooltip("Родидельский элемент всех тайлов")]
    [SerializeField] Transform tileParent;
    [SerializeField] TileView tileView;
    [Tooltip("Позиция тайлов")]
    [SerializeField] float yTilePos;
    [Tooltip("Коллайдей города")]
    [SerializeField] BoxCollider boxCollider;

    CityModel cityModel;
    CityController cityController;
    TileController[,] tilesControllers;

    void Start()
    {        
        cityModel = ReadConfigure.GetCity();
        cityController = new CityController(cityModel, cityView);
        cityTerritory.transform.localScale = new Vector3(cityModel.Length, cityTerritory.transform.localScale.y, cityModel.Width);
        planeEnviroment.transform.localScale = new Vector3(cityTerritory.transform.localScale.x * 5, planeEnviroment.transform.localScale.y, cityTerritory.transform.localScale.z * 5);
        tileView.transform.localScale = new Vector3(ReadConfigure.TileWidht(), tileView.transform.localScale.y, ReadConfigure.TileWidht());
        boxCollider.size = new Vector3(cityTerritory.transform.localScale.x * 5, boxCollider.size.y, cityTerritory.transform.localScale.z * 5); 
        CreateTiles();
        tileParent.gameObject.SetActive(false);
        EditorChangeOn += SetTileParentActive;
       
        EditorChangeOff += SetTileParentActive;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) EditClose();
        
    }


    public void EditOn()
    {
        EditorChangeOn?.Invoke();        
    }

    /// <summary>
    /// Создаём Tiles
    /// </summary>
    /// <returns></returns>
    TileController[,] CreateTiles()
    {
        var width = ReadConfigure.GetCity().widthTile;
        var length = ReadConfigure.GetCity().lengthTile;

        var tilewidth = ReadConfigure.GetCity().tile.width;
        tilesControllers = new TileController[length, width];
        float xPos = -(length*2 - tilewidth/2);
        float zPos = -(width*2 -tilewidth/2) ;
        var startPos = new Vector3(xPos, yTilePos, zPos);
        for (int i = 0; i < length; i++)
        {
            var list = new List<TileController>();
            for (int j = 0; j < width; j++)
            {
                var tile = Instantiate(tileView, tileParent);
                //tile.indexL = i;
                //tile.indexW = j;
                tile.transform.localPosition = startPos;
                //tileView.SetTilesView(NeighborsTiles(i, j));
                tilesControllers[i, j] = new TileController(tile,new TileModel());

                zPos += tileView.transform.localScale.z;
                startPos = new Vector3(startPos.x, yTilePos, zPos);
            }
            zPos = -(width*2 - tilewidth / 2);
            xPos += tileView.transform.localScale.x;
            startPos = new Vector3(xPos, yTilePos, zPos);
        }

        return tilesControllers;
    }

    /// <summary>
    /// Закрыть Режим строительства
    /// </summary>
    internal void EditClose()
    {
        EditorChangeOff?.Invoke();
    }

    /// <summary>
    /// Включаем/отключаем поле с тайлами
    /// </summary>
    public void SetTileParentActive()
    {
        if (tileParent.gameObject.active)
        {
            tileParent.gameObject.SetActive(false);
        }
        else
        {
            tileParent.gameObject.SetActive(true);
        }
       
    }


    /// <summary>
    /// Позиция первого тайла
    /// </summary>
    /// <returns></returns>
    public Vector3 GetFirstTilePos()
    {
        return tilesControllers[0, 0].GetTile.transform.position;
    }

    public delegate void EditorChange();
    /// <summary>
    /// Режим строительства включён
    /// </summary>
    public event EditorChange EditorChangeOn;
    /// <summary>
    /// Режит строительства выключен
    /// </summary>
    public event EditorChange EditorChangeOff;



}
