using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
  public ScenarioData[] scenarios;
  private Dictionary<int, ScenarioData> scenarioLookup;

  public RawImage baseMap;
  public RawImage highlightLayer;
  public RawImage cityLayer;
  public RawImage labelLayer;

  public Transform background;

  private GameObject activeBackground;
  public ScenarioData activeScenario;

  void Awake()
  {
    scenarioLookup = new Dictionary<int, ScenarioData>();
    foreach (var s in scenarios)
      scenarioLookup[s.scenarioID] = s;
  }

  public void LoadScenario(int id)
  {
    if (!scenarioLookup.TryGetValue(id, out activeScenario))
    {
      Debug.LogError($"Scenario ID {id} not found!");
      return;
    }

    // Load map textures
    baseMap.texture = activeScenario.baseMapImage;
    highlightLayer.texture = activeScenario.highlightLayerImage;
    cityLayer.texture = activeScenario.cityLayerImage;
    labelLayer.texture = activeScenario.labelLayerImage;

    // Load background prefab
    if (activeBackground != null)
      Destroy(activeBackground);

    activeBackground = Instantiate(activeScenario.backgroundPrefab, background);

    BroadcastScenarioLoaded();
  }

  void BroadcastScenarioLoaded()
  {
    // MapNavigation, UI, game logic, etc.
    SendMessage("OnScenarioLoaded", activeScenario, SendMessageOptions.DontRequireReceiver);
  }
}
