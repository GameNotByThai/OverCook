using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : GameBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Start()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRecipeNameText();
        this.LoadIconContainer();
        this.LoadIconTemplate();
    }

    private void LoadRecipeNameText()
    {
        if (this.recipeNameText != null) return;

        this.recipeNameText = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadRecipeNameText", gameObject);
    }

    private void LoadIconContainer()
    {
        if (this.iconContainer != null) return;

        this.iconContainer = transform.Find("IconContainer");
        Debug.LogWarning(transform.name + ": LoadIconContainer", gameObject);
    }

    private void LoadIconTemplate()
    {
        if (this.iconTemplate != null) return;

        this.iconTemplate = iconContainer.Find("IconTemplate");
        Debug.LogWarning(transform.name + ": LoadIconTemplate", gameObject);
    }

    public void SetRecipeNameText(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObject in recipeSO.kitchenObjectSOList)
        {
            Transform iconTranform = Instantiate(iconTemplate, iconContainer);
            iconTranform.gameObject.SetActive(true);
            iconTranform.GetComponent<Image>().sprite = kitchenObject.sprite;
        }
    }
}
