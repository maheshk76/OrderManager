CREATE proc InsertUpdateAgent
@AgentCode int ,
@AgentName nvarchar(max),
@WorkingArea nvarchar(30),
@Commission decimal(18,2),
@PhoneNo nchar(10),
@Country int
as 
begin 
	if(@AgentCode=0)
	begin
		insert into Agents(AgentName,WorkingArea,Commission,PhoneNo,CountryId) Values(@AgentName,@WorkingArea,@Commission,@PhoneNo,@Country)

	end
	else
		update Agents
		set AgentName=@AgentName,WorkingArea=@WorkingArea,Commission=@Commission,PhoneNo=@PhoneNo,CountryId=@Country where AgentCode=@AgentCode
end