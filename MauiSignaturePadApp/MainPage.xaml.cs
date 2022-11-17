namespace MauiSignaturePadApp;

using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui;

public partial class MainPage : ContentPage, IQueryAttributable
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();

	}
	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		//DrawingViewControl.WidthRequest = Application.Current.MainPage.Width;
	}

	private async void OnSubmitClicked(object sender, EventArgs e)
	{
		var stream = await SignaturePad.GetImage();
		await Shell.Current.GoToAsync("///ImagePage", true,new Dictionary<string, object> { { "imStream", stream } });
	}

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private void SignaturePad_SignaturePadDidClear(object sender, EventArgs e)
	{
		Console.WriteLine("SignaturePad_SignaturePadDidClear");
	}

	private void SignaturePad_SignatureLineCompleted(object sender, DrawingLineCompletedEventArgs e)
	{
		Console.WriteLine("");
	}
}

