using System;
using System.Collections.Generic;
using UnityEngine;

/*
  Contains data structures and loader for building text descriptions 
  in different scenarios.
*/

// Attributes for building text
[Serializable]
public struct BuildingText 
{
  public string Name;
  public string Description;
  public string Benefit1;
  public string Benefit2;
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
  public int id;
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
  public static Dictionary<int, BuildingTextGroup> TextByScenario { get; private set; }

  public static void LoadFromJson(string json)
  {
    ScenarioTextWrapper wrapper = JsonUtility.FromJson<ScenarioTextWrapper>(json);

    TextByScenario = new Dictionary<int, BuildingTextGroup>();

    foreach (var entry in wrapper.scenarios)
      TextByScenario[entry.id] = entry.buildings;
  }
}
