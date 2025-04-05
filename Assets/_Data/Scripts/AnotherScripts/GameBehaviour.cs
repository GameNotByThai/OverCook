using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponent();
    }

    private void Reset()
    {
        this.LoadComponent();
    }

    protected virtual void LoadComponent()
    {
        //For override
    }
}
