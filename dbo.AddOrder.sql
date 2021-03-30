CREATE PROCEDURE [dbo].[AddOrder]
	@OrderAmount decimal(18,2),
	@AdvanceAmount decimal(18,2),
	@OrderDate date,
	@CustomerCode int,
	@OrderDesc nvarchar(max)
AS
	insert into Orders(OrderAmount,AdvanceAmount,OrderDate,CustomerCode,OrderDescription)
	values(@OrderAmount,@AdvanceAmount,@OrderDate,@CustomerCode,@OrderDesc)
RETURN 0