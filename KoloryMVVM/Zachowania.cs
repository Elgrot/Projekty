using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KoloryMVVM
{
    public class ZamknięcieOknaPoNaciśnięciuKlawisza : Behavior<Window>
    {
        public Key Klawisz { get; set; }

        protected override void OnAttached()
        {
            Window window = this.AssociatedObject;
            if(window != null) window.PreviewKeyDown += window_PreviewKeyDown;
        }

        private void window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Window window = (Window)sender;
            if (e.Key == Klawisz) window.Close();
        }
    }

    public class PrzyciskZamykającyOkno : Behavior<Window>
    {
        public static readonly DependencyProperty PrzyciskProperty = 
            DependencyProperty.Register(
                "Przycisk", 
                typeof(Button), 
                typeof(PrzyciskZamykającyOkno), 
                new PropertyMetadata(null, przyciskZmieniony));

        public Button Przycisk
        {
            get { return (Button)GetValue(PrzyciskProperty);}
            set { SetValue(PrzyciskProperty, value);}
        }
        private static void przyciskZmieniony(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = (d as PrzyciskZamykającyOkno).AssociatedObject;
            RoutedEventHandler button_Click = (object sender, RoutedEventArgs _e) => { window.Close(); };
            if (e.OldValue != null) ((Button)e.OldValue).Click -= button_Click;
            if (e.NewValue != null) ((Button)e.NewValue).Click += button_Click;
        }
    }

    public static class KlawiszWyłączBehavior
    {
        public static Key GetKlawisz(DependencyObject d)
        {
            return (Key)d.GetValue(KlawiszProperty);
        }

        public static void SetKlawisz(DependencyObject d, Key value)
        {
            d.SetValue(KlawiszProperty, value);
        }

        public static readonly DependencyProperty KlawiszProperty =
            DependencyProperty.RegisterAttached(
                "Klawisz",
                typeof(Key),
                typeof(KlawiszWyłączBehavior),
                new PropertyMetadata(Key.None, KlawiszZmieniony));

        private static void KlawiszZmieniony(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Key klawisz = (Key)e.NewValue;
            if(d is Window)
            {
                (d as Window).PreviewKeyDown +=
                    (object sender, KeyEventArgs _e) =>
                    {
                        if (_e.Key == klawisz)
                            (sender as Window).Close();
                    };
            }
            else
            {
                (d as UIElement).PreviewKeyDown +=
                    (object sender, KeyEventArgs _e) =>
                    {
                        if (_e.Key == klawisz)
                            (sender as UIElement).IsEnabled = false;
                    };
            }
        }
    }
}
