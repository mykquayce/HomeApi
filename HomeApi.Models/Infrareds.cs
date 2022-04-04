namespace HomeApi.Models;

[Flags]
public enum Infrareds : byte
{
	None = 0,
	Amp_Louder = 1,
	Amp_Mute = 2,
	Amp_Power = 4,
	Amp_Quieter = 8,
	SpdifSwitcher_1 = 16,
	SpdifSwitcher_2 = 32,
	SpdifSwitcher_3 = 64,
}
