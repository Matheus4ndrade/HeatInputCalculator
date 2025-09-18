namespace HeatInputCalculator
{
    public partial class MainPage : ContentPage
    {
        private bool _isDarkTheme = false;

        public MainPage()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Light;
            _isDarkTheme = false;
            AddButtonEffects();
        }

        private void AddButtonEffects()
        {
            ThemeToggleBtn.Pressed += OnButtonPressed;
            ThemeToggleBtn.Released += OnButtonReleased;

            StartButton.Pressed += OnButtonPressed;
            StartButton.Released += OnButtonReleased;
        }

        private void RemoveButtonEffects()
        {
            ThemeToggleBtn.Pressed -= OnButtonPressed;
            ThemeToggleBtn.Released -= OnButtonReleased;

            StartButton.Pressed -= OnButtonPressed;
            StartButton.Released -= OnButtonReleased;
        }

        private async void OnButtonPressed(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                await button.ScaleTo(0.97, 100, Easing.CubicOut);
                button.Opacity = 0.9;
            }
        }

        private async void OnButtonReleased(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                await button.ScaleTo(1.0, 100, Easing.CubicOut);
                button.Opacity = 1.0;
            }
        }

        private void OnThemeToggleClicked(object sender, EventArgs e)
        {
            _isDarkTheme = !_isDarkTheme;
            Application.Current.UserAppTheme = _isDarkTheme ? AppTheme.Dark : AppTheme.Light;
        }

        private async void OnStartCalculationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalculationPage());
        }

        private async void OnLinkedInClicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://www.linkedin.com/in/matheus-andrade-4449212b7");
        }

        private async void OnGmailClicked(object sender, EventArgs e)
        {
            try
            {
                var email = new EmailMessage
                {
                    Subject = "Contato via Heat Input Calculator",
                    Body = "",
                    To = new List<string> { "matheus014ndrade@gmail.com" }
                };
                await Email.ComposeAsync(email);
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Erro", "Nenhum app de email está disponível neste dispositivo.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Não foi possível abrir o email. {ex.Message}", "OK");
            }
        }

        private async void OnSiteClicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://drillweld.netlify.app/");
        }

        protected override void OnDisappearing()
        {
            RemoveButtonEffects();
            base.OnDisappearing();
        }
    }
}