using UnityEngine;

public class KitchenObjectSpawner : Spawner
{
    private static KitchenObjectSpawner instance;
    public static KitchenObjectSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("Only 1 KitchenObjectSpawner allow exsit");
            return;
        }

        instance = this;
    }
}
