# EasyHouseWebAPI
# Run this Script to create DataBase objects.

CREATE DATABASE EasyHouse
GO

USE EasyHouse

GO

CREATE TABLE dbo.House(
HouseId INT IDENTITY(1,1),
Address VARCHAR(500),
City VARCHAR(100),
State VARCHAR(100),
ZipCode VARCHAR(20),
ContactName VARCHAR(50),
ContactPhone VARCHAR(50),
FrontViewPhoto VARCHAR(500),
Price Money
)

Go

/*Store Procedure*/
CREATE PROCEDURE dbo.sp_AddHouse @address VARCHAR(500),
                   @city			VARCHAR(100),
                   @state			VARCHAR(100),
                   @zipcode			VARCHAR(20),
				   @contactname     VARCHAR(50),
				   @contactphone    VARCHAR(50),
				   @frontviewphoto	VARCHAR(500),
				   @price			MONEY
AS
  BEGIN TRY
    BEGIN TRANSACTION
    INSERT INTO dbo.House (
		Address,
		City,
		State,
		ZipCode,
		ContactName,
		ContactPhone,
		FrontViewPhoto,
		Price
		)
         SELECT
           @address,
                   @city,
                   @state,
                   @zipcode,
				   @contactname,
				   @contactphone,
				   @frontviewphoto,
				   @price			
    COMMIT TRANSACTION
  END TRY
  BEGIN CATCH
      ROLLBACK TRANSACTION
  END CATCH
GO
