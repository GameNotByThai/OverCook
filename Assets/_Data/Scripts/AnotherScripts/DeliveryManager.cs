using System;
using System.Collections.Generic;
using UnityEngine;
public class DeliveryManager : GameBehaviour
{
    [SerializeField] private RecipeListSO recipeListSO;

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    private List<RecipeSO> waitingRecipeList;
    public List<RecipeSO> WaitingRecipeList => waitingRecipeList;

    [SerializeField] private float timer = 0;
    private float recipeSpawnTime = 4f;
    [SerializeField] private int maxRecipe = 4;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRecipeListSO();
    }

    protected override void Awake()
    {
        base.Awake();
        this.waitingRecipeList = new List<RecipeSO>();
    }

    private void LoadRecipeListSO()
    {
        if (recipeListSO != null) return;

        string path = "ScriptableObject/RecipeListSO/RecipeListSO";
        recipeListSO = Resources.Load<RecipeListSO>(path);
        Debug.LogWarning(transform.name + ": LoadRecipeListSO", gameObject);
    }

    private void Update()
    {
        if (waitingRecipeList.Count >= maxRecipe) return;

        timer += Time.deltaTime;
        if (timer < recipeSpawnTime) return;
        timer = 0;

        this.AddRandomRecipeToWaitingList();
    }

    private void AddRandomRecipeToWaitingList()
    {
        RecipeSO waitingRecipeSO = recipeListSO.recipeList[UnityEngine.Random.Range(0, recipeListSO.recipeList.Count)];
        Debug.Log(waitingRecipeSO.recipeName);
        waitingRecipeList.Add(waitingRecipeSO);
        this.OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plate)
    {

        for (int i = 0; i < waitingRecipeList.Count; i++)
        {
            RecipeSO recipe = waitingRecipeList[i];
            if (recipe.kitchenObjectSOList.Count != plate.KitchenObjectList.Count) continue;

            bool isCorrectRecipe = true;
            foreach (KitchenObjectSO kitchenObject in recipe.kitchenObjectSOList)
            {
                bool ingredianFound = false;
                foreach (KitchenObject plateKitchenObject in plate.KitchenObjectList)
                {
                    if (plateKitchenObject.KitchenObjectSO.objName != kitchenObject.objName) continue;

                    ingredianFound = true;
                    break;
                }

                if (!ingredianFound)
                {
                    isCorrectRecipe = false;
                }
            }

            if (isCorrectRecipe)
            {
                //Debug.Log("Correct!");
                waitingRecipeList.RemoveAt(i);
                this.OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                return;
            }
        }

        //Debug.Log("Incorrect!");
    }
}
