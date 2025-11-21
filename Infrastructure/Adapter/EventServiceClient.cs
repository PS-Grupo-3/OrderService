using Application.Interfaces.Adapter;
using Application.Models.External.Request;
using System.Net.Http.Json;

namespace Infrastructure.Adapter
{
    public class EventServiceClient : IEventServiceClient
    {
        private readonly HttpClient _httpClient;

        public EventServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task MarkSeatsAsAvailableAsync(Guid eventId, IEnumerable<Guid> eventSeatIds, CancellationToken ct = default)
        {
            foreach (var seatId in eventSeatIds)
            {
                var request = new UpdateSeatStatusRequest { Available = true };
                var response = await _httpClient.PatchAsJsonAsync($"api/v1/Event/{eventId}/seats/{seatId}", request, ct);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task MarkSeatsAsUnavailableAsync(Guid eventId, IEnumerable<Guid> eventSeatIds, CancellationToken ct = default)
        {
            foreach (var seatId in eventSeatIds)
            {
                var request = new UpdateSeatStatusRequest { Available = false };
                var response = await _httpClient.PatchAsJsonAsync($"api/v1/Event/{eventId}/seats/{seatId}", request, ct);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task MarkSectorAsAvailableAsync(Guid eventSectorId, CancellationToken ct = default)
        {
            var response = await _httpClient.PatchAsJsonAsync($"api/v1/EventSector/{eventSectorId}/availability",true ,ct);
            response.EnsureSuccessStatusCode();
        }

        public async Task MarkSectorAsUnavailableAsync(Guid eventSectorId, CancellationToken ct = default)
        {
            var response = await _httpClient.PatchAsJsonAsync($"api/v1/EventSector/{eventSectorId}/availability", false, ct);
            response.EnsureSuccessStatusCode();
        }
    }
}
