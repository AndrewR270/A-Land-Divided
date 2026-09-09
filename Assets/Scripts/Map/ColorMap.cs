using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public struct ProvinceColorMapEntry
{
  public string color;
  public string id_province;
}

[System.Serializable]
public struct ProvinceColorMapWrapper
{
  public ProvinceColorMapEntry[] mappings;
}

public static class ColorMap
{
  public static Dictionary<Color32, string> ColorToProvinceID { get; private set; }

  public static void Load(string scenarioID)
  {
    string path = Path.Combine(Application.dataPath, "Scenarios", scenarioID, "ColorMap.json");

    if (!File.Exists(path))
    {
      Debug.LogError("ProvinceColorMap.json not found for scenario: " + scenarioID);
      return;
    }

    string json = File.ReadAllText(path);
    var wrapper = JsonUtility.FromJson<ProvinceColorMapWrapper>(json);

    ColorToProvinceID = new Dictionary<Color32, string>();

    foreach (var entry in wrapper.mappings)
    {
      Color32 c = HexToColor(entry.color);
      ColorToProvinceID[c] = entry.id_province;
    }
  }

  private static Color32 HexToColor(string hex)
  {
    Color c;
    ColorUtility.TryParseHtmlString(hex, out c);
    return (Color32)c;
  }
}
