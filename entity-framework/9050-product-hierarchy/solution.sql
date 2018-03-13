-- Process product hierarchy
with Result (ParentProductID, ChildProductID, Amount, [Level]) as (
    -- Get root product (ID = 902)
    select ph.ParentProductID, ph.ChildProductID, ph.Amount, 1 as [Level]
        from ProductHierarchy ph 
        where ParentProductID = 902
    union all
	-- Get children of already inserted parents
    select ph.ParentProductID, ph.ChildProductID, ph.Amount * Result.Amount, Result.[Level] + 1
		from ProductHierarchy ph 
        inner join Result on ph.ParentProductID = Result.ChildProductID
),
-- Aggregate amount and costs per product
Costs (ProductID, Amount, Costs) as (
	select r.ChildProductID, sum(r.Amount) as Amount, sum(r.Amount * p.UnitPrice) as Costs
	from Result r inner join Product p on r.ChildProductID = p.ID
	group by r.ChildProductID
),
-- Calculate rebates
Rebates (ProductID, Amount, Costs, MinQuantity, Rebate, RebatedCosts) as (
	select c.ProductID, c.Amount, c.costs, r.MinQuantity, r.RebatePerc, 
		case when c.Amount >= r.MinQuantity then c.costs * (1 - coalesce(r.RebatePerc, 0)) else c.costs end
	from Costs c left join Rebate r on c.ProductID = r.ProductID
)
-- Aggregate total costs
select sum(RebatedCosts) from Rebates
where Costs is not null
