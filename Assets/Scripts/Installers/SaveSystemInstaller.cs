using SaveSystem;
using UnityEngine;
using Zenject;

public class SaveSystemInstaller : MonoInstaller
{
    [SerializeField] private SaveLoadManager _saveLoadManager;
    
    public override void InstallBindings()
    {
        Container.Bind<SaveLoadManager>().FromMethod(InjectSaveLoadManager).AsSingle().NonLazy();
        Container.BindInterfacesTo<GameRepository>().AsCached();
        Container.Bind<AesEncryptor>().AsSingle().NonLazy();
    }

    private SaveLoadManager InjectSaveLoadManager()
    {
        Container.Inject(_saveLoadManager);
        return _saveLoadManager;
    }
}