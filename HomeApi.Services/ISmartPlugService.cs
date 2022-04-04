using HomeApi.Models;

namespace HomeApi.Services;

public interface ISmartPlugService
{
	Task<(double amps, double volts, double watts)> GetPowerDrawAsync(Devices device, CancellationToken? cancellationToken = default);
}
