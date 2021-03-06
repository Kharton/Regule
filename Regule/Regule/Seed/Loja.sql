CREATE DATABASE [Loja]
GO
USE [Loja]
GO
/****** Object:  Table [dbo].[CliComunicar]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO	
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CliComunicar](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Principal] [bit] NULL,
	[Valor] [varchar](50) NULL,
	[Ordem] [tinyint] NULL,
	[Tel] [bit] NULL,
	[IdPessoa] [int] NOT NULL,
 CONSTRAINT [PK_CliComunicar_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Compra]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compra](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Data] [date] NULL,
	[IdPessoa] [int] NOT NULL,
	[Desconto] [money] NULL,
 CONSTRAINT [PK_Compra] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CompraProduto]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompraProduto](
	[IdPro] [int] NOT NULL,
	[IdComp] [int] NOT NULL,
	[IdUnidade] [int] NOT NULL,
	[Quantidade] [int] NULL,
	[Preco] [money] NULL,
 CONSTRAINT [PK_CompraProduto] PRIMARY KEY CLUSTERED 
(
	[IdPro] ASC,
	[IdComp] ASC,
	[IdUnidade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Fisica]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fisica](
	[Id] [int] NOT NULL,
	[CPF] [char](11) NULL,
 CONSTRAINT [PK_Fisica] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Funcionario]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Funcionario](
	[Id] [int] NOT NULL,
	[RG] [char](9) NULL,
	[Salario] [money] NULL,
	[CarteiraTrb] [nvarchar](50) NULL,
	[Dirige] [bit] NULL,
	[Tecnico] [bit] NULL,
	[Observacao] [varchar](1000) NULL,
 CONSTRAINT [PK_Funcionario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Juridica]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Juridica](
	[Id] [int] NOT NULL,
	[CNPJ] [char](14) NULL,
	[RazaoSocial] [varchar](250) NULL,
 CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pagamento]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagamento](
	[IdFun] [int] NOT NULL,
	[Data] [date] NULL,
	[Referencia] [date] NULL,
	[Valor] [money] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Pagamento_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pessoa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](250) NULL,
	[Fornecedor] [bit] NULL,
 CONSTRAINT [PK_Associado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Produto]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Produto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](150) NULL,
 CONSTRAINT [PK_Produto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Unidade]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Unidade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sigla] [varchar](4) NULL,
	[Descricao] [varchar](450) NULL,
 CONSTRAINT [PK_Unidade] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Venda]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venda](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPessoa] [int] NOT NULL,
	[Data] [date] NULL,
	[Desconto] [money] NULL,
 CONSTRAINT [PK_Venda] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VendaProduto]    Script Date: 13/12/2015 15:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendaProduto](
	[IdPro] [int] NOT NULL,
	[IdVend] [int] NOT NULL,
	[IdUnidade] [int] NOT NULL,
	[Quantidade] [int] NULL,
	[Preco] [money] NULL,
 CONSTRAINT [PK_VendaProduto] PRIMARY KEY CLUSTERED 
(
	[IdPro] ASC,
	[IdVend] ASC,
	[IdUnidade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CliComunicar]  WITH CHECK ADD  CONSTRAINT [FK_CliComunicar_Pessoa] FOREIGN KEY([IdPessoa])
REFERENCES [dbo].[Pessoa] ([Id])
GO
ALTER TABLE [dbo].[CliComunicar] CHECK CONSTRAINT [FK_CliComunicar_Pessoa]
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD  CONSTRAINT [FK_Compra_Associado] FOREIGN KEY([IdPessoa])
REFERENCES [dbo].[Pessoa] ([Id])
GO
ALTER TABLE [dbo].[Compra] CHECK CONSTRAINT [FK_Compra_Associado]
GO
ALTER TABLE [dbo].[CompraProduto]  WITH CHECK ADD  CONSTRAINT [FK_CompraProduto_Compra] FOREIGN KEY([IdComp])
REFERENCES [dbo].[Compra] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompraProduto] CHECK CONSTRAINT [FK_CompraProduto_Compra]
GO
ALTER TABLE [dbo].[CompraProduto]  WITH CHECK ADD  CONSTRAINT [FK_CompraProduto_Produto] FOREIGN KEY([IdPro])
REFERENCES [dbo].[Produto] ([Id])
GO
ALTER TABLE [dbo].[CompraProduto] CHECK CONSTRAINT [FK_CompraProduto_Produto]
GO
ALTER TABLE [dbo].[CompraProduto]  WITH CHECK ADD  CONSTRAINT [FK_CompraProduto_Unidade] FOREIGN KEY([IdUnidade])
REFERENCES [dbo].[Unidade] ([Id])
GO
ALTER TABLE [dbo].[CompraProduto] CHECK CONSTRAINT [FK_CompraProduto_Unidade]
GO
ALTER TABLE [dbo].[Fisica]  WITH CHECK ADD  CONSTRAINT [FK_Fisica_Pessoa] FOREIGN KEY([Id])
REFERENCES [dbo].[Pessoa] ([Id])
GO
ALTER TABLE [dbo].[Fisica] CHECK CONSTRAINT [FK_Fisica_Pessoa]
GO
ALTER TABLE [dbo].[Funcionario]  WITH CHECK ADD  CONSTRAINT [FK_Funcionario_Fisica] FOREIGN KEY([Id])
REFERENCES [dbo].[Fisica] ([Id])
GO
ALTER TABLE [dbo].[Funcionario] CHECK CONSTRAINT [FK_Funcionario_Fisica]
GO
ALTER TABLE [dbo].[Juridica]  WITH CHECK ADD  CONSTRAINT [FK_Juridica_Pessoa] FOREIGN KEY([Id])
REFERENCES [dbo].[Pessoa] ([Id])
GO
ALTER TABLE [dbo].[Juridica] CHECK CONSTRAINT [FK_Juridica_Pessoa]
GO
ALTER TABLE [dbo].[Pagamento]  WITH CHECK ADD  CONSTRAINT [FK_Pagamento_Funcionario] FOREIGN KEY([IdFun])
REFERENCES [dbo].[Funcionario] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pagamento] CHECK CONSTRAINT [FK_Pagamento_Funcionario]
GO
ALTER TABLE [dbo].[Venda]  WITH CHECK ADD  CONSTRAINT [FK_Venda_Associado] FOREIGN KEY([IdPessoa])
REFERENCES [dbo].[Pessoa] ([Id])
GO
ALTER TABLE [dbo].[Venda] CHECK CONSTRAINT [FK_Venda_Associado]
GO
ALTER TABLE [dbo].[VendaProduto]  WITH CHECK ADD  CONSTRAINT [FK_VendaProduto_Produto] FOREIGN KEY([IdPro])
REFERENCES [dbo].[Produto] ([Id])
GO
ALTER TABLE [dbo].[VendaProduto] CHECK CONSTRAINT [FK_VendaProduto_Produto]
GO
ALTER TABLE [dbo].[VendaProduto]  WITH CHECK ADD  CONSTRAINT [FK_VendaProduto_Unidade] FOREIGN KEY([IdUnidade])
REFERENCES [dbo].[Unidade] ([Id])
GO
ALTER TABLE [dbo].[VendaProduto] CHECK CONSTRAINT [FK_VendaProduto_Unidade]
GO
ALTER TABLE [dbo].[VendaProduto]  WITH CHECK ADD  CONSTRAINT [FK_VendaProduto_Venda] FOREIGN KEY([IdVend])
REFERENCES [dbo].[Venda] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VendaProduto] CHECK CONSTRAINT [FK_VendaProduto_Venda]
GO
