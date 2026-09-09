using UnityEngine;

public class GameInitializer : MonoBehaviour
{
  void Awake()
  {
    SettingsManager.Load();
    Debug.Log(SettingsManager.Language);
  }
}
