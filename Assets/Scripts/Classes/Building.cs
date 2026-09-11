using UnityEngine;

/*

	Accepted building types in the game, with unique benefits and upgrade paths.
	An abstract class is used to define the common properties and methods for all building types.

*/

public enum BuildingID
{
	Farms,
	Barracks,
	Markets,
	Port,
	Special
}

public abstract class Building
{
	public abstract BuildingID ID { get; }

	protected int TIER = 0;
	public int Tier => TIER;
	private const int MIN_TIER = 0;
	private const int MAX_TIER = 6;

	public void setTier(int tier) { TIER = Mathf.Clamp(tier, MIN_TIER, MAX_TIER); }

	public bool hasNextTier() { return TIER < MAX_TIER; }

	private static readonly int[] UpgradeCosts = { 200, 400, 900, 1600, 2500, 3600, 0 };
	private static readonly int[] UpgradeTimes = { 2, 4, 6, 8, 10, 12, 0 };

	public int UpgradeCost => UpgradeCosts[TIER];
	public int UpgradeTime => UpgradeTimes[TIER];

	public BuildingTextEntry Text {
		get {
			var text = BuildingText.Text;
			return ID switch {
				BuildingID.Farms => text.Farms,
				BuildingID.Barracks => text.Barracks,
				BuildingID.Markets => text.Markets,
				BuildingID.Port => text.Port,
				BuildingID.Special => text.Special,
				_ => default
			};
		}
	}

	public string Name => Text.name;
	public string Description => Text.description;
}

/*

	Farm Buildings:
	Surplus, Growth.

*/

public struct FarmTier {
	public int Surplus;
	public float Growth;

	public FarmTier(int surplus, float growth) {
		Surplus = surplus;
		Growth = growth;
	}
}

public class Farms : Building {
	public override BuildingID ID => BuildingID.Farms;

	public Farms(int tier) { setTier(tier); }

	public static readonly FarmTier[] tiers = {
		new FarmTier(0, 0.0f),
		new FarmTier(1, 0.6f),
		new FarmTier(2, 0.5f),
		new FarmTier(3, 0.4f),
		new FarmTier(4, 0.4f),
		new FarmTier(6, 0.3f),
		new FarmTier(8, 0.3f),
		new FarmTier(0, 0.0f)
	};

	public int surplus => tiers[TIER].Surplus;
	public float growth => tiers[TIER].Growth;

	public int next_surplus => tiers[TIER + 1].Surplus;
	public float next_growth => tiers[TIER + 1].Growth;

	public string benefits() {
		return $"<b>+{tiers[TIER].Surplus}</b>" + Text.benefit1 + $"\n<b>+{tiers[TIER].Growth}</b>" + Text.benefit2;
	}
}

/*

	Barracks Buildings:
	Replenish Time, Exp Gain.

*/

public struct BarracksTier {
	public int ReplenishTime;
	public float ExpGain;

	public BarracksTier(int replenishTime, float expGain) {
		ReplenishTime = replenishTime;
		ExpGain = expGain;
	}
}

public class Barracks : Building {
	public override BuildingID ID => BuildingID.Barracks;

	public Barracks(int tier) { setTier(tier); }

	public static readonly BarracksTier[] tiers = {
		new BarracksTier(10, 0.00f),
		new BarracksTier(9, 0.05f),
		new BarracksTier(8, 0.10f),
		new BarracksTier(7, 0.15f),
		new BarracksTier(6, 0.20f),
		new BarracksTier(5, 0.25f),
		new BarracksTier(4, 0.30f),
		new BarracksTier(10, 0.00f)
	};

	public int replenish_time => tiers[TIER].ReplenishTime;
	public float exp_gain => tiers[TIER].ExpGain;

	public int next_replenish_time => tiers[TIER + 1].ReplenishTime;
	public float next_exp_gain => tiers[TIER + 1].ExpGain;

	public string benefits() {
		return $"<b>{tiers[TIER].ReplenishTime}</b>" + Text.benefit1 + $"\n<b>+{tiers[TIER].ExpGain * 100}</b>" + Text.benefit2;
	}
}

