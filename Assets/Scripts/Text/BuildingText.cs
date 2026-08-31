using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BuildingText
{
  public string Name;
  public string Description;
  public string Benefit1;
  public string Benefit2;
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
public struct ScenarioTextEntry
{
  public int id;
  public BuildingTextGroup buildings;
}

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
