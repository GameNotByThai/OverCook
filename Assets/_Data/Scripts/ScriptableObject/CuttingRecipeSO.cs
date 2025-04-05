using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipeSO", menuName = "SO/CuttingRecipeSO")]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO beforeCut;
    public KitchenObjectSO afterCut;
    public int cuttingDone;
}
