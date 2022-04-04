namespace HomeApi.Models;

[Flags]
public enum Devices : byte
{
	None = 0,
	AmpSmartPlug = 1,
	IRBlaster = 2,
}
