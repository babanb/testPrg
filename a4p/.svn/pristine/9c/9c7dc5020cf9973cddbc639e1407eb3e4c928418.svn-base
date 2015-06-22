USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[EConsultation]    Script Date: 09/18/2014 16:02:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EConsultation](
	[ID] [int] NOT NULL,
	[TitleConsultation] [nvarchar](255) NULL,
	[DateConsultation] [datetime] NULL,
	[TypeConsultation] [smallint] NULL,
	[UserId] [int] NULL,
	[VetId] [int] NULL,
	[PetId] [int] NULL,
	[EconsultationStatusId] [smallint] NULL,
	[BDateConsultation] [datetime] NULL,
	[BTimeConsultation] [datetime] NULL,
	[RDVDate] [datetime] NULL,
	[RDVDateTime] [datetime] NULL,
	[RequestedTimeRange] [nvarchar](50) NULL,
	[msgConsultation] [ntext] NULL,
	[IDConversation] [numeric](18, 0) NULL,
	[VetCompID] [nvarchar](50) NULL,
	[VetCompIP] [nvarchar](50) NULL,
	[UserCompID] [nvarchar](50) NULL,
	[UserCompIP] [nvarchar](50) NULL,
	[Periods] [numeric](18, 0) NULL,
	[ActionFlag] [smallint] NULL,
	[ActionDateTimeBegin] [datetime] NULL,
	[ActionDateTimeEnd] [datetime] NULL,
	[VetLocationID] [nvarchar](50) NULL,
	[VetTimezoneID] [int] NULL,
	[VetDecalage] [numeric](18, 0) NULL,
	[VetDST] [int] NULL,
	[VetTimeZone] [nvarchar](255) NULL,
	[UserLocationID] [nvarchar](50) NULL,
	[UserTimezoneID] [int] NULL,
	[UserDecalage] [numeric](18, 0) NULL,
	[UserDST] [int] NULL,
	[UserTimeZone] [nvarchar](255) NULL,
	[VetNotificationFlag] [int] NULL,
	[VetNotificationDateTime] [datetime] NULL,
	[UserNotificationFlag] [int] NULL,
	[UserNotificationDateTime] [datetime] NULL,
	[PaymentFlag] [int] NULL,
	[PaymentCharged] [money] NULL,
	[PaymentTransferNum] [nvarchar](50) NULL,
	[PaymentTransferDateTime] [datetime] NULL,
	[PaymentTransferMsg] [nvarchar](50) NULL,
	[PaymentRefNum] [nvarchar](50) NULL,
	[Shared] [int] NULL,
	[VerFlag] [nchar](10) NULL,
	[CenterID] [varchar](255) NULL,
	[PaymentType] [int] NULL,
	[UnitPrice] [money] NULL,
	[PurchaseFlag] [nchar](10) NULL,
	[Survey] [nvarchar](50) NULL,
 CONSTRAINT [PK_EConsultation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EConsultation]  WITH CHECK ADD  CONSTRAINT [FK_EConsultation_Pet] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO

ALTER TABLE [dbo].[EConsultation] CHECK CONSTRAINT [FK_EConsultation_Pet]
GO

ALTER TABLE [dbo].[EConsultation]  WITH CHECK ADD  CONSTRAINT [FK_EConsultation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[EConsultation] CHECK CONSTRAINT [FK_EConsultation_User]
GO

ALTER TABLE [dbo].[EConsultation]  WITH CHECK ADD  CONSTRAINT [FK_EConsultation_Veterinarian] FOREIGN KEY([VetId])
REFERENCES [dbo].[Veterinarian] ([Id])
GO

ALTER TABLE [dbo].[EConsultation] CHECK CONSTRAINT [FK_EConsultation_Veterinarian]
GO


