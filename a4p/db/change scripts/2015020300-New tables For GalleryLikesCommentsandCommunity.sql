USE [ADOPets_Red]
GO
/****** Object:  Table [dbo].[GalleryLikes]    Script Date: 02/03/2015 15:38:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GalleryLikes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GalleryId] [int] NOT NULL,
	[GalleryType] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreationDate] [datetime] NULL,
	[IsRead] [bit] NULL,
 CONSTRAINT [PK_GalleryLikes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GalleryComments]    Script Date: 02/03/2015 15:38:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GalleryComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GalleryId] [int] NOT NULL,
	[GalleryType] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Comment] [nvarchar](2000) NOT NULL,
	[CreationDate] [datetime] NULL,
	[IsRead] [bit] NULL,
 CONSTRAINT [PK_GalleryComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GalleryCommentReplies]    Script Date: 02/03/2015 15:38:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GalleryCommentReplies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommentId] [int] NOT NULL,
	[Comment] [nvarchar](2000) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreationDate] [datetime] NULL,
	[IsRead] [bit] NULL,
 CONSTRAINT [PK_GalleryCommentReplies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SharePetInfoCommunity]    Script Date: 02/03/2015 15:38:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SharePetInfoCommunity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShareCategoryTypeId] [int] NOT NULL,
	[ContactId] [int] NOT NULL,
	[PetId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreationDate] [datetime] NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_SharePetInfoCommunity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_SharePetInfoCommunity_IsRead]    Script Date: 02/03/2015 15:38:42 ******/
ALTER TABLE [dbo].[SharePetInfoCommunity] ADD  CONSTRAINT [DF_SharePetInfoCommunity_IsRead]  DEFAULT ((0)) FOR [IsRead]
GO
/****** Object:  ForeignKey [FK_GalleryLikes_User]    Script Date: 02/03/2015 15:38:42 ******/
ALTER TABLE [dbo].[GalleryLikes]  WITH CHECK ADD  CONSTRAINT [FK_GalleryLikes_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[GalleryLikes] CHECK CONSTRAINT [FK_GalleryLikes_User]
GO
/****** Object:  ForeignKey [FK_GalleryComments_User]    Script Date: 02/03/2015 15:38:42 ******/
ALTER TABLE [dbo].[GalleryComments]  WITH CHECK ADD  CONSTRAINT [FK_GalleryComments_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[GalleryComments] CHECK CONSTRAINT [FK_GalleryComments_User]
GO
/****** Object:  ForeignKey [FK_GalleryCommentReplies_GalleryComments]    Script Date: 02/03/2015 15:38:42 ******/
ALTER TABLE [dbo].[GalleryCommentReplies]  WITH CHECK ADD  CONSTRAINT [FK_GalleryCommentReplies_GalleryComments] FOREIGN KEY([CommentId])
REFERENCES [dbo].[GalleryComments] ([Id])
GO
ALTER TABLE [dbo].[GalleryCommentReplies] CHECK CONSTRAINT [FK_GalleryCommentReplies_GalleryComments]
GO
/****** Object:  ForeignKey [FK_GalleryCommentReplies_User]    Script Date: 02/03/2015 15:38:42 ******/
ALTER TABLE [dbo].[GalleryCommentReplies]  WITH CHECK ADD  CONSTRAINT [FK_GalleryCommentReplies_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[GalleryCommentReplies] CHECK CONSTRAINT [FK_GalleryCommentReplies_User]
GO
/****** Object:  ForeignKey [FK_SharePetInfoCommunity_CommunityContact]    Script Date: 02/03/2015 15:38:42 ******/
ALTER TABLE [dbo].[SharePetInfoCommunity]  WITH CHECK ADD  CONSTRAINT [FK_SharePetInfoCommunity_CommunityContact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[SharePetInfoCommunity] CHECK CONSTRAINT [FK_SharePetInfoCommunity_CommunityContact]
GO
/****** Object:  ForeignKey [FK_SharePetInfoCommunity_Pet]    Script Date: 02/03/2015 15:38:42 ******/
ALTER TABLE [dbo].[SharePetInfoCommunity]  WITH CHECK ADD  CONSTRAINT [FK_SharePetInfoCommunity_Pet] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO
ALTER TABLE [dbo].[SharePetInfoCommunity] CHECK CONSTRAINT [FK_SharePetInfoCommunity_Pet]
GO
/****** Object:  ForeignKey [FK_SharePetInfoCommunity_User]    Script Date: 02/03/2015 15:38:42 ******/
ALTER TABLE [dbo].[SharePetInfoCommunity]  WITH CHECK ADD  CONSTRAINT [FK_SharePetInfoCommunity_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[SharePetInfoCommunity] CHECK CONSTRAINT [FK_SharePetInfoCommunity_User]
GO