/*

	Markets Buildings:
	Trade Bonus, Tax Bonus.

*/

public struct MarketsTier {
	public float TradeBonus;
	public int TaxBonus;

	public MarketsTier(float tradeBonus, int taxBonus) {
		TradeBonus = tradeBonus;
		TaxBonus = taxBonus;
	}
}

public class Markets : Building {
	public override BuildingID ID => BuildingID.Markets;

	public Markets(int tier) { setTier(tier); }

	public static readonly MarketsTier[] tiers = {
		new MarketsTier(0.00f, 0),
		new MarketsTier(0.02f, 1),
		new MarketsTier(0.04f, 1),
		new MarketsTier(0.06f, 2),
		new MarketsTier(0.08f, 2),
		new MarketsTier(0.10f, 3),
		new MarketsTier(0.12f, 3),
		new MarketsTier(0.00f, 0)
	};

	public float trade_bonus => tiers[TIER].TradeBonus;
	public int tax_bonus => tiers[TIER].TaxBonus;

	public float next_trade_bonus => tiers[TIER + 1].TradeBonus;
	public int next_tax_bonus => tiers[TIER + 1].TaxBonus;

	public string benefits() {
		return $"<b>{tiers[TIER].TradeBonus * 100}</b>" + Text.benefit1 + $"\n<b>+{tiers[TIER].TaxBonus}</b>" + Text.benefit2;
	}
}

/*

	Port Buildings:
	Fleets Supported, Ship Cost.

*/

public struct PortTier {
	public int FleetsSupported;
	public int ShipCost;

	public PortTier(int fleetsSupported, int shipCost) {
		FleetsSupported = fleetsSupported;
		ShipCost = shipCost;
	}
}

public class Port : Building {
	public override BuildingID ID => BuildingID.Port;

	public Port(int tier) { setTier(tier); }

	public static readonly PortTier[] tiers = {
		new PortTier(0, 0),
		new PortTier(1, 200),
		new PortTier(2, 180),
		new PortTier(3, 160),
		new PortTier(4, 140),
		new PortTier(5, 120),
		new PortTier(6, 100),
		new PortTier(0, 0)
	};

	public int fleets_supported => tiers[TIER].FleetsSupported;
	public int ship_cost => tiers[TIER].ShipCost;

	public int next_fleets_supported => tiers[TIER + 1].FleetsSupported;
	public int next_ship_cost => tiers[TIER + 1].ShipCost;

	public string benefits() {
		return $"<b>{tiers[TIER].FleetsSupported}</b>" + Text.benefit1 + $"\n<b>{tiers[TIER].ShipCost}</b>" + Text.benefit2;
	}
}

/*

	Special Buildings:
	Victory Points, Stability Bonus.

*/

public struct SpecialTier {
	public int VictoryPoints;
	public float StabilityBonus;

	public SpecialTier(int victoryPoints, float stabilityBonus) {
		VictoryPoints = victoryPoints;
		StabilityBonus = stabilityBonus;
	}
}

public class Special : Building {
	public override BuildingID ID => BuildingID.Special;

	public Special(int tier) { setTier(tier); }

	public static readonly SpecialTier[] tiers = {
		new SpecialTier(0, 0.00f),
		new SpecialTier(1, 0.10f),
		new SpecialTier(2, 0.20f),
		new SpecialTier(3, 0.30f),
		new SpecialTier(4, 0.40f),
		new SpecialTier(5, 0.50f),
		new SpecialTier(6, 0.60f),
		new SpecialTier(0, 0.00f)
	};

	public int victory_points => tiers[TIER].VictoryPoints;
	public float stability_bonus => tiers[TIER].StabilityBonus;

	public int next_victory_points => tiers[TIER + 1].VictoryPoints;
	public float next_stability_bonus => tiers[TIER + 1].StabilityBonus;

	public string benefits() {
		return $"<b>+{tiers[TIER].VictoryPoints}</b>" + Text.benefit1 + $"\n<b>+{tiers[TIER].StabilityBonus}</b>" + Text.benefit2;
	}
}