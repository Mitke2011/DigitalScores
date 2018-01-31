USE [digitalscores]
GO

/****** Object:  Table [dbo].[Utakmica]    Script Date: 31-Jan-18 8:37:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Utakmice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kolo_Id] [int] NOT NULL,
	[Klub_Id] [int] NOT NULL,
	[Pocetak_Vreme] [datetime] NULL,
	[Kraj_Vreme] [datetime] NULL,
	[Sudija1_Id] [int] NOT NULL,
	[Sudija2_Id] [int] NOT NULL,
	[Delegat_Id] [int] NOT NULL,
	[Liga_Id] [int] NOT NULL,
	[Hala_Id] [int] NOT NULL,
	[Sezona_Id] [int] NOT NULL,
	[Napomena_Delegata] [nvarchar](max) NULL,
 CONSTRAINT [PK_Utakmica] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Utakmica]  WITH CHECK ADD  CONSTRAINT [FK_Utakmica_Hala] FOREIGN KEY([Hala_Id])
REFERENCES [dbo].[Hala] ([Id])
GO

ALTER TABLE [dbo].[Utakmica] CHECK CONSTRAINT [FK_Utakmica_Hala]
GO

ALTER TABLE [dbo].[Utakmica]  WITH CHECK ADD  CONSTRAINT [FK_Utakmica_Klub] FOREIGN KEY([Klub_Id])
REFERENCES [dbo].[Klub] ([Id])
GO

ALTER TABLE [dbo].[Utakmica] CHECK CONSTRAINT [FK_Utakmica_Klub]
GO

ALTER TABLE [dbo].[Utakmica]  WITH CHECK ADD  CONSTRAINT [FK_Utakmica_Kolo] FOREIGN KEY([Kolo_Id])
REFERENCES [dbo].[Kolo] ([Id])
GO

ALTER TABLE [dbo].[Utakmica] CHECK CONSTRAINT [FK_Utakmica_Kolo]
GO

ALTER TABLE [dbo].[Utakmica]  WITH CHECK ADD  CONSTRAINT [FK_Utakmica_Liga] FOREIGN KEY([Liga_Id])
REFERENCES [dbo].[Liga] ([Id])
GO

ALTER TABLE [dbo].[Utakmica] CHECK CONSTRAINT [FK_Utakmica_Liga]
GO

ALTER TABLE [dbo].[Utakmica]  WITH CHECK ADD  CONSTRAINT [FK_Utakmica_Sezona] FOREIGN KEY([Sezona_Id])
REFERENCES [dbo].[Sezona] ([Id])
GO

ALTER TABLE [dbo].[Utakmica] CHECK CONSTRAINT [FK_Utakmica_Sezona]
GO

ALTER TABLE [dbo].[Utakmica]  WITH CHECK ADD  CONSTRAINT [FK_Utakmica_Sudije] FOREIGN KEY([Sudija1_Id])
REFERENCES [dbo].[Sudije] ([Id])
GO

ALTER TABLE [dbo].[Utakmica] CHECK CONSTRAINT [FK_Utakmica_Sudije]
GO

ALTER TABLE [dbo].[Utakmica]  WITH CHECK ADD  CONSTRAINT [FK_Utakmica_Sudije1] FOREIGN KEY([Sudija2_Id])
REFERENCES [dbo].[Sudije] ([Id])
GO

ALTER TABLE [dbo].[Utakmica] CHECK CONSTRAINT [FK_Utakmica_Sudije1]
GO

ALTER TABLE [dbo].[Utakmica]  WITH CHECK ADD  CONSTRAINT [FK_Utakmica_Users] FOREIGN KEY([Delegat_Id])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Utakmica] CHECK CONSTRAINT [FK_Utakmica_Users]
GO


