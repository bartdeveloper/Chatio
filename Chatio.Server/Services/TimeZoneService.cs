using Microsoft.JSInterop;

namespace Chatio.Services
{
    public class TimeZoneService
    {

        private readonly IJSRuntime jsRuntime;
        private TimeSpan? userOffset;

        public TimeZoneService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task<TimeSpan> GetLocalOffset()
        {
            if (userOffset is null)
            {
                int offsetInMinutes = await jsRuntime.InvokeAsync<int>("getTimezoneOffset");
                userOffset = TimeSpan.FromMinutes(-offsetInMinutes);
            }

            return userOffset.Value;
        }
    }
}
