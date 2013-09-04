using System.Windows.Forms;

namespace SendKeyByGesture
{
	public partial class PlayerPreviewWindow
	{
		public PlayerPreviewWindow(PlayerPreviewViewModel playerPreviewViewModel)
		{
			DataContext = playerPreviewViewModel;
			InitializeComponent();
		}

		public void SetPosition(Screen screen)
		{
			Left = screen.Bounds.Width + screen.Bounds.Left - Width;
			Top = screen.Bounds.Height + screen.Bounds.Top - Height;
		}
	}
}
