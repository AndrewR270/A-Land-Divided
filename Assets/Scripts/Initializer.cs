using UnityEngine;

public class Initializer : MonoBehaviour
{
    void Start()
    {
      SettingsManager.Load();
      ScenarioManager.Instance.LoadScenario("archaic_greece_650bc");
    }
}