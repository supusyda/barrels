using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBarrelCommand : ICommand
{
    // Start is called before the first frame update
    Vector3 spawnDir;
    List<Box2> spawnBox = new();
    public AddBarrelCommand(Vector3 spawnDir)
    {
        this.spawnDir = spawnDir;
    }
    public void Cancel()
    {
        GridEditManager.instance.DeSpawnMoreBarrel(spawnBox);
        Event.OnDoneSpawnBarrel.Invoke();

    }

    public void Execute()
    {
        spawnBox = GridEditManager.instance.SpawnMoreBarrel(spawnDir);
        Event.OnDoneSpawnBarrel.Invoke();


    }
}
