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

--DELETE FROM ChuyenBay;

INSERT INTO ChuyenBay (MaChuyenBay, MaMayBay, SanBayDen, SanBayDi, DiemDi, DiemDen, ThoiGianKhoiHanh, ThoiGianDen, GiaChuyenBay)
VALUES
-- Chuyến bay 1 - 10
('VN1340', 'AIRBUS_A321', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Đà Nẵng', '2024-12-05 18:40:00', '2024-12-05 20:00:00', 123456),
('VN245', 'AIRBUS_A321', N'Sân bay Cam Ranh', N'Sân bay Nội Bài', N'Hà Nội', N'Nha Trang', '2024-12-06 19:00:00', '2024-12-06 21:10:00', 1149000),
('VN1280', 'BOEING_737', N'Sân bay Cam Ranh', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Nha Trang', '2024-12-06 08:30:00', '2024-12-06 10:00:00', 450000),
('VN185', 'AIRBUS_A320', N'Sân bay Tân Sơn Nhất', N'Sân bay Quốc tế Đà Nẵng', N'Đà Nẵng', N'TP.HCM', '2024-12-06 10:45:00', '2024-12-06 12:30:00', 800000),
('VN350', 'AIRBUS_A321', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Nội Bài', N'Hà Nội', N'Đà Nẵng', '2024-12-06 14:00:00', '2024-12-06 16:20:00', 300000),
('VN225', 'BOEING_737', N'Sân bay Nội Bài', N'Sân bay Cam Ranh', N'Nha Trang', N'Hà Nội', '2024-12-06 16:10:00', '2024-12-06 18:00:00', 650000),
('VN220', 'AIRBUS_A321', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Đà Nẵng', '2024-12-06 18:30:00', '2024-12-06 20:00:00', 1450000),
('VN410', 'AIRBUS_A320', N'Sân bay Tân Sơn Nhất', N'Sân bay Nội Bài', N'Hà Nội', N'TP.HCM', '2024-12-09 08:00:00', '2024-12-09 10:15:00',2290000),
('VN330', 'AIRBUS_A320', N'Sân bay Phú Bài', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Huế', '2024-12-08 11:00:00', '2024-12-08 12:30:00',655000),
('VN540', 'BOEING_737', N'Sân bay Nội Bài', N'Sân bay Quốc tế Đà Nẵng', N'Đà Nẵng', N'Hà Nội', '2024-12-07 15:00:00', '2024-12-07 17:20:00',750000),

-- Chuyến bay 11 - 20
('VN601', 'AIRBUS_A350', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Nội Bài', N'Hà Nội', N'Đà Nẵng', '2024-12-10 07:00:00', '2024-12-10 09:15:00', 1150000),
('VN602', 'BOEING_787', N'Sân bay Cam Ranh', N'Sân bay Nội Bài', N'Hà Nội', N'Nha Trang', '2024-12-11 15:20:00', '2024-12-11 17:30:00', 990000),
('VN603', 'AIRBUS_A330', N'Sân bay Tân Sơn Nhất', N'Sân bay Phú Bài', N'Huế', N'TP.HCM', '2024-12-12 09:00:00', '2024-12-12 10:30:00', 850000),
('VN604', 'BOEING_767', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Nội Bài', N'Hà Nội', N'Đà Nẵng', '2024-12-12 11:45:00', '2024-12-12 14:05:00', 720000),
('VN605', 'AIRBUS_A380', N'Sân bay Cam Ranh', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Nha Trang', '2024-12-13 10:20:00', '2024-12-13 12:00:00', 650000),
('VN606', 'BOEING_747', N'Sân bay Tân Sơn Nhất', N'Sân bay Nội Bài', N'Hà Nội', N'TP.HCM', '2024-12-14 16:00:00', '2024-12-14 18:20:00', 1300000),
('VN607', 'AIRBUS_A321', N'Sân bay Phú Bài', N'Sân bay Cam Ranh', N'Nha Trang', N'Huế', '2024-12-15 07:30:00', '2024-12-15 09:15:00', 760000),
('VN608', 'BOEING_737', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Nội Bài', N'Hà Nội', N'Đà Nẵng', '2024-12-15 13:20:00', '2024-12-15 15:40:00', 980000),
('VN609', 'AIRBUS_A320', N'Sân bay Tân Sơn Nhất', N'Sân bay Phú Bài', N'Huế', N'TP.HCM', '2024-12-16 17:00:00', '2024-12-16 18:45:00', 710000),
('VN610', 'BOEING_777', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Nội Bài', N'Hà Nội', N'Đà Nẵng', '2024-12-17 19:10:00', '2024-12-17 21:30:00', 1200000),

-- Chuyến bay 21 - 30
('VN611', 'AIRBUS_A350', N'Sân bay Tân Sơn Nhất', N'Sân bay Nội Bài', N'Hà Nội', N'TP.HCM', '2024-12-18 06:30:00', '2024-12-18 08:50:00', 1350000),
('VN612', 'BOEING_787', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Phú Bài', N'Huế', N'Đà Nẵng', '2024-12-18 10:00:00', '2024-12-18 11:20:00', 690000),
('VN613', 'AIRBUS_A330', N'Sân bay Cam Ranh', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Nha Trang', '2024-12-19 08:15:00', '2024-12-19 09:45:00', 780000),
('VN614', 'BOEING_767', N'Sân bay Nội Bài', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Hà Nội', '2024-12-20 12:30:00', '2024-12-20 14:45:00', 890000),
('VN615', 'AIRBUS_A380', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Phú Bài', N'Huế', N'Đà Nẵng', '2024-12-21 07:00:00', '2024-12-21 08:30:00', 730000),
('VN616', 'BOEING_747', N'Sân bay Nội Bài', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Hà Nội', '2024-12-22 16:00:00', '2024-12-22 18:15:00', 1450000),
('VN617', 'AIRBUS_A321', N'Sân bay Phú Bài', N'Sân bay Cam Ranh', N'Nha Trang', N'Huế', '2024-12-23 08:00:00', '2024-12-23 09:30:00', 780000),
('VN618', 'BOEING_737', N'Sân bay Cam Ranh', N'Sân bay Nội Bài', N'Hà Nội', N'Nha Trang', '2024-12-24 13:00:00', '2024-12-24 15:20:00', 970000),
('VN619', 'AIRBUS_A320', N'Sân bay Tân Sơn Nhất', N'Sân bay Phú Bài', N'Huế', N'TP.HCM', '2024-12-25 14:00:00', '2024-12-25 15:45:00', 730000),
('VN620', 'BOEING_777', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Nội Bài', N'Hà Nội', N'Đà Nẵng', '2024-12-26 19:30:00', '2024-12-26 21:50:00', 1250000),

-- Chuyến bay 31 - 40
('VN621', 'AIRBUS_A350', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Đà Nẵng', '2024-12-27 08:10:00', '2024-12-27 09:45:00', 950000),
('VN622', 'BOEING_787', N'Sân bay Phú Bài', N'Sân bay Nội Bài', N'Hà Nội', N'Huế', '2024-12-27 11:30:00', '2024-12-27 13:00:00', 820000),
('VN623', 'AIRBUS_A330', N'Sân bay Cam Ranh', N'Sân bay Quốc tế Đà Nẵng', N'Đà Nẵng', N'Nha Trang', '2024-12-28 07:45:00', '2024-12-28 09:15:00', 880000),
('VN624', 'BOEING_767', N'Sân bay Nội Bài', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Hà Nội', '2024-12-28 14:30:00', '2024-12-28 16:50:00', 1340000),
('VN625', 'AIRBUS_A380', N'Sân bay Tân Sơn Nhất', N'Sân bay Nội Bài', N'Hà Nội', N'TP.HCM', '2024-12-29 10:15:00', '2024-12-29 12:25:00', 1275000),
('VN626', 'BOEING_747', N'Sân bay Cam Ranh', N'Sân bay Phú Bài', N'Huế', N'Nha Trang', '2024-12-29 16:45:00', '2024-12-29 18:20:00', 720000),
('VN627', 'AIRBUS_A321', N'Sân bay Quốc tế Đà Nẵng', N'Sân bay Nội Bài', N'Hà Nội', N'Đà Nẵng', '2024-12-30 09:30:00', '2024-12-30 11:40:00', 940000),
('VN628', 'BOEING_737', N'Sân bay Tân Sơn Nhất', N'Sân bay Phú Bài', N'Huế', N'TP.HCM', '2024-12-30 13:00:00', '2024-12-30 14:45:00', 710000),
('VN629', 'AIRBUS_A320', N'Sân bay Phú Bài', N'Sân bay Cam Ranh', N'Nha Trang', N'Huế', '2024-12-31 07:15:00', '2024-12-31 08:55:00', 850000),
('VN630', 'BOEING_777', N'Sân bay Nội Bài', N'Sân bay Quốc tế Đà Nẵng', N'Đà Nẵng', N'Hà Nội', '2024-12-31 15:20:00', '2024-12-31 17:40:00', 1200000),

-- Chuyến bay 41 - 50
('VN631', 'AIRBUS_A350', N'Sân bay Nội Bài', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Hà Nội', '2024-12-31 18:30:00', '2024-12-31 20:40:00', 1375000),
('VN632', 'BOEING_787', N'Sân bay Tân Sơn Nhất', N'Sân bay Cam Ranh', N'Nha Trang', N'TP.HCM', '2024-12-31 21:00:00', '2024-12-31 22:15:00', 990000),
('VN633', 'AIRBUS_A330', N'Sân bay Phú Bài', N'Sân bay Nội Bài', N'Hà Nội', N'Huế', '2024-12-31 16:30:00', '2024-12-31 18:00:00', 860000),
('VN634', 'BOEING_767', N'Sân bay Nội Bài', N'Sân bay Quốc tế Đà Nẵng', N'Đà Nẵng', N'Hà Nội', '2024-12-31 08:00:00', '2024-12-31 10:15:00', 1150000),
('VN635', 'AIRBUS_A380', N'Sân bay Cam Ranh', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Nha Trang', '2024-12-31 11:00:00', '2024-12-31 12:20:00', 1075000),
('VN636', 'BOEING_747', N'Sân bay Nội Bài', N'Sân bay Cam Ranh', N'Nha Trang', N'Hà Nội', '2024-12-31 14:00:00', '2024-12-31 16:20:00', 1240000),
('VN637', 'AIRBUS_A321', N'Sân bay Phú Bài', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Huế', '2024-12-31 09:30:00', '2024-12-31 11:10:00', 910000),
('VN638', 'BOEING_737', N'Sân bay Cam Ranh', N'Sân bay Quốc tế Đà Nẵng', N'Đà Nẵng', N'Nha Trang', '2024-12-31 12:30:00', '2024-12-31 13:45:00', 820000),
('VN639', 'AIRBUS_A320', N'Sân bay Nội Bài', N'Sân bay Tân Sơn Nhất', N'TP.HCM', N'Hà Nội', '2024-12-31 19:00:00', '2024-12-31 21:20:00', 1500000),
('VN640', 'BOEING_777', N'Sân bay Tân Sơn Nhất', N'Sân bay Quốc tế Đà Nẵng', N'Đà Nẵng', N'TP.HCM', '2024-12-31 17:45:00', '2024-12-31 19:15:00', 980000);

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

--DELETE FROM MayBay;

INSERT INTO MayBay (MaMayBay, MaGhe, TenMayBay)
VALUES
-- Máy bay 1 - 10
('AIRBUS_A321', 'A321-1', N'Airbus A321 Neo'),
('BOEING_737', 'B737-1', N'Boeing 737 Max'),
('AIRBUS_A320', 'A320-1', N'Airbus A320 Classic'),
('BOEING_777', 'B777-1', N'Boeing 777X'),
('AIRBUS_A350', 'A350-1', N'Airbus A350-1000'),
('BOEING_787', 'B787-1', N'Boeing 787-9 Dreamliner'),
('AIRBUS_A330', 'A330-1', N'Airbus A330-900'),
('BOEING_767', 'B767-1', N'Boeing 767-400ER'),
('AIRBUS_A380', 'A380-1', N'Airbus A380 Plus'),
('BOEING_747', 'B747-1', N'Boeing 747-8 Intercontinental'),

-- Máy bay 11 - 20
('AIRBUS_A319', 'A319-1', N'Airbus A319-100'),
('BOEING_737MAX8', 'B737M8-1', N'Boeing 737 Max 8'),
('AIRBUS_A310', 'A310-1', N'Airbus A310-300'),
('BOEING_757', 'B757-1', N'Boeing 757-300'),
('AIRBUS_A318', 'A318-1', N'Airbus A318 Elite'),
('BOEING_707', 'B707-1', N'Boeing 707-320B'),
('AIRBUS_A340', 'A340-1', N'Airbus A340-600'),
('BOEING_720', 'B720-1', N'Boeing 720B'),
('AIRBUS_A330-200', 'A330-2', N'Airbus A330-200'),
('BOEING_737MAX9', 'B737M9-1', N'Boeing 737 Max 9'),

-- Máy bay 21 - 30
('AIRBUS_A321XLR', 'A321XLR-1', N'Airbus A321 XLR'),
('BOEING_737MAX10', 'B737M10-1', N'Boeing 737 Max 10'),
('AIRBUS_A350ULR', 'A350ULR-1', N'Airbus A350-900 ULR'),
('BOEING_777F', 'B777F-1', N'Boeing 777 Freighter'),
('AIRBUS_BELUGA', 'BELUGA-1', N'Airbus Beluga XL'),
('BOEING_787-10', 'B787-10', N'Boeing 787-10 Dreamliner'),
('AIRBUS_A330-800', 'A330-800', N'Airbus A330-800'),
('BOEING_747SP', 'B747SP-1', N'Boeing 747SP'),
('AIRBUS_A220', 'A220-1', N'Airbus A220-300'),
('BOEING_737MAX7', 'B737M7-1', N'Boeing 737 Max 7'),

-- Máy bay 31 - 40
('AIRBUS_A310F', 'A310F-1', N'Airbus A310 Freighter'),
('BOEING_727', 'B727-1', N'Boeing 727-200 Advanced'),
('AIRBUS_A321P2F', 'A321P2F-1', N'Airbus A321P2F'),
('BOEING_757F', 'B757F-1', N'Boeing 757 Freighter'),
('AIRBUS_A380-800', 'A380-800', N'Airbus A380-800'),
('BOEING_707-320', 'B707-320', N'Boeing 707-320C'),
('AIRBUS_A340-500', 'A340-500', N'Airbus A340-500'),
('BOEING_747-200', 'B747-200', N'Boeing 747-200B'),
('AIRBUS_A321LR', 'A321LR-1', N'Airbus A321 Long Range'),
('BOEING_787-8', 'B787-8', N'Boeing 787-8 Dreamliner'),

-- Máy bay 41 - 50
('BOEING_767_2', 'B767-2', N'Boeing 767-300ER'),
('AIRBUS_A330_2', 'A330-2', N'Airbus A330-800'),
('BOEING_787_2', 'B787-2', N'Boeing 787-8 Dreamliner'),
('AIRBUS_A320_2', 'A320-2', N'Airbus A320neo'),
('BOEING_737_2', 'B737-2', N'Boeing 737 MAX 8'),
('AIRBUS_A321_2', 'A321-2', N'Airbus A321neo'),
('BOEING_777_2', 'B777-2', N'Boeing 777-200ER'),
('AIRBUS_A350_2', 'A350-2', N'Airbus A350-900'),
('BOEING_747_2', 'B747-2', N'Boeing 747-400'),
('AIRBUS_A380_2', 'A380-2', N'Airbus A380-800');

--DELETE FROM TaiKhoan;

-- Thêm dữ liệu vào bảng TaiKhoan
INSERT INTO TaiKhoan (MatKhau, VaiTro, MaNV, EmailKH)
VALUES 
('123456', 'Admin', 'NV001', NULL), 
('123', 'KhachHang', NULL, 'kh1@gmail.com'), 
('mkNhanVien02', 'NhanVien', 'NV002', NULL),
('123', 'KhachHang', NULL, 'kh2@gmail.com'), 
('matkhauNV03', 'NhanVien', 'NV003', NULL);  

--DELETE FROM NhanVien;

-- Thêm dữ liệu vào bảng NhanVien
INSERT INTO NhanVien (MaNV, TenKH, GioiTinh, NgaySinh, DiaChi, SDT, TinhTrang)
VALUES
('NV001', N'Nguyễn Văn A', N'Nam', '1990-05-15', N'123 Lê Lợi, Quận 1, TP.HCM', '0901234567', N'Đang làm việc'),
('NV002', N'Phạm Thị B', N'Nữ', '1992-09-20', N'456 Nguyễn Huệ, Quận 3, TP.HCM', '0912345678', N'Đang làm việc'),
('NV003', N'Lê Văn C', N'Nam', '1988-03-10', N'789 Cách Mạng, Quận 5, TP.HCM', '0923456789', N'Đang làm việc'),
('NV004', N'Hoàng Minh D', N'Nam', '1985-11-11', N'101 Đoàn Văn Bơ, Quận 4, TP.HCM', '0934567890', N'Đang làm việc'),
('NV005', N'Nguyễn Thị E', N'Nữ', '1995-02-22', N'202 Nguyễn Văn Cừ, Quận 5, TP.HCM', '0945678901', N'Đang làm việc'),
('NV006', N'Võ Minh F', N'Nam', '1989-07-14', N'303 Phạm Ngọc Thạch, Quận 3, TP.HCM', '0956789012', N'Đang làm việc'),
('NV007', N'Đoàn Thanh G', N'Nam', '1993-10-30', N'404 Nguyễn Tri Phương, Quận 10, TP.HCM', '0967890123', N'Đang làm việc'),
('NV008', N'Lê Minh H', N'Nữ', '1990-12-01', N'505 Hoàng Văn Thụ, Quận Tân Bình, TP.HCM', '0978901234', N'Đang làm việc'),
('NV009', N'Nguyễn Thị I', N'Nữ', '1991-06-23', N'606 Lý Thái Tổ, Quận 1, TP.HCM', '0989012345', N'Đang làm việc'),
('NV010', N'Phan Văn J', N'Nam', '1987-03-18', N'707 Lê Duẩn, Quận 1, TP.HCM', '0990123456', N'Đang làm việc'),
('NV011', N'Vũ Minh K', N'Nam', '1994-04-04', N'808 Trần Hưng Đạo, Quận 5, TP.HCM', '0902345678', N'Đang làm việc'),
('NV012', N'Hoàng Thị L', N'Nữ', '1992-08-05', N'909 CMT8, Quận 3, TP.HCM', '0913456789', N'Đang làm việc'),
('NV013', N'Nguyễn Văn M', N'Nam', '1986-12-12', N'1010 Nguyễn Văn Linh, Quận 7, TP.HCM', '0924567890', N'Đang làm việc'),
('NV014', N'Lê Thị N', N'Nữ', '1995-01-25', N'1111 Lý Chính Thắng, Quận 10, TP.HCM', '0935678901', N'Đang làm việc'),
('NV015', N'Phan Thị O', N'Nữ', '1994-11-11', N'1212 Lê Hồng Phong, Quận 10, TP.HCM', '0946789012', N'Đang làm việc'),
('NV016', N'Nguyễn Thanh P', N'Nam', '1988-02-17', N'1313 Trần Phú, Quận 5, TP.HCM', '0957890123', N'Đang làm việc'),
('NV017', N'Võ Thị Q', N'Nữ', '1991-09-08', N'1414 Nguyễn Ái Quốc, Quận Tân Bình, TP.HCM', '0968901234', N'Đang làm việc'),
('NV018', N'Hoàng Minh R', N'Nam', '1990-10-25', N'1515 Nguyễn Duy Trinh, Quận 2, TP.HCM', '0979012345', N'Đang làm việc'),
('NV019', N'Lê Minh S', N'Nam', '1987-05-30', N'1616 Trương Công Định, Quận 12, TP.HCM', '0980123456', N'Đang làm việc'),
('NV020', N'Vũ Thị T', N'Nữ', '1992-03-17', N'1717 Lê Văn Sỹ, Quận 3, TP.HCM', '0991234567', N'Đang làm việc');

--DELETE FROM KhachHang;

-- Thêm dữ liệu vào bảng KhachHang
INSERT INTO KhachHang (EmailKH, TenKH, CCCD, NgaySinh, SDT, GioiTinh)
VALUES
('kh1@gmail.com', N'Trần Thị D', '123456789012', '1995-08-12', '0934567890', N'Nữ'),
('kh2@gmail.com', N'Hoàng Văn E', '987654321098', '1993-12-25', '0945678901', N'Nam'),
('kh3@gmail.com', N'Lê Minh F', '112233445566', '1990-02-15', '0956789012', N'Nam'),
('kh4@gmail.com', N'Nguyễn Thị G', '223344556677', '1992-06-22', '0967890123', N'Nữ'),
('kh5@gmail.com', N'Phạm Thị H', '334455667788', '1994-11-10', '0978901234', N'Nữ'),
('kh6@gmail.com', N'Trần Văn I', '445566778899', '1988-03-19', '0989012345', N'Nam'),
('kh7@gmail.com', N'Vũ Minh J', '556677889900', '1991-01-30', '0990123456', N'Nam'),
('kh8@gmail.com', N'Nguyễn Thị K', '667788990011', '1993-04-17', '0901234567', N'Nữ'),
('kh9@gmail.com', N'Lê Thanh L', '778899001122', '1990-09-28', '0912345678', N'Nam'),
('kh10@gmail.com', N'Võ Thị M', '889900112233', '1989-08-05', '0923456789', N'Nữ'),
('kh11@gmail.com', N'Hoàng Minh N', '990011223344', '1992-07-22', '0934567890', N'Nam'),
('kh12@gmail.com', N'Phan Thị O', '101112223344', '1994-03-05', '0945678901', N'Nữ'),
('kh13@gmail.com', N'Lê Văn P', '112233445577', '1990-05-14', '0956789012', N'Nam'),
('kh14@gmail.com', N'Trần Thị Q', '223344556688', '1988-12-03', '0967890123', N'Nữ'),
('kh15@gmail.com', N'Nguyễn Thị R', '334455667799', '1995-02-27', '0978901234', N'Nữ'),
('kh16@gmail.com', N'Võ Thanh S', '445566778811', '1991-11-18', '0989012345', N'Nam'),
('kh17@gmail.com', N'Lê Minh T', '556677889922', '1993-07-04', '0990123456', N'Nam'),
('kh18@gmail.com', N'Hoàng Thị U', '667788990033', '1992-10-15', '0901234567', N'Nữ'),
('kh19@gmail.com', N'Nguyễn Thanh V', '778899001144', '1990-01-01', '0912345678', N'Nam'),
('kh20@gmail.com', N'Phan Minh W', '889900112255', '1995-04-22', '0923456789', N'Nam');
