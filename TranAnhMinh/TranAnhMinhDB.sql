USE [TranAnhMinhDB]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 6/21/2021 11:34:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 6/21/2021 11:34:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[CustomerID] [int] NULL,
	[TotalMoney] [int] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_fefe] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 6/21/2021 11:34:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ProductID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[Quantity] [int] NULL,
	[Price] [int] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/21/2021 11:34:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Quantity] [int] NULL,
	[Image] [nvarchar](200) NULL,
	[Description] [nvarchar](200) NULL,
	[Status] [bit] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Price] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 6/21/2021 11:34:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (1, N'Xiaomi', N'Hãng Điện thoại China')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (2, N'Iphone', N'Hãng điện thoại của Apple')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (3, N'Samsung', N'Hãng điện thoại của Hàn Quốc')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (4, N'Oppo', N'Hãng điện thoại của China')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (5, N'Google Pixel', N'Hãng điện thoại của Mỹ')
INSERT [dbo].[Category] ([ID], [Name], [Description]) VALUES (6, N'Bphone', N'Hãng điện thoại của Việt Nam')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [Name], [Quantity], [Image], [Description], [Status], [CategoryID], [Price]) VALUES (20, N'Iphone XSMAX', 502, N'th (1).jpg', N'Hãng điện thoại của Apple', 1, 2, 13000000)
INSERT [dbo].[Product] ([ID], [Name], [Quantity], [Image], [Description], [Status], [CategoryID], [Price]) VALUES (21, N'Iphone XR', 50, N'th (2).jpg', N'Hãng điện thoại của Apple', 1, 2, 9200000)
INSERT [dbo].[Product] ([ID], [Name], [Quantity], [Image], [Description], [Status], [CategoryID], [Price]) VALUES (22, N'Iphone 13', 100, N'th (3).jpg', N'Hãng điện thoại của Apple', 1, 2, 23000000)
INSERT [dbo].[Product] ([ID], [Name], [Quantity], [Image], [Description], [Status], [CategoryID], [Price]) VALUES (23, N'Iphone 7S Plus', 502, N'th (3)(1).jpg', N'Hãng điện thoại của Apple', 1, 2, 5000000)
INSERT [dbo].[Product] ([ID], [Name], [Quantity], [Image], [Description], [Status], [CategoryID], [Price]) VALUES (24, N'Samsung A54', 50, N'th (4).jpg', N'Hãng điện thoại của Korea', 1, 3, 4700000)
INSERT [dbo].[Product] ([ID], [Name], [Quantity], [Image], [Description], [Status], [CategoryID], [Price]) VALUES (25, N'Samsung Galaxy x53', 500, N'th (6).jpg', N'Hãng điện thoại của Korea', 1, 3, 8000000)
INSERT [dbo].[Product] ([ID], [Name], [Quantity], [Image], [Description], [Status], [CategoryID], [Price]) VALUES (26, N'Xiaomi X5', 502, N'th (7).jpg', N'Hãng Điện thoại China', 1, 1, 10000000)
INSERT [dbo].[Product] ([ID], [Name], [Quantity], [Image], [Description], [Status], [CategoryID], [Price]) VALUES (27, N'Xiaomi mh464', 1000, N'th.jpg', N'Hãng Điện thoại China', 1, 1, 12000000)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[UserAccount] ON 

INSERT [dbo].[UserAccount] ([ID], [UserName], [Password], [Status]) VALUES (1, N'minh', N'c92f1d1f2619172bf87a12e5915702a6', 1)
INSERT [dbo].[UserAccount] ([ID], [UserName], [Password], [Status]) VALUES (2, N'pro', N'1f0f70bf2b5ad94c7387e64c16dc455a', 1)
SET IDENTITY_INSERT [dbo].[UserAccount] OFF
GO
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
