
CREATE Procedure [dbo].spInsertDataintoTable
(
@ListPrice money= NULL,
@MonthlyRent money= NULL,
@YearBuilt int= NULL,
@GrossYield money= NULL,
@address1 nvarchar(max)= NULL,
@address2 nvarchar(max)= NULL,
@city nvarchar(50)= NULL,
@country nvarchar(50)= NULL,
@county nvarchar(50)= NULL,
@district nvarchar(50)= NULL,
@state nvarchar(50)= NULL,
@zip nvarchar(50)= NULL
)
as
begin

	INSERT INTO dbo.tAddress
	(
		address1,
		address2,
		city,
		country,
		county,
		district,
		state,
		zip
	)
	VALUES
	(
		@address1,
		@address2,
		@city,
		@country,
		@county,
		@district,
		@state,
		@zip
	)

	Declare @addressId int
	select @addressId = max(id) from tAddress
	
	INSERT INTO [SamplerPub]
	(
		[ListPrice],
		[MonthlyRent],
		[YearBuilt],
		[GrossYield],
		[AddressId]
	)
	VALUES
	(
		@ListPrice,
		@MonthlyRent,
		@YearBuilt,
		@GrossYield,
		@addressId
	)
end
GO