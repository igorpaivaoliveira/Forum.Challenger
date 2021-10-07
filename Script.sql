USE DB_FORUM_CHALLENGER

GO

CREATE TABLE dbo.TB_PERSONS
(
	CD_PERSON int primary key identity(1,1),
	DT_PERSON datetime,
	NM_PERSON varchar(50),
	DS_EMAIL varchar(50),
	DS_PASSWORD varchar(12),
	FL_ACTIVE bit
)


CREATE TABLE dbo.TB_TOPICS
(
	CD_TOPIC int primary key identity(1,1),
	DT_TOPIC datetime,
	DT_TOPIC_LAST_EDITION datetime null,
	DS_TITLE varchar(50),
	DS_TEXT text,
	CD_PERSON int,
	FL_ACTIVE bit,
	CONSTRAINT FK_TB_PERSONS_CD_PERSON FOREIGN KEY (CD_PERSON) REFERENCES TB_PERSONS (CD_PERSON) ON DELETE CASCADE
)