using StreamDeckLib;
using StreamDeckLib.Messages;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;
using System.IO;
using System;

namespace streamdecksatisfactory
{
  [ActionUuid(Uuid= "envy.satisfactory.buildings")]
  public class StreamdeckSatisfactoryAction : BaseStreamDeckActionWithSettingsModel<Models.SatisfactorySettingsModel>
  {
    // Keyboard simulator
    readonly InputSimulator sim = new();
	string path = Path.Combine(Environment.CurrentDirectory, @"images\");

	// StreamDeck button press
    public override async Task OnKeyUp(StreamDeckEventPayload args)
	{
		// Open search bar
		sim.Keyboard.KeyPress(VirtualKeyCode.VK_N);
		sim.Keyboard.Sleep(50);

		// Text entry to search. E.G "Miner"
		string searchText = SettingsModel.SearchText[0..^4].Replace('_', ' ');
		sim.Keyboard.TextEntry(searchText);
		sim.Keyboard.Sleep(50);
		PressReturn();

		// Update settings
		await Manager.SetSettingsAsync(args.context, SettingsModel);
	}

	public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
	{
		await base.OnDidReceiveSettings(args);

        // Set icon
		path = Path.Combine(path, SettingsModel.SearchText);
		await Manager.SetImageAsync(args.context, path);
	}

	public override async Task OnWillAppear(StreamDeckEventPayload args)
	{
		await base.OnWillAppear(args);
	}

	/// <summary>
	/// Press Return simulations a enter/return input
	/// </summary>
	private void PressReturn()
	{
		sim.Keyboard.KeyDown(VirtualKeyCode.RETURN);
		sim.Keyboard.Sleep(50);
		sim.Keyboard.KeyUp(VirtualKeyCode.RETURN);
	}
  }
}
