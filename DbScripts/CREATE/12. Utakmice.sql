USE [digitalscores]
GO

/****** Object:  Table [dbo].[Utakmice]    Script Date: 01-Feb-18 11:50:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Utakmice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kolo_Id] [int] NOT NULL,
	[Klub_Domacin_Id] [int] NOT NULL,
	[Klub_Gost_Id] [int] NOT NULL,
	[Pocetak_Vreme] [datetime] NULL,
	[Kraj_Vreme] [datetime] NULL,
	[Sudija1_Id] [int] NOT NULL,
	[Sudija2_Id] [int] NOT NULL,
	[Delegat_Id] [int] NOT NULL,
	[Liga_Id] [int] NOT NULL,
	[Hala_Id] [int] NOT NULL,
	[Sezona_Id] [int] NOT NULL,
	[Napomena_Delegata] [nvarchar](max) NULL,
 CONSTRAINT [PK_Utakmice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Utakmice]  WITH CHECK ADD  CONSTRAINT [FK_Utakmice_Hala] FOREIGN KEY([Hala_Id])
REFERENCES [dbo].[Hala] ([Id])
GO

ALTER TABLE [dbo].[Utakmice] CHECK CONSTRAINT [FK_Utakmice_Hala]
GO

ALTER TABLE [dbo].[Utakmice]  WITH CHECK ADD  CONSTRAINT [FK_Utakmice_Klub] FOREIGN KEY([Klub_Domacin_Id])
REFERENCES [dbo].[Klub] ([Id])
GO

ALTER TABLE [dbo].[Utakmice] CHECK CONSTRAINT [FK_Utakmice_Klub]
GO

ALTER TABLE [dbo].[Utakmice]  WITH CHECK ADD  CONSTRAINT [FK_Utakmice_Klub2] FOREIGN KEY([Klub_Gost_Id])
REFERENCES [dbo].[Klub] ([Id])
GO

ALTER TABLE [dbo].[Utakmice] CHECK CONSTRAINT [FK_Utakmice_Klub2]
GO

ALTER TABLE [dbo].[Utakmice]  WITH CHECK ADD  CONSTRAINT [FK_Utakmice_Kolo] FOREIGN KEY([Kolo_Id])
REFERENCES [dbo].[Kolo] ([Id])
GO

ALTER TABLE [dbo].[Utakmice] CHECK CONSTRAINT [FK_Utakmice_Kolo]
GO

ALTER TABLE [dbo].[Utakmice]  WITH CHECK ADD  CONSTRAINT [FK_Utakmice_Liga] FOREIGN KEY([Liga_Id])
REFERENCES [dbo].[Lige] ([Id])
GO

ALTER TABLE [dbo].[Utakmice] CHECK CONSTRAINT [FK_Utakmice_Liga]
GO

ALTER TABLE [dbo].[Utakmice]  WITH CHECK ADD  CONSTRAINT [FK_Utakmice_Sezona] FOREIGN KEY([Sezona_Id])
REFERENCES [dbo].[Sezone] ([Id])
GO

ALTER TABLE [dbo].[Utakmice] CHECK CONSTRAINT [FK_Utakmice_Sezona]
GO

ALTER TABLE [dbo].[Utakmice]  WITH CHECK ADD  CONSTRAINT [FK_Utakmice_Sudije] FOREIGN KEY([Sudija1_Id])
REFERENCES [dbo].[Sudije] ([Id])
GO

ALTER TABLE [dbo].[Utakmice] CHECK CONSTRAINT [FK_Utakmice_Sudije]
GO

ALTER TABLE [dbo].[Utakmice]  WITH CHECK ADD  CONSTRAINT [FK_Utakmice_Sudije1] FOREIGN KEY([Sudija2_Id])
REFERENCES [dbo].[Sudije] ([Id])
GO

ALTER TABLE [dbo].[Utakmice] CHECK CONSTRAINT [FK_Utakmice_Sudije1]
GO

ALTER TABLE [dbo].[Utakmice]  WITH CHECK ADD  CONSTRAINT [FK_Utakmice_Users] FOREIGN KEY([Delegat_Id])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Utakmice] CHECK CONSTRAINT [FK_Utakmice_Users]
GO


