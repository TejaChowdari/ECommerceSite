Select * from tblProducts
create table Roles (
	RoleId int identity (1,1) primary key,
	RoleDesc varchar(50),
	Createdon datetime
)
select * from Roles
insert into Roles (RoleDesc,Createdon) values ('Admin', GETDATE()),('Seller',GETDATE()),('User',GETDATE())

create table UsersList(
	Uid int identity primary key,
	Name Varchar(50),
	UserId Varchar(50),
	Password Varchar(50),
	isActive bit,
    Role int FOREIGN KEY REFERENCES Roles(RoleID),
	createdon datetime default getdate()
)

insert into UsersList (Name,UserId,Password,isActive,Role) values 
('Admin','Admin','Tej',1,1),
('Seller','Seller','Tej',1,2),
('User','User','Tej',1,3)

select * from UsersList

Drop table UsersList