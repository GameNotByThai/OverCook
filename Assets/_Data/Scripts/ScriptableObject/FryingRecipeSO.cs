using UnityEngine;

[CreateAssetMenu(fileName = "FryingRecipeSO", menuName = "SO/FryingRecipeSO")]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO beforeFrying;
    public KitchenObjectSO afterFrying;
    public float fryingTime;
}
