declare @possiblerows int
exec GetOrders '',0,0,'','',10,1,'fname','DESC', @possiblerows out;

go
select sum(t1.CO)
from(
	select  COUNT(distinct o.CustomerCode) as CO
	from Orders o 
	inner join Customer c  on o.CustomerCode=c.CustomerCode
	inner join City ci on c.CustomerCity=ci.CityId
	inner join Agents ag on ag.AgentCode=c.AgentCode
	group by o.OrderDate,o.CustomerCode,c.FirstName,c.LastName,ci.CityName,ag.AgentName) as t1
