using UnityEngine;
using UnityEngine.UI;
public class PlateIconSingleUI : GameBehaviour
{
    [SerializeField] Image uiImage;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadImage();
    }

    private void LoadImage()
    {
        if (this.uiImage != null) return;

        this.uiImage = transform.Find("Icon").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadImage", gameObject);
    }

    public void SetIconImage(KitchenObject kitchenObject)
    {
        uiImage.sprite = kitchenObject.KitchenObjectSO.sprite;
    }

    public void SetKitchenObjParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
    }
}
