using UnityEngine;
using System.IO;
using System;

/*

  Loads Scenario-specific text, which includes the name, victory point name, money,
  and description of the scenario - variables accessed in ScenarioManager.

*/

[Serializable]
public struct ScenarioText
{
  public string name;
  public string victory_points;
  public string money;
  public string description;
}

public static class ScenarioTextLoader
{
  public static void Load()
  {
    string path = Path.Combine(ScenarioManager.Instance.TextPath, "ScenarioText.json");
    if (!File.Exists(path)) { Debug.LogError("Missing ScenarioText.json"); return; }
    string json = File.ReadAllText(path);
    ScenarioManager.Instance.ScenarioText = JsonUtility.FromJson<ScenarioText>(json);
    Debug.Log("Scenario text loaded for " + ScenarioManager.Instance.ScenarioText.name);
  }
}
