/****** Object:  New Table [dbo].[Centers]    Script Date: 12/04/2014 12:17:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Centers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CenterName] [nvarchar](max) NOT NULL,

 CONSTRAINT [PK_Centers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

/****** Object:  [dbo].[user]  Add new column ******/

Alter table [dbo].[user] 
Add CenterID int null
GO

/****** Object:  [dbo].[user]  Add FK  ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Center] FOREIGN KEY([CenterID])
REFERENCES [dbo].[Centers] ([Id])
GO

/************* Add value in Centers **********************/

Insert into [dbo].[Centers] values('HSBC')
Go