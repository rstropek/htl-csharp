using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProductHierarchy.Logic
{
    public class ProductPriceCalculator
    {
        #region Helper classes for intermediate results
        private class ProductAmount
        {
            public int ChildProductID { get; set; }

            public int Amount { get; set; }

            public decimal Costs { get; set; }
        }

        private class ProductHierarchyData : ProductAmount
        {
            public int ParentProductID { get; set; }
        }
        #endregion

        public async Task<decimal> CalculateProductPriceAsync(ProductHierarchyContext context)
        {
            // Read product hierarchy into memory and make it a dictionary so that we
            // can efficiently look for all child products for a given parent product.
            var products = await context.ProductHierarchies
                .Select(ph => new ProductHierarchyData
                {
                    ParentProductID = ph.ParentProductID,
                    ChildProductID = ph.ChildProductID,
                    Amount = ph.Amount,
                    Costs = ph.Amount * ph.ChildProduct.UnitPrice ?? 0
                })
                .GroupBy(p => p.ParentProductID)
                .ToDictionaryAsync(p => p.Key, p => p.Select(pi => pi).ToList());

            // Traverse product hierarchy recursively and aggregate amount and costs 
            // per unique product
            var productHierarchy = GetSubHierarchy(products, 902)
                .GroupBy(p => p.ChildProductID)
                .Select(p => new ProductAmount
                {
                    ChildProductID = p.Key,
                    Amount = p.Sum(pi => pi.Amount),
                    Costs = p.Sum(pi => pi.Costs)
                })
                .ToList();

            // Get all rebates into memory and make it a dictionary so that we
            // can efficiently look for rebates for a given product.
            var rebates = await context.Rebates.ToDictionaryAsync(r => r.ProductID);

            // Loop over all results and reduce rebate if applicable
            productHierarchy.ForEach(ph =>
            {
                if (rebates.TryGetValue(ph.ChildProductID, out var rebate) 
                    && ph.Amount >= rebate.MinQuantity)
                {
                    ph.Costs = Math.Round(ph.Costs * (1 - rebate.RebatePerc), 2);
                }
            });

            // Calculate total costs
            return productHierarchy.Sum(ph => ph.Costs);
        }

        private IEnumerable<ProductHierarchyData> GetSubHierarchy(
            Dictionary<int, List<ProductHierarchyData>> products, 
            int parentProductID)
        {
            // Project a given parent product to all its child products
            return products[parentProductID].SelectMany(p =>
            {
                // Result record representing the current element
                var currentProductResult = new List<ProductHierarchyData> {
                    new ProductHierarchyData
                    {
                        ParentProductID = p.ParentProductID,
                        ChildProductID = p.ChildProductID,
                        Amount = p.Amount,
                        Costs = p.Costs
                    }
                };

                if (products.ContainsKey(p.ChildProductID))
                {
                    // If the product has children, get children recursively
                    return currentProductResult
                        .Concat(GetSubHierarchy(products, p.ChildProductID)
                        .Select(pi => new ProductHierarchyData
                        {
                            ParentProductID = pi.ParentProductID,
                            ChildProductID = pi.ChildProductID,
                            Amount = p.Amount * pi.Amount,
                            Costs = p.Amount * pi.Costs
                        }));
                }
                else
                {
                    // Product has no children -> no recursion necessary
                    return currentProductResult;
                }
            });
        }
    }
}
