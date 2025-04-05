using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlatesCounter : BaseCounter
{
    [Header("Plate Counter")]
    [SerializeField] private Transform topPoint;
    public Transform TopPoint => topPoint;

    [SerializeField] private List<PlateKitchenObject> plateList;

    [SerializeField] private PlateKitchenObject lastPlate;

    private string plateName = "Plate";
    [SerializeField] private int maxPlate = 3;
    private float plateDistance = 0.1f;
    private float timer = 0;
    [SerializeField] private float plateTimeSpawn = 5f;

    protected override void Start()
    {
        base.Start();
        plateList = new List<PlateKitchenObject>();
    }

    public override void Interact(PlayerCtrl playerCtrl)
    {
        //Player player = GameCtrl.Instance.PlayerCtrl.Player;
        if (this.HasObjInCounter())
        {
            if (playerCtrl.Player.HasObjInHand()) return;

            this.TakeKitchenObj(playerCtrl);

            if (plateList.Count <= 0) return;

            plateList.Remove(lastPlate);
            if (plateList.Count > 0)
            {
                lastPlate = plateList.Last();
            }
            else
            {
                lastPlate = null;
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < plateTimeSpawn) return;
        timer = 0;

        SpawnPlate();
    }

    private void SpawnPlate()
    {
        if (plateList.Count >= maxPlate) return;

        Transform newPlate = KitchenObjectSpawner.Instance.Spawn(plateName, this.TopPoint.position, Quaternion.identity);
        PlateKitchenObject plate = newPlate.GetComponent<PlateKitchenObject>();
        plateList.Add(plate);
        lastPlate = plate;

        plate.SetKitchenObjParent(this.TopPoint);
        plate.transform.localPosition += new Vector3(0, (plateList.Count - 1) * plateDistance, 0);
    }

    protected bool HasObjInCounter()
    {
        return topPoint.childCount > 0;
    }

    protected void TakeKitchenObj(PlayerCtrl playerCtrl)
    {
        Transform playerHoldPoint = playerCtrl.HoldObjPoint;

        lastPlate.SetKitchenObjParent(playerHoldPoint);
    }
}
