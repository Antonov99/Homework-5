using System;
using System.Collections.Generic;
using GameEngine;
using JetBrains.Annotations;
using SaveSystem;
using UnityEngine;
using Zenject;

namespace Domain
{
    [UsedImplicitly]
    public class ResourcesSaveLoader : ISaveLoader
    {
        private ResourceService _resourceService;
        private IGameRepository _repository;

        [Inject]
        public void Construct(ResourceService resourceService, IGameRepository repository)
        {
            _resourceService = resourceService;
            _repository = repository;
        }

        public void SaveGame()
        {
            ResourceData data = ConvertToData();
            _repository.SetData(data);
        }

        public void LoadGame()
        {
            if (_repository.TryGetData(out ResourceData data))
                ApplyData(data);
            else
                throw new Exception($"not loaded");
        }

        private ResourceData ConvertToData()
        {
            List<int> amount = new();
            List<string> id = new();

            foreach (var resource in _resourceService.GetResources())
            {
                amount.Add(resource.Amount);
                id.Add(resource.ID);
            }

            return new ResourceData
            {
                Amount = amount,
                ID = id
            };
        }

        private void ApplyData(ResourceData data)
        {
            List<Resource> resources = new(_resourceService.GetResources());
            for (int i = 0; i < resources.Count; i++)
            {
                Resource resource = resources[i];

                resource.Amount = data.Amount[i];
                resource.ID = data.ID[i];
            }
        }
    }
}