drop table Auteur;
drop table Utilisateur;
drop table Categorie;
drop table Livre;

CREATE TABLE Auteur
(
	id integer,
	nom varchar(100),
	prenom varchar(100),
	sexe varchar(10),
	photo varchar(255),
	email varchar(255),
	date_nais date,
	apropos text,
	nationalite varchar(50),
	typeAuteur varchar(50),
	constraint pk_auteur1 primary key (id),
)

CREATE TABLE Utilisateur
(
	id integer,
	nom varchar(100),
	prenom varchar(100),
	sexe varchar(5),
	photo varchar(255),
	email varchar(255),
	date_nais date,
	nomUser varchar(100),
	motdepasse varchar (255),
	typeUser varchar(10),
	statusCompte varchar(10),
	derniereconnexion dateTime,
	constraint pk_user1 primary key (id),
)

CREATE TABLE Categorie(
	id integer,
	nom varchar(100),
	[description] text,
	image varchar(100),
	constraint pk_categorie1 primary key (id)	
)


CREATE TABLE Livre(
	id integer,
	Titre varchar(100),
	[description] text,
	[resume] varchar(1000),
	quantite integer,
	isbn varchar(20),
	auteurid integer,
	categorieid integer,
	constraint pk_livre1 primary key (id),
	constraint fk_user_livre foreign key (auteurid) references Auteur (id),
	constraint fk_categorie_livre foreign key (categorieid) references Categorie (id),
)

CREATE TABLE Pret(
	id integer,
	datedebut date,
	datefin date,
	userid integer,
	livreid integer,
	constraint pk_pret1 primary key (id),
	constraint fk_livre_pret foreign key (livreid) references Livre (id),
	constraint fk_user_pret foreign key (userid) references Utilisateur (id)
)




