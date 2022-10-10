USE [Java_Florist]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[htUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[htUsers] DROP CONSTRAINT IF EXISTS [DF_htUsers_Active]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[htUsers]') AND type in (N'U'))
ALTER TABLE [dbo].[htUsers] DROP CONSTRAINT IF EXISTS [DF_htUsers_Admin]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[Orders]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[Message]
GO
/****** Object:  Table [dbo].[htUsers]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[htUsers]
GO
/****** Object:  Table [dbo].[htUserFunction]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[htUserFunction]
GO
/****** Object:  Table [dbo].[htFunctions]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[htFunctions]
GO
/****** Object:  Table [dbo].[FlowerBouqueti]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[FlowerBouqueti]
GO
/****** Object:  Table [dbo].[Flower]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[Flower]
GO
/****** Object:  Table [dbo].[DescBouqueti]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[DescBouqueti]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[Customer]
GO
/****** Object:  Table [dbo].[CriteriaBouqueti]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[CriteriaBouqueti]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[CartItem]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[Cart]
GO
/****** Object:  Table [dbo].[BouquetiMessage]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[BouquetiMessage]
GO
/****** Object:  Table [dbo].[Bouqueti]    Script Date: 10/10/2022 5:02:19 PM ******/
DROP TABLE IF EXISTS [dbo].[Bouqueti]
GO
/****** Object:  Table [dbo].[Bouqueti]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bouqueti](
	[BouquetiId] [int] IDENTITY(1,1) NOT NULL,
	[BouquetiName] [nvarchar](255) NULL,
	[Price] [decimal](18, 0) NULL,
	[Image] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[Desc] [nchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[BouquetiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BouquetiMessage]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BouquetiMessage](
	[BouquetiMessageId] [int] IDENTITY(1,1) NOT NULL,
	[BouquetiId] [int] NULL,
	[OccasionId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BouquetiMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItem](
	[CartItemId] [int] IDENTITY(1,1) NOT NULL,
	[BouquetiId] [int] NULL,
	[CartId] [int] NULL,
	[TotalCount] [int] NULL,
	[Message] [nchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[CartItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CriteriaBouqueti]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CriteriaBouqueti](
	[CriteriaBouquetiId] [int] IDENTITY(1,1) NOT NULL,
	[BouquetiId] [int] NULL,
	[Criteria] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[CriteriaBouquetiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[F_Name] [nvarchar](255) NULL,
	[L_Name] [nvarchar](255) NULL,
	[Dob] [date] NULL,
	[Gender] [bit] NULL,
	[Phone] [nvarchar](50) NULL,
	[Address] [nvarchar](255) NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DescBouqueti]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DescBouqueti](
	[DescBouquetiId] [int] IDENTITY(1,1) NOT NULL,
	[BouquetiId] [int] NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[DescBouquetiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flower]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flower](
	[FlowerId] [int] IDENTITY(1,1) NOT NULL,
	[FlowerName] [nvarchar](255) NULL,
	[Color] [nvarchar](255) NULL,
	[Image] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[FlowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FlowerBouqueti]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlowerBouqueti](
	[FlowerBouquetiId] [int] IDENTITY(1,1) NOT NULL,
	[BouquetiId] [int] NULL,
	[FlowerId] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[FlowerBouquetiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[htFunctions]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[htFunctions](
	[FuntionId] [int] IDENTITY(1,1) NOT NULL,
	[FunctionCode] [nvarchar](50) NULL,
	[FunctionName] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[FuntionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[htUserFunction]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[htUserFunction](
	[UserFunctionId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[FunctionId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserFunctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[htUsers]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[htUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[PassWord] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Admin] [bit] NULL,
	[Active] [bit] NULL,
	[Email] [nvarchar](200) NULL,
	[UserCategory] [int] NULL,
 CONSTRAINT [PK_htUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[OccasionId] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[OccasionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 10/10/2022 5:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[Status] [int] NULL,
	[Address_From] [nvarchar](max) NULL,
	[Address_To] [nvarchar](max) NULL,
	[TypePayment] [int] NULL,
	[TimeDelivery] [nvarchar](max) NULL,
	[CartId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bouqueti] ON 
GO
INSERT [dbo].[Bouqueti] ([BouquetiId], [BouquetiName], [Price], [Image], [Status], [Desc]) VALUES (1, N'The Golden Girl | Pretty Things', CAST(20 AS Decimal(18, 0)), N'https://floom.imgix.net/products/a1717546-35c5-40f3-aae9-5e542c2a2559.jpeg?auto=format&crop=focalpoint&fit=crop&fm=pjpg&fp-x=0.5&fp-y=0.5&h=600&ixlib=php-1.1.0&q=75&w=600&s=bf0c9941e9eea803a30c88a6317de4f3', 1, N'It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using                  ')
GO
INSERT [dbo].[Bouqueti] ([BouquetiId], [BouquetiName], [Price], [Image], [Status], [Desc]) VALUES (2, N'Bar Fiore', CAST(5 AS Decimal(18, 0)), N'https://junandus.com.my/wp-content/uploads/2020/12/Carol-Flower-Bouquet-Fresh-Flower-1-Jan22.jpg', 1, N'It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using                  ')
GO
INSERT [dbo].[Bouqueti] ([BouquetiId], [BouquetiName], [Price], [Image], [Status], [Desc]) VALUES (3, N'Mix Flower', CAST(30 AS Decimal(18, 0)), N'https://asset.bloomnation.com/c_pad,d_vendor:global:catalog:product:image.png,f_auto,fl_preserve_transparency,q_auto/v1651981837/vendor/3288/catalog/product/2/0/20210708061405_file_60e7406d5ad51_60e740a7efdc7..jpg', 1, N'It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using                  ')
GO
INSERT [dbo].[Bouqueti] ([BouquetiId], [BouquetiName], [Price], [Image], [Status], [Desc]) VALUES (4, N'Rose', CAST(15 AS Decimal(18, 0)), N'https://www.zieduserviss.lv/1403-large_default/flower-box-autumn-kiss.jpg', 1, N'It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using                  ')
GO
INSERT [dbo].[Bouqueti] ([BouquetiId], [BouquetiName], [Price], [Image], [Status], [Desc]) VALUES (5, N'Lilies Flower', CAST(50 AS Decimal(18, 0)), N'https://i1.fnp.com/images/pr/l/v20201107021055/eternal-assorted-flowers-bouquet_1.jpg', 1, N'It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using                  ')
GO
INSERT [dbo].[Bouqueti] ([BouquetiId], [BouquetiName], [Price], [Image], [Status], [Desc]) VALUES (6, N'Mix Color Rose', CAST(20 AS Decimal(18, 0)), N'https://junandus.com.my/wp-content/uploads/2021/07/Eva-Flower-Bouquet-Small-1-1.jpg', 1, N'It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using                  ')
GO
SET IDENTITY_INSERT [dbo].[Bouqueti] OFF
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 
GO
INSERT [dbo].[Cart] ([CartId], [Status], [UserId]) VALUES (1, 2, 1)
GO
INSERT [dbo].[Cart] ([CartId], [Status], [UserId]) VALUES (2, 2, 1)
GO
INSERT [dbo].[Cart] ([CartId], [Status], [UserId]) VALUES (3, 1, 1)
GO
INSERT [dbo].[Cart] ([CartId], [Status], [UserId]) VALUES (4, 1, 2)
GO
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[CartItem] ON 
GO
INSERT [dbo].[CartItem] ([CartItemId], [BouquetiId], [CartId], [TotalCount], [Message]) VALUES (1, 1, 1, 1, NULL)
GO
INSERT [dbo].[CartItem] ([CartItemId], [BouquetiId], [CartId], [TotalCount], [Message]) VALUES (2, 2, 1, 1, NULL)
GO
INSERT [dbo].[CartItem] ([CartItemId], [BouquetiId], [CartId], [TotalCount], [Message]) VALUES (3, 3, 1, 1, NULL)
GO
INSERT [dbo].[CartItem] ([CartItemId], [BouquetiId], [CartId], [TotalCount], [Message]) VALUES (4, 2, 2, 2, NULL)
GO
INSERT [dbo].[CartItem] ([CartItemId], [BouquetiId], [CartId], [TotalCount], [Message]) VALUES (5, 5, 3, 1, NULL)
GO
INSERT [dbo].[CartItem] ([CartItemId], [BouquetiId], [CartId], [TotalCount], [Message]) VALUES (6, 1, 4, 20, NULL)
GO
INSERT [dbo].[CartItem] ([CartItemId], [BouquetiId], [CartId], [TotalCount], [Message]) VALUES (7, 2, 4, 1, NULL)
GO
INSERT [dbo].[CartItem] ([CartItemId], [BouquetiId], [CartId], [TotalCount], [Message]) VALUES (8, 3, 4, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[CartItem] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 
GO
INSERT [dbo].[Customer] ([CustomerId], [F_Name], [L_Name], [Dob], [Gender], [Phone], [Address], [UserId]) VALUES (1, N'ADMINSTRATOR', N'No Name', CAST(N'2000-10-14' AS Date), 1, N'031294123', N'Ha Noi', 1)
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Flower] ON 
GO
INSERT [dbo].[Flower] ([FlowerId], [FlowerName], [Color], [Image]) VALUES (1, N'Rose', N'Yellow', N'../Content/greeno/images/1.jpg')
GO
INSERT [dbo].[Flower] ([FlowerId], [FlowerName], [Color], [Image]) VALUES (2, N'Flower1', N'Pink', N'../Content/greeno/images/2.jpg')
GO
INSERT [dbo].[Flower] ([FlowerId], [FlowerName], [Color], [Image]) VALUES (3, N'Flower2', N'Pink', N'../Content/greeno/images/3.jpg')
GO
INSERT [dbo].[Flower] ([FlowerId], [FlowerName], [Color], [Image]) VALUES (4, N'Flower3', N'Green', N'https://localhost:44359/Content/greeno/images/4.jpg')
GO
INSERT [dbo].[Flower] ([FlowerId], [FlowerName], [Color], [Image]) VALUES (5, N'Flower4', N'Orange', N'../Content/greeno/images/5.jpg')
GO
INSERT [dbo].[Flower] ([FlowerId], [FlowerName], [Color], [Image]) VALUES (6, N'Flower5', N'Yellow', N'../Content/greeno/images/6.jpg')
GO
SET IDENTITY_INSERT [dbo].[Flower] OFF
GO
SET IDENTITY_INSERT [dbo].[htUsers] ON 
GO
INSERT [dbo].[htUsers] ([UserId], [UserName], [PassWord], [FullName], [Admin], [Active], [Email], [UserCategory]) VALUES (1, N'admin', N'202cb962ac59075b964b07152d234b70', N'ADMINSTRATOR No Name', 1, 1, N'admin@gmail.com', NULL)
GO
SET IDENTITY_INSERT [dbo].[htUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[Message] ON 
GO
INSERT [dbo].[Message] ([OccasionId], [Message]) VALUES (1, N'LOVE YOU FOREVER !')
GO
SET IDENTITY_INSERT [dbo].[Message] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 
GO
INSERT [dbo].[Orders] ([OrderId], [CustomerId], [Status], [Address_From], [Address_To], [TypePayment], [TimeDelivery], [CartId]) VALUES (1, 1, 1, NULL, NULL, 2, N'Item will be delivered in 5 hours from: 10/05/2022 15:47  to: 10/05/2022 20:47', 1)
GO
INSERT [dbo].[Orders] ([OrderId], [CustomerId], [Status], [Address_From], [Address_To], [TypePayment], [TimeDelivery], [CartId]) VALUES (2, 1, 1, NULL, NULL, 2, N'Item will be delivered in 5 hours from: 10/05/2022 15:49  to: 10/05/2022 20:49', 2)
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
ALTER TABLE [dbo].[htUsers] ADD  CONSTRAINT [DF_htUsers_Admin]  DEFAULT ((0)) FOR [Admin]
GO
ALTER TABLE [dbo].[htUsers] ADD  CONSTRAINT [DF_htUsers_Active]  DEFAULT ((1)) FOR [Active]
GO
