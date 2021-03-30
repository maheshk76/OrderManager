CREATE proc GetOrders
    @search nvarchar(max) ='',
    @custId int=0,
    @cityId int=0,
    @sdate nvarchar(max)='',
    @edate nvarchar(max)='',
	@pagesize int = 5,
	@pagenum int = 1,
	@possiblerows int output
	as
	Begin
    set NoCount on;
    DECLARE @SkipRows int = (@pagenum - 1) * @pagesize;

	--a variable that stores total records without pagination--
	select @possiblerows=COUNT(w.OrderNum) from Orders w 
inner join Customer c  on w.CustomerCode=c.CustomerCode
inner join City ci on c.CustomerCity=ci.CityId
inner join Agents ag on ag.AgentCode=c.AgentCode
	
where 
	(@search='' or c.FirstName like '%'+@search+'%' or ci.CityName like '%'+@search+'%' or c.LastName like '%'+@search+'%')
	and (@cityId=0 or ci.CityId=@cityId)
	and (@custId=0 or c.CustomerCode=@custId)
	and (@sdate=''  or w.OrderDate>=@sdate)
	and (@edate=''  or w.OrderDate<=@edate)
group by w.CustomerCode,w.OrderDate,c.FirstName,c.LastName,ci.CityName,ag.AgentName

select SUM(o.OrderAmount) as OrderAmount,SUM(o.AdvanceAmount) AdvanceAmount,o.CustomerCode,o.OrderDate,
c.FirstName+' '+c.LastName as FullName,ci.CityName as City,ag.AgentName
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

group by o.CustomerCode,o.OrderDate,c.FirstName,c.LastName,ci.CityName,ag.AgentName
order by o.OrderDate asc,c.FirstName asc 

OFFSET @SkipRows ROWS 
    FETCH NEXT @pagesize ROWS ONLY

return

End