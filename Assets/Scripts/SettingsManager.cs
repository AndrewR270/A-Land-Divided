using System.IO;
using UnityEngine;

/*

  Default settings data structure for the game. 
  This is used to store user preferences and game settings.

*/

[System.Serializable]
public class SettingsData
{
  public string language = "en";

  public AudioSettings audio = new AudioSettings();
  public VideoSettings video = new VideoSettings();
  public GameplaySettings gameplay = new GameplaySettings();
}

[System.Serializable]
public class AudioSettings
{
  public float master_volume = 1.0f;
  public float music_volume = 0.8f;
  public float effects_volume = 0.9f;
}

[System.Serializable]
public class VideoSettings
{
  public string resolution = "1920x1080";
  public bool fullscreen = true;
  public bool vsync = true;
  public float ui_scale = 1.0f;
}

[System.Serializable]
public class GameplaySettings
{
  public int autosave_turns = 5;
}

[System.Serializable]
public class SettingsWrapper
{
  public SettingsData settings = new SettingsData();
}

/*

  A static class that handles loading, saving, and managing game settings.
  Called on game start by GameInitializer.cs to load settings and apply them to the game.

*/

public static class SettingsManager
{
  private static string SettingsPath => Path.Combine(Application.persistentDataPath, "settings.json");

  public static SettingsWrapper Current { get; private set; }

  public static event System.Action OnLanguageChanged;

  public static string Language => Current.settings.language;

  public static void Load()
  {
    if (File.Exists(SettingsPath))
    {
      string json = File.ReadAllText(SettingsPath);
      Current = JsonUtility.FromJson<SettingsWrapper>(json);
    }
    else
    {
      Current = new SettingsWrapper();
      Save();
    }
  }

  public static void Save()
  {
    string json = JsonUtility.ToJson(Current, true);
    File.WriteAllText(SettingsPath, json);
  }

  public static void SetLanguage(string lang)
  {
    Current.settings.language = lang;
    Save();
    OnLanguageChanged?.Invoke();
  }
}
