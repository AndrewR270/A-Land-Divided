using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Awake()
    {
      SettingsManager.Load();
      Debug.Log("Language: " + SettingsManager.Language);

      // Load Text
      BuildingTextLoader.Load();
      if (BuildingTextLoader.TextByScenario.TryGetValue("archaic_greece_650bc", out var group))
      {
          Debug.Log("Farms Name: " + group.Farms.name);
          Debug.Log("Barracks Name: " + group.Barracks.name);
      }
    }
}