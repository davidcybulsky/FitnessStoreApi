﻿using Api.Dtos;

namespace Api.Interfaces
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDto> GetAsync();
        Task<int> SubscribeAsync(int planId);
        Task UpdateSubscriptionAsync(int subscriptionId, int planId);
        Task UnsubscribeAsync(int subscriptionId);
    }
}
