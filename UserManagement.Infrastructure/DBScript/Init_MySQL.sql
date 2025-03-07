CREATE TABLE IF NOT EXISTS `Users`
(
   `Id` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50) NOT NULL,
    PRIMARY KEY (`Id`)
)

INSERT INTO Users(`Name`) VALUES ('John Doe');
INSERT INTO Users(`Name`) VALUES ('Jane Smith');
INSERT INTO Users(`Name`) VALUES ('Bob Marely');
