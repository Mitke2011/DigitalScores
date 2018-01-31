USE [digitalscores]
GO

/****** Object:  Table [dbo].[Sudije]    Script Date: 31-Jan-18 8:37:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sudije](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ime] [nvarchar](50) NULL,
	[Prezime] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Telefon] [nvarchar](50) NULL,
	[Grad] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sudije] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


