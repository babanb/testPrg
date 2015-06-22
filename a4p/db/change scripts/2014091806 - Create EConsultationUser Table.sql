USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[EconsultationUser]    Script Date: 09/18/2014 16:03:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EconsultationUser](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[EConsultationId] [int] NULL,
	[EconsultationRoomID] [int] NULL,
	[UserId] [int] NULL,
	[UserName] [nvarchar](50) NULL,
	[LastMsgActivity] [datetime] NULL,
	[LastVideoActivity] [datetime] NULL,
	[LastActivity] [timestamp] NULL,
	[UserStatus] [int] NULL,
 CONSTRAINT [PK_Econsultation_user] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EconsultationUser]  WITH CHECK ADD  CONSTRAINT [FK_Econsultation_user_EConsultation] FOREIGN KEY([EConsultationId])
REFERENCES [dbo].[EConsultation] ([ID])
GO

ALTER TABLE [dbo].[EconsultationUser] CHECK CONSTRAINT [FK_Econsultation_user_EConsultation]
GO


