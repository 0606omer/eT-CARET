﻿CREATE TABLE users 
(
	id INT PRIMARY KEY IDENTITY(1,1),
	username VARCHAR (MAX) NULL,
	password VARCHAR (MAX) NULL,
	date_register DATE NULL,
)

SELECT * FROM users 

CREATE TABLE employees
(
	id INT PRIMARY KEY IDENTITY (1,1),
	employee_id VARCHAR (MAX) NULL,
	full_name VARCHAR (MAX) NULL,
	gender VARCHAR (MAX) NULL,
	contac_number VARCHAR (MAX) NULL,
	position VARCHAR (MAX) NULL,
	image VARCHAR (MAX) NULL,
	salary INT NULL, 
	insert_date DATE NULL,
	update_date DATE NULL,
	delete_date DATE NULL,

)

SELECT * FROM employees

ALTER TABLE employees

ADD Status VARCHAR (MAX) NULL 

SELECT *FROM employeeS WHERE delete_date IS NULL 

SELECT *FROM employees

DELETE FROM employees







--buradakı ışlerı sen kendı sql ın de yapabılırsın ama burdan da yapılıyor. 