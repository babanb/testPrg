USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[EconsultationMsg]    Script Date: 09/18/2014 16:02:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EconsultationMsg](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[EConsultationId] [int] NULL,
	[EconsultationRoomId] [int] NULL,
	[MsgSender] [nvarchar](50) NULL,
	[MsgContent] [ntext] NULL,
	[MsgDateTime] [datetime] NULL,
	[MsgTime] [timestamp] NULL,
	[MsgIP] [nvarchar](50) NULL,
	[MsgType] [int] NULL,
	[Msgrq] [nvarchar](50) NULL,
	[Msgsj] [nvarchar](50) NULL,
	[DateTimeGMT] [nvarchar](50) NULL,
	[TimeD] [nvarchar](50) NULL,
	[TimeE] [nvarchar](50) NULL,
	
 CONSTRAINT [PK_Econsultation_msg] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[EconsultationMsg]  WITH CHECK ADD  CONSTRAINT [FK_Econsultation_msg_EConsultation] FOREIGN KEY([EConsultationId])
REFERENCES [dbo].[EConsultation] ([ID])
GO

ALTER TABLE [dbo].[EconsultationMsg] CHECK CONSTRAINT [FK_Econsultation_msg_EConsultation]
GO


