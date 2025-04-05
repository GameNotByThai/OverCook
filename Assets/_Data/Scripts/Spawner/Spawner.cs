using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : GameBehaviour
{
    [SerializeField] private List<Transform> prefabs = new List<Transform>();
    [SerializeField] private List<Transform> pools = new List<Transform>();
    [SerializeField] private Transform holder;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPrefab();
        this.LoadHolder();
    }

    private void LoadPrefab()
    {
        if (this.prefabs.Count > 0) return;

        Transform prefabsObj = transform.Find("Prefabs");

        foreach (Transform prefab in prefabsObj)
        {
            this.prefabs.Add(prefab);
        }

        this.HidePrefabs();
        Debug.LogWarning(transform.name + ": LoadPrefab", gameObject);
    }

    private void HidePrefabs()
    {
        foreach (Transform prefab in prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    private void LoadHolder()
    {
        if (this.holder != null) return;

        this.holder = transform.Find("Holder");
        Debug.LogWarning(transform.name + ": LoadHolder", gameObject);
    }

    public Transform Spawn(string prefabName, Vector3 spawnPos, Quaternion spawnRot)
    {
        Transform newObject = GetPrefabByName(prefabName);
        if (newObject == null)
        {
            Debug.Log("Not found prefab!");
            return null;
        }

        return this.Spawn(newObject, spawnPos, spawnRot);
    }

    public Transform Spawn(Transform prefab, Vector3 spawnPos, Quaternion spawnRot)
    {
        Transform newObject = this.GetObjFromPool(prefab);
        newObject.SetPositionAndRotation(spawnPos, spawnRot);
        newObject.SetParent(this.holder);
        newObject.gameObject.SetActive(true);
        return newObject;
    }

    public void Despawn(Transform obj)
    {
        if (this.pools.Contains(obj)) return;

        this.pools.Add(obj);
        obj.gameObject.SetActive(false);
    }

    private Transform GetObjFromPool(Transform prefab)
    {
        foreach (Transform objPool in pools)
        {
            if (prefab.name == objPool.name)
            {
                this.pools.Remove(objPool);
                return objPool;
            }
        }

        Transform newObject = Instantiate(prefab);
        newObject.name = prefab.name;
        return newObject;
    }

    private Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in prefabs)
        {
            if (prefab.name == prefabName) return prefab;
        }

        return null;
    }

    public Transform GetHolderSpawner()
    {
        return this.holder;
    }
}
