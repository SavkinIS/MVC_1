using TMPro;
using UnityEngine;

public class CityView : MonoBehaviour
{
    [Tooltip("Вывод общей мощи города")]
    [SerializeField] TMP_Text cityPower;
    [SerializeField] BuildingView buildingView;
    [Tooltip("Менеджер города")]
    [SerializeField] CityManager cityManager;
    public CityManager GetCityManager => cityManager;
    /// <summary>
    /// is it in construction mode
    /// </summary>
    bool isBuilding;
    public BuildingView BuildingView { get => buildingView; set => buildingView = value; }
    public delegate void BeginBuilding(BuildingView buildingView);
    /// <summary>
    /// Informs about the beginning of construction
    /// </summary>
    public event BeginBuilding BuildingON;

    private void Start()
    {
        cityManager.EditorChangeOff += () => isBuilding = false;
        cityPower.text = $"Power: 0";
    }
    /// <summary>
    /// Initializes the building
    /// </summary>
    /// <returns></returns>
    BuildingView Build()
    {
       var build =  Instantiate(buildingView,transform);
        build.transform.position = new Vector3(build.transform.position.x, buildingView.transform.localScale.y / 2, build.transform.position.x);
        return build;
    }
    /// <summary>
    /// Start of construction
    /// </summary>
    public void MakeBuilding()
    {
        if (!isBuilding)
        {
            BuildingON?.Invoke(Build());
            cityManager.EditOn();
            isBuilding = true;
        }       
        //cityManager.EditOn();
    }
    /// <summary>
    /// Sets the total capacity of the city
    /// </summary>
    /// <param name="power"></param>
    public void SetPower(int power)
    {
        cityPower.text = $"Power: {power}";
    }
}
