using TMPro;
using UnityEngine;

public class CityView : MonoBehaviour
{
    [Tooltip("����� ����� ���� ������")]
    [SerializeField] TMP_Text cityPower;
    [SerializeField] BuildingView buildingView;
    [Tooltip("�������� ������")]
    [SerializeField] CityManager cityManager;
    public CityManager GetCityManager => cityManager;
    /// <summary>
    /// ���������� �� � ������ �������������
    /// </summary>
    bool isBuilding;
    public BuildingView BuildingView { get => buildingView; set => buildingView = value; }
    public delegate void BeginBuilding(BuildingView buildingView);
    /// <summary>
    /// �������� �� ������ �������������
    /// </summary>
    public event BeginBuilding BuildingON;

    private void Start()
    {
        cityManager.EditorChangeOff += () => isBuilding = false;
        cityPower.text = $"Power: 0";
    }
    /// <summary>
    /// �������������� ������
    /// </summary>
    /// <returns></returns>
    BuildingView Build()
    {
       var build =  Instantiate(buildingView,transform);
        build.transform.position = new Vector3(build.transform.position.x, buildingView.transform.localScale.y / 2, build.transform.position.x);
        return build;
    }
    /// <summary>
    /// ������ �������������
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
    /// ������������� ����� �������� ������
    /// </summary>
    /// <param name="power"></param>
    public void SetPower(int power)
    {
        cityPower.text = $"Power: {power}";
    }
}
