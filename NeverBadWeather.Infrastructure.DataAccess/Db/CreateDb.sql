CREATE TABLE [dbo].[ClothingRule](
	[Id] [uniqueidentifier] NOT NULL,
	[IsRaining] [bit] NULL,
	[FromTemperature] [int] NOT NULL,
	[ToTemperature] [int] NOT NULL,
	[Clothes] [nvarchar](max) NOT NULL,
	[UserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ClothingRule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 09.07.2020 09:42:52 ******/
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[ClothingRule] ([Id], [IsRaining], [FromTemperature], [ToTemperature], [Clothes], [UserId]) VALUES (N'1b050154-b32f-42af-9ed8-2563179a76c9', NULL, 20, 30, N'Shorts og T-skjorte', NULL)
GO
INSERT [dbo].[ClothingRule] ([Id], [IsRaining], [FromTemperature], [ToTemperature], [Clothes], [UserId]) VALUES (N'3035f5ab-2833-4d6a-a37f-2881a9143e0f', NULL, -20, 10, N'Boblejakke', NULL)
GO
INSERT [dbo].[ClothingRule] ([Id], [IsRaining], [FromTemperature], [ToTemperature], [Clothes], [UserId]) VALUES (N'0141423d-f33c-407b-a122-79d73e46eda8', 1, 10, 20, N'Regntøydf', NULL)
GO
INSERT [dbo].[ClothingRule] ([Id], [IsRaining], [FromTemperature], [ToTemperature], [Clothes], [UserId]) VALUES (N'8d13718c-07ba-4ba9-a446-7e2036928bd5', 0, 10, 20, N'Bukse og jakke', NULL)
GO
ALTER TABLE [dbo].[ClothingRule]  WITH CHECK ADD  CONSTRAINT [FK_ClothingRule_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ClothingRule] CHECK CONSTRAINT [FK_ClothingRule_User]
GO
