CREATE PROCEDURE [dbo].[GetAgent]
	@AgentId int = 0
AS
	
	if(@AgentId=0)
	select a.*,Count(cust.AgentCode) as CustCount
	from Agents a
	inner join Customer cust on a.AgentCode=cust.AgentCode
	else
		select a.*,Count(cust.AgentCode) as CustCount
	from Agents a
	inner join Customer cust on a.AgentCode=cust.AgentCode
	where a.AgentCode=@AgentId
RETURN 0
