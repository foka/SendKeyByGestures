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
			Left = screen.WorkingArea.Width + screen.WorkingArea.Left - Width;
			Top = screen.WorkingArea.Height + screen.WorkingArea.Top - Height;
		}
	}
}
