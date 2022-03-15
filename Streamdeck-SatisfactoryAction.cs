using StreamDeckLib;
using StreamDeckLib.Messages;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;
using System;

namespace streamdecksatisfactory
{
  [ActionUuid(Uuid= "envy.satisfactory.buildings")]
  public class StreamdeckSatisfactoryAction : BaseStreamDeckActionWithSettingsModel<Models.SatisfactorySettingsModel>
  {
	// Variable decleration
	InputSimulator sim = new InputSimulator();
    private VirtualKeyCode virtualKeyCode;

	// StreamDeck button press
    public override async Task OnKeyUp(StreamDeckEventPayload args)
	{
		await Manager.SetTitleAsync(args.context, "Miner");
		
		// Open search bar
		KeyPress("VK_N");
		KeyboardSleep(50);

		// Text entry to search. E.G "Miner"
		sim.Keyboard.TextEntry("Miner");
		KeyboardSleep(50);
		PressReturn();

		//update settings
		await Manager.SetSettingsAsync(args.context, SettingsModel);
	}

	/// <summary>
	/// KeyPress simulations a keypress input
	/// </summary>
    private void KeyPress(string input)
	{
		sim.Keyboard.KeyPress(virtualKeyCode);
	}

	/// <summary>
	/// PressReturn simulations a enter/return input
	/// </summary>
    private void KeyboardSleep(int time)
    {
		sim.Keyboard.Sleep(time);
	}

	/// <summary>
	/// PressReturn simulations a enter/return input
	/// </summary>
    private void PressReturn()
    {
		sim.Keyboard.KeyDown(VirtualKeyCode.RETURN);
		sim.Keyboard.Sleep(50);
		sim.Keyboard.KeyUp(VirtualKeyCode.RETURN);
	}

	public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
	{
		await base.OnDidReceiveSettings(args);
		await Manager.SetTitleAsync(args.context, "");
	}

	public override async Task OnWillAppear(StreamDeckEventPayload args)
	{
		await base.OnWillAppear(args);
		await Manager.SetTitleAsync(args.context, "");
	}
  }
}
