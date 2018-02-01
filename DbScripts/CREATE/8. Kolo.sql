USE [digitalscores]
GO

/****** Object:  Table [dbo].[Kolo]    Script Date: 31-Jan-18 8:36:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kolo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NULL,
	[Sezona_Id] [int] NULL,
 CONSTRAINT [PK_Kolo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Kolo]  WITH CHECK ADD  CONSTRAINT [FK_Kolo_Sezona] FOREIGN KEY([Sezona_Id])
REFERENCES [dbo].[Sezone] ([Id])
GO

ALTER TABLE [dbo].[Kolo] CHECK CONSTRAINT [FK_Kolo_Sezona]
GO


