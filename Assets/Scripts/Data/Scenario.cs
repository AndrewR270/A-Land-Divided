using System.IO;
using UnityEngine;

/*

  Reads JSON files (Start.json or Save.json), deserializes them into C# structures,
  and applies them to ScenarioManager.

*/

public static class ScenarioDataLoader
{
  // Root JSON Structures

  [System.Serializable]
  public class ScenarioRoot { public ScenarioData scenario; }

  [System.Serializable]
  public class ScenarioData
  {
    public string id_scenario;
    public int turn;

    public List<Faction> factions;
    public List<Province> provinces;
    public List<Contingent> contingents;
    public List<SeaRegion> sea_regions;
  }

  // Load File

  public static void LoadFile(string path)
  {
    if (!File.Exists(path)) { Debug.LogError("Missing Scenario Data."); return; }
    string json = File.ReadAllText(path);

    ScenarioRoot root;

    try { root = JsonUtility.FromJson<ScenarioRoot>(json); }
    catch { Debug.LogError($"Failed to parse JSON for {path}"); return; }
    if (root == null || root.scenario == null) { Debug.LogError($"Invalid JSON for {path}"); return; }

    // Apply runtime data to ScenarioManager
    ScenarioManager.Instance.ApplyLoadedData(
      root.scenario.id_scenario,
      root.scenario.turn,
      root.scenario.factions,
      root.scenario.provinces,
      root.scenario.contingents,
      root.scenario.sea_regions
    );

    ScenarioManager.Instance.LoadScenarioAssets();
  }
}
