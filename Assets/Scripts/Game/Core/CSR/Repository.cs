using System;
using System.Collections.Generic;

namespace Project.Game.Core
{
	internal class Repository<T> where T : class, IIdentifiable
	{
		//	Fields
		private readonly Dictionary<Guid, T> _entities = new();

		//	Methods
		internal T Get(Guid id) => _entities.GetValueOrDefault(id);
		internal bool Create(T entity) => _entities.TryAdd(entity.Id, entity);
		internal bool Update(T entity)
		{
			if (!_entities.ContainsKey(entity.Id))
				return false;
			
			_entities[entity.Id] = entity;
			return true;
		}
		internal T Delete(Guid id)
		{
			if (_entities.Remove(id, out T entity))
				return entity;
			
			return null;
		}
	}
}