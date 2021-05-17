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

    /// <summary>
    /// Clears canvas, NumberBoxes, parameters panel and recreates it,
    /// </summary>
    public void Clear()
    {
      parameters.Children.Clear();
      shape_canvas.Children.Clear();

      parameters.Children.Add(new TextBlock { Text = "Parametry", FontSize = 20, FontWeight = FontWeight.FromOpenTypeWeight(700), HorizontalAlignment = HorizontalAlignment.Center });

      NumberBoxes.Clear();
      calculated_textblock.Text = "";
    }

    /// <summary>
    /// Creates NumberBox
    /// </summary>
    /// <param name="name">Name of the NumberBox.</param>
    /// <param name="text">Text.</param>
    /// <param name="placeholder">Placeholder.</param>
    /// <param name="tag">Tag.</param>
    public void CreateNumberBox(string name, string text, int defaultValue, object tag, int min = 1, int max = int.MaxValue)
    {
      NumberBox nb = new NumberBox() { Minimum = min, Maximum = max, Tag = tag, Name = name, Header = text, Value = defaultValue, Margin = new Thickness { Top = 10, Bottom = 10 } };
      nb.ValueChanged += NumberBox_ValueChanged;
      parameters.Children.Add(nb);
      NumberBoxes.Add(nb.Name, nb);
    }

    /// <summary>
    /// What happens when values of a NumberBox changes.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Additional arguments.</param>
    private void NumberBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
    {
      if (double.IsNaN(sender.Value)) { sender.Value = sender.Minimum; }

      string origin = (string)sender.Tag;

      switch (origin)
      {
        case "trojuhelnik_button":
          shape_canvas.Children.Clear();
          Trojuhelnik trojuhelnik = new Trojuhelnik(shape_canvas, NumberBoxes["trojuhelnik_strana_a"].Value, NumberBoxes["trojuhelnik_strana_b"].Value, NumberBoxes["trojuhelnik_strana_c"].Value, Brushes.Gray);
          trojuhelnik.VykresliTvar(calculated_textblock);
          break;
        case "ctverec_button":
          Ctverec ctverec = new Ctverec(shape_canvas, NumberBoxes["ctverec_strana"].Value, Brushes.Gray);
          ctverec.VykresliTvar(calculated_textblock);
          break;
        case "obdelnik_button":
          Obdelnik obdelnik = new Obdelnik(shape_canvas, NumberBoxes["obdelnik_strana_a"].Value, NumberBoxes["obdelnik_strana_b"].Value, Brushes.Gray);
          obdelnik.VykresliTvar(calculated_textblock);
          break;
        case "kruh_button":
          Kruh kruh = new Kruh(shape_canvas, NumberBoxes["kruh_polomer"].Value, Brushes.Gray);
          kruh.VykresliTvar(calculated_textblock);
          break;
        case "nsten_button":
          Nsten nsten = new Nsten(shape_canvas, (int)NumberBoxes["nsten_pocet_stran"].Value, NumberBoxes["nsten_vnejsi_polomer"].Value, Brushes.Gray);
          nsten.VykresliTvar(calculated_textblock);
          break;
        default:
          break;
      }
    }

    /// <summary>
    /// What happens when you click on a shape button
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Event.</param>
    public void shape_button_Click(object sender, RoutedEventArgs e)
    {
      string origin = ((Button)sender).Name;

      switch (origin)
      {
        case "trojuhelnik_button":
          Clear();
          CreateNumberBox("trojuhelnik_strana_a", "strana a", 30, origin);
          CreateNumberBox("trojuhelnik_strana_b", "strana b", 40, origin);
          CreateNumberBox("trojuhelnik_strana_c", "strana c", 50, origin);
          Trojuhelnik trojuhelnik = new Trojuhelnik(shape_canvas, NumberBoxes["trojuhelnik_strana_a"].Value, NumberBoxes["trojuhelnik_strana_b"].Value, NumberBoxes["trojuhelnik_strana_c"].Value, Brushes.Gray);
          trojuhelnik.VykresliTvar(calculated_textblock);
          break;
        case "ctverec_button":
          Clear();
          CreateNumberBox("ctverec_strana", "strana", 10, origin);
          Ctverec ctverec = new Ctverec(shape_canvas, NumberBoxes["ctverec_strana"].Value, Brushes.Gray);
          ctverec.VykresliTvar(calculated_textblock);
          break;
        case "obdelnik_button":
          Clear();
          CreateNumberBox("obdelnik_strana_a", "strana a", 10, origin);
          CreateNumberBox("obdelnik_strana_b", "strana b", 20, origin);
          Obdelnik obdelnik = new Obdelnik(shape_canvas, NumberBoxes["obdelnik_strana_a"].Value, NumberBoxes["obdelnik_strana_b"].Value, Brushes.Gray);
          obdelnik.VykresliTvar(calculated_textblock);
          break;
        case "kruh_button":
          Clear();
          CreateNumberBox("kruh_polomer", "poloměr", 20, origin);
          Kruh kruh = new Kruh(shape_canvas, NumberBoxes["kruh_polomer"].Value, Brushes.Gray);
          kruh.VykresliTvar(calculated_textblock);
          break;
        case "nsten_button":
          Clear();
          CreateNumberBox("nsten_vnejsi_polomer", "vnější poloměr", 10, origin);
          CreateNumberBox("nsten_pocet_stran", "počet stran", 8, origin, 3);
          Nsten nsten = new Nsten(shape_canvas, (int)NumberBoxes["nsten_pocet_stran"].Value, NumberBoxes["nsten_vnejsi_polomer"].Value, Brushes.Gray);
          nsten.VykresliTvar(calculated_textblock);
          break;
        default:
          Clear();
          break;
      }
    }

    /// <summary>
    /// Calls Clear() when you click on Clear item in menu
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Event.</param>
    public void menu_clear_Click(object sender, RoutedEventArgs e)
    {
      Clear();
    }

    /// <summary>
    /// Shows about page.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Event.</param>
    public void menu_about_application_Click(object sender, RoutedEventArgs e)
    {
      var dialog = new AboutWindow();
      dialog.ShowDialog();
    }
  }
}
