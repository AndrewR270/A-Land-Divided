using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

/*

  Define a struct for a bonus entry, accessible by id and containing all 10 fields.
  These fields correspond to applicable bonuses on a faction wide and province local
  scale, depending on the prefix, f_ or p_.

*/

[Serializable]
public struct BonusEntry
{
  public string id;
  public BonusSet bonus;
}

[Serializable]
public struct BonusWrapper { public List<BonusEntry> bonuses; }

public static class BonusRegistry
{
  public static Dictionary<string, BonusSet> Bonuses = new Dictionary<string, BonusSet>();

  // Call Load function for both Bonus Files
  public static void Load()
  {
    LoadBonusFile("FactionBonuses.json");
    LoadBonusFile("ProvinceBonuses.json");
    Debug.Log("Loaded all bonuses into unified BonusRegistry.");
  }

  // Load files and pass data into JSON loading functions
  private static void LoadBonusFile(string filename)
  {
    string path = Path.Combine(ScenarioManager.Instance.DataPath, filename);
    if (!File.Exists(path)) { Debug.LogError("Missing bonus file: " + filename); return; }
    string json = File.ReadAllText(path);
    LoadFromJson(json);
  }

  // Unpack text JSON into new BonusSet objects
  private static void LoadFromJson(string json)
  {
    BonusWrapper wrapper = JsonUtility.FromJson<BonusWrapper>(json);
    foreach (var entry in wrapper.bonuses) { Bonuses[entry.id] = entry.bonus; }
  }
}
