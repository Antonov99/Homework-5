namespace SaveSystem
{
    public interface IGameRepository
    {
        void SetData<T>(T data);
        T GetData<T>();
        bool TryGetData<T>(out T data);
        public void SaveState();
        public void LoadState();
    }
}