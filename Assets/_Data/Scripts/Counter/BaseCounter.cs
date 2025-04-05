using UnityEngine;

public abstract class BaseCounter : GameBehaviour
{
    [Header("Base Counter")]
    [SerializeField] private Transform selectedModel;
    public Transform SelectedModel => selectedModel;

    protected virtual void Start()
    {
        GameCtrl.Instance.PlayerCtrl.PlayerInteract.OnSelectedCounterChanged += PlayerInteract_OnSelectedCounterChanged;
    }

    private void PlayerInteract_OnSelectedCounterChanged(object sender, PlayerInteract.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == this)
        {
            this.ShowSelectedModel();
        }
        else
        {
            this.HideSelectedModel();
        }
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSelectedModel();
    }

    private void LoadSelectedModel()
    {
        if (this.selectedModel != null) return;

        this.selectedModel = transform.Find("Selected");
        this.selectedModel.gameObject.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadSelectedModel", gameObject);
    }

    public virtual void Interact(PlayerCtrl playerCtrl)
    {
        //For override
    }

    public virtual void InteractAlternate(PlayerCtrl playerCtrl)
    {
        //For override
    }

    private void ShowSelectedModel()
    {
        //Debug.Log("Is show", gameObject);
        this.selectedModel.gameObject.SetActive(true);
    }

    private void HideSelectedModel()
    {
        //Debug.Log("Is hide", gameObject);
        this.selectedModel.gameObject.SetActive(false);
    }

}
