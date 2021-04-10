using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace obrazce
{
  class Tvar
  {
    protected int Obvod;
    protected int Obsah;
    protected SolidColorBrush Vypln;
    protected Canvas Canvas;

    public Tvar(Canvas canvas, SolidColorBrush vypln)
    {
      this.Canvas = canvas;
      this.Vypln = vypln;
    }

    public static Canvas VykresliIkonu() { return new Canvas { }; }

    public void VykresliTvar() { }

    public int VypocitejObvod()
    {
      return this.Obvod;
    }

    public int VypocitejObsah()
    {
      return this.Obsah;
    }
  }

  class Trojuhelnik : Tvar
  {
    private int Strana_a;
    private int Strana_b;
    private int Strana_c;
    public Trojuhelnik(Canvas canvas, int strana_a, int strana_b, int strana_c, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Strana_a = strana_a;
      this.Strana_b = strana_b;
      this.Strana_c = strana_c;
      this.Canvas = canvas;
    }

    public static void VykresliIkonu(Canvas canvas)
    {
      Trojuhelnik trojuhelnik = new Trojuhelnik(canvas, 15, 15, 15, Brushes.Black);
      trojuhelnik.VykresliTvar();
    }

    public void VykresliTvar()
    {
      double left = (Canvas.ActualWidth - Canvas.ActualWidth) / 2;
      Canvas.SetLeft(Canvas, left);
      double top = (Canvas.ActualHeight - Canvas.ActualHeight) / 2;
      Canvas.SetTop(Canvas, top);

      double vyska = VypocitejVysku();
      double cx = Math.Sqrt((Strana_b * Strana_b) - (vyska * vyska));

      Point a = new Point(0, 0);
      Point b = new Point(Strana_a, 0);
      Point c = new Point(cx, -vyska);

      Polygon tvar = new Polygon { Stroke = Brushes.Gray, Fill = Vypln };

      tvar.Points.Add(a);
      tvar.Points.Add(b);
      tvar.Points.Add(c);

      Canvas.Children.Add(tvar);
      System.Diagnostics.Trace.WriteLine("volání");
    }

    public double VypocitejVysku()
    {
      double vyska = Math.Sqrt(4 * (Strana_c * Strana_c) * (Strana_a * Strana_a) - Math.Pow((Strana_a * Strana_a) + (Strana_c * Strana_c) - (Strana_b * Strana_b), 2)) / (Strana_a * 2);
      return vyska;
    }

    public int VypocitejObvod()
    {
      return Strana_a + Strana_b + Strana_c;
    }

    public int VypocitejObsah()
    {
      // https://cs.wikipedia.org/wiki/Heron%C5%AFv_vzorec
      float s = VypocitejObvod() / 2;
      return (int)Math.Round(Math.Sqrt(s * (s - Strana_a) * (s - Strana_b) * (s - Strana_c)));
    }
  }

  class Ctverec : Tvar
  {
    private int Strana;
    public Ctverec(Canvas canvas, int strana, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Strana = strana;
    }
    public static void VykresliIkonu(Canvas canvas)
    {
      Ctverec ctverec = new Ctverec(canvas, 10, Brushes.Black);
      ctverec.VykresliTvar();
    }

    public void VykresliTvar()
    {
      double left = (Canvas.ActualWidth - Canvas.ActualWidth) / 2;
      Canvas.SetLeft(Canvas, left);
      double top = (Canvas.ActualHeight - Canvas.ActualHeight) / 2;
      Canvas.SetTop(Canvas, top);

      Rectangle tvar = new Rectangle { Width = Strana, Height = Strana, Stroke = Brushes.Gray, Fill = Vypln };

      Canvas.Children.Add(tvar);
    }

    public int VypocitejObvod()
    {
      return 4 * Strana;
    }

    public int VypocitejObsah()
    {
      return Strana * Strana;
    }
  }

  class Obdelnik : Tvar
  {
    private int Strana_a;
    private int Strana_b;
    public Obdelnik(Canvas canvas, int strana_a, int strana_b, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Strana_a = strana_a;
      this.Strana_b = strana_b;
    }

    public static void VykresliIkonu(Canvas canvas)
    {
      Obdelnik obdelnik = new Obdelnik(canvas, 10, 20, Brushes.Black);
      obdelnik.VykresliTvar();
    }

    public void VykresliTvar()
    {
      double left = (Canvas.ActualWidth - Canvas.ActualWidth) / 2;
      Canvas.SetLeft(Canvas, left);
      double top = (Canvas.ActualHeight - Canvas.ActualHeight) / 2;
      Canvas.SetTop(Canvas, top);

      Rectangle tvar = new Rectangle { Width = Strana_a, Height = Strana_b, Stroke = Brushes.Gray, Fill = Vypln };

      Canvas.Children.Add(tvar);
    }

    public int VypocitejObvod()
    {
      return 2 * (Strana_a + Strana_b);
    }

    public int VypocitejObsah()
    {
      return Strana_a * Strana_b;
    }
  }

  class Kruh : Tvar
  {
    private int Polomer;
    public Kruh(Canvas canvas, int polomer, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Polomer = polomer;
    }

    public static void VykresliIkonu(Canvas canvas)
    {
      Kruh kruh = new Kruh(canvas, 15, Brushes.Black);
      kruh.VykresliTvar();
    }

    public void VykresliTvar()
    {
      double left = (Canvas.ActualWidth - Canvas.ActualWidth) / 2;
      Canvas.SetLeft(Canvas, left);
      double top = (Canvas.ActualHeight - Canvas.ActualHeight) / 2;
      Canvas.SetTop(Canvas, top);

      Ellipse tvar = new Ellipse { Width = Polomer, Height = Polomer, Stroke = Brushes.Gray, Fill = Vypln };

      Canvas.Children.Add(tvar);
    }

    public double VypocitejObvod()
    {
      return 2 * Math.PI * Polomer;
    }

    public double VypocitejObsah()
    {
      return Math.PI * Polomer * Polomer;
    }
  }

  class Nsten : Tvar
  {
    private int Vnejsi_polomer;
    private int Pocet_stran;
    public Nsten(Canvas canvas, int pocet_stran, int vnejsi_polomer, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Vnejsi_polomer = vnejsi_polomer;
      this.Pocet_stran = pocet_stran;
    }

    public static void VykresliIkonu(Canvas canvas)
    {
      Nsten nsten = new Nsten(canvas, 10, 5, Brushes.Black);
      nsten.VykresliTvar();
    }

    public void VykresliTvar()
    {
      double left = (Canvas.ActualWidth - Canvas.ActualWidth) / 2;
      Canvas.SetLeft(Canvas, left);
      double top = (Canvas.ActualHeight - Canvas.ActualHeight) / 2;
      Canvas.SetTop(Canvas, top);

      Polygon tvar = new Polygon { Stroke = Brushes.Gray, Fill = Vypln };

      int x = 0;
      int y = 0;

      tvar.Points.Add(new Point(x + Vnejsi_polomer * Math.Cos(0), y + Vnejsi_polomer + Math.Sin(0)));

      for (int strana = 0; strana < Pocet_stran; strana++)
      {
        tvar.Points.Add(new Point(x + Vnejsi_polomer * Math.Cos(strana * 2 * Math.PI / 6), y + Vnejsi_polomer + Math.Sin(strana * Math.PI / 6)));
        x += 360 / Pocet_stran;
        y += 360 / Pocet_stran;
      }

      Canvas.Children.Add(tvar);
    }
  }
}
