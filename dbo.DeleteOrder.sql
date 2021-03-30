CREATE PROCEDURE [dbo].[DeleteOrder]
    @OrderNum int
AS
    Begin
        DECLARE @RemainingAmount int;
        DECLARE @OrderAmount int;
        DECLARE @AdvanceAmount int;
        DECLARE @CustomerCode int;
		
		select @OrderAmount =OrderAmount from Orders where OrderNum=@OrderNum
		select @AdvanceAmount =AdvanceAmount from Orders where OrderNum=@OrderNum
		select @CustomerCode =CustomerCode from Orders where OrderNum=@OrderNum
		
        delete from [Orders]  where [Orders].OrderNum = @OrderNum

 

        set @RemainingAmount = @OrderAmount - @AdvanceAmount

 

        update Customer
        set PaymentAmount = PaymentAmount - @AdvanceAmount, OutstandingAmount =  OutstandingAmount - @RemainingAmount where Customer.CustomerCode =@CustomerCode
    End