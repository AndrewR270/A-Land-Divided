using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

[Serializable]
public struct BuildingText
{
  public string name;
  public string description;
  public string benefit1;
  public string benefit2;
}

[Serializable]
public struct BuildingTextGroup
{
  public BuildingText Farms;
  public BuildingText Barracks;
  public BuildingText Markets;
  public BuildingText Port;
  public BuildingText Victory;
}

[Serializable]
public struct BuildingTextWrapper
{
  public BuildingTextGroup buildings;
}

public static class BuildingTextLoader
{
  public static BuildingTextGroup ScenarioBuildings { get; private set; }

  public static void Load()
  {
    string lang = SettingsManager.Language;
    string scenarioId = ScenarioManager.Instance.ActiveScenarioID;

    string path = Path.Combine(
        Application.dataPath,
        "Scenarios",
        scenarioId,
        "Text",
        lang,
        "BuildingText.json"
    );

    if (!File.Exists(path))
    {
      Debug.LogError("BuildingText.json not found for scenario: " + scenarioId);
      return;
    }

    string json = File.ReadAllText(path);
    LoadFromJson(json);
  }

  private static void LoadFromJson(string json)
  {
    BuildingTextWrapper wrapper = JsonUtility.FromJson<BuildingTextWrapper>(json);
    BuildingTextLoader.ScenarioBuildings = wrapper.buildings;

    Debug.Log("Building text loaded for scenario: " + ScenarioManager.Instance.ActiveScenarioID);
  }
}
