USE [KVALSOKOLOV]
GO
/****** Object:  Table [dbo].[Programmers]    Script Date: 29.06.2021 16:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Programmers](
	[id] [uniqueidentifier] NOT NULL,
	[LName] [nvarchar](50) NOT NULL,
	[FName] [nvarchar](50) NOT NULL,
	[SName] [nvarchar](50) NULL,
	[Salary] [real] NULL,
 CONSTRAINT [PK_Programmers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requests]    Script Date: 29.06.2021 16:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requests](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[description] [nvarchar](255) NULL,
	[Register] [datetime] NOT NULL,
	[DateDone] [datetime] NULL,
	[Programmer] [uniqueidentifier] NULL,
	[Sender] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Senders]    Script Date: 29.06.2021 16:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Senders](
	[id] [uniqueidentifier] NOT NULL,
	[LName] [nvarchar](50) NULL,
	[FName] [nvarchar](50) NULL,
	[SName] [nvarchar](50) NULL,
	[datereg] [datetime] NULL,
	[Version] [nvarchar](10) NULL,
 CONSTRAINT [PK_Senders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Programmers] ([id], [LName], [FName], [SName], [Salary]) VALUES (N'56e4657a-fe33-4ead-af08-211614887877', N'Ларионова', N'Алена', N'Васильевна', 5000)
INSERT [dbo].[Programmers] ([id], [LName], [FName], [SName], [Salary]) VALUES (N'0a115ea9-b9c4-4879-9674-9b8932940b74', N'Соколов', N'Ренат', N'Игоревич', 10000)
INSERT [dbo].[Programmers] ([id], [LName], [FName], [SName], [Salary]) VALUES (N'12fe7f75-271d-4113-85f2-cb9f36cd75b1', N'Глускер', N'Александр', N'Игоревич', 10000)
GO
SET IDENTITY_INSERT [dbo].[Requests] ON 

INSERT [dbo].[Requests] ([id], [Name], [description], [Register], [DateDone], [Programmer], [Sender]) VALUES (1, N'Улучшение качества звука', N'', CAST(N'2021-06-12T12:12:12.000' AS DateTime), CAST(N'2021-06-12T12:15:12.000' AS DateTime), N'12fe7f75-271d-4113-85f2-cb9f36cd75b1', N'a3660d2b-d963-4381-922a-5799e4563e91')
INSERT [dbo].[Requests] ([id], [Name], [description], [Register], [DateDone], [Programmer], [Sender]) VALUES (2, N'Разрешение демонстрации нескольким участникам', N'', CAST(N'2021-06-15T12:12:12.000' AS DateTime), CAST(N'2021-06-17T12:12:12.000' AS DateTime), N'12fe7f75-271d-4113-85f2-cb9f36cd75b1', N'a3660d2b-d963-4381-922a-5799e4563e91')
INSERT [dbo].[Requests] ([id], [Name], [description], [Register], [DateDone], [Programmer], [Sender]) VALUES (9, N'Оптимизация', N'Уменьшение задержек и увеличение количества людей в сессии', CAST(N'2021-06-17T13:40:02.000' AS DateTime), CAST(N'2021-06-17T13:40:02.000' AS DateTime), NULL, N'783a36f9-e046-4ed7-a0ce-d96fe8465044')
SET IDENTITY_INSERT [dbo].[Requests] OFF
GO
INSERT [dbo].[Senders] ([id], [LName], [FName], [SName], [datereg], [Version]) VALUES (N'a3660d2b-d963-4381-922a-5799e4563e91', N'Александров', N'Роман', N'Викторович', CAST(N'2021-06-12T19:12:22.000' AS DateTime), N'1.0.0')
INSERT [dbo].[Senders] ([id], [LName], [FName], [SName], [datereg], [Version]) VALUES (N'887be26c-04cd-4eb9-815a-74ef048c54ee', N'Томашевская', N'Виктория', N'Вячеславна', CAST(N'2021-06-10T12:12:22.000' AS DateTime), N'1.2.0')
INSERT [dbo].[Senders] ([id], [LName], [FName], [SName], [datereg], [Version]) VALUES (N'783a36f9-e046-4ed7-a0ce-d96fe8465044', N'Ковалева', N'Елена', N'Вячеславна', CAST(N'2021-05-08T12:12:22.000' AS DateTime), N'0.9.8')
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Programmers] FOREIGN KEY([Programmer])
REFERENCES [dbo].[Programmers] ([id])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Programmers]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Senders] FOREIGN KEY([Sender])
REFERENCES [dbo].[Senders] ([id])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Senders]
GO
