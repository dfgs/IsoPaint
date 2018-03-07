using IsoPaint.Models;
using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLib;

namespace IsoPaint.ViewModels
{
	public class ColorViewModelCollection : ListViewModelCollection< ColorViewModel, Color>
	{
		public ColorViewModelCollection(ILogger Logger) : base(Logger)
		{
		}
		protected override bool OnAreModelsAreEquals(Color A, Color B)
		{
			return (A.A == B.A) && (A.R == B.R) && (A.G == B.G) && (A.B == B.B) ;
		}
		protected override Color OnCreateModel()
		{
			return new Color();
		}

		protected override ColorViewModel OnCreateViewModel(Color Model)
		{
			return new ColorViewModel(Logger);
		}

		protected override IEnumerable<Color> OnLoadItems(List<Color> Model)
		{
			return Model;
		}
	}
}
