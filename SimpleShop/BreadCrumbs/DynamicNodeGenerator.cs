using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider;
using SimpleShop.Data.Models;

namespace SimpleShop.BreadCrumbs
{
	public class DynamicNodeGenerator : DynamicNodeProviderBase
	{

		public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
		{
			using (var ctx = new ApplicationDbContext())
			{
				var products = ctx.Products.ToList();
				foreach (var product in products)
				{
					var dynamicNode = new DynamicNode("ProductID_" + product.ProductId, product.Name);
					dynamicNode.RouteValues.Add("id", product.ProductId);

					yield return dynamicNode;
				}
			}
		}
	}
}