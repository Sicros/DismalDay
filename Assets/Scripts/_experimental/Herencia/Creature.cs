using UnityEngine;

public class Creature : Entity
{
    public void SendIntoBattle()
    {
        Debug.Log($" {entityName} was sent to battle");
    }

    // Override sobreescribe el método anterior en caso de que se llame desde esta clase.
    public override string GetName()
    {
        return "The creature " + entityName;
    }

    public override void Init()
    {
        base.Init();
    }
}
