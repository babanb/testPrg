use AdoPets_Rev

alter table [UserSubscription]
add [StartDate] datetime null

alter table [Subscription]
add [Duration] int not null DEFAULT 0
