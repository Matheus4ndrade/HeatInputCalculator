using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HeatInputCalculator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnStartCalculationClicked(object? sender, RoutedEventArgs e)
        {
            var calculationWindow = new CalculationWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            calculationWindow.Show(this);
        }

        private void OnLinkedInClicked(object? sender, RoutedEventArgs e)
        {
            OpenUrl("https://www.linkedin.com/in/matheus-andrade-4449212b7");
        }

        private void OnGmailClicked(object? sender, RoutedEventArgs e)
        {
            OpenUrl("mailto:matheus014ndrade@gmail.com?subject=Contato via Heat Input Calculator");
        }

        private void OnSiteClicked(object? sender, RoutedEventArgs e)
        {
            OpenUrl("https://drillweld.netlify.app/");
        }

        private void OpenUrl(string url)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
            }
            catch (Exception ex)
            {
                var errorWindow = new Window
                {
                    Title = "Erro",
                    Width = 400,
                    Height = 150,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Content = new StackPanel
                    {
                        Margin = new Thickness(20),
                        Spacing = 10,
                        Children =
                        {
                            new TextBlock
                            {
                                Text = "Não foi possível abrir o link:",
                                FontWeight = FontWeight.Bold
                            },
                            new TextBlock
                            {
                                Text = ex.Message,
                                TextWrapping = TextWrapping.Wrap
                            },
                            new Button
                            {
                                Content = "OK",
                                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                                Padding = new Thickness(30, 10),
                                Background = new SolidColorBrush(Color.Parse("#6A0DAD")),
                                Foreground = Brushes.White
                            }
                        }
                    }
                };

                if (errorWindow.Content is StackPanel panel && panel.Children[2] is Button okButton)
                {
                    okButton.Click += (s, ev) => errorWindow.Close();
                }

                errorWindow.ShowDialog(this);
            }
        }
    }
}