using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

/*
  Contains data structures and loader for building text descriptions 
  in different scenarios.
*/

// Attributes for building text
[Serializable]
public struct BuildingText
{
  public string name;
  public string description;
  public string benefit1;
  public string benefit2;
}

// Grouping of building texts for a scenario
[Serializable]
public struct BuildingTextGroup
{
  public BuildingText Farms;
  public BuildingText Barracks;
  public BuildingText Markets;
  public BuildingText Port;
  public BuildingText Victory;
}

// Entry for a specific scenario with ID
[Serializable]
public struct ScenarioTextEntry
{
  public string id;
  public BuildingTextGroup buildings;
}


// Wrapper for all scenario text entries
[Serializable]
public struct ScenarioTextWrapper
{
  public ScenarioTextEntry[] scenarios;
}

public static class BuildingTextLoader
{
  public static Dictionary<string, BuildingTextGroup> TextByScenario { get; private set; }

  public static void Load()
  {
    string lang = SettingsManager.Language;

    string path = Path.Combine(Application.dataPath, "Text", lang, "BuildingText.json");

    if (!File.Exists(path))
    {
      Debug.LogError("BuildingText.json not found for language: " + lang);
      return;
    }

    string json = File.ReadAllText(path);
    LoadFromJson(json);
  }

  private static void LoadFromJson(string json)
  {
    ScenarioTextWrapper wrapper = JsonUtility.FromJson<ScenarioTextWrapper>(json);

    TextByScenario = new Dictionary<string, BuildingTextGroup>();

    foreach (var entry in wrapper.scenarios)
      TextByScenario[entry.id] = entry.buildings;

    Debug.Log("Building text loaded for language: " + SettingsManager.Language);
  }
}
