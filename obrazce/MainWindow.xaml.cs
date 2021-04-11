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
    /// <summary>
    ///  Active numberboxes and their current value
    /// </summary>
    public Dictionary<string, NumberBox> NumberBoxes = new Dictionary<string, NumberBox>();
    public MainWindow()
    {
      InitializeComponent();

      Trojuhelnik.VykresliIkonu(shape_canvas, trojuhelnik_stackpanel);
      Ctverec.VykresliIkonu(shape_canvas, ctverec_stackpanel);
      Obdelnik.VykresliIkonu(shape_canvas, obdelnik_stackpanel);
      Kruh.VykresliIkonu(shape_canvas, kruh_stackpanel);
      Nsten.VykresliIkonu(shape_canvas, nsten_stackpanel);
    }

    public void Clear()
    {
      parameters.Children.Clear();
      shape_canvas.Children.Clear();

      parameters.Children.Add(new TextBlock { Text = "Parametry", FontSize = 20, FontWeight = FontWeight.FromOpenTypeWeight(700), HorizontalAlignment = HorizontalAlignment.Center });

      NumberBoxes.Clear();
      calculated_textblock.Text = "";
    }

    public void CreateNumberBox(string name, string text, string placeholder, object tag)
    {
      NumberBox nb = new NumberBox() { Minimum = 1, Tag = tag, Name = name, Header = text, PlaceholderText = placeholder, Margin = new Thickness { Top = 10, Bottom = 10 } };
      nb.ValueChanged += NumberBox_ValueChanged;
      parameters.Children.Add(nb);
      NumberBoxes.Add(nb.Name, nb);
    }

    private void NumberBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
    {
      string origin = (string)sender.Tag;

      switch (origin)
      {
        case "trojuhelnik_button":
          shape_canvas.Children.Clear();
          Trojuhelnik trojuhelnik = new Trojuhelnik(shape_canvas, NumberBoxes["trojuhelnik_strana_a"].Value, NumberBoxes["trojuhelnik_strana_b"].Value, NumberBoxes["trojuhelnik_strana_c"].Value, Brushes.Gray);
          trojuhelnik.VykresliTvar();
          break;
        case "ctverec_button":
          Ctverec ctverec = new Ctverec(shape_canvas, NumberBoxes["ctverec_strana"].Value, Brushes.Gray);
          ctverec.VykresliTvar();
          break;
        case "obdelnik_button":
          Obdelnik obdelnik = new Obdelnik(shape_canvas, NumberBoxes["obdelnik_strana_a"].Value, NumberBoxes["obdelnik_strana_b"].Value, Brushes.Gray);
          obdelnik.VykresliTvar();
          break;
        case "kruh_button":
          Kruh kruh = new Kruh(shape_canvas, NumberBoxes["kruh_polomer"].Value, Brushes.Gray);
          kruh.VykresliTvar();
          break;
        case "nsten_button":
          Nsten nsten = new Nsten(shape_canvas, (int)NumberBoxes["nsten_pocet_stran"].Value, NumberBoxes["nsten_vnejsi_polomer"].Value, Brushes.Gray);
          nsten.VykresliTvar();
          break;
        default:
          break;
      }
    }

    public void shape_button_Click(object sender, RoutedEventArgs e)
    {
      string origin = ((Button)sender).Name;

      switch (origin)
      {
        case "trojuhelnik_button":
          Clear();
          CreateNumberBox("trojuhelnik_strana_a", "strana a", "10", origin);
          CreateNumberBox("trojuhelnik_strana_b", "strana b", "20", origin);
          CreateNumberBox("trojuhelnik_strana_c", "strana c", "30", origin);
          //Trojuhelnik trojuhelnik = new Trojuhelnik(shape_canvas, 100, 100, 100, true);
          //trojuhelnik.VykresliTvar();
          break;
        case "ctverec_button":
          Clear();
          CreateNumberBox("ctverec_strana", "strana", "10", origin);
          break;
        case "obdelnik_button":
          Clear();
          CreateNumberBox("obdelnik_strana_a", "strana a", "10", origin);
          CreateNumberBox("obdelnik_strana_b", "strana b", "20", origin);
          //Obdelnik.AktualizujVypocet(new Obdelnik(shape_canvas, 0, 0, Brushes.Gray), shape_canvas, calculated_textblock);
          break;
        case "kruh_button":
          Clear();
          CreateNumberBox("kruh_polomer", "poloměr", "20", origin);
          break;
        case "nsten_button":
          Clear();
          CreateNumberBox("nsten_vnejsi_polomer", "vnější poloměr", "10", origin);
          CreateNumberBox("nsten_pocet_stran", "počet stran", "8", origin);
          break;
        default:
          Clear();
          break;
      }
    }

    public void menu_clear_Click(object sender, RoutedEventArgs e)
    {
      Clear();
    }

    public void menu_about_application_Click(object sender, RoutedEventArgs e)
    {
      var dialog = new AboutWindow();
      dialog.ShowDialog();
    }
  }
}
