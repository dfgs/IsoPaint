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
		private DocumentViewModel document;

		public VoxelViewModelCollection(ILogger Logger, DocumentViewModel Document) : base(Logger)
		{
			this.document = Document;
		}

		public bool ContainsVoxelAtPos(int X, int Y, int Z)
		{
			return Items.FirstOrDefault(item => (item.X == X) && (item.Y == Y) && (item.Z == Z)) != null;
		}
		public VoxelViewModel GetVoxelAtPos(int X, int Y, int Z)
		{
			return Items.FirstOrDefault(item => (item.X == X) && (item.Y == Y) && (item.Z == Z));
		}

		int IComparer<Tuple<int, int, int>>.Compare(Tuple<int, int, int> A, Tuple<int, int, int> B)
		{
			if (A.Item3 < B.Item3) return -1;
			if (A.Item3 > B.Item3) return 1;
			if (A.Item2 < B.Item2) return -1;
			if (A.Item2 > B.Item2) return 1;
			if (A.Item1 < B.Item1) return -1;
			if (A.Item1 > B.Item1) return 1;

			return 0;
		}

		protected override int OnGetInsertIndex(VoxelViewModel ViewModel)
		{
			VoxelViewModel item;

			for(int t=0;t<Count;t++)
			{
				item = this[t];
				if ((ViewModel.Z <= item.Z) && (ViewModel.Y <= item.Y) && (ViewModel.X <= item.X)) return t;
			}
			return base.OnGetInsertIndex(ViewModel);
		}
		

		protected override Voxel OnCreateModel()
		{
			return new Voxel();
		}

		protected override VoxelViewModel OnCreateViewModel(Voxel Model)
		{
			return new VoxelViewModel(Logger,document);
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
