USE [digitalscores]
GO

/****** Object:  Table [dbo].[Sezone]    Script Date: 01-Feb-18 11:49:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sezone](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[Liga_Id] [int] NULL,
 CONSTRAINT [PK_Sezona] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Sezone]  WITH CHECK ADD  CONSTRAINT [FK_Sezona_Sezona1] FOREIGN KEY([Liga_Id])
REFERENCES [dbo].[Lige] ([Id])
GO

ALTER TABLE [dbo].[Sezone] CHECK CONSTRAINT [FK_Sezona_Sezona1]
GO


