USE [ADOPets_Rev]

Drop Table Payment
GO

/****** Object:  Table [dbo].[PaymentHistory]    Script Date: 07/21/2014 18:40:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PaymentHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentTypeId] [int] NOT NULL,
	[UserSubscriptionId] [int] NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[PaymentDate] [date] NOT NULL,
	[TransactionNumber] [nvarchar](max) NULL,
	[TransactionResultId] [int] NULL,
	[ErrorMessage] [nvarchar](max) NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PaymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_Payment_PaymentType] FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentType] ([Id])
GO

ALTER TABLE [dbo].[PaymentHistory] CHECK CONSTRAINT [FK_Payment_PaymentType]
GO

ALTER TABLE [dbo].[PaymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_PaymentHistory_TransactionResult] FOREIGN KEY([TransactionResultId])
REFERENCES [dbo].[TransactionResult] ([Id])
GO

ALTER TABLE [dbo].[PaymentHistory] CHECK CONSTRAINT [FK_PaymentHistory_TransactionResult]
GO

ALTER TABLE [dbo].[PaymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_PaymentHistory_UserSubscription] FOREIGN KEY([UserSubscriptionId])
REFERENCES [dbo].[UserSubscription] ([Id])
GO

ALTER TABLE [dbo].[PaymentHistory] CHECK CONSTRAINT [FK_PaymentHistory_UserSubscription]
GO


