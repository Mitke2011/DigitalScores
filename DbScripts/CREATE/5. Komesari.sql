USE [digitalscores]
GO

/****** Object:  Table [dbo].[Komesari]    Script Date: 01-Feb-18 11:49:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Komesari](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ime] [nvarchar](50) NOT NULL,
	[Prezime] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Telefon] [nvarchar](50) NULL,
	[Liga_Id] [int] NULL,
 CONSTRAINT [PK_Komesari] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Komesari]  WITH CHECK ADD  CONSTRAINT [FK_Komesari_Liga] FOREIGN KEY([Liga_Id])
REFERENCES [dbo].[Lige] ([Id])
GO

ALTER TABLE [dbo].[Komesari] CHECK CONSTRAINT [FK_Komesari_Liga]
GO


