using System.Globalization;

namespace MauiApp2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var shell = new AppShell();
            shell.FlowDirection = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft
                ? FlowDirection.RightToLeft
                : FlowDirection.LeftToRight;
            return new Window(shell);
        }
    }
}
