using Microsoft.AspNetCore.Mvc;

namespace HomeApi.WebApplication.Controllers;

[Route("[controller]")]
[ApiController]
public class AudioController : ControllerBase
{
	private readonly Services.IInfraredService _infraredService;
	private readonly Services.ISmartPlugService _smartPlugService;

	public AudioController(Services.IInfraredService infraredService!!, Services.ISmartPlugService smartPlugService!!)
	{
		_infraredService = infraredService;
		_smartPlugService = smartPlugService;
	}

	[HttpGet("louder")]
	public async Task<IActionResult> Louder()
	{
		await _infraredService.SendInfraredMessageAsync(Models.Devices.IRBlaster, Models.Infrareds.Amp_Louder);
		return Ok();
	}

	[HttpGet("mute")]
	public async Task<IActionResult> Mute()
	{
		await _infraredService.SendInfraredMessageAsync(Models.Devices.IRBlaster, Models.Infrareds.Amp_Mute);
		return Ok();
	}

	[HttpGet("quieter")]
	public async Task<IActionResult> Quieter()
	{
		await _infraredService.SendInfraredMessageAsync(Models.Devices.IRBlaster, Models.Infrareds.Amp_Quieter);
		return Ok();
	}

	[HttpGet("off")]
	public async Task<IActionResult> Off()
	{
		(_, _, var watts) = await _smartPlugService.GetPowerDrawAsync(Models.Devices.AmpSmartPlug);
		if (watts > 3)
		{
			await _infraredService.SendInfraredMessageAsync(Models.Devices.IRBlaster, Models.Infrareds.Amp_Power);
		}
		return Ok();
	}

	[HttpGet("on")]
	public async Task<IActionResult> On()
	{
		(_, _, var watts) = await _smartPlugService.GetPowerDrawAsync(Models.Devices.AmpSmartPlug);
		if (watts < 3)
		{
			await _infraredService.SendInfraredMessageAsync(Models.Devices.IRBlaster, Models.Infrareds.Amp_Power);
		}
		return Ok();
	}

	[HttpGet("on/pc")]
	public async Task<IActionResult> OnPc()
	{
		await _infraredService.SendInfraredMessageAsync(Models.Devices.IRBlaster, Models.Infrareds.SpdifSwitcher_2);
		return await On();
	}

	[HttpGet("on/xbox")]
	public async Task<IActionResult> OnXbox()
	{
		await _infraredService.SendInfraredMessageAsync(Models.Devices.IRBlaster, Models.Infrareds.SpdifSwitcher_1);
		return await On();
	}
}
