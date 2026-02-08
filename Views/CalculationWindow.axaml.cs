using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Globalization;

namespace HeatInputCalculator.Views
{
    public partial class CalculationWindow : Window
    {
        public CalculationWindow()
        {
            InitializeComponent();
        }

        private void OnCalculateClicked(object? sender, RoutedEventArgs e)
        {
            try
            {
                var amperagemText = entryAmperagem?.Text;
                var tensaoText = entryTensao?.Text;
                var eficienciaText = entryEficiencia?.Text;
                var velocidadeText = entryVelocidade?.Text;

                if (string.IsNullOrWhiteSpace(amperagemText) ||
                    string.IsNullOrWhiteSpace(tensaoText) ||
                    string.IsNullOrWhiteSpace(eficienciaText) ||
                    string.IsNullOrWhiteSpace(velocidadeText))
                {
                    if (lblResultado != null)
                    {
                        lblResultado.Text = "Por favor, preencha todos os campos!";
                    }
                    return;
                }

                if (!double.TryParse(amperagemText, NumberStyles.Any, CultureInfo.InvariantCulture, out double I) ||
                    !double.TryParse(tensaoText, NumberStyles.Any, CultureInfo.InvariantCulture, out double V) ||
                    !double.TryParse(eficienciaText, NumberStyles.Any, CultureInfo.InvariantCulture, out double n) ||
                    !double.TryParse(velocidadeText, NumberStyles.Any, CultureInfo.InvariantCulture, out double v))
                {
                    if (lblResultado != null)
                    {
                        lblResultado.Text = "Por favor, insira valores numéricos válidos!";
                    }
                    return;
                }

                if (I <= 0 || V <= 0 || n <= 0 || n > 100 || v <= 0)
                {
                    if (lblResultado != null)
                    {
                        lblResultado.Text = "Todos os valores devem ser positivos e a eficiência entre 0 e 100!";
                    }
                    return;
                }

                if (pickerUnidade?.SelectedItem == null)
                {
                    if (lblResultado != null)
                    {
                        lblResultado.Text = "Selecione uma unidade de velocidade!";
                    }
                    return;
                }

                string unidade = ((ComboBoxItem)pickerUnidade.SelectedItem).Content?.ToString() ?? "mm/s";

                //Converter velocidades
                double v_mm_s = unidade switch
                {
                    "cm/min" => v * 10.0 / 60.0,
                    "cm/s" => v * 10.0,
                    "mm/min" => v / 60.0,
                    "mm/s" => v,
                    _ => throw new Exception("Unidade inválida")
                };

                //Heat Input (J/mm) = (Tensão × Corrente × Eficiência) / Velocidade
                double heatInput = (V * I * (n / 100.0)) / v_mm_s;

                if (lblResultado != null)
                {
                    lblResultado.Text = $"Heat Input = {heatInput:F2} J/mm\n\n" +
                                       $"Parâmetros utilizados:\n" +
                                       $"Amperagem: {I} A\n" +
                                       $"Tensão: {V} V\n" +
                                       $"Eficiência Térmica: {n}%\n" +
                                       $"Velocidade: {v} {unidade} ({v_mm_s:F2} mm/s)";
                }
            }
            catch (Exception ex)
            {
                if (lblResultado != null)
                {
                    lblResultado.Text = $"Erro ao calcular: {ex.Message}";
                }
            }
        }

        private void OnClearClicked(object? sender, RoutedEventArgs e)
        {
            if (entryAmperagem != null) entryAmperagem.Text = string.Empty;
            if (entryTensao != null) entryTensao.Text = string.Empty;
            if (entryEficiencia != null) entryEficiencia.Text = string.Empty;
            if (entryVelocidade != null) entryVelocidade.Text = string.Empty;

            if (pickerUnidade != null) pickerUnidade.SelectedIndex = 1;

            if (lblResultado != null)
            {
                lblResultado.Text = "Preencha os dados para calcular o Heat Input";
            }
        }

        private void OnBackClicked(object? sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
