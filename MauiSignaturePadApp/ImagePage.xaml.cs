namespace MauiSignaturePadApp;

public partial class ImagePage : ContentPage, IQueryAttributable
{

	public ImagePage()
	{
		InitializeComponent();
	}
	public ImagePage(Stream imStream)
	{
		InitializeComponent();
		TheImage.Source = ImageSource.FromStream(() => imStream);
	}

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
        Stream imStream = query["imStream"] as Stream;
        TheImage.Source = ImageSource.FromStream(() => imStream);
    }
}