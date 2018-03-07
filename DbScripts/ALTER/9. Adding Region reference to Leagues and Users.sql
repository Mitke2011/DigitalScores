alter table Lige
add RegionId int

alter table Lige
add foreign key(RegionId) references Region(Id)

-- Update queries for converting Text region to int
--update users set Region = '1' where Region = 'Central'
--update users set Region = '2' where Region = 'North'
--update users set Region = '3' where Region = 'South'
--update users set Region = '4' where Region = 'East'
--update users set Region = '5' where Region = 'West'

select * from users

alter table Users
alter column Region int

alter table users
add foreign key(Region) references Region(Id)