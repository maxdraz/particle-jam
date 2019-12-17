using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class LevelUpSystem : ComponentSystem
{
    protected override void OnUpdate()  //runs on main thread, so we can use debug log
    {
        //foreach entity with specified component
        Entities.ForEach((ref PlayerStats playerstats) =>
        {
            playerstats.health += Time.deltaTime;
            
        });

    }
}
