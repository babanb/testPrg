USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[EconsultationRoom]    Script Date: 09/18/2014 16:03:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EconsultationRoom](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Status] [int] NOT NULL,
	[EConsultationId] [int] NULL,
 CONSTRAINT [PK_Econsultation_room] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EconsultationRoom]  WITH CHECK ADD  CONSTRAINT [FK_Econsultation_room_EConsultation] FOREIGN KEY([EConsultationId])
REFERENCES [dbo].[EConsultation] ([ID])
GO

ALTER TABLE [dbo].[EconsultationRoom] CHECK CONSTRAINT [FK_Econsultation_room_EConsultation]
GO


