using System;

public interface IHasProgress
{
    public event EventHandler<OnHasProgressChangedEventArgs> OnHasProgressChanged;

    public class OnHasProgressChangedEventArgs
    {
        public float progressNormalized;
    }
}
