using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace obrazce
{
  /// <summary>
  /// Default shape.
  /// </summary>
  class Tvar
  {
    /// <summary>
    /// Perimeter.
    /// </summary>
    protected double Obvod;
    /// <summary>
    /// Area.
    /// </summary>
    protected double Obsah;
    /// <summary>
    /// Color of the shape.
    /// </summary>
    protected SolidColorBrush Vypln;
    /// <summary>
    /// Canvas.
    /// </summary>
    protected Canvas Canvas;

    /// <summary>
    /// Sets values.
    /// </summary>
    /// <param name="canvas">Canvas.</param>
    /// <param name="vypln">Fill.</param>
    public Tvar(Canvas canvas, SolidColorBrush vypln)
    {
      this.Canvas = canvas;
      this.Vypln = vypln;
    }

    /// <summary>
    /// Draws shape.
    /// </summary>
    /// <param name="tb">TextBlock where the perimeter and area is shown.</param>
    public virtual void VykresliTvar(TextBlock tb) { }

    /// <summary>
    /// Calculates perimeter.
    /// </summary>
    public virtual void VypocitejObvod() { }

    /// <summary>
    /// Calculates area.
    /// </summary>
    public virtual void VypocitejObsah() { }

    /// <summary>
    /// Updates calculations and info under drawn shape.
    /// </summary>
    /// <param name="tvar">Shape.</param>
    /// <param name="canvas">Canvas.</param>
    /// <param name="tb">TextBox.</param>
    public void AktualizujVypocet(Tvar tvar, Canvas canvas, TextBlock tb)
    {
      VypocitejObvod();
      VypocitejObsah();

      tb.Text = $"obvod: {Obvod.ToString("F3")} | obsah: {Obsah.ToString("F3")}";
    }
  }

  /// <summary>
  /// Triangle.
  /// </summary>
  class Trojuhelnik : Tvar
  {
    private double Strana_a;
    private double Strana_b;
    private double Strana_c;
    /// <summary>
    /// Sets values.
    /// </summary>
    /// <param name="canvas">Canvas.</param>
    /// <param name="strana_a">Length of a.</param>
    /// <param name="strana_b">Length of b.</param>
    /// <param name="strana_c">Length of c.</param>
    /// <param name="vypln">Fill.</param>
    public Trojuhelnik(Canvas canvas, double strana_a, double strana_b, double strana_c, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Strana_a = strana_a;
      this.Strana_b = strana_b;
      this.Strana_c = strana_c;
      this.Canvas = canvas;
    }

    /// <summary>
    /// Draws icon.
    /// </summary>
    /// <param name="canvas">Canvas.</param>
    /// <param name="sp">StackPanel.</param>
    public static void VykresliIkonu(Canvas canvas, StackPanel sp)
    {
      Trojuhelnik trojuhelnik = new Trojuhelnik(canvas, 15, 15, 15, Brushes.Black);

      sp.Children.Add(trojuhelnik.ZiskejTvar());
      sp.Children.Add(new TextBlock { Margin = new Thickness { Left = 5 }, Text = "trojůhelník" });
    }

    /// <summary>
    /// Gets the shape.
    /// </summary>
    /// <returns>Shape.</returns>
    public Polygon ZiskejTvar()
    {
      double vyska = VypocitejVysku();
      double cx = Math.Sqrt((Strana_b * Strana_b) - (vyska * vyska));
      double teziste_x = (Strana_a + cx) / 3;
      double teziste_y = vyska / 3;

      Point a = new Point(0, vyska);
      Point b = new Point(Strana_a, vyska);
      Point c = new Point(cx, 0);

      Polygon tvar = new Polygon { Stroke = Brushes.Gray, Fill = Vypln };

      double left = Canvas.ActualWidth / 2;
      Canvas.SetLeft(tvar, left - teziste_x);
      double top = Canvas.ActualHeight / 2;
      Canvas.SetTop(tvar, top - teziste_y);

      tvar.Points.Add(a);
      tvar.Points.Add(b);
      tvar.Points.Add(c);

      return tvar;
    }

    public override void VykresliTvar(TextBlock tb)
    {
      Polygon tvar = ZiskejTvar();

      Canvas.Children.Clear();
      Canvas.Children.Add(tvar);
      AktualizujVypocet(new Trojuhelnik(Canvas, Strana_a, Strana_b, Strana_c, Vypln), Canvas, tb);
    }

    /// <summary>
    /// Calculates height.
    /// </summary>
    /// <returns>Height.</returns>
    public double VypocitejVysku()
    {
      double vyska = Math.Sqrt(4 * (Strana_c * Strana_c) * (Strana_a * Strana_a) - Math.Pow((Strana_a * Strana_a) + (Strana_c * Strana_c) - (Strana_b * Strana_b), 2)) / (Strana_a * 2);
      return vyska;
    }

    public override void VypocitejObvod()
    {
      this.Obvod = Strana_a + Strana_b + Strana_c;
    }

    public override void VypocitejObsah()
    {
      // https://cs.wikipedia.org/wiki/Heron%C5%AFv_vzorec
      double s = Obvod / 2;
      this.Obsah = Math.Round(Math.Sqrt(s * (s - Strana_a) * (s - Strana_b) * (s - Strana_c)));
    }
  }

  /// <summary>
  /// Rectangle.
  /// </summary>
  class Ctverec : Tvar
  {
    private double Strana;
    public Ctverec(Canvas canvas, double strana, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Strana = strana;
    }

    /// <summary>
    /// Draws icon.
    /// </summary>
    /// <param name="canvas">Canvas.</param>
    /// <param name="sp">StackPanel.</param>
    public static void VykresliIkonu(Canvas canvas, StackPanel sp)
    {
      Ctverec ctverec = new Ctverec(canvas, 10, Brushes.Black);

      sp.Children.Add(ctverec.ZiskejTvar());
      sp.Children.Add(new TextBlock { Margin = new Thickness { Left = 5 }, Text = "čtverec" });
    }

    /// <summary>
    /// Gets the shape.
    /// </summary>
    /// <returns>Shape.</returns>
    public Rectangle ZiskejTvar()
    {
      Rectangle tvar = new Rectangle { Width = Strana, Height = Strana, Stroke = Brushes.Gray, Fill = Vypln };

      double left = Canvas.ActualWidth / 2;
      Canvas.SetLeft(tvar, left - (Strana / 2));
      double top = Canvas.ActualHeight / 2;
      Canvas.SetTop(tvar, top - (Strana / 2));

      return tvar;
    }

    public override void VykresliTvar(TextBlock tb)
    {
      Canvas.Children.Clear();
      Canvas.Children.Add(ZiskejTvar());
      AktualizujVypocet(new Ctverec(Canvas, Strana, Vypln), Canvas, tb);
    }

    public override void VypocitejObvod()
    {
      this.Obvod = 4 * Strana;
    }

    public override void VypocitejObsah()
    {
      this.Obsah = Strana * Strana;
    }
  }

  class Obdelnik : Tvar
  {
    private double Strana_a;
    private double Strana_b;
    public Obdelnik(Canvas canvas, double strana_a, double strana_b, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Strana_a = strana_a;
      this.Strana_b = strana_b;
    }

    /// <summary>
    /// Draws icon.
    /// </summary>
    /// <param name="canvas">Canvas.</param>
    /// <param name="sp">StackPanel.</param>
    public static void VykresliIkonu(Canvas canvas, StackPanel sp)
    {
      Obdelnik obdelnik = new Obdelnik(canvas, 10, 20, Brushes.Black);

      sp.Children.Add(obdelnik.ZiskejTvar());
      sp.Children.Add(new TextBlock { Margin = new Thickness { Left = 5 }, Text = "obdélník" });
    }

    /// <summary>
    /// Gets the shape.
    /// </summary>
    /// <returns>Shape.</returns>
    public Rectangle ZiskejTvar()
    {
      Rectangle tvar = new Rectangle { Width = Strana_a, Height = Strana_b, Stroke = Brushes.Gray, Fill = Vypln };

      double left = Canvas.ActualWidth / 2;
      Canvas.SetLeft(tvar, left - (Strana_a / 2));
      double top = Canvas.ActualHeight / 2;
      Canvas.SetTop(tvar, top - (Strana_b / 2));

      return tvar;
    }

    public override void VykresliTvar(TextBlock tb)
    {
      Canvas.Children.Clear();
      Canvas.Children.Add(ZiskejTvar());
      AktualizujVypocet(new Obdelnik(Canvas, Strana_a, Strana_b, Vypln), Canvas, tb);
    }

    public override void VypocitejObvod()
    {
      this.Obvod = 2 * (Strana_a + Strana_b);
    }

    public override void VypocitejObsah()
    {
      this.Obsah = Strana_a * Strana_b;
    }
  }

  /// <summary>
  /// Circle.
  /// </summary>
  class Kruh : Tvar
  {
    private double Polomer;
    public Kruh(Canvas canvas, double polomer, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Polomer = polomer;
    }

    /// <summary>
    /// Draws icon.
    /// </summary>
    /// <param name="canvas">Canvas.</param>
    /// <param name="sp">StackPanel.</param>
    public static void VykresliIkonu(Canvas canvas, StackPanel sp)
    {
      Kruh kruh = new Kruh(canvas, 7.5, Brushes.Black);

      sp.Children.Add(kruh.ZiskejTvar());
      sp.Children.Add(new TextBlock { Margin = new Thickness { Left = 5 }, Text = "kruh" });
    }

    /// <summary>
    /// Gets the shape.
    /// </summary>
    /// <returns>Shape.</returns>
    public Ellipse ZiskejTvar()
    {
      Ellipse tvar = new Ellipse { Width = Polomer * 2, Height = Polomer * 2, Stroke = Brushes.Gray, Fill = Vypln };

      double left = Canvas.ActualWidth / 2;
      Canvas.SetLeft(tvar, left - Polomer);
      double top = Canvas.ActualHeight / 2;
      Canvas.SetTop(tvar, top - Polomer);

      return tvar;
    }

    public override void VykresliTvar(TextBlock tb)
    {
      Canvas.Children.Clear();
      Canvas.Children.Add(ZiskejTvar());
      AktualizujVypocet(new Ctverec(Canvas, Polomer, Vypln), Canvas, tb);
    }

    public override void VypocitejObvod()
    {
      this.Obvod = 2 * Math.PI * Polomer;
    }

    public override void VypocitejObsah()
    {
      this.Obsah = Math.PI * Polomer * Polomer;
    }
  }

  /// <summary>
  /// N-gon.
  /// </summary>
  class Nsten : Tvar
  {
    private double Vnejsi_polomer;
    private int Pocet_stran;
    public Nsten(Canvas canvas, int pocet_stran, double vnejsi_polomer, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Vnejsi_polomer = vnejsi_polomer;
      this.Pocet_stran = pocet_stran;
    }

    /// <summary>
    /// Draws icon.
    /// </summary>
    /// <param name="canvas">Canvas.</param>
    /// <param name="sp">StackPanel.</param>
    public static void VykresliIkonu(Canvas canvas, StackPanel sp)
    {
      Nsten nsten = new Nsten(canvas, 6, 15, Brushes.Black);

      sp.Children.Add(nsten.ZiskejTvar());
      sp.Children.Add(new TextBlock { Margin = new Thickness { Left = 10 }, Text = "nsten" });
    }

    /// <summary>
    /// Calculates width.
    /// </summary>
    /// <param name="tvar">Shape.</param>
    /// <returns>Width.</returns>
    public double VypocitejSirku(Polygon tvar)
    {
      double min = tvar.Points[0].X;
      double max = tvar.Points[0].X;

      for (int i = 1; i < tvar.Points.Count; i++)
      {
        if (max < tvar.Points[i].X)
        {
          max = tvar.Points[i].X;
        }
        else if (min > tvar.Points[i].X)
        {
          min = tvar.Points[i].X;
        }
      }

      return max - min;
    }

    /// <summary>
    /// Calculates height.
    /// </summary>
    /// <param name="tvar">Shape.</param>
    /// <returns>Height.</returns>
    public double VypocitejVysku(Polygon tvar)
    {
      double min = tvar.Points[0].Y;
      double max = tvar.Points[0].Y;

      for (int i = 1; i < tvar.Points.Count; i++)
      {
        if (max < tvar.Points[i].Y)
        {
          max = tvar.Points[i].Y;
        }
        else if (min > tvar.Points[i].Y)
        {
          min = tvar.Points[i].Y;
        }
      }

      return max - min;
    }

    /// <summary>
    /// Gets the shape.
    /// </summary>
    /// <returns>Shape.</returns>
    public Polygon ZiskejTvar()
    {
      Polygon tvar = new Polygon { Stroke = Brushes.Gray, Fill = Vypln };

      if (Pocet_stran < 1) { return tvar; }

      double x = 0;
      double y = 0;

      double angle = (2 * Math.PI) / Pocet_stran;
      double delka_strany = Math.Sin(angle / 2) * Vnejsi_polomer;

      for (int strana = 0; strana < Pocet_stran; strana++)
      {
        x += delka_strany * Math.Cos(angle * strana);
        y += delka_strany * Math.Sin(angle * strana);

        tvar.Points.Add(new Point(x, y));
      }

      double vyska = VypocitejVysku(tvar);

      double left = Canvas.ActualWidth / 2;
      Canvas.SetLeft(tvar, left - (tvar.Points[0].X / 2));
      double top = Canvas.ActualHeight / 2;
      Canvas.SetTop(tvar, top - (vyska / 2));

      return tvar;
    }

    public override void VykresliTvar(TextBlock tb)
    {
      Canvas.Children.Clear();
      Canvas.Children.Add(ZiskejTvar());
      AktualizujVypocet(new Nsten(Canvas, Pocet_stran, Vnejsi_polomer, Vypln), Canvas, tb);
    }

    public override void VypocitejObvod()
    {
      double polovicni_strana_mnohouhelniku = Vnejsi_polomer * Math.Sin(Math.PI / Pocet_stran);
      this.Obvod = polovicni_strana_mnohouhelniku * 2 * Pocet_stran;
    }

    public override void VypocitejObsah()
    {
      double vyska = Vnejsi_polomer * Math.Cos(Math.PI / Pocet_stran);
      double polovicni_strana_mnohouhelniku = Vnejsi_polomer * Math.Sin(Math.PI / Pocet_stran);
      double obsah_trojuhelniku = vyska * polovicni_strana_mnohouhelniku;

      this.Obsah = obsah_trojuhelniku * Pocet_stran;
    }
  }
}
