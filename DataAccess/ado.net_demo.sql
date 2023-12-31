USE [db_demos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Demo2]') AND type in (N'U'))
ALTER TABLE [dbo].[Demo2] DROP CONSTRAINT IF EXISTS [FK_Demo2_Demo2]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Demo1]') AND type in (N'U'))
ALTER TABLE [dbo].[Demo1] DROP CONSTRAINT IF EXISTS [DF_Demo1_DateCreated]
GO
/****** Object:  Index [INDEX_Demo2_Demo1Id]    Script Date: 2023-09-26 3:31:50 PM ******/
DROP INDEX IF EXISTS [INDEX_Demo2_Demo1Id] ON [dbo].[Demo2]
GO
/****** Object:  Table [dbo].[Demo2]    Script Date: 2023-09-26 3:31:50 PM ******/
DROP TABLE IF EXISTS [dbo].[Demo2]
GO
/****** Object:  Table [dbo].[Demo1]    Script Date: 2023-09-26 3:31:50 PM ******/
DROP TABLE IF EXISTS [dbo].[Demo1]
GO
/****** Object:  Table [dbo].[Demo1]    Script Date: 2023-09-26 3:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Demo1](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Description] [text] NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Demo1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Demo2]    Script Date: 2023-09-26 3:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Demo2](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Description] [text] NULL,
	[Demo1Id] [int] NULL,
 CONSTRAINT [PK_Demo2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Demo1] ON 

INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (1, N'Une carotte', N'Cest maintenant une carotte!', CAST(N'2023-09-14T14:16:10.473' AS DateTime))
INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (2, N'Bananaman', N'Une banane!', CAST(N'2023-09-14T14:16:10.473' AS DateTime))
INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (3, N'navet', N'C''est un navet.', CAST(N'2023-09-21T13:37:48.620' AS DateTime))
INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (4, N'navet', N'C''est un navet.', CAST(N'2023-09-21T13:39:45.333' AS DateTime))
INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (5, N'navet', N'C''est un navet.', CAST(N'2023-09-21T13:41:35.890' AS DateTime))
INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (6, N'navet', N'C''est un navet.', CAST(N'2023-09-21T13:42:35.650' AS DateTime))
INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (7, N'courge', N'C''est une courge!', CAST(N'2023-09-21T14:00:37.893' AS DateTime))
INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (8, N'celeri', N'C''est pas mal juste de l''eau.', CAST(N'2023-09-21T14:00:37.923' AS DateTime))
INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (9, N'courge', N'C''est une courge!', CAST(N'2023-09-21T14:08:42.933' AS DateTime))
INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (10, N'celeri', N'C''est pas mal juste de l''eau.', CAST(N'2023-09-21T14:08:42.967' AS DateTime))
INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (11, N'courge', N'C''est une courge!', CAST(N'2023-09-21T14:12:01.780' AS DateTime))
INSERT [dbo].[Demo1] ([Id], [Name], [Description], [DateCreated]) VALUES (15, N'chevreMAligne', N'C''est une chèvre!', CAST(N'2023-09-26T13:45:04.107' AS DateTime))
SET IDENTITY_INSERT [dbo].[Demo1] OFF
GO
SET IDENTITY_INSERT [dbo].[Demo2] ON 

INSERT [dbo].[Demo2] ([Id], [Name], [Description], [Demo1Id]) VALUES (1, N'Demo2', N'Entree dans Demo2', 1)
SET IDENTITY_INSERT [dbo].[Demo2] OFF
GO
/****** Object:  Index [INDEX_Demo2_Demo1Id]    Script Date: 2023-09-26 3:31:51 PM ******/
CREATE NONCLUSTERED INDEX [INDEX_Demo2_Demo1Id] ON [dbo].[Demo2]
(
	[Demo1Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Demo1] ADD  CONSTRAINT [DF_Demo1_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Demo2]  WITH CHECK ADD  CONSTRAINT [FK_Demo2_Demo2] FOREIGN KEY([Demo1Id])
REFERENCES [dbo].[Demo1] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Demo2] CHECK CONSTRAINT [FK_Demo2_Demo2]
GO
