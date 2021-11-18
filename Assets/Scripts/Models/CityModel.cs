using Assets.Scripts.Models;
using System;
using System.Collections.Generic;

[Serializable]
public class CityModel
{
    /// <summary>
    /// Width in tiles
    /// </summary>
    public int widthTile;
    /// <summary>
    /// length in tiles
    /// </summary>
    public int lengthTile;

    /// <summary>
    /// Types of buildings
    /// </summary>
    public Building[] buildingsTypes;
    /// <summary>
    /// Tile
    /// </summary>
    public TileModel tile;

    /// <summary>
    /// List of buildings constructed
    /// </summary>
    List<Building> buildings = new List<Building>();

    /// <summary>
    /// current power
    /// </summary>
    int power = 0;

    /// <summary>
    /// width in units of measurement
    /// </summary>
    public int Width => widthTile * tile.width;
    /// <summary>
    /// Length in units of measurement
    /// </summary>
    public int Length => lengthTile * tile.width;

    /// <summary>
    /// The property Transmits a list of constructed buildings
    /// </summary>
    public List<Building> GetBuildings { get => buildings; }

    public event Action<Building> BuildingStart;


    /// <summary>
    /// Returns a random building type
    /// </summary>
    /// <returns></returns>
    public Building GetBuilding()
    {
        var rnd = new Random();
        return buildingsTypes[rnd.Next(buildingsTypes.Length)];
    }

    /// <summary>
    /// Reports an increase in Power
    /// </summary>
    public event Action<int> AddPower;


    /// <summary>
    /// Adds a building
    /// </summary>
    /// <param name="building"></param>
    public void AddBuilding(Building building)
    {
        buildings.Add(building);
        power += building.power;
        AddPower?.Invoke(power);
    }
}
