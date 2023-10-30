namespace Ak.DataWorkshop.Repositories;

public enum ThreeCharacterIsoCode
{
    /// <summary>
    /// United Kingdom
    /// </summary>
    Gbr,

    /// <summary>
    /// United States
    /// </summary>
    Usa,

    /// <summary>
    /// Germany
    /// </summary>
    Deu,
}

public record EnergyDataRecord
{
    public ThreeCharacterIsoCode IsoCode { get; set; } = ThreeCharacterIsoCode.Deu;

    public double RenewablesConsumption { get; set; }
}
