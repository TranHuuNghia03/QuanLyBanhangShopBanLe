-- Tạo cơ sở dữ liệu QLBanHang
CREATE DATABASE QLBanHang;
GO
USE QLBanHang;
GO

-- Bảng khách hàng
CREATE TABLE Customers (
    customer_id INT IDENTITY(1,1) PRIMARY KEY,
    name_kh NVARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    phone VARCHAR(15),
    address NVARCHAR(255)
);

-- Bảng sản phẩm
CREATE TABLE Products (
    product_id INT IDENTITY(1,1) PRIMARY KEY,
    product_name NVARCHAR(100) NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    quantity INT NOT NULL,
    description NVARCHAR(MAX)
);

-- Bảng đơn hàng
CREATE TABLE Orders (
    order_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    order_date DATETIME DEFAULT GETDATE(),
    total_amount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
);

-- Bảng chi tiết đơn hàng
CREATE TABLE OrderDetails (
    order_detail_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);

-- Bảng thanh toán
CREATE TABLE Payments (
    payment_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    payment_date DATETIME DEFAULT GETDATE(),
    amount DECIMAL(10, 2) NOT NULL,
    payment_method NVARCHAR(50) NOT NULL,
    payment_status NVARCHAR(20) DEFAULT 'Pending',
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);

-- Bảng sản phẩm yêu thích
CREATE TABLE Wishlist (
    wishlist_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    product_id INT NOT NULL,
    added_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id),
    UNIQUE (customer_id, product_id)
);

-- Bảng tồn kho
CREATE TABLE Inventory (
    inventory_id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    quantity_in_stock INT NOT NULL,
    min_required_quantity INT NOT NULL,
    last_updated DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);

-- Bảng khuyến mãi
CREATE TABLE Promotions (
    promotion_id INT IDENTITY(1,1) PRIMARY KEY,
    promotion_name NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX),
    discount_type NVARCHAR(50) NOT NULL,
    discount_value DECIMAL(10, 2) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    status NVARCHAR(20) DEFAULT 'active'
);

-- Bảng báo cáo
CREATE TABLE Reports (
    report_id INT IDENTITY(1,1) PRIMARY KEY,
    report_date DATE NOT NULL,
    total_sales DECIMAL(10, 2) NOT NULL,
    total_orders INT NOT NULL,
    total_customers INT NOT NULL,
    best_selling_product INT,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (best_selling_product) REFERENCES Products(product_id)
);

-- Bảng thống kê sản phẩm
CREATE TABLE Product_Statistics (
    statistic_id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    statistic_date DATE NOT NULL,
    total_sold INT NOT NULL,
    total_revenue DECIMAL(10, 2) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);

-- Bảng khách hàng tiềm năng
CREATE TABLE Potential_Customers (
    customer_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name NVARCHAR(100) NOT NULL,
    last_name NVARCHAR(100) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    phone VARCHAR(15),
    source NVARCHAR(100),
    status NVARCHAR(20) DEFAULT 'not contacted',
    notes NVARCHAR(MAX),
    created_at DATETIME DEFAULT GETDATE()
);

-- Thêm dữ liệu mẫu
INSERT INTO Customers (name_kh, email, phone, address) 
VALUES (N'Nguyễn Văn A', 'nguyenvana@example.com', '0123456789', N'123 Đường ABC, Quận 1');

INSERT INTO Products (product_name, price, quantity, description) 
VALUES (N'Áo thun', 150000, 100, N'Áo thun cotton thoáng mát');

INSERT INTO Orders (customer_id, total_amount) 
VALUES (1, 300000);

INSERT INTO OrderDetails (order_id, product_id, quantity, price) 
VALUES (1, 1, 2, 150000);

INSERT INTO Payments (order_id, amount, payment_method, payment_status) 
VALUES (1, 500000, N'Credit Card', N'Paid');

INSERT INTO Inventory (product_id, quantity_in_stock, min_required_quantity) 
VALUES (1, 100, 20);

INSERT INTO Promotions (promotion_name, description, discount_type, discount_value, start_date, end_date) 
VALUES (N'Khuyến mãi Tết', N'Giảm giá 10% tất cả các sản phẩm', N'percentage', 10.00, '2024-01-01', '2024-01-31');

INSERT INTO Reports (report_date, total_sales, total_orders, total_customers, best_selling_product) 
VALUES ('2024-10-01', 5000000.00, 50, 45, 1);