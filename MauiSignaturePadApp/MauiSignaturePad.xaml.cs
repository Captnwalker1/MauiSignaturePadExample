using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace MauiSignaturePadApp;

public partial class MauiSignaturePad : ContentView
{
    readonly WeakEventManager signaturePadEventManager = new();
    public MauiSignaturePad()
	{
		InitializeComponent();

    }

    private void ClearBtn_Clicked(object sender, EventArgs e)
    {
        DrawingViewControl.Clear();

        signaturePadEventManager.HandleEvent(this, null, nameof(SignaturePadDidClear));
    }

    public async Task<Stream> GetImage()
    {
        var drawingLines = DrawingViewControl.Lines.ToList();
        var points = drawingLines.SelectMany(x => x.Points).ToList();
        return await DrawingView.GetImageStream(drawingLines,
            new Size(points.Max(x => x.X) - points.Min(x => x.X), points.Max(x => x.Y) - points.Min(x => x.Y)),
            Colors.White);
        //return await DrawingViewControl.GetImageStream(DrawingViewControl.Width, DrawingViewControl.Height);
    }

    private void DrawingLineCompleted(object sender, DrawingLineCompletedEventArgs e)
    {

        signaturePadEventManager.HandleEvent(this, e, nameof(SignatureLineCompleted));
    }

    /// <summary>
	/// Event occurred when drawing line completed.
	/// </summary>
	public event EventHandler<DrawingLineCompletedEventArgs> SignatureLineCompleted
    {
        add => signaturePadEventManager.AddEventHandler(value);
        remove => signaturePadEventManager.RemoveEventHandler(value);
    }

    /// <summary>
	/// Event occurred when signature pad is cleared.
	/// </summary>
	public event EventHandler SignaturePadDidClear
    {
        add => signaturePadEventManager.AddEventHandler(value);
        remove => signaturePadEventManager.RemoveEventHandler(value);
    }
}