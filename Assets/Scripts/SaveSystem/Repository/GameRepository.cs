using System.Collections.Generic;
using System;
using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    [UsedImplicitly]
    public sealed class GameRepository:IGameRepository
    {
        private Dictionary<Type, string> _storage=new();
        private const string FileName="Storage.json";
        private readonly string _filePath = Application.persistentDataPath + FileName;
        private AesEncryptor _aesEncryptor;
        
        [Inject]
        public void Construct(AesEncryptor aesEncryptor)
        {
            _aesEncryptor = aesEncryptor;
        }

        public void SetData<T>(T data)
        {
            Type dataType = typeof(T);
            string serializedData = JsonUtility.ToJson(data);
            _storage[dataType] = serializedData;
        }

        public T GetData<T>()
        {
            Type dataType = typeof(T);
            string serializedData = _storage[dataType];
            T deserializedData = JsonUtility.FromJson<T>(serializedData);

            return deserializedData;
        }

        public bool TryGetData<T>(out T data)
        {
            Type dataType = typeof(T);
            if (_storage.TryGetValue(dataType, out string serializedData))
            {
                data = JsonUtility.FromJson<T>(serializedData);
                return true;
            }
            data = default;
            return false;
        }

        public void SaveState()
        {
            string serializedData = JsonConvert.SerializeObject(_storage);
            string encryptedData = _aesEncryptor.Encrypt(serializedData);
            File.WriteAllText(_filePath,encryptedData);
        }

        public void LoadState()
        {
            if (!File.Exists(_filePath)) return;

            string encryptedStorage = File.ReadAllText(_filePath);
            string decryptedStorage = _aesEncryptor.Decrypt(encryptedStorage);
            _storage = JsonConvert.DeserializeObject<Dictionary<Type,string>>(decryptedStorage);
        }
    }
}