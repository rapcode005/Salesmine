Go
CREATE NONCLUSTERED INDEX IX_US_NOTES
ON US.Notes (createdby)

Go
DBCC DBREINDEX 
( 
  [US.Notes]
)

USE SalesMine;
GO
  UPDATE STATISTICS US.Notes;
GO

---------------------------------------------------------------------------------------
Go
CREATE NONCLUSTERED INDEX IX_UK_NOTES
ON UK.Notes (createdby)

Go
DBCC DBREINDEX 
( 
  [UK.Notes]
)

USE SalesMine;
GO
  UPDATE STATISTICS UK.Notes;
GO

---------------------------------------------------------------------------------------
Go
CREATE NONCLUSTERED INDEX IX_PC_NOTES
ON PC.Notes (createdby)

Go
DBCC DBREINDEX 
( 
  [PC.Notes]
)

USE SalesMine;
GO
  UPDATE STATISTICS PC.Notes;
GO

---------------------------------------------------------------------------------------
Go
CREATE NONCLUSTERED INDEX IX_DAWG_NOTES
ON DAWG.Notes (createdby)

Go
DBCC DBREINDEX 
( 
  [DAWG.Notes]
)

USE SalesMine;
GO
  UPDATE STATISTICS DAWG.Notes;
GO
