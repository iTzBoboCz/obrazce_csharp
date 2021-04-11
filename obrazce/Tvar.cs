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
    protected double Obvod;
    protected double Obsah;
    protected SolidColorBrush Vypln;
    protected Canvas Canvas;

    public Tvar(Canvas canvas, SolidColorBrush vypln)
    {
      this.Canvas = canvas;
      this.Vypln = vypln;
    }

    public virtual void VykresliTvar() { }

    public virtual void VypocitejObvod() { }

    public virtual void VypocitejObsah() { }

    public void AktualizujVypocet(Tvar tvar, Canvas canvas, TextBlock tb)
    {
      tb.Text = $"obvod: {Obvod} | obsah: {Obsah}";
    }
  }

  class Trojuhelnik : Tvar
  {
    private double Strana_a;
    private double Strana_b;
    private double Strana_c;
    public Trojuhelnik(Canvas canvas, double strana_a, double strana_b, double strana_c, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Strana_a = strana_a;
      this.Strana_b = strana_b;
      this.Strana_c = strana_c;
      this.Canvas = canvas;

      //AktualizujVypocet(new Trojuhelnik(canvas, Strana_a, Strana_b, Strana_c, vypln), Canvas, );
    }

    public static void VykresliIkonu(Canvas canvas, StackPanel sp)
    {
      Trojuhelnik trojuhelnik = new Trojuhelnik(canvas, 15, 15, 15, Brushes.Black);

      sp.Children.Add(trojuhelnik.ZiskejTvar());
      sp.Children.Add(new TextBlock { Margin = new Thickness { Left = 5 }, Text = "trojůhelník" });
    }

    public Polygon ZiskejTvar()
    {
      double vyska = VypocitejVysku();
      double cx = Math.Sqrt((Strana_b * Strana_b) - (vyska * vyska));

      Point a = new Point(0, vyska);
      Point b = new Point(Strana_a, vyska);
      Point c = new Point(cx, 0);

      Polygon tvar = new Polygon { Stroke = Brushes.Gray, Fill = Vypln };

      tvar.Points.Add(a);
      tvar.Points.Add(b);
      tvar.Points.Add(c);

      return tvar;
    }

    public override void VykresliTvar()
    {
      Polygon tvar = ZiskejTvar();

      double left = Canvas.ActualWidth / 2;
      Canvas.SetLeft(tvar, left);
      double top = Canvas.ActualHeight / 2;
      Canvas.SetTop(tvar, top);

      Canvas.Children.Clear();
      Canvas.Children.Add(tvar);
    }

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

  class Ctverec : Tvar
  {
    private double Strana;
    public Ctverec(Canvas canvas, double strana, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Strana = strana;
    }
    public static void VykresliIkonu(Canvas canvas, StackPanel sp)
    {
      Ctverec ctverec = new Ctverec(canvas, 10, Brushes.Black);

      sp.Children.Add(ctverec.ZiskejTvar());
      sp.Children.Add(new TextBlock { Margin = new Thickness { Left = 5 }, Text = "čtverec" });
    }

    public Rectangle ZiskejTvar()
    {
      Rectangle tvar = new Rectangle { Width = Strana, Height = Strana, Stroke = Brushes.Gray, Fill = Vypln };

      return tvar;
    }

    public override void VykresliTvar()
    {
      double left = (Canvas.ActualWidth - Canvas.ActualWidth) / 2;
      Canvas.SetLeft(Canvas, left);
      double top = (Canvas.ActualHeight - Canvas.ActualHeight) / 2;
      Canvas.SetTop(Canvas, top);

      Canvas.Children.Clear();
      Canvas.Children.Add(ZiskejTvar());
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

    public static void VykresliIkonu(Canvas canvas, StackPanel sp)
    {
      Obdelnik obdelnik = new Obdelnik(canvas, 10, 20, Brushes.Black);

      sp.Children.Add(obdelnik.ZiskejTvar());
      sp.Children.Add(new TextBlock { Margin = new Thickness { Left = 5 }, Text = "obdélník" });
    }

    public Rectangle ZiskejTvar()
    {
      Rectangle tvar = new Rectangle { Width = Strana_a, Height = Strana_b, Stroke = Brushes.Gray, Fill = Vypln };

      return tvar;
    }

    public override void VykresliTvar()
    {
      double left = (Canvas.ActualWidth - Canvas.ActualWidth) / 2;
      Canvas.SetLeft(Canvas, left);
      double top = (Canvas.ActualHeight - Canvas.ActualHeight) / 2;
      Canvas.SetTop(Canvas, top);

      Canvas.Children.Clear();
      Canvas.Children.Add(ZiskejTvar());
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

  class Kruh : Tvar
  {
    private double Polomer;
    public Kruh(Canvas canvas, double polomer, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Polomer = polomer;
    }

    public static void VykresliIkonu(Canvas canvas, StackPanel sp)
    {
      Kruh kruh = new Kruh(canvas, 15, Brushes.Black);
      kruh.VykresliTvar();

      sp.Children.Add(kruh.ZiskejTvar());
      sp.Children.Add(new TextBlock { Margin = new Thickness { Left = 5 }, Text = "kruh" });
    }

    public Ellipse ZiskejTvar()
    {
      Ellipse tvar = new Ellipse { Width = Polomer, Height = Polomer, Stroke = Brushes.Gray, Fill = Vypln };

      return tvar;
    }

    public override void VykresliTvar()
    {
      double left = (Canvas.ActualWidth - Canvas.ActualWidth) / 2;
      Canvas.SetLeft(Canvas, left);
      double top = (Canvas.ActualHeight - Canvas.ActualHeight) / 2;
      Canvas.SetTop(Canvas, top);

      Canvas.Children.Clear();
      Canvas.Children.Add(ZiskejTvar());
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

  class Nsten : Tvar
  {
    private double Vnejsi_polomer;
    private int Pocet_stran;
    public Nsten(Canvas canvas, int pocet_stran, double vnejsi_polomer, SolidColorBrush vypln) : base(canvas, vypln)
    {
      this.Vnejsi_polomer = vnejsi_polomer;
      this.Pocet_stran = pocet_stran;
    }

    public static void VykresliIkonu(Canvas canvas, StackPanel sp)
    {
      Nsten nsten = new Nsten(canvas, 6, 15, Brushes.Black);

      sp.Children.Add(nsten.ZiskejTvar());
      sp.Children.Add(new TextBlock { Margin = new Thickness { Left = 10 }, Text = "nsten" });
    }

    public Polygon ZiskejTvar()
    {
      Polygon tvar = new Polygon { Stroke = Brushes.Gray, Fill = Vypln };

      double x = Vnejsi_polomer / 3;
      double y = Vnejsi_polomer / 3;

      double angle = 2 * Math.PI / Pocet_stran;
      double delka_strany = Math.Sin(angle / 2) * Vnejsi_polomer;

      for (int strana = 0; strana < Pocet_stran; strana++)
      {
        x += delka_strany * Math.Cos(angle * strana);
        y += delka_strany * Math.Sin(angle * strana);

        tvar.Points.Add(new Point(x, y));
      }

      return tvar;
    }

    public override void VykresliTvar()
    {
      Canvas.Children.Clear();
      Canvas.Children.Add(ZiskejTvar());
    }

    public override void VypocitejObvod()
    {
      this.Obvod = 2 * Math.PI * Vnejsi_polomer;
    }

    public override void VypocitejObsah()
    {
      this.Obsah = Math.PI * Vnejsi_polomer * Vnejsi_polomer;
    }
  }
}
