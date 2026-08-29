using UnityEngine;

public abstract class Building {
    public int Tier;
    public int UpgradeCost => 100 * Tier * Tier;
    public int UpgradeTime => 2 * Tier;

}

public struct FarmTierData {

    public int SurplusBonus;
    public float GrowthBonus;

    public FarmTierData(int SurplusBonus, float GrowthBonus) { 
        this.SurplusBonus = SurplusBonus; 
        this.GrowthBonus = GrowthBonus; 
    }
}

public class Farm : Building {
    public static readonly FarmTierData[] FarmData = [
        new FarmTierData(0, 0.0f),
        new FarmTierData(1, 0.6f),
        new FarmTierData(2, 0.5f),
        new FarmTierData(3, 0.4f),
        new FarmTierData(4, 0.4f),
        new FarmTierData(6, 0.3f),
        new FarmTierData(8, 0.3f)
    ];
    public int SurplusBonus => FarmData[Tier].SurplusBonus;
    public float GrowthBonus => FarmData[Tier].GrowthBonus;
}

public struct BarracksTierData {

    public int ReplenishmentTimeBonus;
    public float ExperienceGainBonus;

    public BarracksTierData(int ReplenishmentTimeBonus, float ExperienceGainBonus) { 
        this.ReplenishmentTimeBonus = ReplenishmentTimeBonus; 
        this.ExperienceGainBonus = ExperienceGainBonus; 
    }
}

public class Barracks : Building {
    public static readonly BarracksTierData[] BarracksData = [
        new BarracksTierData(0, 0.00f),
        new BarracksTierData(1, 0.05f),
        new BarracksTierData(2, 0.10f),
        new BarracksTierData(3, 0.15f),
        new BarracksTierData(4, 0.20f),
        new BarracksTierData(5, 0.25f),
        new BarracksTierData(6, 0.30f)
    ];
    public int ReplenishmentTimeBonus => BarracksData[Tier].ReplenishmentTimeBonus;
    public float ExperienceGainBonus => BarracksData[Tier].ExperienceGainBonus;
}

public struct MarketTierData {

    public float TradeIncomeBonus;
    public int TaxRevenueBonus;

    public MarketTierData(int TradeIncomeBonus, float TaxRevenueBonus) { 
        this.TradeIncomeBonus = TradeIncomeBonus; 
        this.TaxRevenueBonus = TaxRevenueBonus; 
    }
}

public class Market : Building {
    public static readonly MarketTierData[] MarketData = [
        new MarketTierData(0.00f, 0),
        new MarketTierData(0.02f, 1),
        new MarketTierData(0.04f, 1),
        new MarketTierData(0.06f, 2),
        new MarketTierData(0.08f, 2),
        new MarketTierData(0.10f, 3),
        new MarketTierData(0.12f, 3)
    ];
    public int TradeIncomeBonus => MarketData[Tier].TradeIncomeBonus;
    public float TaxRevenueBonus => MarketData[Tier].TaxRevenueBonus;
}

public struct PortTierData {

    public float FleetsSupported;
    public int ShipCost;

    public PortTierData(float FleetsSupported, int ShipCost) { 
        this.TradeIncomeBonus = FleetsSupported; 
        this.TaxRevenueBonus = ShipCost; 
    }
}

public class Port : Building {
    public static readonly PortTierData[] PortData = [
        new PortTierData(0, 0),
        new PortTierData(1, 200),
        new PortTierData(2, 180),
        new PortTierData(3, 160),
        new PortTierData(4, 140),
        new PortTierData(5, 120),
        new PortTierData(6, 100)
    ];
    public int FleetsSupported => PortData[Tier].FleetsSupported;
    public float ShipCost => PortData[Tier].ShipCost;
}