using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

[Serializable]
public struct AdjacencyEntry
{
  public string id_province;
  public List<string> adjacent;
}

[Serializable]
public struct AdjacencyWrapper { public List<AdjacencyEntry> adjacency; }

public static class Adjacency
{
  public static Dictionary<string, List<string>> Lookup { get; private set; }

  public static void Load()
  {
    string path = Path.Combine(ScenarioManager.Instance.DataPath, "Adjacency.json");
    if (!File.Exists(path)) { Debug.LogError("Missing Adjacency.json"); return; }
    string json = File.ReadAllText(path);
    LoadFromJson(json);
  }

  private static void LoadFromJson(string json)
  {
    AdjacencyWrapper wrapper = JsonUtility.FromJson<AdjacencyWrapper>(json);
    Lookup = new Dictionary<string, List<string>>();
    foreach (var entry in wrapper.adjacency) { Lookup[entry.id_province] = entry.adjacent; }
    Debug.Log("Adjacency loaded for " + ScenarioManager.Instance.ScenarioID);
  }
}
