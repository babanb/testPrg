USE ADOPets_Rev

GO

ALTER Table Gallery
DROP Constraint FK_Gallery_AlbumGallery
GO

Alter Table Gallery
DROP Column AlbumId

GO

Alter table Gallery
Add 
[IsGalleryPhoto] bit NOT NULL DEFAULT 'True',
[IsDeleted] bit NOT NULL DEFAULT 'False'


IF OBJECT_ID('dbo.[AlbumGallery]', 'U') IS NOT NULL
Alter table AlbumGallery_Photo drop constraint [FK_AlbumG_AlbumGalleryPhoto]
  DROP TABLE dbo.AlbumGallery

IF OBJECT_ID('dbo.[AlbumGallery_Photo]', 'U') IS NOT NULL
  DROP TABLE dbo.AlbumGallery_Photo
  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[AlbumGallery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[PetId] [int] NULL,
	[CreatedDate] [date] NULL,
 CONSTRAINT [PK_AlbumGallery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AlbumGallery]  WITH CHECK ADD  CONSTRAINT [FK_AlbumG_Pet] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO



CREATE TABLE [dbo].[AlbumGallery_Photo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AlbumId] [int] NULL,
	[GalleryId] [int] NULL,
	[IsDefaultAlbumCover] bit NOT NULL DEFAULT 'false',
 CONSTRAINT [PK_AlbumGallery_Photo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AlbumGallery_Photo]  WITH CHECK ADD  CONSTRAINT [FK_AlbumG_AlbumGalleryPhoto] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[AlbumGallery] ([Id])
GO

ALTER TABLE [dbo].[AlbumGallery_Photo] CHECK CONSTRAINT [FK_AlbumG_AlbumGalleryPhoto]
GO

ALTER TABLE [dbo].[AlbumGallery_Photo]  WITH CHECK ADD  CONSTRAINT [FK_AlbumGPhoto_Gallery] FOREIGN KEY([GalleryId])
REFERENCES [dbo].[Gallery] ([Id])
GO

ALTER TABLE [dbo].[AlbumGallery_Photo] CHECK CONSTRAINT [FK_AlbumGPhoto_Gallery]
GO


