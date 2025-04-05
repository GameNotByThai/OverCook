using UnityEngine;

[CreateAssetMenu(fileName = "BurningRecipeSO", menuName = "SO/BurningRecipeSO")]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO beforeBurning;
    public KitchenObjectSO afterBurning;
    public float burningTime;
}
