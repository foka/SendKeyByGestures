using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace SendKeyByGesture
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();

			var timer = new Timer(1000);
			timer.Elapsed += (_, __) => SendKeys.SendWait("{F5}");
			timer.Start();
		}
	}
}
