
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PetGender](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_PetGender] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


INSERT INTO [dbo].[PetGender]([Id],[Name])
     VALUES (1,'Female'),
	 (2,'Male')
GO


GO

ALTER TABLE [dbo].[Pet] DROP CONSTRAINT [FK_PetProfile_Gender]
GO
ALTER TABLE [dbo].[Pet]  WITH CHECK ADD  CONSTRAINT [FK_PetProfile_PetGender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[PetGender] ([Id])
GO
ALTER TABLE [dbo].[Pet] CHECK CONSTRAINT [FK_PetProfile_PetGender]
GO
