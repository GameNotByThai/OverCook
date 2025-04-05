using UnityEngine;
public class DeliveryManagerUI : GameBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadContainer();
        this.LoadRecipeTemplate();
    }

    private void LoadContainer()
    {
        if (this.container != null) return;

        this.container = transform.Find("Container");
        Debug.LogWarning(transform.name + ": LoadContainer", gameObject);
    }

    private void LoadRecipeTemplate()
    {
        if (this.recipeTemplate != null) return;

        this.recipeTemplate = container.Find("RecipeTemplate");
        Debug.LogWarning(transform.name + ": LoadRecipeTemplate", gameObject);
    }

    protected override void Awake()
    {
        base.Awake();
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameCtrl.Instance.DeliveryManager.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        GameCtrl.Instance.DeliveryManager.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in GameCtrl.Instance.DeliveryManager.WaitingRecipeList)
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponentInChildren<DeliveryManagerSingleUI>().SetRecipeNameText(recipeSO);
        }
    }
}
