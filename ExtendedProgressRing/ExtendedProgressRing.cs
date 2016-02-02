using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace ExtendedProgressRing
{
    public sealed class ExtendedProgressRing : Control
    {

        public Brush RingForeground
        {
            get { return (Brush)GetValue(ExtendedProgressRing.RingForegroundProperty); }
            set { SetValue(ExtendedProgressRing.RingForegroundProperty, value); }
        }
        public static Brush GetRingForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(RingForegroundProperty);
        }

        public static void SetRingForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(RingForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for RingForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RingForegroundProperty =
            DependencyProperty.RegisterAttached("RingForeground", typeof(Brush), typeof(ExtendedProgressRing), new PropertyMetadata(new SolidColorBrush(Colors.White)));


        public Brush RingBackground
        {
            get { return (Brush)GetValue(ExtendedProgressRing.RingBackgroundProperty); }
            set { SetValue(ExtendedProgressRing.RingBackgroundProperty, value); }
        }
        public static Brush GetRingBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(RingBackgroundProperty);
        }

        public static void SetRingBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(RingBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for RingBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RingBackgroundProperty =
            DependencyProperty.RegisterAttached("RingBackground", typeof(Brush), typeof(ExtendedProgressRing), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));


        public double Thickness
        {
            get { return Convert.ToDouble(GetValue(ExtendedProgressRing.ThicknessProperty)); }
            set { SetValue(ExtendedProgressRing.ThicknessProperty, value); }
        }
        public static double GetThickness(DependencyObject obj)
        {
            return (double)obj.GetValue(ThicknessProperty);
        }
        public static void SetThickness(DependencyObject obj, double value)
        {
            obj.SetValue(ThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for Thickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.RegisterAttached("Thickness", typeof(double), typeof(ExtendedProgressRing), new PropertyMetadata(5));


        public double Value
        {
            get { return Convert.ToDouble(GetValue(ExtendedProgressRing.ValueProperty)); }
            set { SetValue(ExtendedProgressRing.ValueProperty, value); }
        }
        public static double GetValue(DependencyObject obj)
        {
            return (double)obj.GetValue(ValueProperty);
        }

        public static void SetValue(DependencyObject obj, double value)
        {
            obj.SetValue(ValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(double), typeof(ExtendedProgressRing), new PropertyMetadata(0,OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as ExtendedProgressRing;

            var pathRoot = ctrl.GetTemplateChild("foregroundPath") as Path;
            var pathBackground = ctrl.GetTemplateChild("backgroundPath") as Path;
            if (pathRoot == null || pathBackground == null) return;
            ctrl.DrawControl();
        }


        public ExtendedProgressRing()
        {
            this.DefaultStyleKey = typeof(ExtendedProgressRing);
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;
            FontSize = 65;
            this.Loaded += ExtendedProgressRing_Loaded;
            this.SizeChanged += ExtendedProgressRing_SizeChanged;
        }

        void ExtendedProgressRing_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawControl();
        }

        void ExtendedProgressRing_Loaded(object sender, RoutedEventArgs e)
        {
            DrawControl();
        }
        void DrawControl()
        {
            var size = Math.Min(this.RenderSize.Height, this.RenderSize.Width);
            var pathRoot = GetTemplateChild("foregroundPath") as Path;
            var pathBackground = GetTemplateChild("backgroundPath") as Path;
            var strokeThickness = this.Thickness;
            var angle = Value * 360;
            var radius = size / 2 - strokeThickness;
            pathRoot.Width = radius * 2 + strokeThickness;
            pathRoot.Height = radius * 2 + strokeThickness;
            pathRoot.Margin = new Thickness(strokeThickness, strokeThickness, 0, 0);
            pathBackground.Width = radius * 2 + strokeThickness;
            pathBackground.Height = radius * 2 + strokeThickness;
            pathBackground.Margin = new Thickness(strokeThickness, strokeThickness, 0, 0);

            RenderBackground(size);
            RenderArc(size);
        }
        void RenderBackground(double height)
        {
            var strokeThickness = this.Thickness;
            var radius = height / 2 - strokeThickness;
            var pathFigure = GetTemplateChild("backgroundPathFigure") as PathFigure;
            pathFigure.StartPoint = new Point(radius, 0);
            var arcSegment = GetTemplateChild("backgroundArcSegment") as ArcSegment;
            arcSegment.Point = new Point(radius - 0.001, 0);
            arcSegment.Size = new Size(radius, radius);
        }

        public void RenderArc(double height)
        {
            var strokeThickness = this.Thickness;
            var angle = Value * 360/100;
            var radius = height / 2 - strokeThickness;

            Point startPoint = new Point(radius, 0);
            Point endPoint = ComputeCartesianCoordinate(angle, radius);
            endPoint.X += radius;
            endPoint.Y += radius;

            bool largeArc = angle > 180.0;

            Size outerArcSize = new Size(radius, radius);

            var pathFigure = GetTemplateChild("foregroundPathFigure") as PathFigure;
            pathFigure.StartPoint = startPoint;

            //if (true || startPoint.X == Math.Round(endPoint.X) && startPoint.Y == Math.Round(endPoint.Y))
                endPoint.X -= Value == 0 ? 0 : 0.0001;
            
            var pathRoot = GetTemplateChild("foregroundPath") as Path;
            pathRoot.StrokeThickness = strokeThickness;
            var arcSegment = GetTemplateChild("foregroundArcSegment") as ArcSegment;
            arcSegment.Point = endPoint;
            arcSegment.Size = outerArcSize;
            arcSegment.IsLargeArc = largeArc;
        }

        private Point ComputeCartesianCoordinate(double angle, double radius)
        {
            double angleRad = (Math.PI / 180.0) * (angle - 90);
            double x = radius * Math.Cos(angleRad);
            double y = radius * Math.Sin(angleRad);
            return new Point(x, y);
        }
    }
}
