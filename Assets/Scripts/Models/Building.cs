using System;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class Building 
    {
        /// <summary>
        /// назвагние здания
        /// </summary>
        public string nameBuilding;
        /// <summary>
        /// динна здания в тайлах
        /// </summary>
        public int lengthTile;
        /// <summary>
        /// Ширина здания в тайлах
        /// </summary>
        public int widthTile;
        /// <summary>
        /// Высота здания
        /// </summary>
        public int height;
        /// <summary>
        /// мощь здания 
        /// </summary>
        public int power;
        public int SquadTile => lengthTile * widthTile;

        /// <summary>
        /// назвагние здания
        /// </summary>
        public string NameBuilding { get => nameBuilding; set => nameBuilding = value; }

        /// <summary>
        /// динна здания в единацах измерения
        /// </summary>
        public int LengthTile { get => lengthTile * ReadConfigure.TileWidht(); set => lengthTile = value; }
        /// <summary>
        /// Ширина здания в единацах измерения
        /// </summary>
        public int WidthTile { get => widthTile * ReadConfigure.TileWidht(); set => widthTile = value; }
        /// <summary>
        /// Высота здания
        /// </summary>
        public int Height { get => height; set => height = value; }
        /// <summary>
        /// мощь здания 
        /// </summary>
        public int Power { get => power; set => power = value; }
        /// <summary>
        /// Установлино ли дания
        /// </summary>
        public bool IsPlaced { get; set ; }
        /// <summary>
        /// Изменяет состоянее размещения здания
        /// </summary>
        internal void Placed()
        {
            IsPlaced = true;
            PlacedChanged?.Invoke(IsPlaced);
        }

        /// <summary>
        /// Сообщает что состоянее размещения здания изменено
        /// </summary>
        public event Action<bool> PlacedChanged;


    }
}
