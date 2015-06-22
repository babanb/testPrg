
DECLARE  @gtable table
(
Id int,
CreatedDate datetime
)

Insert into @gtable (Id, CreatedDate)
select Id,Convert(datetime,CreatedDate)
FROM Gallery
alter table Gallery alter column CreatedDate datetime
update Gallery set CreatedDate=d.CreatedDate from @gtable d inner join Gallery a on d.Id=a.Id