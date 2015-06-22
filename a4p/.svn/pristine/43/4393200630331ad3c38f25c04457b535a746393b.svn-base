USE [AdoPets_Redesign]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EconsultDocument](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EcId] [int] NOT NULL,
	[DocumentName] [nvarchar](max) NOT NULL,
	[DocumentPath] [varchar](max) NOT NULL,
	[ServiceDate] [datetime] NOT NULL,
	[UploadDate] [datetime] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_EconsultDocument] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EconsultDocument]  WITH CHECK ADD  CONSTRAINT [FK_EconsultDocument_EcId] FOREIGN KEY([EcId])
REFERENCES [dbo].[Econsultation] ([Id])
GO



