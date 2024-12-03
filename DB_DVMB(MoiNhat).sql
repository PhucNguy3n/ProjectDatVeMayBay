CREATE DATABASE DatVeMayBay
GO
USE DatVeMayBay
GO

CREATE TABLE KhachHang (
    EmailKH char(100) PRIMARY KEY,
    TenKH NVARCHAR(50),
	CCCD VARCHAR(50),
	NgaySinh DATE,
    SDT VARCHAR(11),
	GioiTinh NVARCHAR(3)
);


CREATE TABLE MayBay (
    MaMayBay VARCHAR(50) PRIMARY KEY,
	MaGhe VARCHAR(50),
    TenMayBay NVARCHAR(50)
);

CREATE TABLE NhanVien(
	MaNV VARCHAR(50) PRIMARY KEY,
	TenKH NVARCHAR(50),
	GioiTinh NVARCHAR(3),
	NgaySinh DATE,
    DiaChi NVARCHAR(100),
    SDT VARCHAR(11),
	TinhTrang NVARCHAR(50)
);

CREATE TABLE ChuyenBay (
    MaChuyenBay VARCHAR(50) PRIMARY KEY,
    MaMayBay VARCHAR(50),
    SanBayDen NVARCHAR(100),
	SanBayDi NVARCHAR(100),
	DiemDi NVARCHAR(100),
	DiemDen NVARCHAR(100),
	ThoiGianKhoiHanh DATETIME,
	ThoiGianDen DATETIME,
	ThoiGianBayDuTinh NVARCHAR(50),
	GiaChuyenBay int,
    FOREIGN KEY (MaMayBay) REFERENCES MayBay(MaMayBay)
);

CREATE TABLE DatCho (
	MaDatCho VARCHAR(50) PRIMARY KEY,
	SoLuong INT,
    EmailKH CHAR(100),
	FOREIGN KEY (EmailKH) REFERENCES KhachHang(EmailKH),
);

