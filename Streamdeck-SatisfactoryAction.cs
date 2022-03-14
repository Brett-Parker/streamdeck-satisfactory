using StreamDeckLib;
using StreamDeckLib.Messages;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;

namespace streamdecksatisfactory
{
  [ActionUuid(Uuid= "envy.satisfactory.buildings.miner")]
  public class StreamdeckSatisfactoryAction : BaseStreamDeckActionWithSettingsModel<Models.CounterSettingsModel>
  {

	InputSimulator sim = new InputSimulator();

	public override async Task OnKeyUp(StreamDeckEventPayload args)
	{
		await Manager.SetTitleAsync(args.context, "Miner");

		sim.Keyboard.KeyPress(VirtualKeyCode.VK_N);
		sim.Keyboard.Sleep(50);
		sim.Keyboard.TextEntry("Miner");
		sim.Keyboard.Sleep(50);
		sim.Keyboard.KeyDown(VirtualKeyCode.RETURN);
		sim.Keyboard.Sleep(50);
		sim.Keyboard.KeyUp(VirtualKeyCode.RETURN);

		//update settings
		await Manager.SetSettingsAsync(args.context, SettingsModel);
	}

	public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
	{
		await base.OnDidReceiveSettings(args);
		await Manager.SetTitleAsync(args.context, "Miner");
	}

	public override async Task OnWillAppear(StreamDeckEventPayload args)
	{
		await base.OnWillAppear(args);
		await Manager.SetTitleAsync(args.context, "Miner");
	}
  }
}
