using UnityEngine;
using System.Collections.Generic;
using System.IO;

/*

  Structs to define the shape of color entries.
  Each JSON entry has a color and corresponding ID.
    
*/

[System.Serializable]
public struct MapColorEntry
{
  public string color;
  public string id_province;
}

[System.Serializable]
public struct MapColorWrapper
{
  public MapColorEntry[] mappings;
}

/*

  Loads the color map for the current scenario and provides a mapping from color to province ID.
  Converts strings in the JSON file to Color32 objects for use as keys for province IDs.
    
*/

public static class MapColors
{
  public static Dictionary<Color32, string> Colors { get; private set; }

  public static void Load()
  {
    string path = Path.Combine(ScenarioManager.Instance.DataPath, "ColorMap.json");
    if (!File.Exists(path)) { Debug.LogError("Missing Color Map for scenario."); return; }
    string json = File.ReadAllText(path);
    LoadFromJson(json);
  }

  private static void LoadFromJson(string json)
  {
    MapColorWrapper wrapper = JsonUtility.FromJson<MapColorWrapper>(json);
    Colors = new Dictionary<Color32, string>();
    foreach (var entry in wrapper.mappings)
    {
      Color c; 
      ColorUtility.TryParseHtmlString(entry.color, out c);
      Colors[(Color32)c] = entry.id_province;
    }
    Debug.Log("Color map loaded for scenario: " + ScenarioManager.Instance.ScenarioID);
  }

}
