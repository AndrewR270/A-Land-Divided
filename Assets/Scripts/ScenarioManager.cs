using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

/*

  A singleton class that manages the currently active scenario in the game.
  It handles loading scenario data and applying scenario specific assets.

*/

public class ScenarioManager : MonoBehaviour
{
  public static ScenarioManager Instance { get; private set; }

  // Save File Information

  public string SaveFileName { get; private set; }
  public string SaveDirectory => Path.Combine(Application.persistentDataPath, "Saves", ScenarioID);
  public string SavePath => Path.Combine(SaveDirectory, SaveFileName + ".json");

  // Scenario Assets

  public ScenarioAsset[] scenarioData;
  private Dictionary<string, ScenarioAsset> scenarios;
  public ScenarioAsset activeScenario;
  public static event System.Action<ScenarioAsset> ScenarioLoaded;

  // Runtime Game State

  public string ScenarioID { get; private set; }
  public int TurnNumber { get; private set; }

  public List<Faction> Factions { get; private set; }
  public List<Province> Provinces { get; private set; }
  public List<Contingent> Contingents { get; private set; }
  public List<SeaRegion> SeaRegions { get; private set; }

  public ScenarioText ScenarioText { get; private set; }

  // Scenario Asset Paths

  public string ScenarioPath => Path.Combine(Application.dataPath, "Scenarios", ScenarioID);
  public string DataPath => Path.Combine(ScenarioPath, "Data");
  public string TextPath => Path.Combine(ScenarioPath, "Text", SettingsManager.Language);

  // Visual Elements

  public RawImage MapBorders;
  public RawImage BaseMap;
  public RawImage HighlightLayer;
  public RawImage CityLayer;
  public RawImage LabelLayer;

  public Transform Background;
  private GameObject ActiveBackground;

  /*

    Initialization

  */

  void Awake()
  {
    Instance = this;
    scenarios = new Dictionary<string, ScenarioAsset>();
    foreach (var s in scenarioData) { scenarios[s.scenarioID] = s; }
  }

  // Apply Loaded JSON Data

  public void ApplyLoadedData(
    string scenarioID,
    int turn,
    List<Faction> factions,
    List<Province> provinces,
    List<Contingent> contingents,
    List<SeaRegion> seaRegions
  )
  {
    ScenarioID = scenarioID;
    TurnNumber = turn;

    Factions = factions;
    Provinces = provinces;
    Contingents = contingents;
    SeaRegions = seaRegions;
  }

  // Load Scenario Assets

  public void LoadScenarioAssets()
  {
    if (!scenarios.TryGetValue(ScenarioID, out activeScenario)) { 
      Debug.LogError($"Scenario asset not found: {ScenarioID}");
      return;
    }

    // Load adjacency, colors, bonuses

    Adjacency.Load();
    FactionColors.Load();
    ProvinceColors.Load();
    Bonuses.Load();

    // Load scenario text files

    ScenarioTextLoader.Load();
    FactionText.Load();
    ProvinceText.Load();
    BonusText.Load();
    BuildingText.Load();

    // Apply map textures

    MapBorders.texture = activeScenario.MapBorders;
    BaseMap.texture = activeScenario.BaseMap;
    CityLayer.texture = activeScenario.CityLayer;
    LabelLayer.texture = activeScenario.LabelLayer;

    MapBorders.SetNativeSize();
    BaseMap.SetNativeSize();
    HighlightLayer.SetNativeSize();
    CityLayer.SetNativeSize();
    LabelLayer.SetNativeSize();

    // Background

    if (ActiveBackground != null) { Destroy(ActiveBackground); }
    ActiveBackground = Instantiate(activeScenario.Background, Background);
    ScenarioLoaded?.Invoke(activeScenario);
  }

  // Save Game

  public void Save(string fileName)
  {
    SaveFileName = fileName;
    Directory.CreateDirectory(SaveDirectory);
    string path = SavePath;

    // TODO: serialize Factions, Provinces, Contingents, SeaRegions, TurnNumber
    // File.WriteAllText(path, json);
  }
}
