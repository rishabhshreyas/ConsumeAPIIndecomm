
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tAddress](
	[Id] [int] IDENTITY(1,1),
	[address1] [nvarchar](max) NULL,
	[address2] [nvarchar](max) NULL,
	[city] [nvarchar](50) NULL,
	[country] [nvarchar](50) NULL,
	[county] [nvarchar](50) NULL,
	[district] [nvarchar](50) NULL,
	[state] [nvarchar](50) NULL,
	[zip] [nvarchar](50) NULL,
 CONSTRAINT [PK_tAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SamplerPub](
	[ListPrice] [money] NULL,
	[MonthlyRent] [money] NULL,
	[YearBuilt] [int] NULL,
	[GrossYield] [money] NULL,
	[AddressId] [int] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SamplerPub]
ADD CONSTRAINT fk_Address
    FOREIGN KEY ([AddressId])
    REFERENCES [dbo].[tAddress]([Id]);

Go




