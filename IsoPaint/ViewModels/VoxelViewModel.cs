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
	public class VoxelViewModel : ViewModel<Voxel>
	{
		public int X
		{
			get { return Model.X; }
			set { Model.X = value; OnPropertyChanged(); }
		}
		public int Y
		{
			get { return Model.Y; }
			set { Model.Y = value; OnPropertyChanged(); }
		}
		public int Z
		{
			get { return Model.Z; }
			set { Model.Z = value; OnPropertyChanged(); }
		}

		public int ColorID
		{
			get { return Model.ColorID; }
			set { Model.ColorID = value;OnPropertyChanged();OnPropertyChanged("Color"); }
		}

		private Cache<ColorViewModel> colorCache;
		public ColorViewModel Color
		{
			get { return colorCache.Value; }
		}

		public VoxelViewModel(ILogger Logger,DocumentViewModel Document) : base(Logger)
		{
			colorCache = new Cache<ColorViewModel>(() => {return Document.Palette.Colors.FirstOrDefault(item => item.ID == ColorID); },this,"ColorID");
		}


	}



}
