using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScenarioManager : MonoBehaviour
{
  public static ScenarioManager Instance { get; private set; }

  public ScenarioData[] scenarios;
  private Dictionary<string, ScenarioData> scenarioLookup;
  public ScenarioData activeScenario;

  public string ActiveScenarioID => activeScenario.scenarioID;

  public Texture2D ProvinceColorMap => activeScenario.colorMap;

  public RawImage baseMap;
  public RawImage highlightLayer;
  public RawImage cityLayer;
  public RawImage labelLayer;

  public Transform background;
  private GameObject activeBackground;

  void Awake()
  {
    Instance = this;
    scenarioLookup = new Dictionary<string, ScenarioData>();

    foreach (var s in scenarios)
      scenarioLookup[s.scenarioID] = s;
  }

  public void LoadScenario(string id)
  {
    if (!scenarioLookup.TryGetValue(id, out activeScenario))
    {
      Debug.LogError($"Scenario ID {id} not found!");
      return;
    }

    // Load map textures
    baseMap.texture = activeScenario.baseMapImage;
    cityLayer.texture = activeScenario.cityLayerImage;
    labelLayer.texture = activeScenario.labelLayerImage;

    if (activeBackground != null)
      Destroy(activeBackground);

    activeBackground = Instantiate(activeScenario.backgroundPrefab, background);

    ColorMap.Load(ActiveScenarioID);

    BroadcastScenarioLoaded();
  }

  public Province GetProvinceByID(string id)
  {

      /* Land provinces
      foreach (var p in activeScenario.Provinces)
          if (p.ProvinceID == id)
              return p;

      foreach (var s in activeScenario.SeaProvinces)
          if (s.ProvinceID == id)
              return s;
      */

      Debug.LogError("Province ID not found: " + id);
      return null;
  }

  void BroadcastScenarioLoaded()
  {
    SendMessage("OnScenarioLoaded", activeScenario, SendMessageOptions.DontRequireReceiver);
  }
}