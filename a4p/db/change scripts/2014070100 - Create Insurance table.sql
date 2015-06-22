USE [ADOPets_Rev]

CREATE TABLE [dbo].[Insurance](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[AccountNumber] [nvarchar](100) NULL,
	[PlanName] [nvarchar](100) NULL,
	[GroupNumber] [nvarchar](100) NULL,
	[EndDate] [date] NULL,
	[Phone] [nvarchar](100) NULL,
	[Comment] [nvarchar](max) NULL,
	[SendNotificationMail] [bit] NOT NULL,
	[PetId] [int] NOT NULL,
 CONSTRAINT [PK_Insurance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


