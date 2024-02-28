using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace GameEngine
{
    //Нельзя менять!
    [Serializable]
    public sealed class ResourceService
    {
        [ShowInInspector, ReadOnly]
        private Dictionary<string, Resource> sceneResources = new();

        public void SetResources(IEnumerable<Resource> resources)
        {
            sceneResources = resources.ToDictionary(it => it.ID);
        }

        public IEnumerable<Resource> GetResources()
        {
            return sceneResources.Values;
        }
    }
}