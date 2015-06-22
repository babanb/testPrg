USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[EconsultationSummary]    Script Date: 09/18/2014 16:03:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EconsultationSummary](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[EConsultationId] [int] NULL,
	[VetId] [int] NULL,
	[UserId] [int] NULL,
	[DateSummary] [datetime] NULL,
	[FilesFolder] [varchar](max) NULL,
	[FilesName] [varchar](max) NULL,
	[EconsultationStatusId] [int] NULL,
	[CenterID] [varchar](50) NULL,
	[PetName] [nvarchar](max) NULL,
	[PetDOB] [nvarchar](max) NULL,
	[EconsultationDateTime] [nvarchar](max) NULL,
	[EconsultationDuration] [numeric](18, 0) NULL,
	[Diagnoses] [ntext] NULL,
	[Treatment] [ntext] NULL,
	[FollowUp] [int] NULL,
	[VeterianFirstName] [nvarchar](max) NULL,
	[VeterianMiddleName] [nvarchar](max) NULL,
	[VeterianLastName] [nvarchar](max) NULL,
	[VeterianSignature] [nvarchar](max) NULL,
	[Remark] [ntext] NULL,
 CONSTRAINT [PK_Econsultation_summary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EconsultationSummary]  WITH CHECK ADD  CONSTRAINT [FK_Econsultation_summary_EConsultation] FOREIGN KEY([EConsultationId])
REFERENCES [dbo].[EConsultation] ([ID])
GO

ALTER TABLE [dbo].[EconsultationSummary] CHECK CONSTRAINT [FK_Econsultation_summary_EConsultation]
GO


