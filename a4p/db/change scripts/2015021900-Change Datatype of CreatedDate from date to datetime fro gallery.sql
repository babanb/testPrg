DECLARE  @dtable table
(
Id int,
CreatedDate datetime
)

Insert into @dtable (Id, CreatedDate)
select Id,Convert(datetime,CreatedDate)
FROM AlbumGallery
update dbo.AlbumGallery set CreatedDate=null
alter table AlbumGallery alter column CreatedDate datetime
update AlbumGallery set CreatedDate=d.CreatedDate from @dtable d inner join AlbumGallery a on d.Id=a.Id


DECLARE  @gtable table
(
Id int,
CreatedDate datetime
)

Insert into @gtable (Id, CreatedDate)
select Id,Convert(datetime,CreatedDate)
FROM Gallery
update Gallery set CreatedDate=null
alter table Gallery alter column CreatedDate datetime
update Gallery set CreatedDate=d.CreatedDate from @gtable d inner join Gallery a on d.Id=a.Id