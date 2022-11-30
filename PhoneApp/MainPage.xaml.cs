using Microsoft.Maui.Storage;

namespace PhoneApp;

public partial class MainPage : ContentPage
{
	string translatedNumber;

	public MainPage()
	{
		InitializeComponent();
		txtName.Text = Preferences.Get("UserName", "");
		DateTime lastreg = Preferences.Get("LastRegister", DateTime.Now);
		lblDateTime.Text = lastreg.ToString();
	}

	public void RegisterClicked(object sender, EventArgs e)
	{
		//TODO: Register user
		Preferences.Set("UserName", txtName.Text);
		Preferences.Set("LastRegister", DateTime.Now);
	}

	public void LogoutClicked(object sender, EventArgs e) 
	{
		//TODO: Logout User
		Preferences.Clear();
		// Alternative is to remove inidividual keys
		if (Preferences.ContainsKey("UserName"))
		{
			Preferences.Remove("UserName");
			// Clear UI to show removed
			txtName.Text = String.Empty;
		}
	}

	public void OnTranslate(object sender, EventArgs e)
	{
		string enteredNumber = PhoneNumberText.Text;
		translatedNumber = PhoneWordTranslator.toNumber(enteredNumber);
		if(!string.IsNullOrEmpty(translatedNumber))
		{
			//TODO:
			CallButton.IsEnabled = true;
			CallButton.Text = "Call " + enteredNumber;
		}
		else
		{
            //TODO:
            CallButton.IsEnabled = false;
            CallButton.Text = "Call";
        }
	}

	async void OnCall(object sender, EventArgs e)
	{
		if (await DisplayAlert("Dial A Number",
					"Would you like to call " + translatedNumber + "?",
					"Yes",
					"No"))
		{
			//TODO: dial the phone
			try
			{
				if (PhoneDialer.Default.IsSupported)
				{
					PhoneDialer.Default.Open(translatedNumber);
				}
			}
			catch (ArgumentNullException)
			{
				await DisplayAlert("Unable to dial", "Phone Number was not valid", "OK");
			}
			catch (Exception)
			{
				await DisplayAlert("Unable to Dial","Phone Dialing Failed","OK");
			}
		}
		
	}
}

