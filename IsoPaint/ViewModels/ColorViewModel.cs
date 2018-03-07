using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsoPaint.Models;
using LogLib;
using ViewModelLib;

namespace IsoPaint.ViewModels
{
	public class ColorViewModel : ViewModel<Color>
	{
		public System.Windows.Media.Color Value
		{
			get { return System.Windows.Media.Color.FromArgb(Model.A, Model.R, Model.G, Model.B); }
			set { Model.A = value.A;Model.R = value.R; Model.G = value.G; Model.B = value.B; OnPropertyChanged(); OnPropertyChanged("Brush"); }
		}

		private Cache<System.Windows.Media.Brush> brushProperty;
		public System.Windows.Media.Brush Brush
		{
			get { return brushProperty.Value; }
		}


		public ColorViewModel(ILogger Logger) : base(Logger)
		{
			brushProperty = new Cache<System.Windows.Media.Brush>(() => { return new System.Windows.Media.SolidColorBrush(Value); },this, "Value");
		}
	}
}
