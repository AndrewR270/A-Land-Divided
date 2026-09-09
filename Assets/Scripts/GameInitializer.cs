using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Start()
    {
      SettingsManager.Load();
      Debug.Log("Language: " + SettingsManager.Language);

      ScenarioManager.Instance.LoadScenario("archaic_greece_650bc");

      // Load Text
      BuildingTextLoader.Load();
      ProvinceTextLoader.Load();
    }
}