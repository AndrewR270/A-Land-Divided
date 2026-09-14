using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

[Serializable]
public struct BonusEntry
{
  public string id;
  public Bonus bonus;
}

[Serializable]
public struct BonusWrapper
{
  public List<BonusEntry> bonuses;
}

public static class BonusRegistry
{
  public static Dictionary<string, Bonus> Bonuses = new Dictionary<string, Bonus>();

  public static void Load()
  {
    LoadBonusFile("ProvinceBonuses.json");
    LoadBonusFile("FactionBonuses.json");

    Debug.Log("Loaded all bonuses into unified BonusRegistry.");
  }

  private static void LoadBonusFile(string filename)
  {
    string path = Path.Combine(ScenarioManager.Instance.DataPath, filename);
    if (!File.Exists(path))
    {
      Debug.LogError("Missing bonus file: " + filename);
      return;
    }

    string json = File.ReadAllText(path);
    BonusWrapper wrapper = JsonUtility.FromJson<BonusWrapper>(json);

    foreach (var entry in wrapper.bonuses)
    {
      Bonuses[entry.id] = entry.bonus;
    }
  }
}
