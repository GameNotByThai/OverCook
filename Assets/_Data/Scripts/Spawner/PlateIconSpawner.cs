using UnityEngine;

public class PlateIconSpawner : Spawner
{
    private static PlateIconSpawner instance;
    public static PlateIconSpawner Instance => instance;

    private static string iconTemplateName = "IconTemplate";
    public static string IconTemplateName => iconTemplateName;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("Only 1 PlateIconSpawner allow exsit");
            return;
        }

        instance = this;
    }
}
