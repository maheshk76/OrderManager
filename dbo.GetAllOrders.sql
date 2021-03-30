CREATE PROCEDURE [dbo].[GetAllOrders]
	@custId int = 0
AS
	select o.*,c.FirstName+' '+c.LastName as FullName,ci.CityName as City,ag.AgentName
from Orders o 
inner join Customer c  on o.CustomerCode=c.CustomerCode
inner join City ci on c.CustomerCity=ci.CityId
inner join Agents ag on ag.AgentCode=c.AgentCode
where 
(@custId=0 or c.CustomerCode=@custId)
order by o.OrderDate asc,c.FirstName asc 
RETURN 0