using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

/*

  Data shape for each province's text information.
  A wrapper class is used to facilitate JSON deserialization.

*/

[Serializable]
public struct ProvinceTextEntry
{
  public string id_province;
  public string name;
  public string period_name;
  public string pronunciation;
  public string description;
}

[Serializable]
public struct ProvinceTextWrapper
{
  public ProvinceTextEntry[] provinces;
}

/*

  Loads from a JSON file located in the scenario's Text folder, based on the selected language.
  Unpacks the JSON into a dictionary for easy access to province text data.

*/

public static class ProvinceText
{
  public static Dictionary<string, ProvinceTextEntry> Text { get; private set; }

  public static void Load()
  {
    string path = Path.Combine(ScenarioManager.Instance.TextPath, "ProvinceText.json");
    if (!File.Exists(path)) { Debug.LogError("Missing ProvinceText for scenario."); return; }
    string json = File.ReadAllText(path);
    LoadFromJson(json);
  }

  private static void LoadFromJson(string json)
  {
    ProvinceTextWrapper wrapper = JsonUtility.FromJson<ProvinceTextWrapper>(json);
    Text = new Dictionary<string, ProvinceTextEntry>();
    foreach (var p in wrapper.provinces) { Text[p.id_province] = p; }
    Debug.Log("Province text loaded for scenario: " + ScenarioManager.Instance.ScenarioID);
  }
}
