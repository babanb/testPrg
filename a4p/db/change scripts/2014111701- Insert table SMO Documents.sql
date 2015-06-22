USE [ADOPets_rev]
GO

/****** Object:  Table [dbo].[SMODocuments]    Script Date: 11/17/2014 11:30:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SMODocuments](
	[Id] [int] NOT NULL,
	[SMOId] [bigint] NULL,
	[DocumentSubTypeId] [int] NULL,
	[DocumentName] [nvarchar](max) NULL,
	[DocumentPath] [nvarchar](max) NULL,
	[UploadDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_SMODocuments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SMODocuments]  WITH CHECK ADD  CONSTRAINT [FK_SMODocuments_DocumentSubType] FOREIGN KEY([DocumentSubTypeId])
REFERENCES [dbo].[DocumentSubType] ([Id])
GO

ALTER TABLE [dbo].[SMODocuments] CHECK CONSTRAINT [FK_SMODocuments_DocumentSubType]
GO

ALTER TABLE [dbo].[SMODocuments]  WITH CHECK ADD  CONSTRAINT [FK_SMODocuments_SMORequest] FOREIGN KEY([SMOId])
REFERENCES [dbo].[SMORequest] ([ID])
GO

ALTER TABLE [dbo].[SMODocuments] CHECK CONSTRAINT [FK_SMODocuments_SMORequest]
GO


