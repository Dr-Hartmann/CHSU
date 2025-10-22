namespace CNandTMikuLab6.Models;

public record Network(
    string Name,
    string CableType,
    double LeftBase_bt,
    double MidBase_bt,
    double RightBase_bt,
    double PropDelayPerMeter_bt,
    double MaxLength_m,
    double? Leading,
    double? Intermediate)
{
    bool IEquatable<Network>.Equals(Network? obj)
    {
        return Name == obj?.Name
            && CableType == obj?.CableType
            && LeftBase_bt == obj?.LeftBase_bt
            && MidBase_bt == obj?.MidBase_bt
            && RightBase_bt == obj?.RightBase_bt
            && PropDelayPerMeter_bt == obj?.PropDelayPerMeter_bt
            && MaxLength_m == obj?.MaxLength_m
            && Leading == obj?.Leading
            && Intermediate == obj?.Intermediate;
    }

    public override string ToString() => Name;
}
