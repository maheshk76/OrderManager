declare @possiblerows int
exec GetOrders '',0,0,'','',10,1, @possiblerows out;
print 1+@possiblerows