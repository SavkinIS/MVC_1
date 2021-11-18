using System;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class Building 
    {
        /// <summary>
        /// building name
        /// </summary>
        public string nameBuilding;
        /// <summary>
        /// building length in tiles
        /// </summary>
        public int lengthTile;
        /// <summary>
        /// The width of the building in tiles
        /// </summary>
        public int widthTile;
        /// <summary>
        /// Building height
        /// </summary>
        public int height;
        /// <summary>
        /// the power of the building
        /// </summary>
        public int power;
        public int SquadTile => lengthTile * widthTile;

        /// <summary>
        ///  building name
        /// </summary>
        public string NameBuilding { get => nameBuilding; set => nameBuilding = value; }

        /// <summary>
        /// the length of the building in units of measurement
        /// </summary>
        public int LengthTile { get => lengthTile * ReadConfigure.TileWidht(); set => lengthTile = value; }
        /// <summary>
        /// The width of the building in units of measurement
        /// </summary>
        public int WidthTile { get => widthTile * ReadConfigure.TileWidht(); set => widthTile = value; }
        /// <summary>
        /// Building height
        /// </summary>
        public int Height { get => height; set => height = value; }
        /// <summary>
        /// the power of the building
        /// </summary>
        public int Power { get => power; set => power = value; }
        /// <summary>
        /// Are the buildings installed
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
        /// Notifies that the placement status of the building has been changed
        /// </summary>
        public event Action<bool> PlacedChanged;


    }
}
