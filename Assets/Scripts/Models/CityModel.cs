using Assets.Scripts.Models;
using System;
using System.Collections.Generic;

[Serializable]
public class CityModel
{
    /// <summary>
    /// Ширина в тайлах
    /// </summary>
    public int widthTile;
    /// <summary>
    /// длинна в тайлах
    /// </summary>
    public int lengthTile;

    /// <summary>
    /// Типы зданий
    /// </summary>
    public Building[] buildingsTypes;
    /// <summary>
    /// Тайл
    /// </summary>
    public TileModel tile;

    /// <summary>
    /// Список построенных зданий
    /// </summary>
    List<Building> buildings = new List<Building>();

    /// <summary>
    /// текущая мощь
    /// </summary>
    int power = 0; 

    /// <summary>
    /// ширина в единицах измерения
    /// </summary>
    public int Width => widthTile * tile.width;
    /// <summary>
    /// Длинна в единицах измерения
    /// </summary>
    public int Length => lengthTile * tile.width;

    /// <summary>
    /// Свойсво Передает спиисок построееных зданий
    /// </summary>
    public List<Building> GetBuildings { get => buildings; }

    public event Action<Building> BuildingStart;


    /// <summary>
    /// Вернет случайный тип здания
    /// </summary>
    /// <returns></returns>
    public Building GetBuilding()
    {
        var rnd = new Random();
        return buildingsTypes[rnd.Next(buildingsTypes.Length)];
    }

    /// <summary>
    /// Сообщает об увеличении Мощи
    /// </summary>
    public event Action<int> AddPower;


    /// <summary>
    /// Добавляет здание 
    /// </summary>
    /// <param name="building"></param>
    public void AddBuilding(Building building)
    {
        buildings.Add(building);
        power += building.power;
        AddPower?.Invoke(power);
    }
}
