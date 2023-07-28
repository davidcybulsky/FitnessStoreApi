using Api.Dtos;
using Api.Entities;
using Api.Exceptions;
using Api.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApiContext _db;
        private readonly IMapper _mapper;
        private readonly IHttpContextService _httpContextService;

        public SubscriptionService(ApiContext db,
                                   IMapper mapper,
                                   IHttpContextService httpContextService)
        {
            _db = db;
            _mapper = mapper;
            _httpContextService = httpContextService;
        }

        public async Task<SubscriptionDto> GetAsync()
        {
            var userId = _httpContextService.GetUserId ?? throw new UnauthorizedException("The user is unautorized");
            var subscription = await _db.Subscriptions.AsNoTracking().Include(s => s.Plan)
                .SingleOrDefaultAsync(s => s.UserId == userId) ?? throw new NotFoundException("Suscription was not found");
            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscription);
            return subscriptionDto;
        }

        public async Task<int> SubscribeAsync(int planId)
        {

            var userId = _httpContextService.GetUserId ?? throw new UnauthorizedException("The user is unautorized");
            var subscription = await _db.Subscriptions.SingleOrDefaultAsync(s => s.UserId == userId);
            if (subscription is not null)
            {
                throw new BadRequestException("Firstly cancel your current subscription");
            }
            Subscription newSubscription = new()
            {
                UserId = userId,
                PlanId = planId
            };
            _db.Subscriptions.Add(newSubscription);
            await _db.SaveChangesAsync();
            return newSubscription.Id;
        }

        public async Task UnsubscribeAsync(int id)
        {
            var userId = _httpContextService.GetUserId ?? throw new UnauthorizedException("The user is unautorized");
            var subscription = await _db.Subscriptions.SingleOrDefaultAsync(s => s.Id == id) ?? throw new NotFoundException("The subscription was not found");
            if (subscription.UserId != userId)
            {
                throw new ForbiddenException("Forbidden operation");
            }
            _db.Subscriptions.Remove(subscription);
            await _db.SaveChangesAsync();
        }
    }
}
