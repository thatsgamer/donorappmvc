USE [master]
GO
/****** Object:  Database [sample]    Script Date: 12/27/2017 2:11:32 PM ******/
CREATE DATABASE [sample]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sample', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\sample.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'sample_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\sample_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [sample] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [sample].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [sample] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [sample] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [sample] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [sample] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [sample] SET ARITHABORT OFF 
GO
ALTER DATABASE [sample] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [sample] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [sample] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [sample] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [sample] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [sample] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [sample] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [sample] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [sample] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [sample] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [sample] SET  DISABLE_BROKER 
GO
ALTER DATABASE [sample] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [sample] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [sample] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [sample] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [sample] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [sample] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [sample] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [sample] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [sample] SET  MULTI_USER 
GO
ALTER DATABASE [sample] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [sample] SET DB_CHAINING OFF 
GO
ALTER DATABASE [sample] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [sample] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [sample]
GO
/****** Object:  Table [dbo].[AdminSettings]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminSettings](
	[SettingId] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationChargesForParent] [float] NOT NULL,
	[AnnualRenewalChargesForParent] [float] NOT NULL,
	[NewDonorProfileChargesForParents] [float] NOT NULL,
	[ProfileCompletedChargesForDonor] [float] NOT NULL,
	[UpdatedOn] [date] NOT NULL,
 CONSTRAINT [PK_AdminSettings] PRIMARY KEY CLUSTERED 
(
	[SettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonorAilmentDetails]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonorAilmentDetails](
	[DonorAilmentId] [int] IDENTITY(1,1) NOT NULL,
	[DonorId] [int] NOT NULL,
	[TypeOfAilment] [nvarchar](150) NULL,
	[SystemInBodyAffected] [nvarchar](max) NULL,
	[BodyPart] [nvarchar](50) NULL,
 CONSTRAINT [PK_DonorAilmentDetails] PRIMARY KEY CLUSTERED 
(
	[DonorAilmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonorCycles]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonorCycles](
	[DonorCycleId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[DonorId] [int] NOT NULL,
	[MonthAndYearOfRetrieval] [nvarchar](50) NULL,
	[FertilityAttorny] [nvarchar](50) NULL,
	[DonorEyeColor] [nvarchar](50) NULL,
	[DonorHeight] [nvarchar](50) NULL,
	[DonorAge] [nvarchar](50) NULL,
	[isApprovedByParent] [bit] NULL,
	[isApprovedByDonor] [bit] NULL,
	[isApprovedByClinic] [bit] NULL,
	[isApprovedByAgency] [bit] NULL,
 CONSTRAINT [PK_DonorCycles] PRIMARY KEY CLUSTERED 
(
	[DonorCycleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonorCycleUpdate]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonorCycleUpdate](
	[DonorCycleUpdateId] [int] IDENTITY(1,1) NOT NULL,
	[DonorCycleId] [int] NOT NULL,
	[UpdateHeading] [nvarchar](150) NOT NULL,
	[UpdateDescription] [nvarchar](max) NOT NULL,
	[UpdateDate] [date] NOT NULL,
	[isSubmitted] [bit] NOT NULL,
	[isCompleted] [bit] NOT NULL,
	[CompletionDate] [date] NULL,
 CONSTRAINT [PK_DonorCycleUpdate] PRIMARY KEY CLUSTERED 
(
	[DonorCycleUpdateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonorFamilyTreeDiseaseDetails]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonorFamilyTreeDiseaseDetails](
	[DonorFamilyDiseaseTreeId] [int] IDENTITY(1,1) NOT NULL,
	[DonorId] [int] NOT NULL,
	[FamilyTreeDiseaseId] [int] NOT NULL,
	[FamilyDiseaseQuestionId] [int] NOT NULL,
	[Answer] [nvarchar](50) NULL,
 CONSTRAINT [PK_DonorFamilyTreeDiseaseDetails] PRIMARY KEY CLUSTERED 
(
	[DonorFamilyDiseaseTreeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonorHealthQuestionsAnswered]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonorHealthQuestionsAnswered](
	[DonorHealthQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[DonorId] [int] NOT NULL,
	[HealthQuestionId] [int] NOT NULL,
	[Answer] [nvarchar](max) NULL,
 CONSTRAINT [PK_DonorHealthQuestionsAnswered] PRIMARY KEY CLUSTERED 
(
	[DonorHealthQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Donors]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Donors](
	[DonorId] [int] IDENTITY(20000,1) NOT NULL,
	[Salutation] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[ContactNumber] [nvarchar](50) NOT NULL,
	[ContactVerificationCode] [nvarchar](50) NOT NULL,
	[isContactVerified] [bit] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[EmailVerificationCode] [nvarchar](50) NOT NULL,
	[isEmailVerified] [bit] NOT NULL,
	[DateOfBirth] [nvarchar](50) NOT NULL,
	[EyeColor] [nvarchar](50) NOT NULL,
	[Height] [int] NOT NULL,
	[Race] [nvarchar](50) NOT NULL,
	[Photo] [nvarchar](max) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[Password] [nvarchar](max) NULL,
	[Salt] [nvarchar](max) NULL,
 CONSTRAINT [PK_Donors] PRIMARY KEY CLUSTERED 
(
	[DonorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonorsRegisteredWithPartners]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonorsRegisteredWithPartners](
	[DonorPartnerId] [int] IDENTITY(1,1) NOT NULL,
	[DonorId] [int] NOT NULL,
	[PartnerContactsId] [int] NOT NULL,
	[isApproved] [bit] NOT NULL,
	[DateOfApproval] [date] NULL,
	[Status] [nvarchar](max) NULL,
	[DonorIdOnPartnerSystem] [nvarchar](50) NULL,
 CONSTRAINT [PK_DonorsRegisteredWithPartners] PRIMARY KEY CLUSTERED 
(
	[DonorPartnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FamilyDiseaseQuestions]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamilyDiseaseQuestions](
	[FamilyDiseaseQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](50) NULL,
 CONSTRAINT [PK_FamilyDiseaseQuestions] PRIMARY KEY CLUSTERED 
(
	[FamilyDiseaseQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FamilyOfIntendedParents]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamilyOfIntendedParents](
	[IntendedFamilyId] [int] IDENTITY(1,1) NOT NULL,
	[PrimaryParentId] [int] NOT NULL,
	[IntendedParentId] [int] NOT NULL,
	[Relation] [nvarchar](50) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[isVerified] [bit] NOT NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_IntendedParentsFamily] PRIMARY KEY CLUSTERED 
(
	[IntendedFamilyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FamilyTreeDiseaseDetails]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamilyTreeDiseaseDetails](
	[FamilyTreeDiseaseId] [int] IDENTITY(1,1) NOT NULL,
	[DiseaseName] [nvarchar](max) NULL,
 CONSTRAINT [PK_FamilyTreeDiseaseDetails] PRIMARY KEY CLUSTERED 
(
	[FamilyTreeDiseaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HealthQuestions]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HealthQuestions](
	[HealthQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[HealthQuestion] [nvarchar](max) NULL,
	[Options] [nvarchar](max) NULL,
 CONSTRAINT [PK_HealthQuestions] PRIMARY KEY CLUSTERED 
(
	[HealthQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MatchDetails]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchDetails](
	[MatchId] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
	[DonorId] [int] NOT NULL,
	[isApprovedByParent] [bit] NULL,
	[isApprovedByDonor] [bit] NULL,
	[Status] [nvarchar](50) NULL,
	[DateOfCompletion] [date] NULL,
 CONSTRAINT [PK_MatchDetails] PRIMARY KEY CLUSTERED 
(
	[MatchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MatchRequestedByDonor]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchRequestedByDonor](
	[DonorMatchRequestId] [int] IDENTITY(1,1) NOT NULL,
	[DonorId] [int] NOT NULL,
	[RequestDate] [date] NULL,
	[isApproved] [bit] NOT NULL,
	[Status] [nvarchar](150) NULL,
 CONSTRAINT [PK_MatchRequestedByDonor] PRIMARY KEY CLUSTERED 
(
	[DonorMatchRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MatchRequestedByParent]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchRequestedByParent](
	[ParentMatchRequestId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[RequestDate] [date] NULL,
	[isApproved] [bit] NOT NULL,
	[Status] [nvarchar](150) NULL,
	[isPaidByParent] [bit] NULL,
	[ParentsPaymentId] [int] NULL,
	[Note] [nvarchar](250) NULL,
 CONSTRAINT [PK_MatchRequestedByParent] PRIMARY KEY CLUSTERED 
(
	[ParentMatchRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ParentAilmentDetails]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParentAilmentDetails](
	[ParentAilmentId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[TypeOfAilment] [nvarchar](150) NULL,
	[SystemInBodyAffected] [nvarchar](max) NULL,
	[BodyPart] [nvarchar](50) NULL,
 CONSTRAINT [PK_ParentAilmentDetails] PRIMARY KEY CLUSTERED 
(
	[ParentAilmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ParentFamilyTreeDiseaseDetails]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParentFamilyTreeDiseaseDetails](
	[ParentFamilyDiseaseTreeId] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
	[FamilyTreeDiseaseId] [int] NOT NULL,
	[FamilyDiseaseQuestionId] [int] NOT NULL,
	[Answer] [nvarchar](50) NULL,
 CONSTRAINT [PK_ParentFamilyTreeDiseaseDetails] PRIMARY KEY CLUSTERED 
(
	[ParentFamilyDiseaseTreeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Parents]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parents](
	[ParentId] [int] IDENTITY(10000,1) NOT NULL,
	[Salutation] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[ContactNumber] [nvarchar](50) NOT NULL,
	[ContactVerificationCode] [nvarchar](50) NOT NULL,
	[isContactVerified] [bit] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[EmailVerificationCode] [nvarchar](50) NOT NULL,
	[isEmailVerified] [bit] NOT NULL,
	[DateOfBirth] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[Zip] [nvarchar](50) NOT NULL,
	[CreatinDate] [date] NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Salt] [nvarchar](max) NOT NULL,
	[isPaid] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Parents] PRIMARY KEY CLUSTERED 
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ParentsHealthQuestionsAnswered]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParentsHealthQuestionsAnswered](
	[ParentHealthQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[HealthQuestionId] [int] NOT NULL,
	[Answer] [nvarchar](max) NULL,
 CONSTRAINT [PK_ParentsHealthQuestionsAnswered] PRIMARY KEY CLUSTERED 
(
	[ParentHealthQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ParentsRegisteredWithPartners]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParentsRegisteredWithPartners](
	[ParentPartnerId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[PartnerContactsId] [int] NOT NULL,
	[isApproved] [bit] NOT NULL,
	[DateOfApproval] [datetime] NULL,
	[Status] [nvarchar](max) NULL,
	[ParentIdOnPartnersSystem] [nvarchar](50) NULL,
 CONSTRAINT [PK_ParentsRegisteredWithPartners] PRIMARY KEY CLUSTERED 
(
	[ParentPartnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PartnerAndTheirContacts]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartnerAndTheirContacts](
	[PartnerContactsId] [int] IDENTITY(1,1) NOT NULL,
	[PartnerId] [int] NOT NULL,
	[ContactDesignation] [nvarchar](50) NULL,
	[ContactName] [nvarchar](50) NULL,
	[ContactEmail] [nvarchar](50) NULL,
	[ContactPhone] [nvarchar](50) NULL,
	[CreatedDate] [date] NULL,
 CONSTRAINT [PK_PartnerContacts] PRIMARY KEY CLUSTERED 
(
	[PartnerContactsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Partners]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partners](
	[PartnerId] [int] IDENTITY(30000,1) NOT NULL,
	[AssociationType] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[State] [nvarchar](50) NOT NULL,
	[Zip] [nvarchar](50) NOT NULL,
	[BusinessContact] [nvarchar](50) NOT NULL,
	[BusinessWebsite] [nvarchar](150) NULL,
	[BusinessEmail] [nvarchar](150) NOT NULL,
	[ContactPersonName] [nvarchar](50) NOT NULL,
	[ContactPersonContact] [nvarchar](50) NOT NULL,
	[ContactPersonEmail] [nvarchar](150) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Salt] [nvarchar](max) NOT NULL,
	[CreationDate] [date] NOT NULL,
 CONSTRAINT [PK_Partners] PRIMARY KEY CLUSTERED 
(
	[PartnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PartnersAttachedToDonorCycle]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartnersAttachedToDonorCycle](
	[DonorCyclePartnersId] [int] IDENTITY(1,1) NOT NULL,
	[DonorCycleId] [int] NOT NULL,
	[PartnerContactsId] [int] NOT NULL,
	[isApprovedByPartner] [bit] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[DateOfApproval] [date] NULL,
 CONSTRAINT [PK_PartnersAttachedToDonorCycle] PRIMARY KEY CLUSTERED 
(
	[DonorCyclePartnersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentSummuryDonors]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentSummuryDonors](
	[DonorPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[DonorId] [int] NOT NULL,
	[Amount] [float] NULL,
	[PaymentFor] [nvarchar](50) NULL,
	[PaymentMode] [nvarchar](50) NULL,
	[PaymentDescription] [nvarchar](max) NULL,
	[PaymentStatus] [bit] NOT NULL,
	[TransactionReferenceNumber] [nvarchar](50) NULL,
	[isDue] [bit] NOT NULL,
	[DueDate] [date] NULL,
	[Note] [nvarchar](150) NULL,
 CONSTRAINT [PK_PaymentSummuryDonors] PRIMARY KEY CLUSTERED 
(
	[DonorPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentSummuryParents]    Script Date: 12/27/2017 2:11:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentSummuryParents](
	[ParentsPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[Amount] [float] NOT NULL,
	[PaymentFor] [nvarchar](50) NOT NULL,
	[PaymentDescription] [nvarchar](max) NOT NULL,
	[PaymentStatus] [bit] NOT NULL,
	[TransactionReferenceNumber] [nvarchar](50) NULL,
	[isDue] [bit] NULL,
	[DueDate] [date] NULL,
	[Note] [nvarchar](150) NULL,
 CONSTRAINT [PK_PaymentSummuryParents] PRIMARY KEY CLUSTERED 
(
	[ParentsPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[DonorAilmentDetails]  WITH CHECK ADD  CONSTRAINT [FK_DonorAilmentDetails_Donors] FOREIGN KEY([DonorId])
REFERENCES [dbo].[Donors] ([DonorId])
GO
ALTER TABLE [dbo].[DonorAilmentDetails] CHECK CONSTRAINT [FK_DonorAilmentDetails_Donors]
GO
ALTER TABLE [dbo].[DonorCycles]  WITH CHECK ADD  CONSTRAINT [FK_DonorCycles_Donors] FOREIGN KEY([DonorId])
REFERENCES [dbo].[Donors] ([DonorId])
GO
ALTER TABLE [dbo].[DonorCycles] CHECK CONSTRAINT [FK_DonorCycles_Donors]
GO
ALTER TABLE [dbo].[DonorCycles]  WITH CHECK ADD  CONSTRAINT [FK_DonorCycles_Parents] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Parents] ([ParentId])
GO
ALTER TABLE [dbo].[DonorCycles] CHECK CONSTRAINT [FK_DonorCycles_Parents]
GO
ALTER TABLE [dbo].[DonorCycleUpdate]  WITH CHECK ADD  CONSTRAINT [FK_DonorCycleUpdate_DonorCycles] FOREIGN KEY([DonorCycleId])
REFERENCES [dbo].[DonorCycles] ([DonorCycleId])
GO
ALTER TABLE [dbo].[DonorCycleUpdate] CHECK CONSTRAINT [FK_DonorCycleUpdate_DonorCycles]
GO
ALTER TABLE [dbo].[DonorFamilyTreeDiseaseDetails]  WITH CHECK ADD  CONSTRAINT [FK_DonorFamilyTreeDiseaseDetails_Donors] FOREIGN KEY([DonorId])
REFERENCES [dbo].[Donors] ([DonorId])
GO
ALTER TABLE [dbo].[DonorFamilyTreeDiseaseDetails] CHECK CONSTRAINT [FK_DonorFamilyTreeDiseaseDetails_Donors]
GO
ALTER TABLE [dbo].[DonorFamilyTreeDiseaseDetails]  WITH CHECK ADD  CONSTRAINT [FK_DonorFamilyTreeDiseaseDetails_FamilyDiseaseQuestions] FOREIGN KEY([FamilyDiseaseQuestionId])
REFERENCES [dbo].[FamilyDiseaseQuestions] ([FamilyDiseaseQuestionId])
GO
ALTER TABLE [dbo].[DonorFamilyTreeDiseaseDetails] CHECK CONSTRAINT [FK_DonorFamilyTreeDiseaseDetails_FamilyDiseaseQuestions]
GO
ALTER TABLE [dbo].[DonorFamilyTreeDiseaseDetails]  WITH CHECK ADD  CONSTRAINT [FK_DonorFamilyTreeDiseaseDetails_FamilyTreeDiseaseDetails] FOREIGN KEY([FamilyTreeDiseaseId])
REFERENCES [dbo].[FamilyTreeDiseaseDetails] ([FamilyTreeDiseaseId])
GO
ALTER TABLE [dbo].[DonorFamilyTreeDiseaseDetails] CHECK CONSTRAINT [FK_DonorFamilyTreeDiseaseDetails_FamilyTreeDiseaseDetails]
GO
ALTER TABLE [dbo].[DonorHealthQuestionsAnswered]  WITH CHECK ADD  CONSTRAINT [FK_DonorHealthQuestionsAnswered_Donors] FOREIGN KEY([DonorId])
REFERENCES [dbo].[Donors] ([DonorId])
GO
ALTER TABLE [dbo].[DonorHealthQuestionsAnswered] CHECK CONSTRAINT [FK_DonorHealthQuestionsAnswered_Donors]
GO
ALTER TABLE [dbo].[DonorHealthQuestionsAnswered]  WITH CHECK ADD  CONSTRAINT [FK_DonorHealthQuestionsAnswered_HealthQuestions] FOREIGN KEY([HealthQuestionId])
REFERENCES [dbo].[HealthQuestions] ([HealthQuestionId])
GO
ALTER TABLE [dbo].[DonorHealthQuestionsAnswered] CHECK CONSTRAINT [FK_DonorHealthQuestionsAnswered_HealthQuestions]
GO
ALTER TABLE [dbo].[DonorsRegisteredWithPartners]  WITH CHECK ADD  CONSTRAINT [FK_DonorsRegisteredWithPartners_Donors] FOREIGN KEY([DonorId])
REFERENCES [dbo].[Donors] ([DonorId])
GO
ALTER TABLE [dbo].[DonorsRegisteredWithPartners] CHECK CONSTRAINT [FK_DonorsRegisteredWithPartners_Donors]
GO
ALTER TABLE [dbo].[DonorsRegisteredWithPartners]  WITH CHECK ADD  CONSTRAINT [FK_DonorsRegisteredWithPartners_PartnerAndTheirContacts] FOREIGN KEY([PartnerContactsId])
REFERENCES [dbo].[PartnerAndTheirContacts] ([PartnerContactsId])
GO
ALTER TABLE [dbo].[DonorsRegisteredWithPartners] CHECK CONSTRAINT [FK_DonorsRegisteredWithPartners_PartnerAndTheirContacts]
GO
ALTER TABLE [dbo].[FamilyOfIntendedParents]  WITH CHECK ADD  CONSTRAINT [FK_IntendedParentsFamily_Parents] FOREIGN KEY([PrimaryParentId])
REFERENCES [dbo].[Parents] ([ParentId])
GO
ALTER TABLE [dbo].[FamilyOfIntendedParents] CHECK CONSTRAINT [FK_IntendedParentsFamily_Parents]
GO
ALTER TABLE [dbo].[FamilyOfIntendedParents]  WITH CHECK ADD  CONSTRAINT [FK_IntendedParentsFamily_Parents1] FOREIGN KEY([IntendedParentId])
REFERENCES [dbo].[Parents] ([ParentId])
GO
ALTER TABLE [dbo].[FamilyOfIntendedParents] CHECK CONSTRAINT [FK_IntendedParentsFamily_Parents1]
GO
ALTER TABLE [dbo].[MatchDetails]  WITH CHECK ADD  CONSTRAINT [FK_MatchDetails_Donors] FOREIGN KEY([DonorId])
REFERENCES [dbo].[Donors] ([DonorId])
GO
ALTER TABLE [dbo].[MatchDetails] CHECK CONSTRAINT [FK_MatchDetails_Donors]
GO
ALTER TABLE [dbo].[MatchDetails]  WITH CHECK ADD  CONSTRAINT [FK_MatchDetails_Parents] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Parents] ([ParentId])
GO
ALTER TABLE [dbo].[MatchDetails] CHECK CONSTRAINT [FK_MatchDetails_Parents]
GO
ALTER TABLE [dbo].[MatchRequestedByDonor]  WITH CHECK ADD  CONSTRAINT [FK_MatchRequestedByDonor_Donors] FOREIGN KEY([DonorId])
REFERENCES [dbo].[Donors] ([DonorId])
GO
ALTER TABLE [dbo].[MatchRequestedByDonor] CHECK CONSTRAINT [FK_MatchRequestedByDonor_Donors]
GO
ALTER TABLE [dbo].[MatchRequestedByParent]  WITH CHECK ADD  CONSTRAINT [FK_MatchRequestedByParent_Parents] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Parents] ([ParentId])
GO
ALTER TABLE [dbo].[MatchRequestedByParent] CHECK CONSTRAINT [FK_MatchRequestedByParent_Parents]
GO
ALTER TABLE [dbo].[ParentAilmentDetails]  WITH CHECK ADD  CONSTRAINT [FK_ParentAilmentDetails_Parents] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Parents] ([ParentId])
GO
ALTER TABLE [dbo].[ParentAilmentDetails] CHECK CONSTRAINT [FK_ParentAilmentDetails_Parents]
GO
ALTER TABLE [dbo].[ParentFamilyTreeDiseaseDetails]  WITH CHECK ADD  CONSTRAINT [FK_ParentFamilyTreeDiseaseDetails_FamilyDiseaseQuestions] FOREIGN KEY([FamilyDiseaseQuestionId])
REFERENCES [dbo].[FamilyDiseaseQuestions] ([FamilyDiseaseQuestionId])
GO
ALTER TABLE [dbo].[ParentFamilyTreeDiseaseDetails] CHECK CONSTRAINT [FK_ParentFamilyTreeDiseaseDetails_FamilyDiseaseQuestions]
GO
ALTER TABLE [dbo].[ParentFamilyTreeDiseaseDetails]  WITH CHECK ADD  CONSTRAINT [FK_ParentFamilyTreeDiseaseDetails_FamilyTreeDiseaseDetails] FOREIGN KEY([FamilyTreeDiseaseId])
REFERENCES [dbo].[FamilyTreeDiseaseDetails] ([FamilyTreeDiseaseId])
GO
ALTER TABLE [dbo].[ParentFamilyTreeDiseaseDetails] CHECK CONSTRAINT [FK_ParentFamilyTreeDiseaseDetails_FamilyTreeDiseaseDetails]
GO
ALTER TABLE [dbo].[ParentFamilyTreeDiseaseDetails]  WITH CHECK ADD  CONSTRAINT [FK_ParentFamilyTreeDiseaseDetails_Parents] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Parents] ([ParentId])
GO
ALTER TABLE [dbo].[ParentFamilyTreeDiseaseDetails] CHECK CONSTRAINT [FK_ParentFamilyTreeDiseaseDetails_Parents]
GO
ALTER TABLE [dbo].[ParentsHealthQuestionsAnswered]  WITH CHECK ADD  CONSTRAINT [FK_ParentsHealthQuestionsAnswered_HealthQuestions] FOREIGN KEY([HealthQuestionId])
REFERENCES [dbo].[HealthQuestions] ([HealthQuestionId])
GO
ALTER TABLE [dbo].[ParentsHealthQuestionsAnswered] CHECK CONSTRAINT [FK_ParentsHealthQuestionsAnswered_HealthQuestions]
GO
ALTER TABLE [dbo].[ParentsHealthQuestionsAnswered]  WITH CHECK ADD  CONSTRAINT [FK_ParentsHealthQuestionsAnswered_Parents] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Parents] ([ParentId])
GO
ALTER TABLE [dbo].[ParentsHealthQuestionsAnswered] CHECK CONSTRAINT [FK_ParentsHealthQuestionsAnswered_Parents]
GO
ALTER TABLE [dbo].[ParentsRegisteredWithPartners]  WITH CHECK ADD  CONSTRAINT [FK_ParentsRegisteredWithPartners_Parents] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Parents] ([ParentId])
GO
ALTER TABLE [dbo].[ParentsRegisteredWithPartners] CHECK CONSTRAINT [FK_ParentsRegisteredWithPartners_Parents]
GO
ALTER TABLE [dbo].[ParentsRegisteredWithPartners]  WITH CHECK ADD  CONSTRAINT [FK_ParentsRegisteredWithPartners_PartnerAndTheirContacts] FOREIGN KEY([PartnerContactsId])
REFERENCES [dbo].[PartnerAndTheirContacts] ([PartnerContactsId])
GO
ALTER TABLE [dbo].[ParentsRegisteredWithPartners] CHECK CONSTRAINT [FK_ParentsRegisteredWithPartners_PartnerAndTheirContacts]
GO
ALTER TABLE [dbo].[ParentsRegisteredWithPartners]  WITH CHECK ADD  CONSTRAINT [FK_ParentsRegisteredWithPartners_Partners1] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Partners] ([PartnerId])
GO
ALTER TABLE [dbo].[ParentsRegisteredWithPartners] CHECK CONSTRAINT [FK_ParentsRegisteredWithPartners_Partners1]
GO
ALTER TABLE [dbo].[PartnerAndTheirContacts]  WITH CHECK ADD  CONSTRAINT [FK_PartnerContacts_Partners] FOREIGN KEY([PartnerId])
REFERENCES [dbo].[Partners] ([PartnerId])
GO
ALTER TABLE [dbo].[PartnerAndTheirContacts] CHECK CONSTRAINT [FK_PartnerContacts_Partners]
GO
ALTER TABLE [dbo].[PartnersAttachedToDonorCycle]  WITH CHECK ADD  CONSTRAINT [FK_PartnersAttachedToDonorCycle_DonorCycles] FOREIGN KEY([DonorCycleId])
REFERENCES [dbo].[DonorCycles] ([DonorCycleId])
GO
ALTER TABLE [dbo].[PartnersAttachedToDonorCycle] CHECK CONSTRAINT [FK_PartnersAttachedToDonorCycle_DonorCycles]
GO
ALTER TABLE [dbo].[PartnersAttachedToDonorCycle]  WITH CHECK ADD  CONSTRAINT [FK_PartnersAttachedToDonorCycle_PartnerAndTheirContacts] FOREIGN KEY([PartnerContactsId])
REFERENCES [dbo].[PartnerAndTheirContacts] ([PartnerContactsId])
GO
ALTER TABLE [dbo].[PartnersAttachedToDonorCycle] CHECK CONSTRAINT [FK_PartnersAttachedToDonorCycle_PartnerAndTheirContacts]
GO
ALTER TABLE [dbo].[PaymentSummuryDonors]  WITH CHECK ADD  CONSTRAINT [FK_PaymentSummuryDonors_Donors] FOREIGN KEY([DonorId])
REFERENCES [dbo].[Donors] ([DonorId])
GO
ALTER TABLE [dbo].[PaymentSummuryDonors] CHECK CONSTRAINT [FK_PaymentSummuryDonors_Donors]
GO
ALTER TABLE [dbo].[PaymentSummuryParents]  WITH CHECK ADD  CONSTRAINT [FK_PaymentSummuryParents_Parents] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Parents] ([ParentId])
GO
ALTER TABLE [dbo].[PaymentSummuryParents] CHECK CONSTRAINT [FK_PaymentSummuryParents_Parents]
GO
USE [master]
GO
ALTER DATABASE [sample] SET  READ_WRITE 
GO
