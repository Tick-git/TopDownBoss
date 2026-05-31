public class AttackDecision
{
    public float BaseWeight { get; }
    public BossAttack Type { get; }
    public float Modifier { get; set; }
    public float Weight => BaseWeight * Modifier;

    public AttackDecision(BossAttack type, float baseWeight)
    {
        Type = type;
        BaseWeight = baseWeight;
        Modifier = 1;
    }
}