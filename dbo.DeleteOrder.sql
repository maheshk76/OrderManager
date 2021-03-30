create proc DeleteOrder
	@OrderId int
	as
	begin
	delete from Orders where OrderNum=@OrderId
	end