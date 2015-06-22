USE [AdoPets_Redesign]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EPrescriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EcId] [int] NULL,	
	[DatePrescription] [datetime] NULL,
	[FilesID] [varchar](max) NULL,
	[PrescriptionsStatus] [int] NOT NULL,	
	[PrescriptionContent] [ntext] NULL,
	[Label] [int] NULL,
	[Refill] [numeric](18, 0) NULL,
	[Brand] [int] NULL,
	[Voluntary] [int] NULL,
	[MedXPTitle1] [nvarchar](max) NULL,
	[MedXPTitle2] [nvarchar](max) NULL,
	[DEA] [nvarchar](max) NULL,
	[NPI] [nvarchar](max) NULL,	
	[PrescriptionTitle] [nvarchar](max) NULL,
	[TypePrescription] [int] NOT NULL,
	[RPPS] [nvarchar](max) NULL,
CONSTRAINT [PK_EPrescriptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EPrescriptions]  WITH CHECK ADD  CONSTRAINT [FK_EPrescriptions_EcId] FOREIGN KEY([EcId])
REFERENCES [dbo].[Econsultation] ([Id])
GO
