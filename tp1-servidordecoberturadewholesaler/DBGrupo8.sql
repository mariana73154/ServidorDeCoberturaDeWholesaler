USE MASTER
GO

CREATE DATABASE DBGrupo8

USE DBGrupo8
GO

CREATE TABLE OPERADORA (
    Operadora VARCHAR(50) NOT NULL,
    PRIMARY KEY (Operadora)
)

CREATE TABLE MUNICIPIO (
	Nome_municipio VARCHAR(50) NOT NULL,
	PRIMARY KEY (Nome_municipio),
)

CREATE TABLE FICHEIRO (
    Id_ficheiro INT NOT NULL IDENTITY(1,1),
    Nome_fich VARCHAR(50) NOT NULL,
    Operadora VARCHAR(50) NOT NULL,
    Data_sub DATETIME NOT NULL,
    Estado VARCHAR(50) NOT NULL,
    PRIMARY KEY (Id_ficheiro),
    FOREIGN KEY (Operadora) REFERENCES OPERADORA(Operadora)
)


CREATE TABLE COBERTURA (
    Id_cobertura INT NOT NULL IDENTITY(1,1),
    Id_ficheiro INT NOT NULL,
    Municipio VARCHAR(50) NOT NULL,
    Localidade VARCHAR(50) NOT NULL,
    Rua VARCHAR(250) NOT NULL,
    Num_porta INT NOT NULL,
    Cod_postal VARCHAR(50) NOT NULL,
	N_Owner VARCHAR(50),
    PRIMARY KEY (Id_cobertura, Id_ficheiro),
    FOREIGN KEY (Id_ficheiro) REFERENCES FICHEIRO(Id_ficheiro),
	FOREIGN KEY (N_Owner) REFERENCES OPERADORA(Operadora),
	FOREIGN KEY (Municipio) REFERENCES MUNICIPIO(Nome_municipio)
)

