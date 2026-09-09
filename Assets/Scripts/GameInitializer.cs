using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Awake()
    {
      SettingsManager.Load();
      Debug.Log("Language: " + SettingsManager.Language);

      // Load Text
      BuildingTextLoader.Load();
      ProvinceTextLoader.Load();
    }
}