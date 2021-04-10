using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace obrazce
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      Trojuhelnik.VykresliIkonu(trojuhelnik_button_icon);
      Ctverec.VykresliIkonu(ctverec_button_icon);
      Obdelnik.VykresliIkonu(obdelnik_button_icon);
      Kruh.VykresliIkonu(kruh_button_icon);
      Nsten.VykresliIkonu(nsten_button_icon);
    }

    public void Clear()
    {
      parameters.Children.Clear();
      shape_canvas.Children.Clear();

      parameters.Children.Add(new TextBlock { Text = "Parametry", FontSize = 20, FontWeight = FontWeight.FromOpenTypeWeight(700), HorizontalAlignment = HorizontalAlignment.Center });
    }

    public void CreateNumberBox(string text, string placeholder)
    {
      NumberBox nb = new NumberBox() { Header = text, PlaceholderText = placeholder, Margin = new Thickness { Top = 10, Bottom = 10 } };
      nb.ValueChanged += NumberBox_ValueChanged;
      parameters.Children.Add(nb);
      // parameters.Children.Add(new StackPanel { Name = $"stackpanel_params_{name}", Margin = new Thickness { Top=20 } });
      // string = ($"stackpanel_params_{name}")
      // <StackPanel Margin="0,20,0,0">
      //   <Label> strana:</Label>
      //   <TextBox Margin = "0,5,0,0" />
      // </StackPanel>
    }

    private void NumberBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
    {
      // přepočítej, vykresli
    }

    private void shape_button_Click(object sender, RoutedEventArgs e)
    {
      string origin = ((Button)sender).Name;

      switch (origin)
      {
        case "trojuhelnik_button":
          Clear();
          CreateNumberBox("strana a", "10");
          CreateNumberBox("strana b", "20");
          CreateNumberBox("strana c", "30");
          //Trojuhelnik trojuhelnik = new Trojuhelnik(shape_canvas, 100, 100, 100, true);
          //trojuhelnik.VykresliTvar();
          break;
        case "ctverec_button":
          System.Diagnostics.Trace.WriteLine("test");
          Clear();
          CreateNumberBox("strana", "10");
          break;
        case "obdelnik_button":
          Clear();
          CreateNumberBox("strana a", "10");
          CreateNumberBox("strana b", "20");
          break;
        case "kruh_button":
          Clear();
          CreateNumberBox("poloměr", "20");
          break;
        case "ngon_button":
          Clear();
          CreateNumberBox("poloměr", "10");
          CreateNumberBox("počet stran", "8");
          break;
        default:
          Clear();
          break;
      }
    }

    private void menu_clear_Click(object sender, RoutedEventArgs e)
    {
      Clear();
    }

    private void menu_about_application_Click(object sender, RoutedEventArgs e)
    {
      var dialog = new AboutWindow();
      dialog.ShowDialog();
    }
  }
}
