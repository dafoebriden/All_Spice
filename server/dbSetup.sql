CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

CREATE TABLE IF NOT EXISTS recipes(
  id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title VARCHAR(50) NOT NULL,
  instructions VARCHAR(500) NOT NULL,
  img VARCHAR(500) NOT NULL,
  category VARCHAR(25) NOT NULL,
  creatorId VARCHAR(255),
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS ingredients(
  id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(50) NOT NULL,
  quantity VARCHAR(50) NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id)
);

CREATE TABLE IF NOT EXISTS favorites(
  id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  recipeId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  UNIQUE(recipeId, creatorId)
);


SELECT * FROM favorites

DROP TABLE favorites

INSERT INTO 
recipes(title, instructions, image, category, creatorId) 
VALUES(Title, Instructions, Image, Category, CreatorId) 

INSERT INTO
ingredients(name, quantity, creatorId, recipeId)
VALUES(@Name, @Quantity, @CreatorId, @RecipeId)


DROP TABLE recipes

DROP TABLE ingredients


        INSERT INTO recipes(title, instructions, image, category, creatorId)
        VALUES('burrito', 'fill tortilla with ingredients', 'httpslkdfj', 'mexican', '65e8d4d1de0c8eeb42af624d' );


        SELECT 
        recipe.*,
        account.*
        FROM recipes recipe
        JOIN accounts account On recipe.creatorId = account.id
        WHERE recipe.id = LAST_INSERT_ID();


        SELECT
        recipe.*,
        account*
        FROM recipes recipe
        JOIN accounts account ON recipe.creatorId = account.id;

SELECT 
        ingredient.*,
        account.*
        FROM ingredients ingredient
        JOIN accounts account ON ingredient.creatorId = account.id
        WHERE ingredient.recipeId = 41;
        
