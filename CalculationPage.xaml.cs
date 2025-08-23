using Microsoft.Maui.Controls;
using System;

namespace HeatInputCalculator
{
    public partial class CalculationPage : ContentPage
    {
        public CalculationPage()
        {
            InitializeComponent();
        }

        private void OnToggleThemeClicked(object sender, EventArgs e)
        {
            if (App.Current.UserAppTheme == AppTheme.Dark)
                App.Current.UserAppTheme = AppTheme.Light;
            else
                App.Current.UserAppTheme = AppTheme.Dark;
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
                    _ => throw new Exception("Unidade inválida")
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
    }
 }
