using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serialize;

namespace Store
{
	[Serializable]
	struct Product
	{
		static public int CURRENT_ID = 0;

		public int Id;
		public string Name;
		public string Contractor;
		public DateTime DeliveryDate;
		public int SelfLifeDays;
		public int Balance;
	}
}
