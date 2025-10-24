CREATE TABLE products (
	product_id INTEGER IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	price DECIMAL(10, 2) NOT NULL,
	stock INT NOT NULL
);

INSERT INTO products (name, price, stock) VALUES
('Laptop', 999.99, 50),
('Smartphone', 499.99, 200),
('Tablet', 299.99, 150),
('Headphones', 89.99, 300),
('Smartwatch', 199.99, 100);

SELECT * FROM products;