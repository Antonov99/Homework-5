using System.Collections.Generic;
using Domain;
using GameEngine;
using SaveSystem;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private SaveLoadManager _saveLoadManager;
    [SerializeField] private ResourceService _resourceService;
    [SerializeField] private UnitManager _unitManager;
    
    
    public override void InstallBindings()
    {
        Container.Bind<SaveLoadManager>().FromMethod(InjectSaveLoadManager).AsSingle().NonLazy();
        Container.BindInterfacesTo<GameRepository>().AsCached();
        Container.Bind<AesEncryptor>().AsSingle().NonLazy();
        Container.Bind<ResourceService>().FromMethod(ServicesBinding).AsSingle().NonLazy();
        Container.Bind<UnitManager>().FromMethod(UnitManagerBinding).AsSingle().NonLazy();

        SaveLoadersBinding();
        Container.Bind<IEnumerable<Resource>>().FromMethod(ResourcesBinding).AsSingle().NonLazy();
        Container.Bind<IEnumerable<Unit>>().FromMethod(UnitsBinding).AsSingle().NonLazy();
    }
    
    private void SaveLoadersBinding()
    {
        Container.BindInterfacesTo<ResourcesSaveLoader>().AsSingle().NonLazy();
        Container.BindInterfacesTo<UnitsSaveLoader>().AsSingle().NonLazy();
    }
    
    private IEnumerable<Resource> ResourcesBinding()
    {
        IEnumerable<Resource> resources = FindObjectsOfType<Resource>();
        return resources;
    }
    
    private IEnumerable<Unit> UnitsBinding()
    {
        IEnumerable<Unit> units = FindObjectsOfType<Unit>();
        return units;
    }

    private UnitManager UnitManagerBinding()
    {
        Container.Inject(_unitManager);
        return _unitManager;
    }
    
    private ResourceService ServicesBinding()
    {
        Container.Inject(_resourceService);
        return _resourceService;
    }

    private SaveLoadManager InjectSaveLoadManager()
    {
        Container.Inject(_saveLoadManager);
        return _saveLoadManager;
    }
}