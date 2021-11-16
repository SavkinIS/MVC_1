using Assets.Scripts.Models;
using System;
using System.Collections.Generic;

[Serializable]
public class CityModel
{
    /// <summary>
    /// ������ � ������
    /// </summary>
    public int widthTile;
    /// <summary>
    /// ������ � ������
    /// </summary>
    public int lengthTile;

    /// <summary>
    /// ���� ������
    /// </summary>
    public Building[] buildingsTypes;
    /// <summary>
    /// ����
    /// </summary>
    public TileModel tile;

    /// <summary>
    /// ������ ����������� ������
    /// </summary>
    List<Building> buildings = new List<Building>();

    /// <summary>
    /// ������� ����
    /// </summary>
    int power = 0; 

    /// <summary>
    /// ������ � �������� ���������
    /// </summary>
    public int Width => widthTile * tile.width;
    /// <summary>
    /// ������ � �������� ���������
    /// </summary>
    public int Length => lengthTile * tile.width;

    /// <summary>
    /// ������� �������� ������� ����������� ������
    /// </summary>
    public List<Building> GetBuildings { get => buildings; }

    public event Action<Building> BuildingStart;


    /// <summary>
    /// ������ ��������� ��� ������
    /// </summary>
    /// <returns></returns>
    public Building GetBuilding()
    {
        var rnd = new Random();
        return buildingsTypes[rnd.Next(buildingsTypes.Length)];
    }

    /// <summary>
    /// �������� �� ���������� ����
    /// </summary>
    public event Action<int> AddPower;


    /// <summary>
    /// ��������� ������ 
    /// </summary>
    /// <param name="building"></param>
    public void AddBuilding(Building building)
    {
        buildings.Add(building);
        power += building.power;
        AddPower?.Invoke(power);
    }
}
