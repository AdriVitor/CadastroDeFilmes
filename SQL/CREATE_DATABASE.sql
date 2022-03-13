CREATE DATABASE [FilmesOnline];

CREATE TABLE [Filmes]
(
    [Id] INT NOT NULL IDENTITY(1, 1),
    [Nome] VARCHAR(40) NOT NULL,
    [Genero] VARCHAR(30) NOT NULL,
    [Descricao] TEXT,

    CONSTRAINT [Pk_Filmes] PRIMARY KEY ([Id])
);