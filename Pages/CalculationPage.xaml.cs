using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace HeatInputCalculator
{
    public partial class CalculationPage : ContentPage
    {
        private bool _isDarkTheme = false;

        public CalculationPage()
        {
            InitializeComponent();
            _isDarkTheme = Application.Current.UserAppTheme == AppTheme.Dark;
            AddButtonEffects();
        }

        private void AddButtonEffects()
        {
            ThemeToggleBtn.Pressed += OnButtonPressed;
            ThemeToggleBtn.Released += OnButtonReleased;
            CalculateButton.Pressed += OnButtonPressed;
            CalculateButton.Released += OnButtonReleased;
        }

        private void RemoveButtonEffects()
        {
            ThemeToggleBtn.Pressed -= OnButtonPressed;
            ThemeToggleBtn.Released -= OnButtonReleased;
            CalculateButton.Pressed -= OnButtonPressed;
            CalculateButton.Released -= OnButtonReleased;
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
            // Alterna entre tema claro e escuro
            _isDarkTheme = !_isDarkTheme;
            Application.Current.UserAppTheme = _isDarkTheme ? AppTheme.Dark : AppTheme.Light;
        }

        private void OnCalcularClicked(object sender, EventArgs e)
        {
            try
            {
                double I = double.Parse(entryAmperagem.Text);
                double V = double.Parse(entryTensao.Text);
                double n = double.Parse(entryEficiencia.Text);
                double v = double.Parse(entryVelocidade.Text);

                if (pickerUnidade.SelectedItem == null)
                {
                    lblResultado.Text = "Selecione uma unidade de velocidade!";
                    return;
                }

                string unidade = pickerUnidade.SelectedItem.ToString();
                double v_mm_s = unidade switch
                {
                    "cm/min" => v * 10.0 / 60.0,
                    "cm/s" => v * 10.0,
                    "mm/min" => v / 60.0,
                    "mm/s" => v,
                    _ => throw new Exception("Unidade inv�lida")
                };

                double heatInput = (V * I * (n / 100.0)) / v_mm_s;
                lblResultado.Text = $"Heat Input = {heatInput:F2} J/mm";
            }
            catch
            {
                lblResultado.Text = "Preencha todos os campos corretamente.";
            }
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
                await DisplayAlert("Erro", "Nenhum app de email est� dispon�vel neste dispositivo.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"N�o foi poss�vel abrir o email. {ex.Message}", "OK");
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