CREATE PROCEDURE [dbo].[GetCustomer]
	@CustCode int = -1
AS
	
	if(@CustCode<0)
	select cust.CustomerCode,cust.FirstName+' '+cust.LastName as FullName,cust.WorkingArea,cust.Grade,cust.OpeningAmount,
	cust.ReceivingAmount,cust.PaymentAmount,cust.OutstandingAmount,cust.PhoneNo,cust.AgentCode,
	ci.CityName as City,coun.Name as Country,agt.AgentName as AgentName 
	from Customer cust
	inner join City ci on ci.CityId=cust.CustomerCity
	inner join Country coun on coun.Id=cust.CustomerCountry
	inner join Agents agt on agt.AgentCode=cust.AgentCode
	else
	select * from Customer cust
	where cust.CustomerCode=@CustCode