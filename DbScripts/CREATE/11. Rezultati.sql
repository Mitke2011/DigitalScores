USE [digitalscores]
GO

/****** Object:  Table [dbo].[Rezultati]    Script Date: 31-Jan-18 8:37:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rezultati](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Utakmica_Id] [int] NOT NULL,
	[rezultat_Q1_D] [int] NULL,
	[rezultat_Q2_D] [int] NULL,
	[rezultat_Q3_D] [int] NULL,
	[rezultat_Q4_D] [int] NULL,
	[rezultat_OT1_D] [int] NULL,
	[rezultat_OT2_D] [int] NULL,
	[rezultat_H1_D] [int] NULL,
	[rezultat_H2_D] [int] NULL,
	[rezultat_Q1_G] [int] NULL,
	[rezultat_Q2_G] [int] NULL,
	[rezultat_Q3_G] [int] NULL,
	[rezultat_Q4_G] [int] NULL,
	[rezultat_OT1_G] [int] NULL,
	[rezultat_OT2_G] [int] NULL,
	[rezultat_H1_G] [int] NULL,
	[rezultat_H2_G] [int] NULL,
	[Rezultat_Konacni_D] [int] NULL,
	[Rezultat_Konacni_G] [int] NULL,
 CONSTRAINT [PK_Rezultati] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Rezultati]  WITH CHECK ADD  CONSTRAINT [FK_Rezultati_Utakmica] FOREIGN KEY([Utakmica_Id])
REFERENCES [dbo].[Utakmice] ([Id])
GO

ALTER TABLE [dbo].[Rezultati] CHECK CONSTRAINT [FK_Rezultati_Utakmica]
GO


