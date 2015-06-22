USE [ADOPets_Rev]
GO

IF OBJECT_ID('dbo.[VideoGallery]', 'U') IS NOT NULL
  DROP TABLE dbo.[VideoGallery]


/****** Object:  Table [dbo].[VideoGallery]    Script Date: 10/28/2014 18:53:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VideoGallery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[VideoURL] [nvarchar](100) NOT NULL,
	[PetId] [int] NULL,
	[CreatedDate] DateTime Null,
 CONSTRAINT [PK_VideoGallery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[VideoGallery]  WITH CHECK ADD  CONSTRAINT [FK_VideoGallery_Pet] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO

ALTER TABLE [dbo].[VideoGallery] CHECK CONSTRAINT [FK_VideoGallery_Pet]
GO

