using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
    //Нельзя менять!
    [Serializable]
    public sealed class UnitManager
    {
        [SerializeField]
        private Transform container;

        [ShowInInspector, ReadOnly]
        private HashSet<Unit> sceneUnits = new();
        
        public void SetupUnits(IEnumerable<Unit> units)
        {
            sceneUnits = new HashSet<Unit>(units);
        }

        [Button]
        public Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation)
        {
            var unit = Object.Instantiate(prefab, position, rotation, container);
            sceneUnits.Add(unit);
            return unit;
        }

        [Button]
        public void DestroyUnit(Unit unit)
        {
            if (sceneUnits.Remove(unit))
            {
                Object.Destroy(unit.gameObject);
            }
        }

        public void DestroyUnits()
        {
            foreach (var unit in sceneUnits)
            {
                Object.Destroy(unit.gameObject);
            }
            
            sceneUnits.Clear();
        }

        public IEnumerable<Unit> GetAllUnits()
        {
            return sceneUnits;
        }
    }
}