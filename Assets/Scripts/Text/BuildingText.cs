using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

/*

  Structs for building text entries, groups, and wrappers. 
  These are used to load and manage building-related text data for different scenarios in the game.

*/

[Serializable]
public struct BuildingTextEntry
{
  public string name;
  public string description;
  public string benefit1;
  public string benefit2;
}

[Serializable]
public struct BuildingTextGroup
{
  public BuildingTextEntry Farms;
  public BuildingTextEntry Barracks;
  public BuildingTextEntry Markets;
  public BuildingTextEntry Port;
  public BuildingTextEntry Prestige;
}

[Serializable]
public struct BuildingTextWrapper
{
  public BuildingTextGroup buildings;
}

/*

  Loads from a JSON file located in the scenario's Text folder, based on the selected language.
  Unpacks the JSON into a BuildingTextGroup for easy access to building text data.

*/

public static class BuildingText
{
  public static BuildingTextGroup Text { get; private set; }

  public static void Load()
  {
    string path = Path.Combine(ScenarioManager.Instance.TextPath, "BuildingText.json");
    if (!File.Exists(path)) { Debug.LogError("Missing BuildingText for scenario."); return; }
    string json = File.ReadAllText(path);
    LoadFromJson(json);
  }

  private static void LoadFromJson(string json)
  {
    Text = JsonUtility.FromJson<BuildingTextWrapper>(json).buildings;
    Debug.Log("Building text loaded for scenario: " + ScenarioManager.Instance.ScenarioID);
  }
}
