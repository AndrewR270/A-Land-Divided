using UnityEngine;

public class Initializer : MonoBehaviour
{
  void Start()
  {
    SettingsManager.Load();

    ScenarioManager.Instance.LoadScenario("archaic_greece_650bc");

    // NEW: choose whether to load a save or start fresh
    if (SettingsManager.LoadLastSave)
      ScenarioManager.Instance.LoadSave("save_001.json");
    else
      ScenarioManager.Instance.LoadStartState();
  }

}