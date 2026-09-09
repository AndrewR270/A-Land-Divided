using UnityEngine;
using System.Collections.Generic;


public class Faction
{
    public string Name;
    public string PeriodName;
    public string Pronunciation;

    public Color FactionColor;

    public int Money;
    public int VictoryPoints;

    public List<Province> Provinces = new List<Province>();
    public List<Army> Armies = new List<Army>();
    public List<Fleet> Fleets = new List<Fleet>();

    public DiplomacyState Diplomacy; // alliances, wars, peace

    public bool IsDestroyed;
}

