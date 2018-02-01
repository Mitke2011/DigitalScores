USE [digitalscores]
GO

/****** Object:  Table [dbo].[KluboviStats]    Script Date: 01-Feb-18 11:49:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[KluboviStats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Klub_Id] [int] NOT NULL,
	[Primljeni_Poeni] [int] NOT NULL,
	[Postignuti_Poeni] [int] NOT NULL,
	[Odigrano_Utakmica] [int] NOT NULL,
	[Bodovi_Pobeda] [int] NOT NULL,
	[Bodovi_Poraz] [int] NOT NULL,
	[Ukupno_Bodova] [int] NOT NULL,
	[Sezona_Id] [int] NOT NULL,
 CONSTRAINT [PK_KluboviStatistika] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[KluboviStats]  WITH CHECK ADD  CONSTRAINT [FK_KluboviStatistika_Klub] FOREIGN KEY([Klub_Id])
REFERENCES [dbo].[Klub] ([Id])
GO

ALTER TABLE [dbo].[KluboviStats] CHECK CONSTRAINT [FK_KluboviStatistika_Klub]
GO

ALTER TABLE [dbo].[KluboviStats]  WITH CHECK ADD  CONSTRAINT [FK_KluboviStatistika_Sezona] FOREIGN KEY([Sezona_Id])
REFERENCES [dbo].[Sezone] ([Id])
GO

ALTER TABLE [dbo].[KluboviStats] CHECK CONSTRAINT [FK_KluboviStatistika_Sezona]
GO


