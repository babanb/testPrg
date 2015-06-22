USE [ADOPets_Rev]


CREATE TABLE [dbo].[BillingInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentTypeId] [int] NOT NULL,
	[CreditCardNumber] [nvarchar](max) NOT NULL,
	[ExpirationDate] [date] NOT NULL,
	[CVV] [nvarchar](max) NOT NULL,
	[Address1] [nvarchar](max) NOT NULL,
	[Address2] [nvarchar](max) NULL,
	[City] [nvarchar](max) NOT NULL,
	[CountryId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[Zip] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_BillingInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BillingInformation]  WITH CHECK ADD  CONSTRAINT [FK_BillingInformation_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO

ALTER TABLE [dbo].[BillingInformation] CHECK CONSTRAINT [FK_BillingInformation_Country]
GO

ALTER TABLE [dbo].[BillingInformation]  WITH CHECK ADD  CONSTRAINT [FK_BillingInformation_PaymentType] FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentType] ([Id])
GO

ALTER TABLE [dbo].[BillingInformation] CHECK CONSTRAINT [FK_BillingInformation_PaymentType]
GO

ALTER TABLE [dbo].[BillingInformation]  WITH CHECK ADD  CONSTRAINT [FK_BillingInformation_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO

ALTER TABLE [dbo].[BillingInformation] CHECK CONSTRAINT [FK_BillingInformation_State]
GO


