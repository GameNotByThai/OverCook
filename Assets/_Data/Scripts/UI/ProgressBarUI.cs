using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : GameBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private Transform hasProgressTransform;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressTransform.GetComponent<IHasProgress>();
        hasProgress.OnHasProgressChanged += HasProgress_OnHasProgressChanged;
        barImage.fillAmount = 0;
        this.Hide();
    }

    private void HasProgress_OnHasProgressChanged(object sender, IHasProgress.OnHasProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (barImage.fillAmount == 0 || barImage.fillAmount == 1)
        {
            this.Hide();
        }
        else
        {
            this.Show();
        }
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBarImage();
        this.LoadHasProgressTransform();
    }

    private void LoadBarImage()
    {
        if (this.barImage != null) return;

        this.barImage = transform.Find("Bar").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadBarImage", gameObject);
    }

    private void LoadHasProgressTransform()
    {
        if (this.hasProgressTransform != null) return;

        this.hasProgressTransform = transform.parent;
        Debug.LogWarning(transform.name + ": LoadHasProgressTransform", gameObject);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }


}
