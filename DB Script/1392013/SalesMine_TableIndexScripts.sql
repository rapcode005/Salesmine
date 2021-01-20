Go
CREATE NONCLUSTERED INDEX IX_CA_CONTINFO
ON CA.CONTINFO (CONTACT)

Go
DBCC DBREINDEX 
( 
  [CA.CONTINFO]
)

USE SalesMine;
GO
  UPDATE STATISTICS CA.CONTINFO;
GO
-------------------------------------------------------------------------

Go
CREATE NONCLUSTERED INDEX IX_CL_CONTINFO
ON CL.CONTINFO (CONTACT)

Go
DBCC DBREINDEX 
( 
  [CL.CONTINFO]
)

USE SalesMine;
GO
  UPDATE STATISTICS CL.CONTINFO;
GO

-------------------------------------------------------------------------------

Go
CREATE NONCLUSTERED INDEX IX_EMED_CONTINFO
ON EMED.CONTINFO (CONTACT)

Go
DBCC DBREINDEX 
( 
  [EMED.CONTINFO]
)

USE SalesMine;
GO
  UPDATE STATISTICS EMED.CONTINFO;
GO

------------------------------------------------------------------------------------

Go
CREATE NONCLUSTERED INDEX IX_PC_CONTINFO
ON PC.CONTINFO (CONTACT)

Go
DBCC DBREINDEX 
( 
  [PC.CONTINFO]
)

USE SalesMine;
GO
  UPDATE STATISTICS PC.CONTINFO;
GO

------------------------------------------------------------------------------------


Go
CREATE NONCLUSTERED INDEX IX_UK_CONTINFO
ON UK.CONTINFO (CONTACT)

Go
DBCC DBREINDEX 
( 
  [UK.CONTINFO]
)

USE SalesMine;
GO
  UPDATE STATISTICS UK.CONTINFO;
GO

------------------------------------------------------------------------------------

Go
CREATE NONCLUSTERED INDEX IX_US_CONTINFO
ON US.CONTINFO (CONTACT)

Go
DBCC DBREINDEX 
( 
  [US.CONTINFO]
)

USE SalesMine;
GO
  UPDATE STATISTICS US.CONTINFO;
GO

--------------------------------------------------------------------------------------

Go
ALTER TABLE dbo.DISP_DESC
ALTER COLUMN [CODE] char(6) NOT NULL

Go
ALTER table dbo.DISP_DESC
ADD CONSTRAINT IX_Codes_PK primary key (CODE)

---------------------------------------------------------------------------------------
Go
CREATE NONCLUSTERED INDEX IX_US_NOTES
ON US.Notes (createdby)

Go
DBCC DBREINDEX 
( 
  US.Notes
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
  UK.Notes
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
  PC.Notes
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
  DAWG.Notes
)

USE SalesMine;
GO
  UPDATE STATISTICS DAWG.Notes;
GO


---------------------------------------------------------------------------------------
Go
CREATE NONCLUSTERED INDEX IX_EMED_NOTES
ON EMED.Notes (createdby)

Go
DBCC DBREINDEX 
( 
  EMED.Notes
)

USE SalesMine;
GO
  UPDATE STATISTICS EMED.Notes;
GO

---------------------------------------------------------------------------------------
Go
CREATE NONCLUSTERED INDEX IX_CL_NOTES
ON CL.Notes (createdby)

Go
DBCC DBREINDEX 
( 
  CL.Notes
)

USE SalesMine;
GO
  UPDATE STATISTICS CL.Notes;
GO

---------------------------------------------------------------------------------------
Go
CREATE NONCLUSTERED INDEX IX_CA_NOTES
ON CA.Notes (createdby)

Go
DBCC DBREINDEX 
( 
  CA.Notes
)

USE SalesMine;
GO
  UPDATE STATISTICS CA.Notes;
GO