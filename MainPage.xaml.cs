using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui.ApplicationModel;

namespace HeatInputCalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            // Forçar tema inicial claro
            App.Current.UserAppTheme = AppTheme.Light;

            // Posicionar thumb à esquerda
            toggleThumb.TranslationX = 0;

        }

        private async void OnUsarClicked(object sender, EventArgs e)
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
                    Subject = "Contato via Heat Input",
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


        private void OnToggleThemeClicked(object sender, EventArgs e)
        {
            if (App.Current.UserAppTheme == AppTheme.Dark)
            {
                App.Current.UserAppTheme = AppTheme.Light;
                toggleThumb.TranslateTo(0, 0, 200, Easing.CubicInOut);
            }
            else
            {
                App.Current.UserAppTheme = AppTheme.Dark;
                double endX = toggleFrame.Width - toggleThumb.Width - 4; // 4 = padding interno do Frame
                toggleThumb.TranslateTo(endX, 0, 200, Easing.CubicInOut);
            }


        }
    }
}