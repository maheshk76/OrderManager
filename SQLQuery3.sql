declare @possiblerows int
exec GetOrders 'fdsgvdf',0,0,'','',6,1, @possiblerows out;
print 1+@possiblerows