using Api.Dtos;
using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    [Route("subscription")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _service;

        public SubscriptionController(ISubscriptionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Endpoint odpowiada z otrzymywanie informacji o subscrypcji
        /// </summary>
        /// <returns>
        /// Model transferu danych zawierający informacje o subskrypcji
        /// </returns>

        [HttpGet]
        public async Task<ActionResult<SubscriptionDto>> Get()
        {
            var subscriptionDto = await _service.GetAsync();
            return StatusCode(200, subscriptionDto);
        }

        /// <summary>
        /// Endpoind odpowiada za tworzenie subskrypcji
        /// </summary>
        /// <param name="planId">Plan, który użytkownik chce subskrybować</param>
        /// <returns>
        /// Zwraca Id nowo utworzonej subskrypcji
        /// </returns>

        [HttpPost("{planId}")]
        public async Task<ActionResult<int>> Subscribe([FromRoute] int planId)
        {
            var subId = await _service.SubscribeAsync(planId);
            return StatusCode(201, subId);
        }

        /// <summary>
        /// Endpoint odpowiada za zmianę planu w subskrypcji
        /// </summary>
        /// <param name="subscriptionId">Id subskrypcji do zmiany</param>
        /// <param name="planId">Id planu do subskrypcji</param>
        /// <returns>
        /// Nic nie zwraca
        /// </returns>

        [HttpPut("{subscriptionId}")]
        public async Task<ActionResult> UpdatePlan([FromRoute] int subscriptionId, [FromQuery][Required] int planId)
        {
            await _service.UpdateSubscriptionAsync(subscriptionId, planId);
            return StatusCode(204);
        }


        /// <summary>
        /// Endpoint odpowiada za anulowanie subskrypcji
        /// </summary>
        /// <param name="subId">Id subskrypcji do anulowania</param>
        /// <returns>
        /// Nic nie zwraca
        /// </returns>

        [HttpDelete("{subId}")]
        public async Task<ActionResult> Unsubscribe([FromRoute] int subId)
        {
            await _service.UnsubscribeAsync(subId);
            return StatusCode(204);
        }
    }
}
