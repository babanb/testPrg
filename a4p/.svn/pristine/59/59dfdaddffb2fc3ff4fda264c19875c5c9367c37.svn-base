USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[EconsultationData]    Script Date: 09/18/2014 16:02:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EconsultationData](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[EConsultationId] [int] NULL,
	[VeterianBrowser] [nvarchar](max) NULL,
	[UserBrowser] [nvarchar](max) NULL,
	[VeterianDevice] [nvarchar](max) NULL,
	[UserDevice] [nvarchar](max) NULL,
	[VeterianAudio] [nvarchar](max) NULL,
	[UserAudio] [nvarchar](max) NULL,
	[VeterianVideo] [nvarchar](max) NULL,
	[UserVideo] [nvarchar](max) NULL,
	[Survey] [nvarchar](50) NULL,
	[FlagRecord] [bit] NULL,
 CONSTRAINT [PK_Econsultation_data] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EconsultationData]  WITH CHECK ADD  CONSTRAINT [FK_Econsultation_data_Econsultation_data] FOREIGN KEY([EConsultationId])
REFERENCES [dbo].[EConsultation] ([ID])
GO

ALTER TABLE [dbo].[EconsultationData] CHECK CONSTRAINT [FK_Econsultation_data_Econsultation_data]
GO


