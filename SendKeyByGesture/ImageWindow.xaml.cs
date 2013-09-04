using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Timer = System.Timers.Timer;

namespace SendKeyByGesture
{
	/// <summary>
	/// Interaction logic for ImageWindow.xaml
	/// </summary>
	public partial class ImageWindow : Window
	{
		public ImageWindow()
		{
			hideTimer = new Timer(300);
			hideTimer.Elapsed += (_, __) => Dispatcher.Invoke((Action) Hide);
			hideTimer.AutoReset = false;

			InitializeComponent();
		}

		public void SetPosition(Screen screen)
		{
			Left = 0;
			Top = 0;
			Width = screen.Bounds.Width;
			Height = screen.Bounds.Height;
		}

		public void ShowImage(ImageSource source)
		{
			Dispatcher.Invoke((Action) (() =>
			{
				hideTimer.Stop();
				image.Source = source;
				Show();
				hideTimer.Start();
			}));
		}


		readonly Timer hideTimer;
	}
}
