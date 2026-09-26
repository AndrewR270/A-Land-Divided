using System.Collections.Generic;

public class DiplomacyState
{
    // ally, enemy, none
    public Dictionary<string, string> States = new Dictionary<string, string>();

    // war outcome tracking (optional, but useful)
    public Dictionary<string, int> BattlesWon = new Dictionary<string, int>();
    public Dictionary<string, int> BattlesLost = new Dictionary<string, int>();
    public Dictionary<string, int> ProvincesTaken = new Dictionary<string, int>();
    public Dictionary<string, int> ProvincesLost = new Dictionary<string, int>();
}

public static class DiplomacyManager
{
    public static void DeclareWar(Faction a, Faction b)
    {
        a.Diplomacy.States[b.ID] = "enemy";
        b.Diplomacy.States[a.ID] = "enemy";
    }

    public static void FormAlliance(Faction a, Faction b)
    {
        a.Diplomacy.States[b.ID] = "ally";
        b.Diplomacy.States[a.ID] = "ally";
    }

    public static void BreakAlliance(Faction a, Faction b)
    {
        a.Diplomacy.States[b.ID] = "none";
        b.Diplomacy.States[a.ID] = "none";
    }

    public static void OfferPeace(Faction a, Faction b)
    {
        a.Diplomacy.States[b.ID] = "none";
        b.Diplomacy.States[a.ID] = "none";
    }
}
