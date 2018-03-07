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
	public class PaletteViewModel : ViewModel<Palette>
	{
		
		public ColorViewModelCollection Colors
		{
			get;
			private set;
		}

		public PaletteViewModel(ILogger Logger) : base(Logger)
		{
			Colors = new ColorViewModelCollection(Logger);
		}

		protected override async Task OnLoadedAsync(Palette Model)
		{
			await Colors.LoadAsync(Model.Colors);
		}


	}
}
