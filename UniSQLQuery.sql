use UniAppDb
go
--select * from UniUser
--create table UniUser
--(
--Id int identity(1,1) primary key,
--UserName varchar(50) not null,
--UserPassword varchar(100) not null,
--UserPortrait text ,
--UserNickName varchar(50),
--UserAddress text,
--UserSex int,
--PersonalSignature varchar(100),
--BackgroundPicture varchar(100),
--CreationTime datetime,
--UpdateTime datetime
--)

--create table UniCommunity
--(
--   Cid int identity(1,1) primary key,
--   PublisherID int not null,
--   CommunityContent text not null,
--   ContentFigure text not null,
--   ContentType int not null,
--   FabulousNumber int not null,
--   CollectionNumber int not null,
--   CommentaryNumber int not null,
--   CreationTime datetime not null
--)

--create table UniCommunityOperation(
--	Oid int identity(1,1) primary key,
--	CommunityId int not null,
--	UserId int not null,
--	FabulousState bit,
--	CollectionState bit,
--	FollowState bit,
--	CreationTime datetime 
--)

--create table UniCommunityComment(
--	Pid int identity(1,1) primary key,
--	CommunityId int not null,
--	UserId int not null,
--	UserName varchar(50),
--	UserImgUrl text,
--	Comment text not null,
--	CommentLikeNumber int not null,
--	CreationTime datetime not null
--)

--create table UniCommunityFollow(
--	Fid int identity(1,1) primary key,
--	FollwId int not null,
--	UserId int Not null,
--	FollwState bit not null,
--	CreationTime DateTime not null
--)

create table CommentReoly(
   CommentId int identity(1,1) primary key,
   CommunityId int not null,
   CommentParent int not null,
   CommentNickName varchar(100) not null,
   CommentPortrait text not null,
   CommentAddress text,
   CommentContent text not null,
   ReolyQuantity int not null,
   ReolyNickName varchar(100),
   BeInterestedNumber int not null,
   BeInterestedState bit not null,
   UninterestedNumber int not null,
   UninterestedState bit not null,
   CreationTime datetime not null,
)