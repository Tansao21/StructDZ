using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store;


namespace Serialize
{
	[Serializable]
	struct ProductsList
	{
		public int CURRENT_ID;

		public Product[] products;
	}
}
