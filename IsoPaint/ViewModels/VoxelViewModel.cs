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

		public VoxelViewModel(ILogger Logger) : base(Logger)
		{
		}
	}



}
