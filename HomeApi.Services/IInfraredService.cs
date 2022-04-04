using HomeApi.Models;

namespace HomeApi.Services
{
	public interface IInfraredService
	{
		Task SendInfraredMessageAsync(Devices device, Infrareds infrared, CancellationToken? cancellationToken = null);
	}
}