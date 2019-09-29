using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct PlayerStats : IComponentData
{
    public float health;
    public float mana;    
    public float strength;
    public float dexterity;
}
