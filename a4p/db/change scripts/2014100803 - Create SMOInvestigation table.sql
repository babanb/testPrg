USE [ADOPets_rev]
GO

CREATE TABLE [dbo].[SMOInvestigation](
	[ID] [bigint] NOT NULL,
	[SMORequestID] [bigint] NULL,
	[TestType] [nvarchar](200) NULL,
	[TestDescription] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_SMOInvestigation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SMOInvestigation]  WITH CHECK ADD  CONSTRAINT [FK_SMOInvestigation_SMORequest] FOREIGN KEY([SMORequestID])
REFERENCES [dbo].[SMORequest] ([ID])
GO

ALTER TABLE [dbo].[SMOInvestigation] CHECK CONSTRAINT [FK_SMOInvestigation_SMORequest]
GO

