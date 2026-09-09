using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

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

public static class ProvinceTextLoader
{
  public static Dictionary<string, ProvinceTextEntry> Provinces { get; private set; }

  public static void Load()
  {
    string lang = SettingsManager.Language;
    string scenarioId = ScenarioManager.Instance.ActiveScenarioID;

    string path = Path.Combine(
        Application.dataPath,
        "Text",
        lang,
        scenarioId,
        "ProvinceText.json"
    );

    if (!File.Exists(path))
    {
      Debug.LogError("ProvinceText.json not found for scenario: " + scenarioId);
      return;
    }

    string json = File.ReadAllText(path);
    LoadFromJson(json);
  }

  private static void LoadFromJson(string json)
  {
    ProvinceTextWrapper wrapper = JsonUtility.FromJson<ProvinceTextWrapper>(json);

    Provinces = new Dictionary<string, ProvinceTextEntry>();

    foreach (var p in wrapper.provinces)
      Provinces[p.id_province] = p;

    Debug.Log("Province text loaded for scenario: " + ScenarioManager.Instance.ActiveScenarioID);
  }
}
