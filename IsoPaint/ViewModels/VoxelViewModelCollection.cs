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
	public class VoxelViewModelCollection : ViewModelCollection<List<Voxel>, VoxelViewModel, Voxel>,IComparer<Tuple<int,int,int>>
	{
		public VoxelViewModelCollection(ILogger Logger) : base(Logger)
		{
		}

		int IComparer<Tuple<int, int, int>>.Compare(Tuple<int, int, int> A, Tuple<int, int, int> B)
		{
			if ((A.Item3 == B.Item3) && (A.Item2 == B.Item2) && (A.Item1 == B.Item1)) return 0;
			if ((A.Item3 < B.Item3) && (A.Item2 < B.Item2) && (A.Item1 < B.Item1)) return -1;
			return 1;
		}

		protected override int OnGetInsertIndex(VoxelViewModel ViewModel)
		{
			VoxelViewModel item;

			for(int t=0;t<Count;t++)
			{
				item = this[t];
				if ((ViewModel.Z <= item.Z) && (ViewModel.Y <= item.Y) && (ViewModel.X >= item.X)) return t;
			}
			return base.OnGetInsertIndex(ViewModel);
		}
		

		protected override Voxel OnCreateModel()
		{
			return new Voxel();
		}

		protected override VoxelViewModel OnCreateViewModel(Voxel Model)
		{
			return new VoxelViewModel(Logger);
		}


		protected override bool OnAreModelsAreEquals(Voxel A, Voxel B)
		{
			return (A.X == B.X) && (A.Y == B.Y) && (A.Z == B.Z);
		}


		protected override IEnumerable<Voxel> OnLoadItems(List<Voxel> Model)
		{
			return Model.OrderBy((item) => new Tuple<int,int,int>(item.X,item.Y,item.Z),this );
		}

		

		protected override async Task OnAddModelAsync(Voxel Item, int Index)
		{
			Model.Insert(Index, Item);
			await Task.Yield();
		}

		protected override async Task OnEditModelAsync(Voxel Item, int Index)
		{
			await Task.Yield();
		}
		
		protected override async Task OnRemoveModelAsync(Voxel Item, int Index)
		{
			Model.RemoveAt(Index);
			await Task.Yield();
		}

		
	}
}
