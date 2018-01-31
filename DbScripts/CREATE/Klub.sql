USE [digitalscores]
GO

/****** Object:  Table [dbo].[Klub]    Script Date: 31-Jan-18 8:36:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Klub](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[Grad] [nvarchar](50) NOT NULL,
	[Trener] [nvarchar](50) NOT NULL,
	[LicencaPDF] [nvarchar](50) NOT NULL,
	[Sport_Id] [int] NOT NULL,
 CONSTRAINT [PK_Klub] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Klub]  WITH CHECK ADD  CONSTRAINT [FK_Klub_Sport] FOREIGN KEY([Sport_Id])
REFERENCES [dbo].[Sport] ([Id])
GO

ALTER TABLE [dbo].[Klub] CHECK CONSTRAINT [FK_Klub_Sport]
GO


