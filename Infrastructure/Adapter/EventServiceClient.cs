using Application.Interfaces.Adapter;
using Application.Models.External.Request;
using System.Net.Http.Json;
using Application.Models.Responses;

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
        
        public async Task ReleaseFreeSectorAsync(Guid sectorId, CancellationToken ct = default)
        {
            var response = await _httpClient.PatchAsync(
                $"api/v1/Event/{sectorId}/release",
                null,
                ct
            );

            response.EnsureSuccessStatusCode();
        }

        
        public async Task<EventSectorDto> GetSectorAsync(Guid eventId, Guid sectorId, CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync($"api/v1/Event/{eventId}/sectors/{sectorId}", ct);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EventSectorDto>(ct);
        }


    }



}
