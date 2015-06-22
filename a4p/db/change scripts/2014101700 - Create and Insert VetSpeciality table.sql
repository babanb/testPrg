USE [ADOPets_Rev]
GO
/****** Object:  Table [dbo].[VetSpeciality]    Script Date: 10/17/2014 11:30:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VetSpeciality](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Description] [varchar](200) NULL,
 CONSTRAINT [PK_VetSpeciality] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[VetSpeciality] ON
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (1, N'Alternative medicine', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (2, N'Anaesthesiology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (3, N'Animal behavior', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (4, N'Animal welfare', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (5, N'Birds', N'Birds pet and ornamental')
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (6, N'Bovine', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (7, N'Canine', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (8, N'Cardiology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (9, N'Chiropractic', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (10, N'Clinical pathology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (11, N'Clinical pharmacology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (12, N'Dentistry', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (13, N'Dermatology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (14, N'Diagnostic imaging', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (15, N'Equine', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (16, N'Emergency and critical care', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (17, N'Feline', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (18, N'Internal medicine', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (19, N'Laboratory animal medicine', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (20, N'Microbiology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (21, N'Neurology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (22, N'Nutrition', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (23, N'Oncology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (24, N'Ophthalmology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (25, N'Parasitology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (26, N'Pathology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (27, N'Poultry', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (28, N'Preventive medicine', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (29, N'Radiology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (30, N'Reptile and amphibian', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (31, N'Shelter medicine', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (32, N'State veterinary medicine', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (33, N'Sports medicine', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (34, N'Surgery', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (35, N'Theriogenology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (36, N'Toxicology', NULL)
INSERT [dbo].[VetSpeciality] ([ID], [Name], [Description]) VALUES (37, N'Zoological medicine', N'Zoological medicine includes zoo, wildlife, aquatics, and exotic pet species')
SET IDENTITY_INSERT [dbo].[VetSpeciality] OFF