CREATE TABLE Ve (
    MaVe VARCHAR(50) PRIMARY KEY,
    MaChuyenBay VARCHAR(50),
    MaNV VARCHAR(50),
	MaDatCho VARCHAR(50),
	ThoiGianDatVe DATETIME,
	ThanhTien DECIMAL,
	DaThanhToan BIT,
	TrangThai NVARCHAR(50),
	Hang NVARCHAR(50),
    FOREIGN KEY (MaDatCho) REFERENCES DatCho(MaDatCho),
    FOREIGN KEY (MaChuyenBay) REFERENCES ChuyenBay(MaChuyenBay),
	FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

CREATE TABLE ChiTietVe (
    MaVe VARCHAR(50),
	MaCTVe Char(50),
    DanhXung NVARCHAR(50),                 
    Ho NVARCHAR(100),             
    Ten NVARCHAR(100),        
    NgaySinh DATE,             
    LoaiHanhKhach NVARCHAR(50),            
    Email NVARCHAR(100),        
    LoaiDienThoai NVARCHAR(50),          
    SoDienThoai NVARCHAR(20),
	FOREIGN KEY (MaVe) REFERENCES Ve(MaVe),
	PRIMARY KEY (MaVe,MaCTVe)
);

Create table TaiKhoan
(
	IDTK int IDENTITY(1,1),
	MatKhau varchar(50) not null,
	VaiTro varchar(50) not null,
	MaNV VARCHAR(50),
	EmailKH char(100),
	Constraint PK_TaiKhoan primary key(IDTK),
	Constraint FK_TaiKhoan_KhachHang foreign key(EmailKH) references KhachHang(EmailKH),
	Constraint FK_TaiKhoan_NhanVien foreign key(MaNV) references NhanVien(MaNV),
)


INSERT INTO ChuyenBay (MaChuyenBay, MaMayBay, SanBayDen, SanBayDi, DiemDi, DiemDen, ThoiGianKhoiHanh, ThoiGianDen, GiaChuyenBay)
VALUES
-- Chuyến bay 1
('VN1340', 'AIRBUS_A321', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Đà Nẵng', '2024-12-05 18:40:00', '2024-12-05 20:00:00', 123456),

-- Chuyến bay 2
('VN245', 'AIRBUS_A321', N'Sân bay Cam Ranh', N'Sân bay Nội Bài', N'Hà Nội', N'Nha Trang', '2024-12-06 19:00:00', '2024-12-06 21:10:00', 1149000),

-- Chuyến bay 3
('VN1280', 'BOEING_737', N'Sân bay Cam Ranh', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Nha Trang', '2024-12-06 08:30:00', '2024-12-06 10:00:00', 450000),

-- Chuyến bay 4
('VN185', 'AIRBUS_A320', N'Sân bay Tân Sơn Nhất', N'Sân bay Quốc tế Đà Nẵng', N'Đà Nẵng', N'TP.HCM', '2024-12-06 10:45:00', '2024-12-06 12:30:00', 800000),

-- Chuyến bay 5
('VN350', 'AIRBUS_A321', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Nội Bài', N'Hà Nội', N'Đà Nẵng', '2024-12-06 14:00:00', '2024-12-06 16:20:00', 300000),

-- Chuyến bay 6
('VN225', 'BOEING_737', N'Sân bay Nội Bài', N'Sân bay Cam Ranh', N'Nha Trang', N'Hà Nội', '2024-12-06 16:10:00', '2024-12-06 18:00:00', 650000),

-- Chuyến bay 7
('VN220', 'AIRBUS_A321', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Đà Nẵng', '2024-12-06 18:30:00', '2024-12-06 20:00:00', 1450000),

-- Chuyến bay 8
('VN410', 'AIRBUS_A320', N'Sân bay Tân Sơn Nhất', N'Sân bay Nội Bài', N'Hà Nội', N'TP.HCM', '2024-12-09 08:00:00', '2024-12-09 10:15:00',2290000),

-- Chuyến bay 9
('VN330', 'AIRBUS_A320', N'Sân bay Phú Bài', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Huế', '2024-12-08 11:00:00', '2024-12-08 12:30:00',655000),

-- Chuyến bay 10
('VN540', 'BOEING_737', N'Sân bay Nội Bài', N'Sân bay Quốc tế Đà Nẵng', N'Đà Nẵng', N'Hà Nội', '2024-12-07 15:00:00', '2024-12-07 17:20:00',750000);


go
CREATE TRIGGER trg_CalculateThoiGianBay
ON ChuyenBay
AFTER INSERT
AS
BEGIN
    -- Cập nhật ThoiGianBayDuTinh (chuỗi) dựa trên giờ khởi hành và giờ đến
    UPDATE cb
    SET cb.ThoiGianBayDuTinh = CAST(DATEDIFF(MINUTE, i.ThoiGianKhoiHanh, i.ThoiGianDen) AS NVARCHAR(50)) + N' phút'
    FROM ChuyenBay cb
    INNER JOIN Inserted i ON cb.MaChuyenBay = i.MaChuyenBay
END



INSERT INTO MayBay (MaMayBay, MaGhe, TenMayBay)
VALUES
-- Máy bay 1
('AIRBUS_A321', 'A321-1', N'Airbus A321 Neo'),

-- Máy bay 2
('BOEING_737', 'B737-1', N'Boeing 737 Max'),

-- Máy bay 3
('AIRBUS_A320', 'A320-1', N'Airbus A320 Classic'),

-- Máy bay 4
('BOEING_777', 'B777-1', N'Boeing 777X'),

-- Máy bay 5
('AIRBUS_A350', 'A350-1', N'Airbus A350-1000'),

-- Máy bay 6
('BOEING_787', 'B787-1', N'Boeing 787-9 Dreamliner'),

-- Máy bay 7
('AIRBUS_A330', 'A330-1', N'Airbus A330-900'),

-- Máy bay 8
('BOEING_767', 'B767-1', N'Boeing 767-400ER'),

-- Máy bay 9
('AIRBUS_A380', 'A380-1', N'Airbus A380 Plus'),

-- Máy bay 10
('BOEING_747', 'B747-1', N'Boeing 747-8 Intercontinental');


-- Thêm dữ liệu vào bảng TaiKhoan
INSERT INTO TaiKhoan (MatKhau, VaiTro, MaNV, EmailKH)
VALUES 
('123456', 'Admin', 'NV001', NULL), 
('123', 'KhachHang', NULL, 'kh1@gmail.com'), 
('mkNhanVien02', 'NhanVien', 'NV002', NULL),
('123', 'KhachHang', NULL, 'kh2@gmail.com'), 
('matkhauNV03', 'NhanVien', 'NV003', NULL);  


-- Thêm dữ liệu vào bảng NhanVien
INSERT INTO NhanVien (MaNV, TenKH, GioiTinh, NgaySinh, DiaChi, SDT, TinhTrang)
VALUES
('NV001', N'Nguyễn Văn A', N'Nam', '1990-05-15', N'123 Lê Lợi, Quận 1, TP.HCM', '0901234567', N'Đang làm việc'),
('NV002', N'Phạm Thị B', N'Nữ', '1992-09-20', N'456 Nguyễn Huệ, Quận 3, TP.HCM', '0912345678', N'Đang làm việc'),
('NV003', N'Lê Văn C', N'Nam', '1988-03-10', N'789 Cách Mạng, Quận 5, TP.HCM', '0923456789', N'Đang làm việc');


-- Thêm dữ liệu vào bảng KhachHang
INSERT INTO KhachHang (EmailKH, TenKH, CCCD, NgaySinh, SDT, GioiTinh)
VALUES
('kh1@gmail.com', N'Trần Thị D', '123456789012', '1995-08-12', '0934567890', N'Nữ'),
('kh2@gmail.com', N'Hoàng Văn E', '987654321098', '1993-12-25', '0945678901', N'Nam');
