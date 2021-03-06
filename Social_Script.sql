/*
** Copyright Microsoft, Inc. 1994 - 2000
** All Rights Reserved.
*/

SET NOCOUNT ON
GO

USE master
GO
if exists (select * from sysdatabases where name='Social')
		drop database Social
go

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE Social
  ON PRIMARY (NAME = N''Social'', FILENAME = N''' + @device_directory + N'social.mdf'')
  LOG ON (NAME = N''Social_log'',  FILENAME = N''' + @device_directory + N'social.ldf'')')
go

set quoted_identifier on
GO

/* Set DATEFORMAT so that the date strings are interpreted correctly regardless of
   the default DATEFORMAT on the server.
*/
SET DATEFORMAT mdy
GO
use "Social"
go

DROP TABLE IF EXISTS [Follow]
GO

DROP TABLE IF EXISTS [Likes]
GO

DROP TABLE IF EXISTS [Comment]
GO

DROP TABLE IF EXISTS [Post]
GO

DROP TABLE IF EXISTS [Profile]
GO

CREATE TABLE [Profile] (
	user_id bigint IDENTITY NOT NULL,
	username nvarchar(50) UNIQUE NOT NULL,
	biografy nvarchar(255),
	email nvarchar(255) UNIQUE NOT NULL,
	first_name nvarchar(255) NOT NULL,
	last_name nvarchar(255) NOT NULL,
  CONSTRAINT [PK_PROFILE] PRIMARY KEY CLUSTERED
  (
  [user_id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Post] (
	post_id bigint IDENTITY NOT NULL,
	user_id bigint NOT NULL,
	content nvarchar(2048) NOT NULL,
	post_date datetime NOT NULL,
  CONSTRAINT [PK_POST] PRIMARY KEY CLUSTERED
  (
  [post_id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Comment] (
	comment_id bigint IDENTITY NOT NULL,
	content nvarchar(2048) NOT NULL,
	parent_comment bigint,
	user_id bigint NOT NULL,
	post_id bigint NOT NULL,
	comment_date datetime NOT NULL,
  CONSTRAINT [PK_COMMENT] PRIMARY KEY CLUSTERED
  (
  [comment_id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Follow] (
	user_id bigint NOT NULL,
	follower_id bigint NOT NULL,
	follow_date datetime NOT NULL,
  CONSTRAINT [PK_FOLLOW] PRIMARY KEY CLUSTERED
  (
  [user_id] , [follower_id]
  ) WITH (IGNORE_DUP_KEY = OFF),
	CHECK (follower_id != user_id)
)
GO
CREATE TABLE [Likes] (
	user_id bigint NOT NULL,
	post_id bigint NOT NULL,
	like_date datetime NOT NULL,
  CONSTRAINT [PK_LIKES] PRIMARY KEY CLUSTERED
  (
    [user_id] , [post_id]
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

ALTER TABLE [Post] WITH CHECK ADD CONSTRAINT [Post_fk0] FOREIGN KEY ([user_id]) REFERENCES [Profile]([user_id])
GO
ALTER TABLE [Post] CHECK CONSTRAINT [Post_fk0]
GO

ALTER TABLE [Comment] WITH CHECK ADD CONSTRAINT [Comment_fk0] FOREIGN KEY ([parent_comment]) REFERENCES [Comment]([comment_id])
GO
ALTER TABLE [Comment] CHECK CONSTRAINT [Comment_fk0]
GO
ALTER TABLE [Comment] WITH CHECK ADD CONSTRAINT [Comment_fk1] FOREIGN KEY ([user_id]) REFERENCES [Profile]([user_id])
GO
ALTER TABLE [Comment] CHECK CONSTRAINT [Comment_fk1]
GO
ALTER TABLE [Comment] WITH CHECK ADD CONSTRAINT [Comment_fk2] FOREIGN KEY ([post_id]) REFERENCES [Post]([post_id])
GO
ALTER TABLE [Comment] CHECK CONSTRAINT [Comment_fk2]
GO

ALTER TABLE [Follow] WITH CHECK ADD CONSTRAINT [Follow_fk0] FOREIGN KEY ([user_id]) REFERENCES [Profile]([user_id])
GO
ALTER TABLE [Follow] CHECK CONSTRAINT [Follow_fk0]
GO
ALTER TABLE [Follow] WITH CHECK ADD CONSTRAINT [Follow_fk1] FOREIGN KEY ([follower_id]) REFERENCES [Profile]([user_id])
GO
ALTER TABLE [Follow] CHECK CONSTRAINT [Follow_fk1]
GO

ALTER TABLE [Likes] WITH CHECK ADD CONSTRAINT [Likes_fk0] FOREIGN KEY ([user_id]) REFERENCES [Profile]([user_id])
GO
ALTER TABLE [Likes] CHECK CONSTRAINT [Likes_fk0]
GO
ALTER TABLE [Likes] WITH CHECK ADD CONSTRAINT [Likes_fk1] FOREIGN KEY ([post_id]) REFERENCES [Post]([post_id])
GO
ALTER TABLE [Likes] CHECK CONSTRAINT [Likes_fk1]
GO


insert into Profile (username, first_name, last_name, email, biografy) values ('aindge0', 'Abram', 'Indge', 'aindge0@reverbnation.com', 'Advanced web-enabled product');
insert into Profile (username, first_name, last_name, email, biografy) values ('ehavelin1', 'Edik', 'Havelin', 'ehavelin1@prweb.com', 'Organic secondary instruction set');
insert into Profile (username, first_name, last_name, email, biografy) values ('htoten2', 'Hammad', 'Toten', 'htoten2@imdb.com', 'Stand-alone foreground model');
insert into Profile (username, first_name, last_name, email, biografy) values ('edownage3', 'Edith', 'Downage', 'edownage3@google.com.au', 'Enhanced foreground orchestration');
insert into Profile (username, first_name, last_name, email, biografy) values ('mknewstubb4', 'Marsha', 'Knewstubb', 'mknewstubb4@ehow.com', 'Pre-emptive asymmetric software');
insert into Profile (username, first_name, last_name, email, biografy) values ('vbutterick5', 'Vernor', 'Butterick', 'vbutterick5@wordpress.org', 'Sharable holistic knowledge user');
insert into Profile (username, first_name, last_name, email, biografy) values ('npatron6', 'Nickie', 'Patron', 'npatron6@pagesperso-orange.fr', 'Total bifurcated attitude');
insert into Profile (username, first_name, last_name, email, biografy) values ('haleksidze7', 'Halsy', 'Aleksidze', 'haleksidze7@hao123.com', 'Configurable bottom-line Graphical User Interface');
insert into Profile (username, first_name, last_name, email, biografy) values ('jwehnerr8', 'Jerry', 'Wehnerr', 'jwehnerr8@blogger.com', 'Devolved zero tolerance contingency');
insert into Profile (username, first_name, last_name, email, biografy) values ('hdickens9', 'Helen-elizabeth', 'Dickens', 'hdickens9@forbes.com', 'Distributed well-modulated collaboration');
insert into Profile (username, first_name, last_name, email, biografy) values ('cstansfielda', 'Carleen', 'Stansfield', 'cstansfielda@columbia.edu', 'Sharable multimedia task-force');
insert into Profile (username, first_name, last_name, email, biografy) values ('gjarnellb', 'Giffer', 'Jarnell', 'gjarnellb@sakura.ne.jp', 'Re-contextualized bandwidth-monitored migration');
insert into Profile (username, first_name, last_name, email, biografy) values ('escaddenc', 'Etienne', 'Scadden', 'escaddenc@i2i.jp', 'Fully-configurable empowering data-warehouse');
insert into Profile (username, first_name, last_name, email, biografy) values ('cdemoged', 'Charley', 'Demoge', 'cdemoged@sina.com.cn', 'Front-line multi-state projection');
insert into Profile (username, first_name, last_name, email, biografy) values ('lvossgene', 'Leslie', 'Vossgen', 'lvossgene@cdbaby.com', 'Monitored multi-tasking solution');
insert into Profile (username, first_name, last_name, email, biografy) values ('aanearf', 'Aldo', 'Anear', 'aanearf@sciencedirect.com', 'Realigned fresh-thinking complexity');
insert into Profile (username, first_name, last_name, email, biografy) values ('oleesg', 'Orson', 'Lees', 'oleesg@google.fr', 'Progressive maximized budgetary management');
insert into Profile (username, first_name, last_name, email, biografy) values ('dpardieh', 'Der', 'Pardie', 'dpardieh@bravesites.com', 'Inverse full-range access');
insert into Profile (username, first_name, last_name, email, biografy) values ('ldanheli', 'Leslie', 'Danhel', 'ldanheli@bloglovin.com', 'Function-based radical projection');
insert into Profile (username, first_name, last_name, email, biografy) values ('ndurtnalj', 'Nettle', 'Durtnal', 'ndurtnalj@discuz.net', 'Switchable maximized toolset');
insert into Profile (username, first_name, last_name, email, biografy) values ('eattoek', 'Ernestus', 'Attoe', 'eattoek@hhs.gov', 'Profound national project');
insert into Profile (username, first_name, last_name, email, biografy) values ('iobrianl', 'Isacco', 'O''Brian', 'iobrianl@instagram.com', 'Organic disintermediate access');
insert into Profile (username, first_name, last_name, email, biografy) values ('sstonehewerm', 'Sashenka', 'Stonehewer', 'sstonehewerm@feedburner.com', 'Multi-channelled maximized data-warehouse');
insert into Profile (username, first_name, last_name, email, biografy) values ('vrylstonen', 'Virginia', 'Rylstone', 'vrylstonen@berkeley.edu', 'Quality-focused logistical architecture');
insert into Profile (username, first_name, last_name, email, biografy) values ('bclueso', 'Bartel', 'Clues', 'bclueso@scribd.com', 'Front-line fault-tolerant solution');
insert into Profile (username, first_name, last_name, email, biografy) values ('cmcalpinep', 'Con', 'McAlpine', 'cmcalpinep@sun.com', 'Horizontal holistic orchestration');
insert into Profile (username, first_name, last_name, email, biografy) values ('bchaineyq', 'Berget', 'Chainey', 'bchaineyq@state.gov', 'Self-enabling object-oriented leverage');
insert into Profile (username, first_name, last_name, email, biografy) values ('scasador', 'Sherlocke', 'Casado', 'scasador@umn.edu', 'Organized client-server policy');
insert into Profile (username, first_name, last_name, email, biografy) values ('gfairbourns', 'Giulietta', 'Fairbourn', 'gfairbourns@go.com', 'Switchable mission-critical groupware');
insert into Profile (username, first_name, last_name, email, biografy) values ('wjenninst', 'Winna', 'Jennins', 'wjenninst@123-reg.co.uk', 'Business-focused content-based focus group');
insert into Profile (username, first_name, last_name, email, biografy) values ('udorrityu', 'Ulric', 'Dorrity', 'udorrityu@posterous.com', 'Reactive 3rd generation monitoring');
insert into Profile (username, first_name, last_name, email, biografy) values ('lringev', 'Liv', 'Ringe', 'lringev@ameblo.jp', 'Multi-layered homogeneous local area network');
insert into Profile (username, first_name, last_name, email, biografy) values ('cbravingtonw', 'Cortie', 'Bravington', 'cbravingtonw@typepad.com', 'Integrated 3rd generation standardization');
insert into Profile (username, first_name, last_name, email, biografy) values ('scousenx', 'Sherrie', 'Cousen', 'scousenx@mozilla.com', 'Universal bifurcated database');
insert into Profile (username, first_name, last_name, email, biografy) values ('hbemrosey', 'Holt', 'Bemrose', 'hbemrosey@springer.com', 'Focused mission-critical framework');
insert into Profile (username, first_name, last_name, email, biografy) values ('mmishz', 'Meryl', 'Mish', 'mmishz@eventbrite.com', 'Front-line context-sensitive analyzer');
insert into Profile (username, first_name, last_name, email, biografy) values ('cjuan10', 'Christabella', 'Juan', 'cjuan10@ycombinator.com', 'Grass-roots high-level implementation');
insert into Profile (username, first_name, last_name, email, biografy) values ('kroads11', 'Kala', 'Roads', 'kroads11@taobao.com', 'Exclusive national protocol');
insert into Profile (username, first_name, last_name, email, biografy) values ('msamarth12', 'Mada', 'Samarth', 'msamarth12@home.pl', 'Versatile interactive solution');
insert into Profile (username, first_name, last_name, email, biografy) values ('jhigginbottam13', 'Jessy', 'Higginbottam', 'jhigginbottam13@who.int', 'Public-key needs-based throughput');
insert into Profile (username, first_name, last_name, email, biografy) values ('sbutlin14', 'Staffard', 'Butlin', 'sbutlin14@admin.ch', 'Distributed logistical synergy');
insert into Profile (username, first_name, last_name, email, biografy) values ('ntamblyn15', 'Nadeen', 'Tamblyn', 'ntamblyn15@umn.edu', 'Innovative foreground encoding');
insert into Profile (username, first_name, last_name, email, biografy) values ('diacabucci16', 'Dario', 'Iacabucci', 'diacabucci16@businessweek.com', 'Multi-lateral secondary open system');
insert into Profile (username, first_name, last_name, email, biografy) values ('bogready17', 'Beverlee', 'O''Gready', 'bogready17@google.it', 'Customer-focused asynchronous groupware');
insert into Profile (username, first_name, last_name, email, biografy) values ('bdashwood18', 'Brok', 'Dashwood', 'bdashwood18@usda.gov', 'Future-proofed national orchestration');
insert into Profile (username, first_name, last_name, email, biografy) values ('aadrianello19', 'Abbey', 'Adrianello', 'aadrianello19@sourceforge.net', 'User-centric bi-directional monitoring');
insert into Profile (username, first_name, last_name, email, biografy) values ('hlife1a', 'Hugo', 'Life', 'hlife1a@jugem.jp', 'Organic cohesive customer loyalty');
insert into Profile (username, first_name, last_name, email, biografy) values ('eharston1b', 'Elise', 'Harston', 'eharston1b@ftc.gov', 'Reactive didactic groupware');
insert into Profile (username, first_name, last_name, email, biografy) values ('aklug1c', 'Alfonso', 'Klug', 'aklug1c@imgur.com', 'Advanced user-facing alliance');
insert into Profile (username, first_name, last_name, email, biografy) values ('tdumpleton1d', 'Tiphanie', 'Dumpleton', 'tdumpleton1d@seattletimes.com', 'Front-line heuristic utilisation');
insert into Profile (username, first_name, last_name, email, biografy) values ('ksissel1e', 'Konrad', 'Sissel', 'ksissel1e@princeton.edu', 'Synchronised upward-trending implementation');
insert into Profile (username, first_name, last_name, email, biografy) values ('wjeays1f', 'Wash', 'Jeays', 'wjeays1f@google.de', 'Grass-roots upward-trending solution');
insert into Profile (username, first_name, last_name, email, biografy) values ('sardling1g', 'Saba', 'Ardling', 'sardling1g@moonfruit.com', 'Reduced multimedia function');
insert into Profile (username, first_name, last_name, email, biografy) values ('rcoffelt1h', 'Raddie', 'Coffelt', 'rcoffelt1h@mozilla.com', 'Quality-focused dedicated matrices');
insert into Profile (username, first_name, last_name, email, biografy) values ('chaslum1i', 'Cyrillus', 'Haslum', 'chaslum1i@shinystat.com', 'Vision-oriented tangible monitoring');
insert into Profile (username, first_name, last_name, email, biografy) values ('aarmell1j', 'Ardyth', 'Armell', 'aarmell1j@si.edu', 'Seamless executive architecture');
insert into Profile (username, first_name, last_name, email, biografy) values ('awooding1k', 'Adriena', 'Wooding', 'awooding1k@shinystat.com', 'Total eco-centric matrices');
insert into Profile (username, first_name, last_name, email, biografy) values ('cbrobak1l', 'Colan', 'Brobak', 'cbrobak1l@github.io', 'Streamlined grid-enabled extranet');
insert into Profile (username, first_name, last_name, email, biografy) values ('esea1m', 'Ewell', 'Sea', 'esea1m@cdbaby.com', 'Enterprise-wide bandwidth-monitored definition');
insert into Profile (username, first_name, last_name, email, biografy) values ('lwebermann1n', 'Lexi', 'Webermann', 'lwebermann1n@sogou.com', 'Business-focused optimal solution');
insert into Profile (username, first_name, last_name, email, biografy) values ('emcdermid1o', 'Erik', 'McDermid', 'emcdermid1o@nbcnews.com', 'Streamlined optimal superstructure');
insert into Profile (username, first_name, last_name, email, biografy) values ('pduro1p', 'Poul', 'Duro', 'pduro1p@last.fm', 'Object-based responsive intranet');
insert into Profile (username, first_name, last_name, email, biografy) values ('dkilmaster1q', 'Dara', 'Kilmaster', 'dkilmaster1q@aboutads.info', 'User-friendly bandwidth-monitored software');
insert into Profile (username, first_name, last_name, email, biografy) values ('sconybear1r', 'Sherie', 'Conybear', 'sconybear1r@yolasite.com', 'Fundamental well-modulated support');
insert into Profile (username, first_name, last_name, email, biografy) values ('hjeanes1s', 'Harlen', 'Jeanes', 'hjeanes1s@wunderground.com', 'Configurable demand-driven algorithm');
insert into Profile (username, first_name, last_name, email, biografy) values ('cmeadows1t', 'Chariot', 'Meadows', 'cmeadows1t@foxnews.com', 'De-engineered bottom-line approach');
insert into Profile (username, first_name, last_name, email, biografy) values ('bjarrold1u', 'Barbey', 'Jarrold', 'bjarrold1u@arizona.edu', 'Configurable exuding methodology');
insert into Profile (username, first_name, last_name, email, biografy) values ('unewland1v', 'Ulrike', 'Newland', 'unewland1v@g.co', 'Cross-group context-sensitive local area network');
insert into Profile (username, first_name, last_name, email, biografy) values ('hsawyers1w', 'Hailee', 'Sawyers', 'hsawyers1w@economist.com', 'Re-engineered empowering knowledge base');
insert into Profile (username, first_name, last_name, email, biografy) values ('mbaack1x', 'Muhammad', 'Baack', 'mbaack1x@myspace.com', 'Cross-platform fault-tolerant local area network');
insert into Profile (username, first_name, last_name, email, biografy) values ('sdmitr1y', 'Sharla', 'Dmitr', 'sdmitr1y@icq.com', 'Streamlined explicit matrix');
insert into Profile (username, first_name, last_name, email, biografy) values ('bcoda1z', 'Belita', 'Coda', 'bcoda1z@reddit.com', 'Reactive contextually-based conglomeration');
insert into Profile (username, first_name, last_name, email, biografy) values ('ckissick20', 'Cullen', 'Kissick', 'ckissick20@reuters.com', 'Fundamental directional architecture');
insert into Profile (username, first_name, last_name, email, biografy) values ('kslyford21', 'Korry', 'Slyford', 'kslyford21@wp.com', 'Configurable intangible methodology');
insert into Profile (username, first_name, last_name, email, biografy) values ('zearlam22', 'Zsazsa', 'Earlam', 'zearlam22@artisteer.com', 'Devolved bifurcated implementation');
insert into Profile (username, first_name, last_name, email, biografy) values ('epensom23', 'Emma', 'Pensom', 'epensom23@harvard.edu', 'Enhanced multimedia customer loyalty');
insert into Profile (username, first_name, last_name, email, biografy) values ('sglasebrook24', 'Sibbie', 'Glasebrook', 'sglasebrook24@amazon.co.uk', 'Synchronised zero tolerance extranet');
insert into Profile (username, first_name, last_name, email, biografy) values ('rbuller25', 'Reider', 'Buller', 'rbuller25@privacy.gov.au', 'Digitized optimal model');
insert into Profile (username, first_name, last_name, email, biografy) values ('tfarrear26', 'Timmy', 'Farrear', 'tfarrear26@delicious.com', 'Fully-configurable reciprocal implementation');
insert into Profile (username, first_name, last_name, email, biografy) values ('rogriffin27', 'Ruben', 'O''Griffin', 'rogriffin27@businessinsider.com', 'Proactive 6th generation collaboration');
insert into Profile (username, first_name, last_name, email, biografy) values ('ostockall28', 'Obed', 'Stockall', 'ostockall28@bloomberg.com', 'Realigned multi-state installation');
insert into Profile (username, first_name, last_name, email, biografy) values ('mcarradice29', 'Melvin', 'Carradice', 'mcarradice29@ibm.com', 'Public-key 6th generation solution');
insert into Profile (username, first_name, last_name, email, biografy) values ('sfolomin2a', 'Sarene', 'Folomin', 'sfolomin2a@craigslist.org', 'Focused impactful parallelism');
insert into Profile (username, first_name, last_name, email, biografy) values ('ckinnier2b', 'Christean', 'Kinnier', 'ckinnier2b@walmart.com', 'Innovative bottom-line service-desk');
insert into Profile (username, first_name, last_name, email, biografy) values ('ewarrick2c', 'Elora', 'Warrick', 'ewarrick2c@usa.gov', 'Ameliorated bandwidth-monitored parallelism');
insert into Profile (username, first_name, last_name, email, biografy) values ('spatise2d', 'Stefa', 'Patise', 'spatise2d@walmart.com', 'Advanced regional throughput');
insert into Profile (username, first_name, last_name, email, biografy) values ('bmcadam2e', 'Burton', 'McAdam', 'bmcadam2e@rambler.ru', 'Enhanced responsive service-desk');
insert into Profile (username, first_name, last_name, email, biografy) values ('fsteely2f', 'Flynn', 'Steely', 'fsteely2f@spotify.com', 'Mandatory client-server process improvement');
insert into Profile (username, first_name, last_name, email, biografy) values ('tgutteridge2g', 'Tallie', 'Gutteridge', 'tgutteridge2g@jigsy.com', 'Organic high-level challenge');
insert into Profile (username, first_name, last_name, email, biografy) values ('fkenway2h', 'Fredra', 'Kenway', 'fkenway2h@list-manage.com', 'Pre-emptive bandwidth-monitored model');
insert into Profile (username, first_name, last_name, email, biografy) values ('mkunzelmann2i', 'Maridel', 'Kunzelmann', 'mkunzelmann2i@lulu.com', 'Decentralized human-resource installation');
insert into Profile (username, first_name, last_name, email, biografy) values ('csteynor2j', 'Cheslie', 'Steynor', 'csteynor2j@elpais.com', 'Persistent multimedia database');
insert into Profile (username, first_name, last_name, email, biografy) values ('hcuffley2k', 'Hyacinthia', 'Cuffley', 'hcuffley2k@mozilla.com', 'Open-architected object-oriented matrices');
insert into Profile (username, first_name, last_name, email, biografy) values ('rscurfield2l', 'Romeo', 'Scurfield', 'rscurfield2l@msn.com', 'Progressive mobile superstructure');
insert into Profile (username, first_name, last_name, email, biografy) values ('bdeeny2m', 'Beatriz', 'Deeny', 'bdeeny2m@sun.com', 'Adaptive uniform protocol');
insert into Profile (username, first_name, last_name, email, biografy) values ('gharriot2n', 'Gipsy', 'Harriot', 'gharriot2n@twitpic.com', 'Public-key system-worthy synergy');
insert into Profile (username, first_name, last_name, email, biografy) values ('croffe2o', 'Coralyn', 'Roffe', 'croffe2o@studiopress.com', 'Vision-oriented local encoding');
insert into Profile (username, first_name, last_name, email, biografy) values ('ktongs2p', 'Karleen', 'Tongs', 'ktongs2p@va.gov', 'Multi-tiered composite encoding');
insert into Profile (username, first_name, last_name, email, biografy) values ('starborn2q', 'Shandee', 'Tarborn', 'starborn2q@google.it', 'Profound attitude-oriented open architecture');
insert into Profile (username, first_name, last_name, email, biografy) values ('svanderhoeven2r', 'Sherwynd', 'Van der Hoeven', 'svanderhoeven2r@com.com', 'Advanced reciprocal attitude');


insert into Post (user_id, content, post_date) values (1,'asdasdasdasdas','4/1/1998');