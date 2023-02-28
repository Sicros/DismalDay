using UnityEngine;

public class Pokemon : Creature
{
    public override string GetName()
    {
        return "The Pokemon " + base.GetName();
    }

    public string GetWeakness()
    {
        return default;
    }

    public override void Init()
    {
        base.Init();
    }
}
