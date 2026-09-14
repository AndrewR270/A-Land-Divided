using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

/*

  Define a struct for a bonus text entry, accessible by id and containing
  name and two benefits. These fields are applied to BonusSet objects which
  are created by loading Bonus Data first.

*/

[Serializable]
public struct BonusTextEntry
{
  public string id;
  public string name;
  public string benefit1;
  public string benefit2;
}

[Serializable]
public struct BonusTextWrapper { public List<BonusTextEntry> bonuses; }

public static class BonusText
{
  // Call Load function for both Bonus Text Files
  public static void Load()
  {
    LoadFile("FactionBonusText.json");
    LoadFile("ProvinceBonusText.json");
    Debug.Log("Loaded Bonuses text.");
  }

  // Load files and pass data into JSON loading functions
  private static void LoadFile(string filename)
  {
    string path = Path.Combine(ScenarioManager.Instance.TextPath, filename);
    if (!File.Exists(path)) { Debug.LogError("Missing " + filename + "."); return; }
    string json = File.ReadAllText(path);
    LoadFromJson(json);
  }

  // Unpack text JSON into existing BonusSet objects
  private static void LoadFromJson(string json)
  {
    BonusTextWrapper wrapper = JsonUtility.FromJson<BonusTextWrapper>(json);
    foreach (var entry in wrapper.bonuses)
    {
      if (Bonuses.BonusSets.TryGetValue(entry.id, out BonusSet bonus))
      {
        bonus.name = entry.name;
        bonus.benefit1 = entry.benefit1;
        bonus.benefit2 = entry.benefit2;
      }
      else { Debug.LogWarning("Bonus text found for missing bonus ID: " + entry.id); }
    }
  }
}
