using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    [Serializable]
    public sealed class SaveLoadManager
    {
        private IGameRepository _gameRepository;
        private IEnumerable<ISaveLoader> _saveLoaders;

        [Inject]
        public void Construct(IGameRepository gameRepository, IEnumerable<ISaveLoader> saveLoaders)
        {
            _gameRepository = gameRepository;
            _saveLoaders = saveLoaders;
        }

        [Button]
        public void Save()
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.SaveGame();
            }
            _gameRepository.SaveState();
        }
        
        [Button]
        public void Load()
        {
            _gameRepository.LoadState();
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.LoadGame();
            }
        }
    }
}