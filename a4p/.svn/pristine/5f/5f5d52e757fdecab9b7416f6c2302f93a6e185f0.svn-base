USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[Gallery]    Script Date: 10/20/2014 14:48:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Gallery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[ImageURL] [nvarchar](max) NULL,
	[CreatedDate] [date] NOT NULL,
	[PetId] [int] NOT NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Gallery]  WITH CHECK ADD  CONSTRAINT [FK_GalleryPet] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO

ALTER TABLE [dbo].[Gallery] CHECK CONSTRAINT [FK_GalleryPet]
GO


