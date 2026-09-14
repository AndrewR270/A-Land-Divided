public class Faction
{
    // Identifiers
    public string ID { get; private set; }
    public Color32 Color => FactionColors.Colors[ID];
    public bool IsAlive => Provinces.Count > 0;

    // Province Text
    public string Name => FactionText.Text[ID].name;
    public string PeriodName => FactionText.Text[ID].period_name;
    public string Pronunciation => FactionText.Text[ID].pronunciation;
    public string Description => FactionText.Text[ID].description;

    // Bonuses
    public Bonus Bonuses => BonusRegistry.Bonuses[ID];
    public string BonusName => BonusText.Text[ID].name;
    public string BonusBenefit1 => BonusText.Text[ID].benefit1;
    public string BonusBenefit2 => BonusText.Text[ID].benefit2;

    // Variable Data
    public int VictoryPoints;
    public int Money;

    // Holdings
    public List<Province> Provinces = new List<Province>();
    public List<Army> Armies = new List<Army>();
    public List<Fleet> Fleets = new List<Fleet>();

    // Diplomacy
    public DiplomacyState Diplomacy;

    public Faction(string id) { ID = id; }
}

