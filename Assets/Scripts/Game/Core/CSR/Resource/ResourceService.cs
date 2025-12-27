using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Game.Core
{
	public interface IResourceService
	{
		//	Delegates
		delegate void OnResourceStackChangedDelegate(Guid playerId, Resource resource, uint previousAmount, uint newAmount);

		//	Events
		event OnResourceStackChangedDelegate OnResourceStackChanged;

		//	Methods
		void Register(Guid playerId);
		void Deregister(Guid playerId);
		bool TryGet(Guid playerId, Resource resource, out uint amount);
		bool TryAdd(Guid playerId, Resource resource, uint amount);
	}
	internal interface IResourceInternalService : IResourceService
	{
		//	Methods
		bool TryRemove(Guid playerId, Resource resource, uint amount);
	}
	internal class ResourceService : IResourceInternalService
	{
		//	Fields
		private readonly Dictionary<Guid, ResourceStacks> _playerResources = new();

		//	Events
		public event IResourceService.OnResourceStackChangedDelegate OnResourceStackChanged;

		//	Methods
		public void Register(Guid playerId) => _playerResources.TryAdd(playerId, new());
		public void Deregister(Guid playerId) => _playerResources.Remove(playerId);
		public bool TryGet(Guid playerId, Resource resource, out uint amount)
		{
			amount = 0;
			
			if (!TryGetResourceStacks(playerId, out ResourceStacks resourceStacks))
				return false;
			
			amount = resourceStacks[resource].Amount;

			return true;
		}

		public bool TryAdd(Guid playerId, Resource resource, uint amount)
		{
			if (!TryGetResourceStacks(playerId, out ResourceStacks resourceStacks))
				return false;

			Stack stack = resourceStacks[resource];
			uint previousAmount = stack.Amount;

			stack.Add(amount);

			OnResourceStackChanged?.Invoke(playerId, resource, previousAmount, stack.Amount);

			return true;
		}
		public bool TryRemove(Guid playerId, Resource resource, uint amount)
		{
			if (!TryGetResourceStacks(playerId, out ResourceStacks resourceStacks))
				return false;

			Stack stack = resourceStacks[resource];
			uint previousAmount = stack.Amount;

			if (!stack.Remove(amount))
				return false;

			OnResourceStackChanged?.Invoke(playerId, resource, previousAmount, stack.Amount);

			return true;
		}

		private bool TryGetResourceStacks(Guid playerId, out ResourceStacks resourceStacks)
		{
			if (_playerResources.TryGetValue(playerId, out resourceStacks))
				return true;
				
			Debug.LogWarning($"Could not find resource stacks for player with ID {playerId}");

			return false;
		}

	}
}