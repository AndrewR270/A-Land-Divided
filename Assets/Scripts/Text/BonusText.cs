using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

[Serializable]
public struct BonusTextEntry
{
  public string id;
  public string name;
  public string benefit1;
  public string benefit2;
}

[Serializable]
public struct BonusTextWrapper
{
  public List<BonusTextEntry> bonuses;
}

public static class BonusTextLoader
{
  public static void Load()
  {
    LoadBonusTextFile("ProvinceBonusText.json");
    LoadBonusTextFile("FactionBonusText.json");

    Debug.Log("Loaded all bonus text into unified BonusRegistry.");
  }

  private static void LoadBonusTextFile(string filename)
  {
    string path = Path.Combine(ScenarioManager.Instance.TextPath, filename);
    if (!File.Exists(path))
    {
      Debug.LogError("Missing bonus text file: " + filename);
      return;
    }

    string json = File.ReadAllText(path);
    BonusTextWrapper wrapper = JsonUtility.FromJson<BonusTextWrapper>(json);

    foreach (var entry in wrapper.bonuses)
    {
      if (BonusRegistry.Bonuses.TryGetValue(entry.id, out Bonus bonus))
      {
        bonus.name = entry.name;
        bonus.benefit1 = entry.benefit1;
        bonus.benefit2 = entry.benefit2;
      }
      else
      {
        Debug.LogWarning("Bonus text found for missing bonus ID: " + entry.id);
      }
    }
  }
}
