CREATE proc InsertUpdateCustomer
@CustCode int,
@FirstName nvarchar(max),
@LastName nvarchar(max),
@CustomerCity int,
@WorkingArea nvarchar(max),
@CustomerCountry int,
@Grade nvarchar(max),
@OpeningAmount decimal(18,2),
@PhoneNo nchar(10),
@AgentCode int
as 
begin 
	if(@CustCode=0)
	begin
		insert into Customer(FirstName,LastName,CustomerCity,WorkingArea,CustomerCountry,Grade,
		OpeningAmount,ReceivingAmount,PaymentAmount,OutstandingAmount,PhoneNo,AgentCode)

		Values(@FirstName,@LastName,@CustomerCity,@WorkingArea,@CustomerCountry,@Grade,
		@OpeningAmount,0,0,0,@PhoneNo,@AgentCode)

	end
	else
		update Customer
		set FirstName=@FirstName,
		LastName=@LastName,
		CustomerCity=@CustomerCity,
		WorkingArea=@WorkingArea,
		CustomerCountry=@CustomerCountry,
		Grade=@Grade,
		OpeningAmount=@OpeningAmount,
		PhoneNo=@PhoneNo,
		AgentCode=@AgentCode
		where CustomerCode=@CustCode
end