namespace Game.Core.Saving
{
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);

        bool ShouldBeSaved();
    }
}