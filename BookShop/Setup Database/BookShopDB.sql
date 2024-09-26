DROP DATABASE IF EXISTS BookShop;
create database BookShop;
use BookShop;

# Create Book
create user if not exists 'BookShopUser'@'localhost' identified by 'BookSh0pP*s3w00rd';
create user if not exists 'BookShopUser'@'%' identified by 'BookSh0pP*s3w00rd';

grant all privileges on BookShop.* to 'BookShopUser'@'%';
grant all privileges on BookShop.* to 'BookShopUser'@'localhost';

FLUSH PRIVILEGES;

create table Book
(
    Id int auto_increment primary key,
    Title varchar(20),
    Author varchar (50),
    PublicationYear int,
    ISBN varchar(100),
    InStock int
);