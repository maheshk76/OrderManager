CREATE PROCEDURE [dbo].[GetAgent]
	@AgentCode int = -1
AS
	
	if(@AgentCode<0)
	select a.*,cs.Name as AgentCountry,(Select count(*) from Customer c where a.AgentCode=c.AgentCode) as CustCount
	from Agents a
	inner join Country cs on a.CountryId=cs.Id
	else
	select a.*,cs.Name as AgentCountry,
	(Select count(*) from Customer c where a.AgentCode=c.AgentCode) as CustCount
	from Agents a
	inner join Country cs on a.CountryId=cs.Id
	where a.AgentCode=@AgentCode