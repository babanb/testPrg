USE ADOPets_Rev

ALTER TABLE Gallery
ADD AlbumId int 



ALTER TABLE [dbo].[Gallery]  WITH CHECK ADD  CONSTRAINT [FK_Gallery_AlbumGallery] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[AlbumGallery] ([Id])
GO

ALTER TABLE [dbo].[Gallery] CHECK CONSTRAINT [FK_Gallery_AlbumGallery]
GO