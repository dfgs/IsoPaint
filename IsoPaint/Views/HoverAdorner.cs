using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace IsoPaint.Views
{
	public enum HoverFaces { Bottom, Left, Back, Top, Right, Front };


	public class HoverAdorner : Adorner
	{


		public static readonly DependencyProperty HoverXProperty = DependencyProperty.Register("HoverX", typeof(int), typeof(HoverAdorner),new FrameworkPropertyMetadata(0,FrameworkPropertyMetadataOptions.AffectsRender));
		public int HoverX
		{
			get { return (int)GetValue(HoverXProperty); }
			set { SetValue(HoverXProperty, value); }
		}

		public static readonly DependencyProperty HoverYProperty = DependencyProperty.Register("HoverY", typeof(int), typeof(HoverAdorner), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender));
		public int HoverY
		{
			get { return (int)GetValue(HoverYProperty); }
			set { SetValue(HoverYProperty, value); }
		}


		public static readonly DependencyProperty HoverZProperty = DependencyProperty.Register("HoverZ", typeof(int), typeof(HoverAdorner), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender));
		public int HoverZ
		{
			get { return (int)GetValue(HoverZProperty); }
			set { SetValue(HoverZProperty, value); }
		}
		public static readonly DependencyProperty HoverFaceProperty = DependencyProperty.Register("HoverFace", typeof(HoverFaces), typeof(HoverAdorner), new FrameworkPropertyMetadata(HoverFaces.Bottom, FrameworkPropertyMetadataOptions.AffectsRender));
		public HoverFaces HoverFace
		{
			get { return (HoverFaces)GetValue(HoverFaceProperty); }
			set { SetValue(HoverFaceProperty, value); }
		}



		public static readonly DependencyProperty ShowProperty = DependencyProperty.RegisterAttached("Show", typeof(bool), typeof(HoverAdorner), new PropertyMetadata(false, ShowPropertyChanged));
		public static readonly DependencyProperty XProperty = DependencyProperty.RegisterAttached("X", typeof(int), typeof(HoverAdorner), new PropertyMetadata(0, XPropertyChanged));
		public static readonly DependencyProperty YProperty = DependencyProperty.RegisterAttached("Y", typeof(int), typeof(HoverAdorner), new PropertyMetadata(0, YPropertyChanged));
		public static readonly DependencyProperty ZProperty = DependencyProperty.RegisterAttached("Z", typeof(int), typeof(HoverAdorner), new PropertyMetadata(0, ZPropertyChanged));

		public static readonly DependencyProperty AdornerProperty = DependencyProperty.RegisterAttached("Adorner", typeof(HoverAdorner), typeof(HoverAdorner), new FrameworkPropertyMetadata(null));
		public static readonly DependencyProperty FaceProperty = DependencyProperty.RegisterAttached("Face", typeof(HoverFaces), typeof(HoverAdorner), new PropertyMetadata(HoverFaces.Bottom,FacePropertyChanged));





		public HoverAdorner(UIElement adornedElement) : base(adornedElement)
		{
		}



		public static bool GetShow(UIElement Element)
		{
			return (bool)Element.GetValue(ShowProperty);
		}
		public static void SetShow(UIElement Element, bool Value)
		{
			Element.SetValue(ShowProperty, Value);
		}

		public static int GetX(UIElement Element)
		{
			return (int)Element.GetValue(XProperty);
		}
		public static void SetX(UIElement Element, int Value)
		{
			Element.SetValue(XProperty, Value);
		}
		public static int GetY(UIElement Element)
		{
			return (int)Element.GetValue(YProperty);
		}
		public static void SetY(UIElement Element, int Value)
		{
			Element.SetValue(YProperty, Value);
		}
		public static int GetZ(UIElement Element)
		{
			return (int)Element.GetValue(ZProperty);
		}
		public static void SetZ(UIElement Element, int Value)
		{
			Element.SetValue(ZProperty, Value);
		}
		public static HoverAdorner GetAdorner(UIElement Element)
		{
			return (HoverAdorner)Element.GetValue(AdornerProperty);
		}
		public static void SetAdorner(UIElement Element, HoverAdorner Value)
		{
			Element.SetValue(AdornerProperty, Value);
		}
		public static HoverFaces GetFace(UIElement Element)
		{
			return (HoverFaces)Element.GetValue(FaceProperty);
		}
		public static void SetFace(UIElement Element, HoverFaces Value)
		{
			Element.SetValue(FaceProperty, Value);
		}

		private static void ShowPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement visual;
			AdornerLayer adornerLayer;
			HoverAdorner adorner;

			visual = d as UIElement;
			if (visual == null) return;

			
			adornerLayer = AdornerLayer.GetAdornerLayer(visual);
			if (ValueType.Equals(e.NewValue , true))
			{
				adorner = GetAdorner(visual);
				if (adorner == null)
				{
					adorner=new HoverAdorner(visual);
					SetAdorner(visual, adorner);
				}
				adornerLayer.Add(adorner);
			}
			else
			{
				adorner = GetAdorner(visual);
				if (adorner != null) adornerLayer.Remove(adorner);
			}

		}

		private static void XPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement visual;
			HoverAdorner adorner;

			visual = d as UIElement;
			if (visual == null) return;

			adorner = GetAdorner(visual);
			if (adorner == null) return;
			adorner.HoverX = (int)e.NewValue;
		}
		private static void YPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement visual;
			HoverAdorner adorner;

			visual = d as UIElement;
			if (visual == null) return;

			adorner = GetAdorner(visual);
			if (adorner == null) return;
			adorner.HoverY = (int)e.NewValue;
		}
		private static void ZPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement visual;
			HoverAdorner adorner;

			visual = d as UIElement;
			if (visual == null) return;

			adorner = GetAdorner(visual);
			if (adorner == null) return;
			adorner.HoverZ = (int)e.NewValue;
		}
		private static void FacePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement visual;
			HoverAdorner adorner;

			visual = d as UIElement;
			if (visual == null) return;

			adorner = GetAdorner(visual);
			if (adorner == null) return;
			adorner.HoverFace = (HoverFaces)e.NewValue;
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			IsometricPanel panel;
			Point A, B, C, D;
			Pen hoverPen;

			base.OnRender(drawingContext);
			panel = AdornedElement as IsometricPanel;
			if (panel == null) return;

			

			hoverPen = new Pen(Brushes.Red, 1);

			if ((HoverX >= 0) && (HoverY >= 0) && (HoverZ >= 0) && (HoverX < panel.SizeX) && (HoverY < panel.SizeY) && (HoverZ < panel.SizeZ))
			{
				switch (HoverFace)
				{
					case HoverFaces.Bottom:
						A = panel.GetIsometricCoordinates(HoverX, HoverY, HoverZ);
						B = panel.GetIsometricCoordinates(HoverX + 1, HoverY, HoverZ);
						C = panel.GetIsometricCoordinates(HoverX + 1, HoverY + 1, HoverZ);
						D = panel.GetIsometricCoordinates(HoverX, HoverY + 1, HoverZ);
						break;
					case HoverFaces.Top:
						A = panel.GetIsometricCoordinates(HoverX, HoverY, HoverZ + 1);
						B = panel.GetIsometricCoordinates(HoverX + 1, HoverY, HoverZ + 1);
						C = panel.GetIsometricCoordinates(HoverX + 1, HoverY + 1, HoverZ + 1);
						D = panel.GetIsometricCoordinates(HoverX, HoverY + 1, HoverZ + 1);
						break;
					case HoverFaces.Left:
						A = panel.GetIsometricCoordinates(HoverX, HoverY, HoverZ);
						B = panel.GetIsometricCoordinates(HoverX + 1, HoverY, HoverZ);
						C = panel.GetIsometricCoordinates(HoverX + 1, HoverY, HoverZ + 1);
						D = panel.GetIsometricCoordinates(HoverX, HoverY, HoverZ + 1);
						break;
					case HoverFaces.Right:
						A = panel.GetIsometricCoordinates(HoverX, HoverY + 1, HoverZ);
						B = panel.GetIsometricCoordinates(HoverX + 1, HoverY + 1, HoverZ);
						C = panel.GetIsometricCoordinates(HoverX + 1, HoverY + 1, HoverZ + 1);
						D = panel.GetIsometricCoordinates(HoverX, HoverY + 1, HoverZ + 1);
						break;
					case HoverFaces.Back:
						A = panel.GetIsometricCoordinates(HoverX, HoverY, HoverZ);
						B = panel.GetIsometricCoordinates(HoverX, HoverY + 1, HoverZ);
						C = panel.GetIsometricCoordinates(HoverX, HoverY + 1, HoverZ + 1);
						D = panel.GetIsometricCoordinates(HoverX, HoverY, HoverZ + 1);
						break;
					default:
						A = panel.GetIsometricCoordinates(HoverX + 1, HoverY, HoverZ);
						B = panel.GetIsometricCoordinates(HoverX + 1, HoverY + 1, HoverZ);
						C = panel.GetIsometricCoordinates(HoverX + 1, HoverY + 1, HoverZ + 1);
						D = panel.GetIsometricCoordinates(HoverX + 1, HoverY, HoverZ + 1);
						break;
				}

				drawingContext.DrawLine(hoverPen, A, B);
				drawingContext.DrawLine(hoverPen, B, C);
				drawingContext.DrawLine(hoverPen, C, D);
				drawingContext.DrawLine(hoverPen, D, A);
			}

		}


	}
}
