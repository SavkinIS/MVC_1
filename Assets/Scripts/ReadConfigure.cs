using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ReadConfigure
{
    static CityModel city;
    /// <summary>
    /// вернет ширину плитки
    /// </summary>
    /// <returns></returns>
    public static int TileWidht()
    {
        if (city != null)
        {
            return city.tile.width;
        }
        else
        {
            return ReadConfig().tile.width;
        }
    }

    /// <summary>
    /// название файла конфигурации
    /// </summary>
    const string gameConfig = "gameConfig.json";

    /// <summary>
    /// Вернет City
    /// </summary>
    /// <returns></returns>
    public static CityModel GetCity()
    {

        if (city != null)
        {
            return city;
        }
        else
        {
            return ReadConfig();
        }
    }


    /// <summary>
    /// Получает модель города из файла конфигурации 
    /// </summary>
    /// <returns>CityModel Модель города</returns>
    static CityModel ReadConfig()
    {
        string path = Application.dataPath + "/Settings/" + gameConfig;
        //print(path);

        ///Инициализирует файл с конфигурациями игры
        #region init
        
        //Building police = new Building()
        //{
        //    nameBuilding = "pilice",
        //    lengthTile = 2,
        //    widthTile = 2,
        //    height = 3,
        //    power = 4
        //};

        //Building school = new Building()
        //{
        //    nameBuilding = "school",
        //    lengthTile = 3,
        //    widthTile = 4,
        //    height = 2,
        //    power = 6
        //};

        //Building dormitory = new Building()
        //{
        //    nameBuilding = "dormitory",
        //    lengthTile = 5,
        //    widthTile = 2,
        //    height = 3,
        //    power = 2
        //};

        //Building[] buildings = new Building[] { police, school, dormitory };

        //CityModel city = new CityModel
        //{
        //    tile = new TileModel
        //    {
        //        width = 2
        //    },
        //    buildingsTypes = buildings,
        //    widthTile = 20, 
        //    lengthTile = 15

        //};
        //string json = JsonUtility.ToJson(city);
        //using (var sw = new StreamWriter(path))
        //{
        //    sw.Write(json);
        //}

        #endregion

        string jsonR = File.ReadAllText(path);
        city = JsonUtility.FromJson<CityModel>(jsonR);
        return city;
    }

}
