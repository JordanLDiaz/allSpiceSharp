-- Active: 1680282356336@@54.187.169.182@3306@classroom_demos

CREATE TABLE
    IF NOT EXISTS JDaccounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS JDrecipes(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        title VARCHAR(50) NOT NULL,
        instructions VARCHAR(255) NOT NULL,
        img VARCHAR(255) NOT NULL DEFAULT 'https://images.unsplash.com/photo-1633981744930-15bb79ca2c41?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80',
        category VARCHAR(50) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY (creatorId) REFERENCES JDaccounts (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE JDrecipes;

ALTER TABLE JDrecipes
ADD
    img VARCHAR(255) NOT NULL DEFAULT 'https://images.unsplash.com/photo-1633981744930-15bb79ca2c41?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80';

SELECT
    JDrecipes.*,
    JDaccounts.*
FROM JDrecipes
    JOIN JDaccounts ON JDaccounts.id = JDrecipes.creatorId;

DELETE from JDrecipes WHERE id = 2;

CREATE TABLE
    IF NOT EXISTS JDingredients(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(50) NOT NULL,
        quantity VARCHAR(50) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES JDrecipes (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE JDingredients;

ALTER TABLE JDingredients ADD quantity VARCHAR(50) NOT NULL;

CREATE TABLE
    IF NOT EXISTS JDfavorites(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        accountId VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (accountId) REFERENCES JDaccounts (id) ON DELETE CASCADE,
        FOREIGN KEY (recipeId) REFERENCES JDrecipes (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE JDfavorites;

INSERT INTO
    favorites (accountId, recipeId)
VALUES ('639c98024e8bdfbd33b7666a', 3);

SELECT
    JDaccounts.*,
    JDfavorites.id
FROM JDfavorites
    JOIN JDaccounts ON JDfavorites.`accountId` = JDaccounts.id;

INSERT INTO
    JDrecipes (
        title,
        instructions,
        img,
        category,
        creatorId
    )
VALUES (
        'Pizza',
        'Make the dough, make the sauce, cut the toppings, make the za',
        'https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8cGl6emF8ZW58MHx8MHx8&auto=format&fit=crop&w=500&q=60',
        'Entree',
        '639c98024e8bdfbd33b7666a'
    );

SELECT LAST_INSERT_ID();

UPDATE JDrecipes
Set
    title = 'Pizza',
    instructions = 'Make the dough, make the sauce, cut the toppings, make the za, bake it in the oven',
    img = 'https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8cGl6emF8ZW58MHx8MHx8&auto=format&fit=crop&w=500&q=60',
    category = 'Entree'
WHERE id = 2;