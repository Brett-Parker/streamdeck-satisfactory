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

	// StreamDeck button press
    public override async Task OnKeyUp(StreamDeckEventPayload args)
	{     
        if (int.TryParse(SettingsModel.SearchText, out _))
	    {
            // Hotbar switcher
            AltNumber(SettingsModel.SearchText);
        }
	    else
	    {
            // Text entry to search. E.G "Miner"
            SearchBar(SettingsModel.SearchText);
        }

	    // Update settings
	    await Manager.SetSettingsAsync(args.context, SettingsModel);
	}

	public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
	{
		await base.OnDidReceiveSettings(args);
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

	/// <summary>
	/// Press alt + number
	/// </summary>
	private void AltNumber(string searchText)
	{
        int number = Int32.Parse(searchText);
        sim.Keyboard.KeyPress(VirtualKeyCode.MENU);
        sim.Keyboard.Sleep(50);
        switch (number)
        {
            case 1:
                sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD1);
                break;
            case 2:
                sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD2);
                break;
            case 3:
                sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD3);
                break;
            case 4:
                sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD4);
                break;
            case 5:
                sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD5);
                break;
            case 6:
                sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD6);
                break;
            case 7:
                sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD7);
                break;
            case 8:
                sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD8);
                break;
            case 9:
                sim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD9);
                break;
        }
    }   

	/// <summary>
	/// N for search bar, then text
	/// </summary>
	private void SearchBar(string text)
	{
        // Open search bar
        sim.Keyboard.KeyDown(VirtualKeyCode.VK_N);
        sim.Keyboard.Sleep(50);
        var searchText = text[0..^4].Replace('_', ' ');
        sim.Keyboard.TextEntry(searchText);
        sim.Keyboard.Sleep(50);
        PressReturn();
    }        
  }
}
