﻿using System.Collections.Generic;
using GameEngine;
using JetBrains.Annotations;
using SaveSystem;
using UnityEngine;
using Zenject;

namespace Domain
{
    [UsedImplicitly]
    public class UnitsSaveLoader : ISaveLoader
    {
        private UnitManager _unitManager;
        private IGameRepository _repository;

        [Inject]
        public void Construct(UnitManager unitManager, IGameRepository repository)
        {
            _unitManager = unitManager;
            _repository = repository;
        }

        public void SaveGame()
        {
            UnitData data = ConvertToData();
            _repository.SetData(data);
        }

        public void LoadGame()
        {
        }

        private UnitData ConvertToData()
        {
            List<string> type = new();
            List<int> hitPoints = new();
            List<Vector3> position = new();
            List<Vector3> rotation = new();

            foreach (var unit in _unitManager.GetAllUnits())
            {
                type.Add(unit.Type);
                hitPoints.Add(unit.HitPoints);
                position.Add(unit.Position);
                rotation.Add(unit.Rotation);
            }

            return new UnitData
            {
                Type = type,
                HitPoints = hitPoints,
                Position = position,
                Rotation = rotation
            };
        }
        
        private void ApplyData(UnitData data)
        {
            List<Unit> units = new(_unitManager.GetAllUnits());
            for (int i = 0; i < units.Count; i++)
            {
                Unit unit = units[i];

                unit.Type = data.Type[i];
                unit.HitPoints = data.HitPoints[i];
                unit.Rotation = data.Rotation[i];
                unit.Position = data.Position[i];
            }
        }
    }
}