using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

/*

  Data shape for each faction's text information.
  A wrapper class is used to facilitate JSON deserialization.

*/

[Serializable]
public struct FactionTextEntry
{
  public string id_faction;
  public string name;
  public string period_name;
  public string pronunciation;
  public string description;
}

[Serializable]
public struct FactionTextWrapper { public FactionTextEntry[] factions; }

/*

  Loads from a JSON file located in the scenario's Text folder, based on the selected language.
  Unpacks the JSON into a dictionary for easy access to province text data.

*/

public static class FactionText
{
  public static Dictionary<string, FactionTextEntry> Text { get; private set; }

  public static void Load()
  {
    string path = Path.Combine(ScenarioManager.Instance.TextPath, "FactionText.json");
    if (!File.Exists(path)) { Debug.LogError("Missing FactionText.json."); return; }
    string json = File.ReadAllText(path);
    LoadFromJson(json);
  }

  private static void LoadFromJson(string json)
  {
    FactionTextWrapper wrapper = JsonUtility.FromJson<FactionTextWrapper>(json);
    Text = new Dictionary<string, FactionTextEntry>();
    foreach (var p in wrapper.factions) { Text[p.id_faction] = p; }
    Debug.Log("Faction text loaded for " + ScenarioManager.Instance.ScenarioID);
  }
}
