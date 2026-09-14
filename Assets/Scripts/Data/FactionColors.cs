using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

[Serializable]
public struct FactionColorEntry
{
  public string id_faction;
  public string color;
}

[Serializable]
public struct FactionColorWrapper { public FactionColorEntry[] colors; }

public static class FactionColors
{
  public static Dictionary<string, Color32> Colors { get; private set; }

  public static void Load()
  {
    string path = Path.Combine(ScenarioManager.Instance.DataPath, "FactionColors.json");
    if (!File.Exists(path)) { Debug.LogError("Missing FactionColors.json."); return; }
    string json = File.ReadAllText(path);
    LoadFromJson(json);
  }

  private static void LoadFromJson(string json)
  {
    FactionColorWrapper wrapper = JsonUtility.FromJson<FactionColorWrapper>(json);
    Colors = new Dictionary<string, Color32>();
    foreach (var entry in wrapper.colors)
    {
      Color c;
      if (!ColorUtility.TryParseHtmlString(entry.color, out c)) 
      { 
        Debug.LogError("Invalid color for faction: " + entry.id_faction);
        continue;
      }
      Colors[entry.id_faction] = (Color32)c;
    }
    Debug.Log("Faction colors loaded for " + ScenarioManager.Instance.ScenarioID);
  }
}
