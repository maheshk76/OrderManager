CREATE proc GetOrders
    @search nvarchar(max) ='',
    @custId int=0,
    @cityId int=0,
    @sdate nvarchar(max)='',
    @edate nvarchar(max)='',
	@pagesize int = 5,
	@pagenum int = 1,
	@sortBy nvarchar(max),
	@sortOrder nvarchar(5),
	@possiblerows int output
	as
	Begin
    set NoCount on;
    DECLARE @SkipRows int = (@pagenum - 1) * @pagesize;

	--a variable that stores total records without pagination--
	select @possiblerows= sum(t1.CO)
from(
	select  COUNT(distinct o.CustomerCode) as CO
	from Orders o 
	inner join Customer c  on o.CustomerCode=c.CustomerCode
	inner join City ci on c.CustomerCity=ci.CityId
	inner join Agents ag on ag.AgentCode=c.AgentCode
	where 
	(@search='' or c.FirstName like '%'+@search+'%' or ci.CityName like '%'+@search+'%' or c.LastName like '%'+@search+'%')
	and (@cityId=0 or ci.CityId=@cityId)
	and (@custId=0 or c.CustomerCode=@custId)
	and (@sdate=''  or o.OrderDate>=@sdate)
	and (@edate=''  or o.OrderDate<=@edate)
	group by o.OrderDate,o.CustomerCode,c.FirstName,c.LastName,ci.CityName,ag.AgentName) as t1

select SUM(o.OrderAmount) as OrderAmount,SUM(o.AdvanceAmount) AdvanceAmount,o.CustomerCode,o.OrderDate,
c.FirstName+' '+c.LastName as FullName,ci.CityName as City,ag.AgentName,COUNT(o.OrderNum) as OrderPerDate
from Orders o 
inner join Customer c  on o.CustomerCode=c.CustomerCode
inner join City ci on c.CustomerCity=ci.CityId
inner join Agents ag on ag.AgentCode=c.AgentCode
where 
	(@search='' or c.FirstName like '%'+@search+'%' or ci.CityName like '%'+@search+'%' or c.LastName like '%'+@search+'%')
	and (@cityId=0 or ci.CityId=@cityId)
	and (@custId=0 or c.CustomerCode=@custId)
	and (@sdate=''  or o.OrderDate>=@sdate)
	and (@edate=''  or o.OrderDate<=@edate)

group by o.OrderDate,o.CustomerCode,c.FirstName,c.LastName,ci.CityName,ag.AgentName
order by 
case when (@sortBy='fname' and @sortOrder='ASC') then c.FirstName 
end asc,
case when (@sortBy='fname' and @sortOrder='DESC') then c.FirstName 
end desc,
case when (@sortBy='oamt' and @sortOrder='ASC') then SUM(o.OrderAmount)
end asc,
case when (@sortBy='oamt' and @sortOrder='DESC') then SUM(o.OrderAmount)
end desc,
case when (@sortBy='aamt' and @sortOrder='ASC') then SUM(o.AdvanceAmount)
end asc,
case when (@sortBy='aamt' and @sortOrder='DESC') then SUM(o.AdvanceAmount)
end desc,
case when (@sortBy='odate' and @sortOrder='ASC') then o.OrderDate 
end asc,
case when (@sortBy='odate' and @sortOrder='DESC') then o.OrderDate
end desc,
case when (@sortBy='ct' and @sortOrder='ASC') then ci.CityName 
end asc,
case when (@sortBy='ct' and @sortOrder='DESC') then ci.CityName
end desc,
case when (@sortBy='aname' and @sortOrder='ASC') then ag.AgentName 
end asc,
case when (@sortBy='aname' and @sortOrder='DESC') then ag.AgentName
end desc
OFFSET @SkipRows ROWS 
    FETCH NEXT @pagesize ROWS ONLY

return

End