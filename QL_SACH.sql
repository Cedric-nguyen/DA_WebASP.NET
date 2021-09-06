--------------------------------------------------------------------TẠO DATABASE---------------------------------------------------------------------
USE MASTER 
GO
IF EXISTS(SELECT *FROM SYS.databases WHERE NAME='QL_SACH')
DROP DATABASE QL_SACH
GO
CREATE DATABASE QL_SACH
GO 
USE QL_SACH
GO
---------------------------------------------------------------TẠO BẢNG,KHOÁ CHÍNH,KHOÁ NGOẠI--------------------------------------------------------
------TẠO BẢNG CTKM
CREATE TABLE CTKHUYENMAI
(
	MAKM CHAR(10) NOT NULL PRIMARY KEY,
	TENKM NVARCHAR(100),
	GIAMGIA FLOAT
)
------TẠO BẢNG NHAXUATBAN
CREATE TABLE NHAXUATBAN
(
	MANXB CHAR(10) NOT NULL PRIMARY KEY,
	TENNXB NVARCHAR(50) NOT NULL
)
------TẠO BẢNG THELOAI
CREATE TABLE THELOAI
(
	MATHELOAI CHAR(10) NOT NULL PRIMARY KEY,
	TENTHELOAI NVARCHAR(50)
)

------TẠO BẢNG SACH
CREATE TABLE SACH
(
	MASACH CHAR(10) NOT NULL PRIMARY KEY, 
	TENSACH NVARCHAR(200),
	DONGIA FLOAT,
	DONVITINH NVARCHAR(10),
	MOTA NTEXT,
	HINHMINHHOA NVARCHAR(50),
	NAMXB DATE,
	NGAYCAPNHAT DATE,
	TENTACGIA NVARCHAR(100),
	MATHELOAI CHAR(10),
	MANXB CHAR(10),
	MAKM CHAR(10),
	GIAMGIA FLOAT,
	CONSTRAINT FK_SACH_KM FOREIGN KEY(MAKM) REFERENCES  CTKHUYENMAI(MAKM),
	CONSTRAINT FK_SACH_THELOAI FOREIGN KEY (MATHELOAI) REFERENCES THELOAI(MATHELOAI),
	CONSTRAINT FK_SACH_NHAXUATBAN FOREIGN KEY (MANXB) REFERENCES NHAXUATBAN (MANXB)
)

------TẠO BẢNG KHACHHANG
CREATE TABLE KHACHHANG
(
	EMAIL VARCHAR(30) not null primary key,	
	MATKHAU VARCHAR(100),	
	TENKH NVARCHAR(100) NOT NULL,
	DIACHI NVARCHAR(50),
	SDT CHAR(12),
	GIOITINH NVARCHAR(5)
)
------TẠO BẢNG QUANTRI
CREATE TABLE QUANTRI
(
	EMAIL VARCHAR(30) NOT NULL PRIMARY KEY,
	MATKHAU VARCHAR(100),
	TENHIENTHI NVARCHAR(50)
)


------TẠO BẢNG NHANVIEN
CREATE TABLE NHANVIEN
(
	MANV CHAR(10) NOT NULL PRIMARY KEY,
	TENNV NVARCHAR(30) NOT NULL,
	DIACHI NVARCHAR(50),
	SDT CHAR(12),
	GIOITINH NVARCHAR(5),
	NGAYVL DATE NOT NULL,
	LUONG MONEY
)
------TẠO BẢNG HOADON
CREATE TABLE HOADON
(
	MAHD int NOT NULL PRIMARY KEY identity,
	NGAYLAP DATE NOT NULL,
	THANHTIEN money,
	EMAIL VARCHAR(30),
	MANV CHAR(10),
	CONSTRAINT FK_HOADON_KHACHHANG FOREIGN KEY(EMAIL) REFERENCES KHACHHANG(EMAIL),
	CONSTRAINT FK_HOADON_NHANVIEN FOREIGN KEY(MANV) REFERENCES NHANVIEN(MANV)
)

------TẠO BẢNG DONDATHANG
CREATE TABLE DONDATHANG
(
	MADONHANG int NOT NULL PRIMARY KEY identity ,
	NGAYTAO DATE,
	THANHTIEN MONEY,
	DIACHINHANHANG NVARCHAR(100),		
	EMAIL VARCHAR(30),	
	TINHTRANG NVARCHAR(30),	
	CONSTRAINT FK_DONDATHANG_KHACHHANG FOREIGN KEY (EMAIL) REFERENCES KHACHHANG(EMAIL)
)

------TẠO BẢNG CHITIETDONHANG
CREATE TABLE CHITIETDONDATHANG
(
	MADONDATHANG int NOT NULL,
	MASACH CHAR(10) NOT NULL,
	SOLUONG INT,
	DONGIA FLOAT,
	GIAMGIA FLOAT,
	THANHTIEN FLOAT,
	CONSTRAINT PK_CHITITETDONDATHANG PRIMARY KEY(MADONDATHANG,MASACH),
	CONSTRAINT FK_CHITIETDONDATHANG_DONDATHANG FOREIGN KEY(MADONDATHANG) REFERENCES DONDATHANG(MADONHANG),
	CONSTRAINT FK_CHITIETDONDATHANG_SACH FOREIGN KEY(MASACH) REFERENCES SACH(MASACH)
)

------TẠO BẢNG CHITIETHD
CREATE TABLE CHITIETHD
(
	MAHD int NOT NULL,
	MASACH CHAR(10) NOT NULL,
	SOLUONG INT,
	DONGIA FLOAT,
	GIAMGIA FLOAT,
	THANHTIEN FLOAT,
	CONSTRAINT PK_CHITIETHD PRIMARY KEY(MAHD,MASACH),
	CONSTRAINT FK_CHITIETHD_SACH FOREIGN KEY(MASACH) REFERENCES SACH(MASACH),
	CONSTRAINT FK_CHITIETHD_HOADON FOREIGN KEY(MAHD) REFERENCES HOADON(MAHD)
)
-------------------------------------------------------------TẠO RÀNG BUỘC TOÀN VẸN------------------------------------------------------------------
-----------------Bảng NHAXUATBAN
----TÊN NHÀ XUẤT BẢN LÀ DUY NHẤT
ALTER TABLE NHAXUATBAN ADD CONSTRAINT UNI_TENNXB UNIQUE(TENNXB)
-----------------Bảng THELOAI
----TÊN THỂ LOẠI LÀ DUY NHẤT
ALTER TABLE THELOAI ADD CONSTRAINT UNI_TENTHELOAI UNIQUE(TENTHELOAI)
-----------------Bảng SACH
----ĐƠN GIÁ PHẢI LỚN HƠN 0
ALTER TABLE SACH ADD CONSTRAINT CHK_DONGIA CHECK(DONGIA>0)
----TÊN SÁCH LÀ DUY NHẤT
ALTER TABLE SACH ADD CONSTRAINT UNI_TENSACH UNIQUE(TENSACH)
-----------------Bảng KHACHHANG
----GIỚI TÍNH THUỘC NAM HOẶC NỮ
ALTER TABLE KHACHHANG ADD CONSTRAINT CHK_GIOITINH_KH CHECK(GIOITINH IN(N'NAM', N'NỮ'))
----EMAIL LÀ DUY NHẤT
ALTER TABLE KHACHHANG ADD CONSTRAINT UNI_EMAIL UNIQUE(EMAIL)
-----------------Bảng NHANVIEN
----GIỚI TÍNH THUỘC NAM HOẶC NỮ
ALTER TABLE NHANVIEN ADD CONSTRAINT CHK_GIOITINH_NV CHECK(GIOITINH IN(N'NAM', N'NỮ'))
----LƯƠNG PHẢI LỚN HƠN 0
ALTER TABLE NHANVIEN ADD CONSTRAINT CHK_LUONG CHECK(LUONG>0)
-----------------Bảng HOADON
----THÀNH TIỀN PHẢI LỚN HƠN 0
ALTER TABLE HOADON ADD CONSTRAINT CHK_THANHTIEN_HD CHECK(THANHTIEN>0)
-----------------Bảng DONDATHANG
----THÀNH TIỀN PHẢI LỚN HƠN 0
ALTER TABLE DONDATHANG ADD CONSTRAINT CHK_THANHTIEN_DDH CHECK(THANHTIEN>0)
------TẠO RÀNG BUỘC BẢNG CHITIETDONDATHANG
----SỐ LƯỢNG PHẢI LỚN HƠN 0

ALTER TABLE CHITIETDONDATHANG ADD CONSTRAINT CHK_SOLUONG_CTDDH CHECK(SOLUONG>0)
----ĐƠN GIÁ PHẢI LỚN HƠN 0
ALTER TABLE CHITIETDONDATHANG ADD CONSTRAINT CHK_DONGIA_CTDDH CHECK(DONGIA>0)
-----------------Bảng CHITIETTHD
----SỐ LƯỢNG PHẢI LỚN HƠN 0
ALTER TABLE chitiethd ADD CONSTRAINT CHK_SOLUONG_CTHD CHECK(SOLUONG>0)
----ĐƠN GIÁ PHẢI LỚN HƠN 0
ALTER TABLE chitiethd ADD CONSTRAINT CHK_DONGIA_CTHD CHECK(DONGIA>0)
select *from KHACHHANG

------------------------------------------------------------INSERT DỮ LIỆU---------------------------------------------------------------------------
-----------------Bảng NHAXUATBAN
SET DATEFORMAT DMY
INSERT INTO NHAXUATBAN
VALUES
('NXB01',N'NHÀ XUẤT BẢN KIM ĐỒNG'),
('NXB02',N'NHÀ XUẤT BẢN TRẺ'),
('NXB03',N'NHÀ XUẤT BẢN TỔNG HỢP TPHCM'),
('NXB04',N'NHÀ XUẤT BẢN CHÍNH TRỊ QUỐC GIA SỰ THẬT'),
('NXB05',N'NHÀ XUẤT BẢN GIÁO DỤC'),
('NXB06',N'NHÀ XUẤT BẢN HỘI NHÀ VĂN'),
('NXB07',N'NHÀ XUẤT BẢN TƯ PHÁP'),
('NXB08',N'NHÀ XUẤT BẢN THÔNG TIN VÀ TRUYỀN THÔNG'),
('NXB09',N'NHÀ XUẤT BẢN LAO ĐỘNG'),
('NXB10',N'NHÀ XUẤT BẢN GIAO THÔNG VẬN TẢI')
---quản trị
go
insert into  quantri values('admin@gmail.com','21232f297a57a5a743894a0e4a801fc3','admin')
------TẠO BẢNG CTKHUYENMAI
INSERT INTO CTKHUYENMAI
VALUES
('KM01',N'Nhân dịp lễ 30/4 1/5 chúng tôi giảm giá 10% đối với 1 số sách',10)
------TẠO BẢNG THELOAI
INSERT INTO THELOAI
VALUES
('TL01',N'CHÍNH TRỊ – PHÁP LUẬT'),
('TL02',N'KHOA HỌC CÔNG NGHỆ - KINH TẾ'),
('TL03',N'VĂN HỌC NGHỆ THUẬT'),
('TL04',N'VĂN HÓA XÃ HỘI - LỊCH SỬ'),
('TL05',N'GIÁO TRÌNH'),
('TL06',N'TRUYỆN - TIỂU THUYẾT'),
('TL07',N'TÂM LÝ - TÂM LINH - TÔN GIÁO'),
('TL08',N'THIẾU NHI')
------TẠO BẢNG SACH
SET DATEFORMAT DMY

INSERT INTO SACH VALUES
('MS01' , N'Bộ Luật Dân Sự' , 63000 , N'Quyển' , N'Bộ luật dân sự bao gồm 689 điều, có những nội dung nổi bật về chuyển đổi giới tính. Thực hiện hợp đồng khi hoàn cảnh thay đổi cơ bản. Các nghĩa vụ tài sản và các khoản chi phí liên quan đến thừa kế...' , N'Hinh1.jpg' , '01/01/2019' , '01/05/2020' , N'QUỐC HỘI' , 'TL01' , 'NXB04' , 'KM01' ,NULL),
('MS02',N'Luật Doanh Nghiệp',49000,N'Quyển',N'Giới thiệu toàn văn Luật Doanh nghiệp bao gồm những qui định chung và qui định cụ thể về thành lập doanh nghiệp; công ty trách nhiệm hữu hạn; doanh nghiệp nhà nuớc; công ty cổ phần; công ty hợp danh; doanh nghiệp tư nhân; nhóm công ti; tổ chức lại, giải thể và phá sản doanh nghiệp; tổ chức thực hiện',N'Hinh2.jpg','01/05/2019','01/02/2020',N'QUỐC HỘI','TL01','NXB07','KM01',NULL),
('MS03',N'Luật Đất Đai (Hiện Hành, Sửa Đổi Bsung 2018)',51000,N'Quyển',N'Luật đất đai (hiện hành, sửa đổi bsung 2018)',N'Hinh3.jpg','19/05/2018','01/03/2019',N'QUỐC HỘI','TL01','NXB04','KM01',NULL),
('MS04',N'Bách khoa toàn thư về Khoa học',450000,N'Quyển',N'Cuốn sách như một kho tri thức tuyệt vời với cách tiếp cận trực quan mới lạ cung cấp những thông tin toàn diện & chuẩn xác nhất về Khoa học dành cho cả gia đình.',N'Hinh4.jfif','10/04/2019','19/06/2019',N'Thế Tuấn','TL02','NXB02','KM01',NULL),
('MS05',N'Các Thế Giới Song Song',80000,N'Quyển',N'Một chuyến du hành đầy trí tuệ qua các vũ trụ, được dẫn dắt tài tình bởi "thuyền trưởng" Michio Kaku và độc giả có dịp chiêm ngưỡng vẻ đẹp kỳ vĩ của vũ trụ kể từ vụ nổ lớn, vượt qua những hố đen, lỗ sâu, bước vào các thế giới lượng tử từ muôn màu kỳ lạ nằm ngay trước mũi chúng ta, vốn dĩ tồn tại song song trên một màng bên ngoài không - thời gian bốn chiều, ngắm nhìn thực tại vật chất quen thuộc hoà quyện với thế giới của những điều kỳ diệu như năng lượng và vật chất tối, sự nảy chồi của các vũ trụ, những chiều không gian bí ẩn và sự biến ảo của các dây rung siêu nhỏ',N'Hinh5.jpg','04/06/2017','03/05/2019',N'Michio Kaku','TL02','NXB06','KM01',NULL),
('MS06',N'Cách Nền Kinh Tế Vận Hành - How The Economy Works',90000,N'Quyển',N'Tiền chảy vào đâu là THIÊN THỜI sáng ở đó. Khi lãi suất huy động 20% tiền sẽ chảy vào ngân hàng lợi nhuận nhiều hơn mua bất động sản. Khi lãi suất tiền gửi giảm về 6% (2013-nay) thì người dân sẽ rút tiền ngân hàng về mua bất động sản, chứng khoán tích trữ => Bất động sản, chứng khoán sẽ bắt đầu có thanh khoản, dễ bán, nhiều người quan tâm, đó là giai đoạn bắt đầu phục hồi',N'Hinh6.jfif','01/07/2017','04/05/2019',N'Roger E.A.Farmer','TL02','NXB05','KM01',NULL),
('MS07',N'Cây Cam Ngọt Của Tôi',79800,N'Quyển',N'Tiền chảy vào đâu là THIÊN THỜI sáng ở đó. Khi lãi suất huy động 20% tiền sẽ chảy vào ngân hàng lợi nhuận nhiều hơn mua bất động sản. Khi lãi suất tiền gửi giảm về 6% (2013-nay) thì người dân sẽ rút tiền ngân hàng về mua bất động sản, chứng khoán tích trữ => Bất động sản, chứng khoán sẽ bắt đầu có thanh khoản, dễ bán, nhiều người quan tâm, đó là giai đoạn bắt đầu phục hồi',N'Hinh7.jfif','23/07/2016','04/07/2019',N'Nguyễn Bích Lan','TL03','NXB06','KM01',NULL),
('MS08',N'Việt Nam Sử Lược',233000,N'Quyển',N'Đầu thế kỷ XX, giữa lúc nền học thuật nước nhà chỉ có các bộ đại tác như Đại Việt sử ký toàn thư hay Khâm định Việt sử thông giám cương mục là nguồn sử liệu chính thống nhưng chưa đáp ứng nhu cầu tìm hiểu lịch sử của phần đông dân chúng, thì Việt Nam sử lược, với tư cách là bộ thông sử chi tiết đầu tiên được viết bằng chữ quốc ngữ, đã xuất hiện và nhanh chóng thu hút sự quan tâm của độc giả lẫn giới nghiên cứu cả nước. Từ đó đến nay đã 100 năm trôi qua, tác phẩm vẫn giữ nguyên giá trị và là quyển sách vỡ lòng quen thuộc cho những ai bắt đầu tìm hiểu lịch sử Việt Nam.',N'Hinh8.jpg','11/11/2020','05/01/2021',N'Trần Trọng Kim','TL04','NXB01','KM01',NULL),
('MS09',N'Từ Điển Tiếng “Em”',48200,N'Quyển',N'Bạn sẽ bất ngờ, khi cầm cuốn “từ điển” xinh xinh này trên tay. Và sẽ còn ngạc nhiên hơn nữa, khi bắt đầu đọc từng trang sách…Dĩ nhiên là vì “Từ điển tiếng “Em” không phải là một cuốn từ điển thông thường rồi!.',N'Hinh9.jpg','11/10/2020','05/02/2021',N'Khotudien','TL03','NXB08','KM01',NULL),
('MS10',N'Sự Trỗi Dậy Và Suy Tàn Của Đế Chế Thứ Ba - Lịch Sử Đức Quốc Xã',229100,N'Quyển',N'Ngay trong năm đầu tiên phát hành - 1960, Sự trỗi dậy và suy tàn của Đế chế thứ ba đã bán được tới 1 triệu bản tại Mỹ và được tái bản hơn 20 lần. Cuốn sách là bản tường thuật hết sức chi tiết về nước Đức, dưới sự cai trị của Adolf Hitler và Đảng Quốc xã. Tác giả đã nghiên cứu kĩ lưỡng về sự ra đời của Đế chế thứ ba ở Đức, con đường dẫn đến quyền lực tuyệt đối của Đảng Quốc xã, diễn biến của Chiến tranh thế giới lần thứ hai và sự thất bại của Phát xít Đức. Nguồn tài liệu của cuốn sách bao gồm lời khai của các nhà lãnh đạo Đảng Quốc xã, nhật kí của các quan chức, cùng hàng loạt các quân lệnh và thư mật. Sự trỗi dậy và suy tàn của Đế chế thứ ba là một trong những công trình nghiên cứu lịch sử quan trọng nhất, nói về một trong những giai đoạn u ám nhất của lịch sử loài người.',N'Hinh10.jpg','03/03/2020','05/04/2021',N'William L. Shirer','TL04','NXB01','KM01',NULL)
INSERT INTO SACH(MASACH,TENSACH,DONGIA,DONVITINH,MOTA,HINHMINHHOA,NAMXB,NGAYCAPNHAT,TENTACGIA,MATHELOAI,MANXB)
VALUES
('MS11',N'Phân Tích Báo Cáo Tài Chính - Hướng Dẫn Thực Hành',169000,N'Quyển',N'Phân Tích Báo Cáo Tài Chính là một kỹ năng cơ bản của những ai liên quan đến quản lý đầu tư, tài chính doanh nghiệp, tín dụng thương mại và gia hạn tín dụng. Nhiều năm qua, nó trở thành một nỗ lực ngày càng phức tạp vì báo cáo tài chính doanh nghiệp ngày càng trở nên khó giải mã. Nhưng với quyền Phân tích báo cáo tài chính ấn bản lần thứ tư này, bạn đọc sẽ học cách thức xử lý những thách thức mà là một phần của doanh nghiệp trong thực tiễn!',N'Hinh11.jpg','11/03/2013','05/08/2015',N'Fernando Alvarez, Martin Fridson','TL05','NXB03'),
('MS12',N'Giáo Trình Luật Hiến Pháp Việt Nam',110000,N'Quyển',N'GIÁO TRÌNH LUẬT HIẾN PHÁP VIỆT NAM (TÁI BẢN LẦN THỨ 6 CÓ SỬA CHỮA, BỔ SUNG)',N'Hinh12.png','11/12/2017','05/08/2018',N'Nhiều tác giả','TL05','NXB04'),
('MS13',N'Giáo Trình Các Lí Thuyết Phát Triển Tâm Lí Người',99750,N'Quyển',N'Giáo trình Các lí thuyết phát triển tâm lí người Phan Trọng Ngọ (Chủ biên) - Lê Minh Nguyệt',N'Hinh13.jpg','11/9/2016','05/03/2019',N'Phan Trọng Ngọ','TL05','NXB08'),
('MS14',N'All In Love - Ngập Tràn Yêu Thương',79600,N'Quyển',N'All in love - Ngập tràn yêu thương là cuốn sách nổi tiếng nhất trong sự nghiệp của tác giả Cố Tây Tước, cuốn sách được xuất bản lần đầu tại Việt Nam vào 2014, liên tục được tái bản và lọt Top sách văn học lãng mạn bán chạy của Tiki trong nhiều năm.',N'Hinh14.jpg','11/9/2017','05/03/2020',N'Cố Tây Tước','TL06','NXB03'),
('MS15',N'Ếch Ộp',56000,N'Quyển',N'Hài hước, mặn mà, bắt sóng nhanh chóng các xu hướng được giới trẻ quan tâm chính là những gì có trong cuốn sách “Ếch ộp – Tuyển tập truyện siêu ngắn”.',N'Hinh15.jpg','01/12/2020','05/03/2021',N' Nguyễn Hưng','TL06','NXB04'),
('MS16',N'Doraemon - Chú Mèo Máy Đến Từ Tương Lai',39900,N'Quyển',N'Doraemon là một trong những nhân vật truyện tranh nổi tiếng nhất thế giới, tại Việt Nam, tác phẩm cũng đã có gần 30 năm đồng hành cùng nhiều thế hệ độc giả.',N'Hinh16.jpg','01/12/1978','15/03/2020',N'Fujiko F Fujio','TL06','NXB04'),
('MS17',N'Muôn Kiếp Nhân Sinh-(Many Lives - Many Times)',67200,N'Quyển',N'“Muôn kiếp nhân sinh” là một bức tranh lớn với vô vàn mảnh ghép cuộc đời, là một cuốn phim đồ sộ, sống động về những kiếp sống huyền bí, trải dài từ nền văn minh Atlantis hùng mạnh đến vương quốc Ai Cập cổ đại của các Pharaoh quyền uy, đến Hợp Chủng Quốc Hoa Kỳ ngày nay.',N'Hinh17.jfif','01/12/2020','01/03/2021',N'Fujiko F Fujio','TL07','NXB05'),
('MS18',N'Giận',95900,N'Quyển',N'Giận là một cuốn sách hay của Thiền sư Thích Nhất Hạnh, nó mở ra cho ta những khả năng kỳ diệu, nhưng lại rất dễ thực hành để ta tự mình từng bướ thoát khỏi cơn giận và sống đẹp với xã hội quanh mình.',N'Hinh18.png','01/05/2020','01/01/2021',N'Thích Nhất Hạnh','TL07','NXB06'),
('MS19',N'Thiền Sư Và Em Bé 5 Tuổi',76600,N'Quyển',N'Trong lòng chúng ta ai cũng có một em bé đang đau khổ. Ai cũng có một thời gian khó khăn khi còn thơ ấu và nhiều người đã trải qua những chấn động tâm lý, những tổn thương lớn mà vết thương còn lưu lại đến bây giờ. Để tự bảo vệ và phòng hộ trước những khổ đau trong tương lai, chúng ta thường cố quên đi thời gian đau lòng đó. Mỗi khi tiếp xúc với những kinh nghiệm khổ đau ấy, chúng ta nghĩ rằng chúng ta sẽ không chịu đựng nổi, sẽ không có khả năng xử lý nên chúng ta nén chặt những cảm xúc và ký ức của mình vào đáy sâu vô thức. Đó có thể là do đã từ lâu chúng ta không đủ can đảm để đối diện với em bé đó.',N'Hinh19.jpg','01/10/2020','01/02/2021',N'Thích Nhất Hạnh','TL07','NXB07'),
('MS20',N'Hoàng Tử Bé',50800,N'Quyển',N'Hoàng tử bé được viết ở New York trong những ngày tác giả sống lưu vong và được xuất bản lần đầu tiên tại New York vào năm 1943, rồi đến năm 1946 mới được xuất bản tại Pháp. Không nghi ngờ gì, đây là tác phẩm nổi tiếng nhất, được đọc nhiều nhất và cũng được yêu mến nhất của Saint-Exupéry. Cuốn sách được bình chọn là tác phẩm hay nhất thế kỉ 20 ở Pháp, đồng thời cũng là cuốn sách Pháp được dịch và được đọc nhiều nhất trên thế giới. Với 250 ngôn ngữ dịch khác nhau kể cả phương ngữ cùng hơn 200 triệu bản in trên toàn thế giới, Hoàng tử bé được coi là một trong những tác phẩm bán chạy nhất của nhân loại.',N'Hinh20.jfif','29/05/2019','01/08/2020',N'Antoine De Saint-Exupéry','TL08','NXB08'),
('MS21',N'Đại Việt Sử Ký Toàn Thư Trọn Bộ',143900,N'Quyển',N'Trong các sách lịch sử cũ của ta , thì " Đại Việt sử ký toàn thư " là một bộ sử lớn chép từ Hồng Bàng đến Ất Mão ( 1675) đời vua Gia Tôn nhà Lê. " Đại Việt sử ký toàn thư " là bộ sách lịch sử quý báu trong tủ sách sử cũ của nước Việt Nam , rất cần thiết cho những người nghiên cứu lịch sử dân tộc.',N'Hinh21.jpg','29/05/2020','01/02/2021',N'Cao Huy Giu','TL04','NXB09'),
('MS22',N'Sapiens Lược Sử Loài Người',223600,N'Quyển',N'Sapiens là một câu chuyện lịch sử lớn về nền văn minh nhân loại – cách chúng ta phát triển từ xã hội săn bắt hái lượm thuở sơ khai đến cách chúng ta tổ chức xã hội và nền kinh tế ngày nay.',N'Hinh22.jfif','30/05/2020','01/03/2021',N' Nina Shapiro, Kristin Loberg','TL07','NXB10'),
('MS23',N'Luật Thuế',59000,N'Quyển',N'Nội dung sách bao gồm các văn bản quy phạm pháp luật quy định về người nộp thuế, thu nhập chịu thuế, thu nhập được miễn thuế, căn cứ tính thuế, phương pháp tính thuế và ưu đãi thuế thu nhập doanh nghiệp.',N'Hinh23.jpg','18/05/2019','01/03/2020',N'Quốc hội','TL01','NXB10'),
('MS24',N'Kinh Tế Học Vĩ Mô',253800,N'Quyển',N'N.Gregory Mankiw là giáo sư kinh tế Đại học Harvard. Ông có nhiều bài viết và thường xuyên tham gia các chương trình tranh luận về học thuật cũng như các chính sách về kinh tế. Là một trong 25 Nhà kinh tế học nổi tiếng trên thế giới và sách Kinh tế học của ông đã và đang được nhiều trường đại học trên thế giới sử dụng. Ông cũng là tác giả của giáo trình Kinh tế Vĩ mô trình độ trung cấp bán chạy nhất (Nhà xuất bản Worth).',N'Hinh24.jpg','25/02/2018','18/06/2019',N'NGregory Mankiw','TL05','NXB05'),
('MS25',N'Code Dạo Kí Sự - Lập Trình Viên Đâu Phải Chỉ Biết Code',142500,N'Quyển',N'Nếu các bạn có đọc các blog về lập trình ở Việt Nam thì có lẽ cái tên Tôi đi code dạo không có gì quá xa lạ đối với các bạn. Về tác giả của blog Tôi đi code dạo, anh tên thật là Phạm Huy Hoàng, một Developer Full Stack, cựu sinh viên trường FPT University, hiện tại anh đang học Thạc sĩ Computer Science tại Đại học Lancaster ở Anh (học bổng $18000). Trước khi qua Xứ Sở Sương Mù, anh đã từng làm việc tại FPT Software và ASWIG Solutions.',N'Hinh25.jpg','12/01/2017','28/05/2018',N'Phạm Huy Hoàng (Developer)','TL02','NXB03'),
('MS26',N'Kiếp Nào Ta Cũng Tìm Thấy Nhau',51900,N'Quyển',N'Kiếp nào ta cũng tìm thấy nhau là cuốn sách thứ ba của Brain L. Weiss – một nhà tâm thần học có tiếng. Trước đó ông đã viết hai cuốn sách: cuốn đầu tiên là Ám ảnh từ kiếp trước, cuốn sách mô tả câu chuyện có thật về một bệnh nhân trẻ tuổi cùng với những liệu pháp thôi miên về kiếp trước đã làm thay đổi cả cuộc đời tác giả lẫn cô ấy. Cuốn sách đã bán chạy trên toàn thế giới với hơn 2 triệu bản in và được dịch sang hơn 20 ngôn ngữ. Cuốn sách thứ hai Through  Time  into  Healing (Đi  qua  thời  gian  để chữa lành), mô tả những gì tác giả đã học được về tiềm năng chữa bệnh của liệu pháp hồi quy tiền kiếp. Trong cuốn sách đều là những câu chuyện người thật việc thật. Nhưng câu chuyện hấp dẫn nhất lại nằm trong cuốn sách thứ ba.',N'Hinh26.jpg','12/02/2019','28/05/2020',N'Brian L. Weiss','TL06','NXB04'),
('MS27',N'Harry Potter Và Hội Phượng Hoàng',214400,N'Quyển',N'Harry tức giận vì bị bỏ rơi ở nhà Dursley trong dịp hè, cậu ngờ rằng Chúa tể hắc ám Voldemort đang tập hợp lực lượng, và vì cậu có nguy cơ bị tấn công, những người Harry luôn coi là bạn đang cố che giấu tung tích cậu. Cuối cùng, sau khi được giải cứu, Harry khám phá ra rằng giáo sư Dumbledore đang tập hợp lại Hội Phượng Hoàng – một đoàn quân bí mật đã được thành lập từ những năm trước nhằm chống lại Chúa tể Voldemort. Tuy nhiên, Bộ Pháp thuật không ủng hộ Hội Phượng Hoàng, những lời bịa đặt nhanh chóng được đăng tải trên Nhật báo Tiên tri – một tờ báo của giới phù thủy, Harry lo ngại rằng rất có khả năng cậu sẽ phải gánh vác trách nhiệm chống lại cái ác một mình.',N'Hinh27.jpg','12/06/2017','28/01/2020',N'J. K. Rowling, J. K. Rowling','TL08','NXB05'),
('MS28',N'Thần Thoại Hy Lạp',104000,N'Quyển',N'Thần thoại Hy Lạp là tài sản vô cùng quý báu trong gia tài văn hóa nhân loại. Thần thoại cũng có mối liên hệ mật thiết với tôn giáo trong thế giới Hy Lạp, khi giải thích nguồn gốc, cuộc sống của các vị thần, nơi loài người đến và đi sau khi chết, từ ấy đưa ra những lời khuyên tốt nhất để có cuộc sống hạnh phúc cho con người. Ngày nay, dường như niềm tin thần thoại và tư duy thần thoại đã lùi vào dĩ vãng, nhưng chúng ta vẫn trân trọng, lưu giữ tài sản thần thoại như những chiến công hiển hách của loài người trong tiến trình lịch sử. Thần thoại cũng được sử dụng để kể lại các sự kiện lịch sử liên quan đến tổ tiên lâu đời của loài người, các cuộc chiến mà họ đã chiến đấu, và những nơi họ khám phá.',N'Hinh28.jpg','12/05/2020','03/03/2021',N'Nguyễn Văn Khỏa','TL08','NXB01'),
('MS29',N'Có Hai Con Mèo Ngồi Bên Cửa Sổ',50100,N'Quyển',N'Gấu và Tí Hon thân nhau đến mức có thể chia sẻ từng chuyện vui buồn trong những phút giây mềm yếu, lo lắng và chăm sóc, giúp nhau từ miếng ăn đến “chiến lược” để tồn tại lâu dài.Tình bạn là gì? Bạn gái là gì? Tình yêu là gì? Bọn mèo chuột kể với chúng ta nhiều câu chuyện nhỏ, gửi thông điệp rằng, tình yêu có sức mạnh tuyệt diệu, có thể làm nên mọi điều phi thường trong cuộc sống muôn loài.',N'Hinh29.jpg','12/05/2018','03/04/2020',N'Nguyễn Nhật Ánh','TL08','NXB02'),
('MS30',N'Nhà Giả Kim',53200,N'Quyển',N'Tất cả những trải nghiệm trong chuyến phiêu du theo đuổi vận mệnh của mình đã giúp Santiago thấu hiểu được ý nghĩa sâu xa nhất của hạnh phúc, hòa hợp với vũ trụ và con người.',N'Hinh30.jpg','13/11/2017','14/05/2020',N'Paulo Coelho','TL03','NXB07'),
('MS31',N'Hai Số Phận',131250,N'Quyển',N'không chỉ đơn thuần là một cuốn tiểu thuyết, đây có thể xem là "thánh kinh" cho những người đọc và suy ngẫm, những ai không dễ dãi, không chấp nhận lối mòn.',N'Hinh31.jpg','23/11/2016','14/04/2021',N'Jeffrey Archer','TL03','NXB04'),
('MS32',N'Bình luận chuyên sâu phán chung bộ luật hình sự 2015',280000,N'Quyển',N'Trong tố tụng hình sự thì hoạt động định tội danh có vai trò rất quan trọng, nó là trọng tâm mà các hoạt động tố tụng khác hướng tới, bởi lẽ tất cả các hoạt động tố tung hình sự ở giai đoạn điều tra, truy tố, xét xử xét cho cùng đều phải đi đến kết luận về một tội phạm nào đó đã xảy ra và ai là người đã thực hiện tội phạm hay không thực hiện tội phạm đó....',N'Hinh32.jpg','01/01/2015','01/05/2019',N'TT Giới Thiệu Sách TP. HCM','TL01','NXB04'),
('MS33',N'Chính Trị Học',155000,N'Quyển',N'Quyền lực nhà nước và kiểm soát quyền lực nhà nước là vấn đề vô cùng quan trọng, nhưng cũng rất phức tạp. Đối với mỗi nhà nước đương đại, việc xây dựng mô hình tổ chức, sử dụng và kiểm soát quyền lực nhà nước phù hợp sẽ là điều kiện để thực hiện hoạt động quản lý nhà nước đúng mục đích, đạt hiệu quả cao, bảo đảm được quyền và lợi ích chính đáng cho nhân dân – chủ thể cao nhất của quyền lực nhà nước, đây cũng chính là cơ sở để xây dựng đất nước phát triển bền vững.....',N'Hinh33.jpg','01/01/2018','01/05/2020',N'Nguyễn Đăng Dung','TL01','NXB07'),
('MS34',N'Nguồn Gốc Trật Tự Chính Trị',244080,N'Quyển',N'Trước khi đế chế Liên Xô sụp đổ, nhà khoa học chính trị người Mỹ Francis Fukuyama đã nổi tiếng sau khi tuyên bố về chiến thắng toàn cầu của ý tưởng dân chủ tự do kiểu Mỹ. Gần ba thập kỷ sau, mọi thứ có vẻ tồi tệ đối với luận điểm kết thúc lịch sử của ông, mặc dù những độc giả thân thiết của cuốn sách mới nhất của Fukuyama chắc chắn sẽ kết luận rằng mặc dù tư duy của ông đã bị bảo vệ bởi trình độ chuyên môn, và theo sự tổng hợp, câu chuyện cũ của ông về chiến thắng ở Mỹ trong thế giới ý tưởng về cơ bản không thay đổi......',N'Hinh34.jpg','01/01/2018','01/05/2020',N'Francis Fukuyama','TL01','NXB10'),
('MS35',N'100 Mưu Lược Trong Chính Trị, Quân Sự & Đời Sống (Tái Bản)',97900,N'Quyển',N'Xã hội không ngừng phát triển, trên mọi phương diện đều có sự cạnh tranh gay gắt, vì thế việc vận dụng mưu lược là không thể thiếu. Trong các lĩnh vực chính trị, quân sự hay kinh tế, ngoại giao, ngoài tầm nhìn xa trông rộng, tài phân tích, phán đoán nhạy bén, người lãnh đạo còn phải biết cách nhìn người và dùng người trong mọi hoàn cảnh.',N'Hinh35.jpg','01/01/2018','02/02/2021',N'Quách Thành','TL01','NXB01'),
('MS36',N'101 Tư Vấn Pháp LuậT Thường Thức Về Đất Đai',100000,N'Quyển',N'Vấn đề đất đai rất phức tạp, luôn là nỗi ưu tư của người dân bởi nó sát sườn với cuộc sống. Vì vậy, ngay từ ngày đầu ra mắt Báo Sài Gòn Đầu Tư Tài Chính, chúng tôi đã dành 2 trang cho thông tin bất động sản, trong đó có mục Tư vấn, đều đặn ra thường kỳ mỗi số báo. Và đến nay, Báo Sài Gòn Đầu Tư Tài Chính đã đăng trên 900 tư vấn pháp luật. Cuốn sách: “101 tư vấn pháp luật thường thức về đất đai” của Luật gia Nguyễn Văn Khôi chắt lọc từ những bài giải đáp về đất đai đã đăng trên chuyên mục này, tác giả đã thực hiện trong suốt thời gian qua.',N'Hinh36.jpg','02/07/2018','02/02/2021',N'Nguyễn Văn Khôi','TL01','NXB01'),
('MS37',N'Khoa Học - Nghề Nghiệp Và Sứ Mệnh',56000,N'Quyển',N'Max Weber – Tư tưởng gia người Đức, có vai vai trò nổi bật trong quá trình phát triển môn xã hội học hồi cuối thế kỉ XIX, đầu thế kỉ XX.',N'Hinh37.jpg','02/07/2018','02/02/2021',N'Max Weber','TL01','NXB01'),
('MS38',N'Nghiên Cứu Hồ Chí Minh - Một Số Công Trình Tuyển Chọn',211900,N'Quyển',N'Mỗi tập có nội dung cụ thể bám sát vào chủ đề, nhưng nhìn chung, cả ba tập là một chỉnh thể thống nhất về tư tưởng Hồ Chí Minh với ý nghĩa là một hệ thống quan điểm toàn diện và sâu sắc về những vấn đề cơ bản của cách mạng Việt Nam và cách mạng giải phóng dân tộc ở các nước thuộc địa. Gắn kết cả ba tập sách, người đọc có thể cảm nhận thấy trục xuyên suốt của bộ sách bắt đầu từ nhận thức của Đảng Cộng sản Việt Nam về tư tưởng Hồ Chí Minh và cuối cùng là tư tưởng Hồ Chí Minh mãi mãi soi đường cho sự nghiệp cách mạng của nhân dân Việt Nam dành thắng lợi…',N'Hinh38.jpg','02/07/2018','02/02/2021',N'PGS. TS. Bùi Đình Phong','TL01','NXB01'),
('MS39',N'Từ điển pháp luật Việt Nam',300000,N'Quyển',N'Trong những năm qua, cùng với việc từng bước hoàn thiện hệ thống pháp luật thì việc giải thích các từ ngữ pháp luật trong các văn bản mang tính pháp lý cũng được các cơ quan soạn thảo coi trọng, vì vậy đa số các văn bản qui phạm pháp luật và văn bản hướng dẫn thi hành thông thường đều có quy định giải thích những từ ngữ cơ bản nhằm làm rõ nghĩa của từ ngữ sử dụng trong văn bản đó, nhằm tạo sự thống nhất trong cách hiểu, hiểu đúng tinh thần của khái niệm, trên cơ sở đó giúp cho việc trong áp dụng pháp luật được dễ dàng và thuận lợi. Bên cạnh đó, trong hoạt động nghiên cứu, học tập cũng như áp dụng pháp luật thì việc sử dụng các từ ngữ, định nghĩa pháp lý ngày càng trở nên phổ biến và cũng là một yêu cầu cần thiết để hỗ trợ việc nâng cao chất lượng của các hoạt động nói trên, nhất là trong việc soạn thảo các văn bản mang tính pháp lý.',N'Hinh39.jfif','02/07/2019','02/02/2021',N'Luật gia Nguyễn Ngọc Điệp','TL01','NXB01'),
('MS40',N'Tư Bản Thế Kỷ 21',208500,N'Quyển',N'Phân phối tài sản là một trong những vấn đề gây nhiều tranh cãi và được thảo luận rộng rãi nhất thời nay. Nhưng thật ra, ta biết gì về quá trình tiến hóa của phân phối tài sản trong dài hạn? Liệu sự tích lũy tư bản tư nhân có nhất thiết dẫn đến sự tập trung tài sản vào tay một nhóm ngày càng nhỏ, như niềm tin của Karl Marx hồi thế kỷ 19? Hay là các áp lực cân bằng giữa tăng trưởng, cạnh tranh và tiến bộ công nghệ sẽ dẫn đến giảm bất bình đẳng và tăng tính đại đồng giữa các tầng lớp xã hội, như tư tưởng của Simon Kuznets thế kỷ 20? Thật ra, ta biết gì về quá trình tiến hóa của tài sản và thu nhập từ thế kỷ 18, và ta rút ra những bài học gì từ những hiểu biết đó cho thời đại ngày nay? Đây là những câu hỏi mà tác giả cố gắng trả lời trong quyển sách này. Những câu trả lời ở đây không hoàn hảo và chưa hoàn chỉnh, nhưng chúng dựa trên những dữ liệu lịch sử và so sánh rộng lớn hơn nhiều so với những dữ liệu của các nhà nghiên cứu trước đây. Dữ liệu trong nghiên cứu này bao trùm ba thế kỷ và hơn 20 quốc gia, cũng như dựa trên một khung lý thuyết mới, qua đó giúp chúng ta tìm hiểu sâu xa hơn về những cơ chế vận động căn bản của tài sản và thu nhập.',N'Hinh40.jpg','10/04/2019','02/03/2021',N'Thomas Piketty','TL02','NXB02'),
('MS41',N'Thống Kê Trong Kinh Tế Và Kinh Doanh - Statistics For Business And Economics',437000,N'Quyển',N'SG Trading xin giới thiệu quyển sách Thống Kê Ứng Dụng Trong Kinh Tế Và Kinh Doanh, quyển sách 889 trang này được dịch từ ấn phẩm Statistics For Business And Economics của nhà xuất bản nổi tiếng Cengage Learning, góp phần cho những bạn đọc được tiếp cận với những kiến thức thống kê được các trường đại học kinh doanh hàng đầu trên thế giới giảng dạy. Nhằm nâng cao kiến thức, tư duy nguồn nhân lực chất lượng cao trong lĩnh vực thống kê, phân tích dữ liệu trong kinh doanh.',N'Hinh41.jpg','03/03/2019','02/03/2021',N'David R. Anderson','TL02','NXB04'),
('MS42',N'AI Trong Cuộc Cách Mạng Công Nghệ 4.0 - Con Đường Ngắn Nhất Để Phát Triển Doanh Nghiệp',129500,N'Quyển',N'AI giờ đây đã không còn là khái niệm xa lạ nữa. Nó có mặt ở mọi nơi: trong điện thoại, trong xe ô tô, trong những trải nghiệm mua sắm, ứng dụng hẹn hò, ở trong bệnh viện, ngân hàng hay ở trên các tin tức. Một cuộc chạy đua về AI đang diễn ra trên phạm vi toàn cầu. Mọi giám đốc doanh nghiệp, những nhà quản lý, người khởi nghiệp, nhà đầu tư, tư vấn viên, nhà hoạch định chính sách đều gấp rút học hỏi, phát triển và sở hữu trí thông minh nhân tạo, bởi họ hiểu rằng nó sẽ tạo ra những thay đổi đột phá lên doanh nghiệp và tổ chức của họ.',N'Hinh42.jpg','03/03/2019','02/03/2021',N'Nhiều Tác Giả','TL02','NXB04'),
('MS43',N'Kinh Tế Học Vi Mô',359000,N'Quyển',N'Tiền chảy vào đâu là THIÊN THỜI sáng ở đó. Khi lãi suất huy động 20% tiền sẽ chảy vào ngân hàng lợi nhuận nhiều hơn mua bất động sản. Khi lãi suất tiền gửi giảm về 6% (2013-nay) thì người dân sẽ rút tiền ngân hàng về mua bất động sản, chứng khoán tích trữ => Bất động sản, chứng khoán sẽ bắt đầu có thanh khoản, dễ bán, nhiều người quan tâm, đó là giai đoạn bắt đầu phục hồi. Tiếp đó để kích thích nền kinh tế, Ngân hàng Nhà nước sẽ in tiền, và mua lại trái phiếu chính phủ hoặc tài sản tài chính, giúp chính phủ BƠM TIỀN cho cả nền kinh tế = Cung cấp tín dụng cho vay nhiều hơn => Các doanh nghiệp và cá nhân tiếp cận vốn vay lãi suất thấp, làm ăn tốt hơn, thu nhập và giá cả tăng lên => tiền này lại chảy qua mọi ngả ngách và vào bất động sản, chứng khoán. Lúc này bạn đầu tư bất động sản và chứng khoán là có lợi nhất, cụ thể giai đoạn những năm 2013 - 2015 là tốt nhất để mua vào.',N'Hinh43.jpg','01/03/2018','02/03/2021',N'Robert S. Pindyck, Daniel L. Rubinfeld','TL02','NXB04'),
('MS44',N'Thời Kỳ Hậu Corona: Luôn Có Cơ Hội Trong Khủng Hoảng',180500,N'Quyển',N'Diễn biến của thời cuộc là bất trắc. Trong khủng hoảng, nguy và cơ tồn tại song hành nhưng quan trọng là chúng tác động đến mọi người theo cách khác nhau',N'Hinh44.jfif','01/03/2017','02/03/2020',N'Scott Galloway','TL02','NXB04'),
('MS45',N'Tại Sao Chúng Tôi Muốn Bạn Giàu',78800,N'Quyển',N'Họ là những doanh nhân hàng đầu, có một không hai của thế giới, nổi tiếng và thành đạt. Họ khác nhau ở điểm xuất phát nhưng sẽ cùng nhau chỉ bạn điểm xuất phát cho sự giàu có bằng chính kinh nghiệm của họ. Từ quan niệm “cho người ta một con cá, và bạn nuôi sống anh ta một ngày. Dạy người ta câu cá, và bạn nuôi sống anh ta cả đời” hai nhà tỷ phú hàng đầu sẽ cho bạn thấy tại sao tiền bạc làm nên sự giàu có nhưng lại không giúp ta thoát khỏi sự nghèo khó.',N'Hinh45.jpg','01/03/2018','02/03/2020',N'Donald J.Trump & Robert T.Kiyosaki','TL02','NXB04'),
('MS46',N'Giải Quyết Vấn Đề Bằng Tư Duy Thiết Kế',115000,N'Quyển',N'Sự cạnh tranh trên thị trường ngày càng khốc liệt, trong khi nguồn lực cả về người lẫn vốn trong doanh nghiệp đều rơi vào tình trạng cạn kiệt. Chỉ một vài công ty có thực lực mới có thể vực dậy. Còn những công ty trên bờ vực sụp đổ sẽ phải làm sao?',N'Hinh46.png','01/03/2018','02/03/2020',N'Nhiều Tác Giả','TL02','NXB05'),
('MS47',N'Gia Tộc Morgan - Một Triều Đại Ngân Hàng Mỹ Và Sự Trỗi Dậy Của Nền Tài Chính Hiện Đại',315000,N'Quyển',N'Hình thành, phát triển, sụp đổ rồi lại hồi sinh, có lẽ không một tổ chức nào ẩn chứa nhiều giai thoại, bí mật hay chủ đề gây tranh cãi gay gắt như đế chế ngân hàng Mỹ – Gia tộc Morgan. Đạt Giải thưởng Sách quốc gia và hiện được coi là một tác phẩm kinh điển, Gia tộc Morgan là cuốn tiểu sử tham vọng nhất từng được viết về một triều đại ngân hàng Mỹ. Cuốn sách vẽ nên bức tranh toàn diện về bốn thế hệ nhà Morgan và các công ty bí mật, mạnh mẽ mà họ sở hữu. Với thế lực của mình, đế chế Morgan đã biến nền kinh tế non trẻ của Mỹ thành một cường quốc công nghiệp mạnh nhất thế giới và khiến trung tâm tài chính thế giới dịch chuyển từ London sang New York. Vượt xa cả lịch sử đơn thuần của ngành ngân hàng Mỹ, cuốn sách chính là câu chuyện về sự tiến hóa của nền tài chính hiện đại. Dựa trên các cuộc phỏng vấn rộng rãi cùng quyền truy cập đặc biệt vào kho lưu trữ của gia tộc này, tác đã khắc họa nên bức chân dung hấp dẫn về câu chuyện riêng của nhà Morgan và thế giới hiếm hoi của giới tinh hoa Mỹ và Anh.',N'Hinh47.jpg','01/03/2018','02/03/2020',N'Ron Cherrow','TL02','NXB05'),
('MS48',N'Câu Chuyện Nghệ Thuật',155000,N'Quyển',N'Cuốn sách là một bước dạo đầu mới lạ vào chủ đề nghệ thuật. Với kết cấu đơn giản, cuốn sách này điểm qua 50 tác phẩm then chốt, từ các bức vẽ trên vách động Lascaux tới những tác phẩm sắp đặt đương đại, và liên hệ các tác phẩm ấy với những trào lưu, chủ đề cùng kĩ thuật chính yếu trong nghệ thuật.',N'Hinh48.jpg','19/05/2018','01/03/2019',N'Susie Hodge','TL03','NXB02'),
('MS49',N'The Story Of Art - Câu Chuyện Nghệ Thuật',849000,N'Quyển',N'Tác giả E.H Gombrich (1909-2001) là một trong những nhà sử học nghệ thuật lỗi lạc nhất của nửa sau thế kỷ 20, đối với giới hàn lâm cũng như với tầng lớp công chúng rộng rãi. Những tác phẩm khác mang tính lý thuyết của ông cũng đã trở thành những công trình then chốt đối với các nhà nghiên cứu lịch sử nghệ thuật.',N'Hinh49.jfif','19/05/2018','01/03/2019',N'Gombrich','TL03','NXB02'),
('MS50',N'ISMS - Hiểu Về Nghệ Thuật Hiện Đại',161000,N'Quyển',N'Nghệ thuật hiện đại, hơn bất kỳ đề tài nào khác, luôn được cấu trúc bởi các ism: các phong trào, các khuynh hướng, các phong cách hoặc các trường phái hoạt động như những loại hình thực hành nghệ thuật của các nghệ sĩ khác nhau. Cuốn sách cho chúng ta thấy rằng nghệ thuật hiện đại và hậu hiện đại thay đổi quá nhanh chóng, khác hẳn nghệ thuật hàn lâm và thời kỳ phục hưng.',N'Hinh50.jpg','19/05/2018','01/03/2019',N'Sam Phillips','TL03','NXB02'),
('MS51',N'Quân Tử Hoa - Nghệ Thuật Vẽ Màu Nước Cổ Trang',143000,N'Quyển',N'Cuốn sách gồm nhiều tranh màu tuyệt đẹp in trên khổ lớn và các bước cụ thể, chi tiết hướng dẫn vẽ hoa và mĩ nam bằng màu nước theo phong cách cổ trang. Sách hướng dẫn chi tiết từ việc chọn họa cụ, giấy vẽ, kĩ thuật phác thảo, tô màu nước, cách tạo hình hoa và mĩ nam, cũng như thực hành vẽ tranh chân dung, tranh bán thân, tranh toàn thâ một cách bài bản và đẹp nhất.',N'Hinh51.jpg','19/05/2018','01/03/2019',N'Nhật Xuất Tiểu Thái Dương','TL03','NXB07'),
('MS52',N'Tôi Tự Học',42000,N'Quyển',N'Cuốn sách này tuy đã được xuất bản từ rất lâu nhưng giá trị của sách vẫn còn nguyên vẹn. Những tư tưởng, chủ đề của sách vẫn phù hợp và có thể áp dụng trong đời sống hiện nay. Thiết nghĩ, cuốn sách này rất cần cho mọi đối tượng bạn đọc vì không có giới hạn nào cho việc truy tầm kiến thức, việc học là sự nghiệp lâu dài của mỗi con người. Đặc biệt, cuốn sách là một tài liệu quý để các bạn học sinh – sinh viên tham khảo, tổ chức lại việc học của mình một cách hợp lý và khoa học. Các bậc phụ huynh cũng cần tham khảo sách này để định hướng và tư vấn cho con em mình trong quá trình học tập.',N'Hinh52.jpg','19/05/2018','01/03/2021',N'Thu Giang - Nguyễn Duy Cần','TL03','NXB07'),
('MS53',N'Quo Vadis',192000,N'Quyển',N'Quo vadis vẽ nên một bức tranh về thời kỳ sơ khai của Thiên chúa giáo, mấy chục năm sau ngày Chúa Jesus bị đóng đinh câu rút. Câu chuyện xoay quanh mối tình của Vinicius và Lygia, trong bối cảnh rộng lớn hơn là vụ hỏa tai thành Roma năm 64, đưa đến cuộc khủng bố giáo dân sau đó. Tình yêu của đôi trẻ là hạt mầm tốt đẹp đã được gieo bởi đức tin chính vào những thời khắc ác nghiệt tăm tối nhất, thời của bạo chúa Nero và xã hội La Mã trụy lạc sa đọa. Quo vadis tái hiện sinh động trước mắt ta một thành Roma đầy màu sắc, có những tiện dân từ mọi miền thế giới chen chúc nhau trên những con đường dẫn tới Forum Romanum, có những tiệc rượu trong tiếng nhạc lời thơ Anacreon, có tiếng sư tử gầm vang trong đấu trường, và có tiếng lửa thiêu da thịt kẻ tuẫn đạo treo mình trên thập tự.',N'Hinh53.jpg','19/05/2018','01/03/2021',N'Henryk Sienkiewicz','TL03','NXB07'),
('MS54',N'Lược Sử Vạn Vật - Phiên bản dành cho nhà khoa học nhí',251000,N'Quyển',N'Lược sử vạn vật phiên bản gốc dành cho người lớn (cuốn sách này đã được Alpha Books xuất bản năm 2017 và tái bản nhiều lần) là cuốn sách phổ biến khoa học trình bày một cách ngắn gọn lịch sử nghiên cứu khoa học tự nhiên, những thành tựu khoa học trong các lĩnh vực khoa học tự nhiên chính: vật lý, hóa học, sinh học, địa chất, thiên văn… với nhiều tên tuổi, giai thoại và sự thật.',N'Hinh54.jfif','19/05/2018','01/03/2021',N'Bill Bryson','TL03','NXB06'),
('MS55',N'Suối Nguồn',282000,N'Quyển',N'Tác phẩm đứng đầu bảng xếp hạng những tiểu thuyết hay nhất thế kỷ 20 do độc giả bình chọn (theo điều tra của New York Time)',N'Hinh55.jpg','19/05/2018','01/03/2021',N'Ayn Rand','TL03','NXB06'),
('MS56',N'Lịch Sử Văn Minh Trung Hoa',103000,N'Quyển',N'Giữa những năm 30 của thế kỉ này, khi các đế quốc châu Âu, châu Mĩ đang đà cường thịnh, khi bản đồ Á Phi còn tô một màu thuộc địa xám xịt, thì Will Durant cho ra bộ LỊCH sử VĂN MINH THẾ GIỚI với phần MỞ ĐẦU là lịch sử nền văn minh của các nước phương Đông: Ai Cập, Tây A, Ấn Độ, Trung Hoa, Nhật Bản',N'Hinh56.jpg','11/11/2020','05/01/2021',N'Will Durant','TL04','NXB01'),
('MS57',N'Napoleon Đại Đế',424000,N'Quyển',N'Napoleon là một nhân vật đặc biệt vĩ đại và hấp dẫn trong lịch sử Pháp cũng như lịch sửthế giới. Cuộc đời, sự nghiệp, quan điểm, tài năng của ông đã là chủ đề của hàng nghìncuốn sách trong suốt hai thế kỉ qua, và có lẽ sẽ còn được nghiên cứu tiếp trong nhiều thế kỉ sau nữa.Thuở nhỏ, cậu bé ham mê đọc sách, nhưng trình độ học vấn không cao. Pháp văn của ông rất tệ, ông thường bị trêu chọc ở trường vì chất giọng khôi hài.',N'Hinh57.jpg','11/11/2020','05/01/2021',N'Will Durant','TL04','NXB01'),
('MS58',N'Nguồn Gốc Văn Minh',75600,N'Quyển',N'Ngược dòng thời gian để xem văn minh nhân loại và vạn vật muôn loài được hình thành như thế nào.',N'Hinh58.jpg','11/11/2020','05/01/2021',N'Will Durant','TL04','NXB01'),
('MS59',N'Ba Thế Hệ Trí Thức Người Việt (1862 - 1954) - Nghiên Cứu Lịch Sử Xã Hội',161000,N'Quyển',N'Cuốn sách Ba thế hệ trí thức người Việt (1862 - 1954) - Nghiên cứu Lịch sử xã hội được xuất bản lần đầu với nội dung là các bài giảng của Trịnh Văn Thảo tại Collège International de Philosophie (Trường Quốc tế về Triết học) từ tháng 2 đến tháng 6 năm 1987. Tác phẩm đã thể hiện lại hành trình xã hội (của tập thể và của các cá nhân) của ba thế hệ trí thức Việt Nam.',N'Hinh59.jpg','11/11/2020','05/01/2021',N'Trịnh Văn Thảo','TL04','NXB01'),
('MS60',N'Hàn Phi Tử',160000,N'Quyển',N'Tác phẩm Hàn Phi Tử của Hàn Phi là “cuốn sách giáo khoa dạy làm vua” độc đáo, mang đậm dấu ấn của chế độ phong kiến phương Đông.',N'Hinh60.png','11/11/2020','05/01/2021',N'Hàn Phi Tử','TL04','NXB01'),
('MS61',N'Lịch Sử Văn Minh Ấn Độ',127000,N'Quyển',N'Trong giới biên khảo, sử gia giữ một địa vị dặc biệt, vì sức làm việc phi thường của họ. Họ kiên nhẫn, cặm cụi hơn hết thảy các nhà khác, hi sinh suốt đời cho vãn hóa khòng màng danh vọng, lợi lộc, bỏ ra từ ba đến năm chục năm để lập nên sự nghiệp. Họ đọc sách nhiều, du lịch nhiều, suy tư nhiều, và nếu họ ít có thanh kiến, thì tác phẩm của họ càng lâu đời càng có giá trị, hiện nay ở phương Tây, loại sách về sử được phố biến rất rộng, có cái cơ muốn lấn át tiểu thuyết.',N'Hinh61.jpg','11/11/2020','05/04/2020',N'Will Durant','TL04','NXB04'),
('MS62',N'Lịch Sử Văn Minh Ả Rập',127000,N'Quyển',N'Thế giới Ả Rập còn gọi là dân tộc Ả Rập hoặc các quốc gia Ả Rập, hiện gồm có 22 quốc gia nói tiếng Ả Rập thuộc Liên đoàn Ả Rập. Lãnh thổ của các quốc gia Ả Rập trải dài từ Đại Tây Dương tại phía tây đến biển Ả Rập tại phía đông, và từ Địa Trung Hải tại phía bắc đến Sừng châu Phi và Ấn Độ Dương tại phía đông nam. Tổng dân số thế giới Ả Rập là khoảng 422 triệu người theo số liệu năm 2012, trên một nửa trong số đó dưới 25 tuổi.',N'Hinh62.jpg','11/11/2020','05/04/2020',N'Will Durant','TL04','NXB04'),
('MS63',N'Lịch Sử Do Thái',356000,N'Quyển',N'Lịch sử Do Thái của Paul Johnson bắt đầu bằng những sự kiện được viết trong Kinh Thánh và kết thúc khi thành lập Nhà nước Israel. Cuốn sách không chỉ giới thiệu về lịch sử 4.000 năm tồn tại của người Do Thái mà còn đề cập đến những tác động, ảnh hưởng cũng như những đóng góp của họ cho nhân loại.',N'Hinh63.png','11/11/2020','05/04/2020',N'Paul Johnson','TL04','NXB04'),
('MS64',N'Nghi Thức Tang Lễ Của Người An Nam',160500,N'Quyển',N'Gustave Dumoutier là nhà Việt Nam học người Pháp thuộc thế hệ đầu tiên, là một học giả có tài, một con người gắn bó và yêu mến lịch sử văn hóa Việt Nam, là nhà Đông phương học đầy nhiệt huyết có chủ trương hợp tác với giới nho sĩ Việt Nam, trân trọng và bảo tồn nền văn hóa Việt Nam truyền thống, duy trì chữ nho và khuyến khích chữ Quốc ngữ.',N'Hinh64.jfif','11/11/2020','05/04/2020',N'Gustave Dumoutier','TL04','NXB04'),
('MS65',N'Giáo Trình Hán Ngữ',59000,N'Quyển',N'Trong khi tiếng Việt và tiếng Anh được viết bằng chữ cái Latinh thì tiếng Trung được viết bằng chữ tượng hình nên rất khó khi bạn tự tìm hiểu ngôn ngữ này. Bạn là người mới bắt đầu học tiếng Trung, bạn muốn tự học nhưng không biết nên học những kiến thức nào, đâu là những kiến thức nền tảng phù hợp với trình độ của những người mới học. Và quan trọng nhất là bạn không biết được nên tìm học từ những tài liệu nào. Trên thị trường tài liệu học tiếng Trung thì nhiều nhưng tài liệu vừa bám sát chương trình dạy học, vừa cập nhật những điểm đổi mới cùng như được biên soạn dễ hiểu thì lại rất ít.',N'Hinh65.jpg','11/11/2020','05/04/2020',N'Nhiều Tác Giả','TL05','NXB05'),
('MS66',N'Giáo Trình Tội Phạm Học',85000,N'Quyển',N'Giới thiệu nhập môn tội phạm học; quá trình hình thành và phát triển của tội phạm học; các phương pháp nghiên cứu tội phạm học; tình hình tội phạm; nguyên nhân của tội phạm; nạn nhân học; dự báo tội phạm; phòng ngừa tội phạm; kiểm soát xã hội đối với tội phạm học.',N'Hinh66.jpg','11/11/2020','05/04/2020',N'Trịnh Tiến Việt - Nguyễn Khắc Hải','TL05','NXB05'),
('MS67',N'Giáo Trình Thực Hành Microsoft Excel 2019',89000,N'Quyển',N'Chúng tôi nhóm biên soạn, xin hân hạnh giới thiệu cùng quý bạn đọc quyển sách "Giáo Trình Thực Hành Microsoft Excel 2019 Căn Bản & Nâng cao". Sách bao gồm 8 CHƯƠNG và kèm theo CD Bài tập thực hành thực tiễn, giúp bạn đọc tự học - tự thực hành một cách nhanh chóng và dễ dàng hơn theo cách cầm tay chỉ việc từng bước một.',N'Hinh68.jfif','11/11/2020','05/04/2020',N'Nhiều tác giả','TL05','NXB05'),
('MS68',N'Giáo Trình C++ Và Lập Trình Hướng Đối Tượng',84300,N'Quyển',N'Lập trình cấu trúc là phương pháp tổ chức, phân chia chương trình thành các hàm, thủ tục. Chúng được dùng để xử lý dữ liệu nhưng lại tách rời các cấu trúc dữ liệu.',N'Hinh69.jpg','11/11/2020','05/04/2020',N'Phạm Văn Ất, Lê Trường Thông','TL05','NXB05'),
('MS69',N'Giáo Trình Phương Pháp Luận Nghiên Cứu Khoa Học',49000,N'Quyển',N'Từ nửa sau thế kỷ XIX, các nhà khoa học đã bắt đầu tìm kiếm câu trả lời, và đến nửa sau thế kỷ XX đã chính thức hình thành một lĩnh vực nghiên cứu có tên gọi tiếng Anh là Theory of Science, tạm đặt tên tiếng Việt là Khoa học luận. Khoa học luận phân biệt với một lĩnh vực nghiên cứu khác, có tên tiếng Anh là Epistemology, tiếng Việt nên hiểu là “Nhận thức luận khoa học”. Khoa học luận là lý thuyết chung về khoa học; còn nhận thức luận khoa học là lý thuyết về phương pháp nhận thức khoa học.',N'Hinh70.jpg','11/11/2020','05/04/2020',N'Vũ Cao Đàm','TL05','NXB05'),
('MS70',N'Giáo Trình Tâm Lí Học Đại Cương',50000,N'Quyển',N'Tâm lí học là một khoa học',N'Hinh71.jfif','11/11/2020','05/04/2020',N'Nhiều tác giả','TL05','NXB05'),
('MS71',N'Giáo Trình Chủ Nghĩa Xã Hội Khoa Học',115000,N'Quyển',N'Để giúp việc nghiên cứu, giảng dạy và học tập môn Chủ Nghĩa Xã Hội Khoa Học một cách có hệ thống trên cơ sở đổi mới cả về cách tiếp cận và phân tích lý luận cũng như cố gắng cập nhật với thực tiễn của thời đại, dưới sự chỉ đạo của Hội Đồng Trung ương chỉ đạo biên soạn giáo trình quốc gia các bộ môn khoa học Mác - Lênnin, tư tưởng Hồ Chí Minh, Ban biên soạn giáo trình chủ nghĩa xã hội khoa học gồm các nhà khoa học đầu đàn trên lĩnh vực này, do GS. TS Đỗ Nguyên Phương chủ biên, đã hoàn thành việc biên soạn Chủ Nghĩa Xã Hội Khoa Học.',N'Hinh72.jpg','11/11/2020','05/04/2020',N'Nhiều tác giả','TL05','NXB05'),
('MS72',N'Giáo Trình Tiếng Hàn Tổng Hợp Dành Cho Người Việt Nam',107000,N'Quyển',N'Mặc dù hiện nay trên thị trường có rất nhiều giáo trình dạy tiếng Hàn khác nhau, nhưng giáo trình Tiếng Hàn tổng hợp vẫn được coi là bộ sách dạy tiếng Hàn “quốc dân” được sử dụng phổ biến nhất tại Việt Nam.',N'Hinh73.png','11/11/2020','05/04/2020',N'Cho Hang Rok, Lee I Hye','TL05','NXB05'),
('MS73',N'Khi Lỗi Thuộc Về Những Vì Sao',89000,N'Quyển',N'Mặc dù phép màu y học đã giúp thu hẹp khối u và ban thêm vài năm sống cho Hazel nhưng cuộc đời cô bé đang ở vào giai đoạn cuối, từng chương kế tiếp được viết theo kết quả chẩn đoán. Nhưng khi có một nhân vật điển trai tên là Augustus Waters đột nhiên xuất hiện tại Hội Tương Trợ Bệnh Nhi Ung Thư, câu chuyện của Hazel sắp được viết lại hoàn toàn.',N'Hinh74.jpg','11/01/2020','05/03/2021',N'John Green','TL06','NXB06'),
('MS74',N'Biệt Thự LONGBOURN',115000,N'Quyển',N'Nhưng cuộc tình giữa Elizabeth Bennet và Fitzwilliam Darcy trong Kiêu Hãnh và Định Kiến đã thành công, không chỉ nhờ sự vượt qua các rào cản từ phía họ, mà chính còn bởi những xung lực ngầm, những sự trợ giúp, những tác động rất nhỏ theo hiệu ứng cánh bướm, từ những người thân chung quanh. Trong đó, có các gia nhân của Biệt Thự Longbourn.',N'Hinh75.jpg','11/01/2020','05/03/2021',N'Jo Baker','TL06','NXB06'),
('MS75',N'Nhật Ký Anne Frank (Tái Bản)',143000,N'Quyển',N'Được tìm thấy trong căn gác áp mái, nơi Anne Frank đã sống hai năm cuối đời mình, từ đó cuốn đó nhật ký đặc sắc của cô đã trở thành một tác phẩm kinh điển của thế giới – một lời nhắc nhở thật mạnh mẽ về sự rùng rợn của chiến tranh và là một lời tuyên bố về tinh thần của loài người.',N'Hinh76.png','11/01/2020','05/03/2021',N'Anne Frank','TL06','NXB06'),
('MS76',N'Outlander - Vòng tròn đá thiêng 2',10000,N'Quyển',N'Outlander - Vòng Tròn Đá Thiêng 2 là cuốn đầu tiên trong bộ tiểu thuyết lịch sử xuyên thời gian của Diana Gabaldon. Bà đã dệt nên một câu chuyện đầy ma lực đan xen giữa lịch sử và thần thoại, nó chứa đầy những đam mê mãnh liệt và sự táo bạo.',N'Hinh77.jpg','11/01/2020','05/03/2021',N'Diana Gabaldon','TL06','NXB06'),
('MS77',N'Tìm Em Nơi Anh',78500,N'Quyển',N'Đây là cuốn tiếp theo sau "Gọi em bằng tên anh" (Call me by your name). Những nhân vật gặp lại, và câu chuyện nhiều năm sau, khi họ đều đã lớn tuổi hơn và có những ngã rẽ riêng trong cuộc đời mình.',N'Hinh79.jpg','11/01/2020','05/03/2021',N'André Aciman','TL06','NXB06'),
('MS78',N'Truyền Thuyết Các Nhân Vật Tam Quốc',143500,N'Quyển',N'Nếu không nói trên diện rộng là toàn cõi Á Đông này, hay cho đến toàn thế giới, riêng ở Việt Nam thì hầu như không ai không biết đến tác phẩm trứ danh của La Quán Trung là Tam quốc diễn nghĩa, được xếp vào tứ đại danh tác của Trung Quốc bên cạnh Hồng lâu mộng, Tây du ký và Thuỷ hử truyện. Tác phẩm Tam quốc diễn nghĩa là một tác phẩm dựa theo bộ sử Tam quốc chí của Trần Thọ, qua tài nghệ văn chương của tác giả đã tạo tác ra một tác phẩm như là sử nhưng lại là tiểu thuyết, lưu truyền rộng rãi đến người đọc, vừa được thưởng thức văn chương, vừa hiểu được thêm về sử.',N'Hinh80.jpg','11/01/2020','05/03/2021',N'Khuyết Danh','TL06','NXB06'),
('MS79',N'Chúa Ruồi',57200,N'Quyển',N'Trong một cuộc chiến tranh nguyên tử, mấy chục đứa trẻ chưa đến tuổi thiếu niên “may mắn” sống sót trên một hoang đảo sau khi chiếc máy bay chở chúng đi sơ tán bị trúng đạn. Chúng tập họp dưới bầu trời Nam Thái Bình Dương nắng gắt, chia sẻ gánh nặng và đặt niềm tin vào thủ lĩnh. Nhưng rồi, cái đói và thiên nhiên khắc nghiệt từng bước vắt kiệt bọn trẻ. Bản năng sinh tồn đã dần bóp nghẹt sự ngây thơ - từ đây thực tại của chúng tan hòa vào ác mộng.',N'Hinh81.jpg','11/01/2020','05/03/2021',N'William Golding','TL06','NXB06'),
('MS80',N'Nhà',69800,N'Quyển',N'Có nhiều người đã trải qua biết bao lần thăng trầm, đã hiểu được thế nào là mái nhà thế nào là mái ấ họ sẽ đi thật chậm, nương vào nhau mà bước. Họ hiểu ai cũng có lỡ lầm, không trọn vẹ, vì thế họ biết giữ nhau bằng sự rộng lượng và bao dung.',N'Hinh82.jpg','11/01/2020','05/03/2021',N'Nguyễn Bảo Trung','TL06','NXB06'),
('MS81',N'Tâm Lí Học - Khái Lược Những Tư Tưởng Lớn',241800,N'Quyển',N'Ta có thực sự là những cá nhân tự do với bản sắc riêng… hay tất cả chúng ta đều chỉ biết tuân phục số đông? Thiên tài là nhờ dưỡng dục hay tự nhiên? Vô thức điều khiển chúng ta như thế nào? Những câu hỏi như trên chính là tiền đề cho các công trình của nhiều tư tưởng gia và khoa học gia lớn của thế giới, trong một lĩnh vực giàu sức lôi cuốn, đó là tâm lí học.',N'Hinh83.png','11/01/2020','05/03/2021',N'DK','TL07','NXB07'),
('MS82',N'How Psychology Works - Hiểu Hết Về Tâm Lý Học',276500,N'Quyển',N'Ám sợ là gì, ám sợ có thực sự đáng sợ không? Rối loạn tâm lý là gì, làm thế nào để thoát khỏi tình trạng suy nhược và xáo trộn đó? Trầm cảm là gì, vì sao con người hiện đại thường xuyên gặp và chống chọi với tình trạng u uất, mệt mỏi và tuyệt vọng này?',N'Hinh84.jpg','11/01/2020','05/03/2021',N'Jo Hemmings','TL07','NXB07'),
('MS83',N'Phiếm thần luận - Một lối đi tâm linh cho Thiên niên kỷ mới',99000,N'Quyển',N'Bạn có phải là một người có khuynh hướng Phiếm thần không? Bạn có một cảm thức về sự an bình, sự “thuộc về” và sự kinh ngạc thán phục khi ở giữa Thiên nhiên - trong một khu rừng, bên bờ đại dương, hay trên một đỉnh núi?',N'Hinh85.jpg','11/01/2020','05/03/2021',N'Paul Harrison','TL07','NXB07'),
('MS84',N'Ám Ảnh Từ Kiếp Trước - Bí Mật Của Sự Sống Và Cái Chết',618000,N'Quyển',N'Cuốn sách hay và khiêu khích suy nghĩ này đã phá vở những rào cản trong trị liệu tâm lý truyền thống và trình bày một biện pháp trị liệu cách tân và hiệu quả cao. Những ai làm việc chuyên về sức khỏe tâm thần cần phải xem xét nó nghiêm túc.” – Edith Fiore, TS., bác sỹ tâm lý học lâm sàng và là tác giả cuốn sách Bạn từng ở đây trước kia (You Have Been Here Before)',N'Hinh86.jpg','11/01/2020','05/03/2021',N'Brian L. Weiss','TL07','NXB07'),
('MS85',N'Nếu Biết Trăm Năm Là Hữu Hạn',87600,N'Quyển',N'Chỉ xuất hiện vỏn vẹn trong hơn 40 bài viết trên chuyên mục Cảm thức của Bán nguyệt san 2! (số Chuyên đề của báo Sinh Viên Việt Nam), Phạm Lữ Ân là một tác giả đã âm thầm tạo nên hiện tượng đặc biệt trong văn hoá đọc của giới trẻ Việt nam hiện nay. Các bài viết của Phạm Lữ Ân được đăng tải, trích dẫn rất nhiều lần trên các trang web, trên blog cá nhân, đươc đọc trên Youtube, thành cảm hứng cho sáng tác ca khúc và cả kịch bản phim với những lời bình ưu ái.',N'Hinh87.png','11/01/2020','05/03/2021',N'Phạm Lữ Ân','TL07','NXB07'),
('MS86',N'Đường Xưa Mây Trắng - Theo Gót Chân Bụt',223500,N'Quyển',N'Đường Xưa Mây Trắng là một câu chuyện vô cùng lý thú về cuộc đời của Bụt được kể lại dưới ngòi bút hùng hồn đầy chất thơ của tác giả. Với văn phong nhẹ nhàng giản dị, với lối kể chuyện sinh động lôi cuốn, tác giả đã đưa chúng ta trở về tắm mình trong dòng sông Nguyên thỉ cách đây gần 2.600 năm, để được hiểu và gần gũi với một bậc giác ngộ mà cuộc đời của Ngài tỏa rạng nếp sống đầy tuệ giác và từ bi. Đọc Đường Xưa Mây Trắng cho chúng ta cảm tưởng như đang đọc một thiên tình sử, nhẹ nhàng, gần gũi mà sâu lắng.',N'Hinh88.jpg','11/01/2020','05/03/2021',N'Thích Nhất Hạnh','TL07','NXB07'),
('MS87',N'Lược Sử Tôn Giáo',83500,N'Quyển',N'Hơn bảy tỷ người trên thế giới có thể viết một thứ gì đó khác chữ “Không” vào mục Tôn giáo trong hồ sơ của mình. Một số sinh ra đã theo một tôn giáo được chọn sẵn; số khác có thể tự lựa chọn theo sở thích, theo định hướng, theo đám đô Thế rồi họ thực hành đức tin của mình hằng ngày, tự hào về nó và muốn truyền bá nó cho nhiều người khác nữa. Đó là con đường phát triển hết sức tự nhiên của tôn giáo suốt hàng nghìn năm qua, kết quả là vô số tín ngưỡng với cành nhánh xum xuê mà chúng ta thấy ngày nay.',N'Hinh89.jfif','11/01/2020','05/03/2021',N'Richard Holloway','TL07','NXB07'),
('MS88',N'Sói đội lốt Cừu - Kẻ hiếu chiến ngầm và các thủ thuật thao túng tâm lí',95000,N'Quyển',N'Mỗi khi bị thao túng tâm lí và đối xử tàn tệ, nạn nhân thường tự diễn giải và hợp lí hóa cho các hành vi lệch lạc của kẻ thao túng. Họ cho rằng kẻ thao túng—những con sói đội lốt cừu—thường không cố ý làm hại người xung quanh, rằng các hành vi xấu ấy vốn có nguyên nhân là những tổn thương tâm lí ẩn bên trong kẻ thao túng.',N'Hinh90.jfif','11/01/2020','05/03/2021',N'Bác sĩ George K. Simon','TL07','NXB07'),
('MS89',N'Truyện Tranh Giấc mơ kì lạ của cô bé Hạt Tiêu',29000,N'Quyển',N'Cô bé hạt tiêu là một cô bé ngoan và vô cùng tốt bụng. Mặc dù vóc dáng bé nhỏ nhưng cô bé lại luôn ao ước làm được thật nhiều việc lớn, việc có ích. Và cô đã có một giấc mơ thật đẹp, thật lạ đó là hiện thực hóa được những mong ước đó với một cây bút thần.',N'Hinh91.jpg','11/01/2020','05/03/2021',N'Việt An','TL08','NXB08'),
('MS90',N'Nhật ký của nhóc Alvin siêu quậy',85000,N'Quyển',N'Cuốn sách là câu chuyện về cậu bé Alvin hiếu động luôn thích nghĩ ra những trò nghịch ngợm. Mọi chuyện bắt đầu khi cậu được bố mẹ chuyển nhà đến khu phố mới, và từ đó gặp hai cậu bạn mới và đi học ở ngôi trường mới. Trong cuốn sách hơn 200 trang với nhiều hình minh hoạ sinh động, độc giả bị lôi kéo và bật cười theo hành trình hàng ngày của cậu khi ở nhà và ở trường. Alvin đã hóa giải mọi hoạt động học tập ở trường, với bạn bè cũng như kỳ vọng của người lớn bằng những trò tinh nghịch, hồn nhiên mà trí tuệ, đôi khi còn hơi “quá đà” nhưng đâu đó vẫn toát lên tính giáo dục sâu sắc.',N'Hinh92.jpg','11/01/2020','05/03/2021',N'Nguyễn Khang Thịnh','TL08','NXB08'),
('MS91',N'Truyện Tranh Mây Trắng và hành trình giải cứu Trái đất',25000,N'Quyển',N'Các em nhỏ yêu quí! Các em biết không môi trường sống của chúng ta đang “kêu cứu” rồi đấy. Trên khắp hành tinh này nhân loại cũng đang rất nỗ lực để chung tay nhau cải tạo môi trường sống và bảo vệ Trái Đất.',N'Hinh93.jfif','11/01/2020','05/03/2021',N'Việt An','TL08','NXB08'),
('MS92',N'Những Truyện Hay Viết Cho Thiếu Nhi - Ma Văn Kháng',414000,N'Quyển',N'Tập truyện bạn đọc đang cầm trên tay đây là những truyện ngắn có nội dung gần gũi với tuổi thơ. Được chọn lọc từ hơn hai trăm truyện ngắn của Ma Văn Kháng, những trang văn có nhân vật là trẻ em hoặc những nhân vật, sự việc xoay quanh thế giới tuổi thơ cũng đã được viết từ nguồn cảm hứng, niềm mến yêu cuộc sống và lòng yêu thương dành cho con trẻ và tuổi trẻ của nhà văn',N'Hinh94.jpg','11/01/2020','05/03/2021',N'Ma Văn Kháng','TL08','NXB08'),
('MS93',N'Những Truyện Hay Viết Cho Thiếu Nhi - Vũ Hùng',544000,N'Quyển',N'Tập truyện tuyển chọn những truyện hay viết cho thiếu nhi của nhà văn Vũ Hùng: Sao Sao, Các bạn của Đam Đam, Phía Tây Trường Sơn, Ngày hè.',N'Hinh95.jfif','11/01/2020','05/03/2021',N'Vũ Hùng','TL08','NXB08'),
('MS94',N'Cô gà mái đỏ',874000,N'Quyển',N'Mầm Lá là cô gà duy nhất trong đàn đến giờ ăn không chúi đầu vào máng mà lại thò cổ qua lưới sắt để ngắm tán cây Mimosa ngoài vườn, cũng là cô gà duy nhất có một cái tên riêng do cô tự đặt. Không chấp nhận cuộc sống quẩn quanh trong chuồng chỉ để xơi cám và đẻ trứng, Ipssac mơ ước được đi lại đó đây, được ấp trứng rồi dẫn bầy con của mình tha thẩn khắp nơi như Gà Mái nhà – thành viên của gia đình sân vườn.',N'Hinh96.jpg','11/01/2020','05/03/2021',N'Hwang Sun Mi','TL08','NXB08'),
('MS95',N'Tri Thức Kinh Điển Bằng Tranh - Lịch Sử Tự Nhiên',271000,N'Quyển',N'Bá tước Buffon (7/9/1707 – 16/4/1788) là nhà tự nhiên học và nhà toán học người Pháp. Các công trình của ông có ảnh hưởng đến giới tự nhiên học sau này, trong đó có Jean-Baptiste Lamarck và Georges Cuvier.',N'Hinh97.jpg','11/01/2020','05/03/2021',N'Miao Desui','TL08','NXB08'),
('MS96',N'Truyện Tranh Ngụ Ngôn Dành Cho Thiếu Nhi: Kiến Và Chim',28400,N'Quyển',N'Truyện tranh ngụ ngôn dành cho thiếu nhi ( song ngữ Anh - Việt ) là những câu chuyện nổi tiếng trong văn học dành cho thiếu nhi, sách được thiết kế và vẽ câu chuyện theo tranh , sách được thiết kế phần tiếng anh và tiếng việt , với sự kết hợp cả hai thứ tiếng , giúp các bạn nhỏ thích thú hơn trong từng câu chuyện hay nhất được chọn lọc , bạn nhỏ có thể vừa đọc truyện tiếng anh và tiếng việt , để nâng cao phần ngoại ngữ thêm cho bé , cuối mỗi cuốn truyện đều có phần câu hỏi theo tranh cho bé thích thú hơn khi đọc xong câu chuyện, Bộ sách này có 10 chủ đề các em nhỏ tìm cho đủ tập nhé, Chúc Các bạn thiếu nhi học tập tốt.',N'Hinh98.jpg','11/01/2020','05/03/2021',N'Nhiều Tác Giả','TL08','NXB08')
------TẠO BẢNG QUANTRI
INSERT INTO QUANTRI
VALUES
('trungthanh200156@gmail.com','202cb962ac59075b964b07152d234b70',N'Nguyễn Thành Trung'),
('dongduy0612@gmail.com','202cb962ac59075b964b07152d234b70',N'Dương Đông Duy'),
('thuha10032001@gmail.com','202cb962ac59075b964b07152d234b70',N'Võ Thị Thu Hà')
------TẠO BẢNG NHAVIEN
INSERT INTO NHANVIEN
VALUES
('NV01',N'Trương Đình Văn',N'TP.HCM','0123.456.789',N'NAM','03/05/2021',11000000),
('NV02',N'Nguyễn Tuấn Kiệt',N'TP.HCM','0789.632.456',N'NAM','01/04/2021',10000000),
('NV03',N'Lê Quang Tuấn',N'TPHCM','1239.568.956',N'Nam','01/02/2021',9000000),
('NV04',N'Trần Khánh Ngọc',N'TPHCM','0785.568.156',N'Nữ','31/03/2020',20000000),
('NV05',N'Nguyễn Nhật My',N'TPHCM','0125.325.635',N'Nữ','14/12/2020',14000000),
('NV06',N'Lê Bảo Hoàng Việt',N'TPHCM','1245.369.852',N'Nam','14/12/2019',15000000),
('NV07',N'Hà Anh Tuấn',N'TPHCM','7856.369.752',N'Nam','14/12/2018',50000000),
('NV08',N'Nguyễn Trần Yến Nhi',N'TPHCM','1247.369.752',N'Nữ','14/12/2017',100000000),
('NV09',N'Trương Văn Tấn Trung',N'TPHCM','784.325.965',N'Nam','14/11/2020',300000000),
('NV10',N'Nguyễn Ngọc Hương',N'TPHCM','4478.325.639',N'Nữ','29/12/2017',400000000)
GO

--THỦ TỤC TÌM KIẾM SÁCH THEO TỪ KHOÁ
CREATE PROC TIMKIEMSACHTHEOTUKHOA
@TUKHOA NVARCHAR(50)
AS
BEGIN
select *from sach,theloai,nhaxuatban where sach.matheloai=theloai.matheloai and sach.manxb=nhaxuatban.manxb
and (masach like '%'+@TUKHOA+'%' or tensach like N'%'+@TUKHOA+'%' or tentheloai like N'%'+@TUKHOA+'%' or tennxb like N'%'+@TUKHOA+'%' or TENTACGIA like N'%'+@TUKHOA+'%')
END
GO

---THỦ TỤC SÁCH BÁN CHẠY
CREATE PROC SACHBANCHAY
AS
BEGIN
SELECT *FROM SACH WHERE MASACH IN (SELECT MASACH FROM CHITIETDONDATHANG GROUP BY MASACH HAVING SUM(SOLUONG)>=5)
END
go

--cập nhật giảm giá cho bảng sách
update SACH
set GIAMGIA=(select GIAMGIA from CTKHUYENMAI where CTKHUYENMAI.MAKM=SACH.MAKM )
go
update SACH 
set GIAMGIA=0
where MAKM is null
go
---Trigger xoá thể loại các bảng liên quan cũng xoá theo
create TRIGGER xoaTheLoai ON theloai
INSTEAD OF DELETE
AS
BEGIN
	DECLARE @matheloai CHAR(10),@count int
	SELECT @matheloai=matheloai FROM deleted	
	delete SACH where MATHELOAI=@matheloai
	delete theloai where matheloai=@matheloai
END

GO
---trigger xoá hoá đơn các bảng liên quan xoá theo
create TRIGGER XOAHD ON HOADON
instead of DELETE
AS
BEGIN
DECLARE @MAHD int
SELECT @MAHD=MAHD FROM deleted
DELETE CHITIETHD WHERE MAHD=@MAHD
DELETE HOADON WHERE MAHD=@MAHD
END

GO
---XOÁ ĐƠN HÀNG CÁC BẢNG LIÊN QUAN XOÁ THEO
create TRIGGER XOADH ON DONDATHANG
INSTEAD OF DELETE
AS
BEGIN
DECLARE @MADDH int
SELECT @MADDH=MADONHANG FROM deleted
DELETE CHITIETDONDATHANG WHERE MADONDATHANG=@MADDH
DELETE DONDATHANG WHERE MADONHANG=@MADDH
END

go
---XOÁ NHÂN VIÊN CÁC BẢNG LIÊN QUAN XOÁ THEO
CREATE TRIGGER XOANV ON NHANVIEN
INSTEAD OF DELETE
AS
BEGIN
DECLARE @MANV CHAR(10),@COUNT INT
SELECT @MANV=MANV FROM deleted
SELECT @COUNT=COUNT(*) FROM HOADON WHERE MANV=@MANV
WHILE(@COUNT>0)
BEGIN
DELETE HOADON WHERE MANV=@MANV
SET @COUNT-=1
END
DELETE NHANVIEN WHERE MANV=@MANV
END
GO
---XOÁ KHÁCH HÀNG CÁC BẢNG LIÊN QUAN XOÁ THEO
create TRIGGER XOAKH ON KHACHHANG
INSTEAD OF DELETE
AS
BEGIN
DECLARE @EMAIL CHAR(50),@COUNT INT
SELECT @EMAIL=EMAIL FROM deleted
SELECT @COUNT=COUNT(*) FROM DONDATHANG WHERE EMAIL=@EMAIL
WHILE(@COUNT>0)
BEGIN
DELETE DONDATHANG WHERE EMAIL=@EMAIL
SET @COUNT-=1
END
SELECT @COUNT=COUNT(*) FROM HOADON WHERE EMAIL=@EMAIL
WHILE(@COUNT>0)
BEGIN
DELETE HOADON WHERE EMAIL=@EMAIL
SET @COUNT-=1
END
DELETE KHACHHANG WHERE EMAIL=@EMAIL
END
GO
--Trigger xoá nhà xuất bản các bảng liên quan xoá theo
CREATE TRIGGER XOANXB ON NHAXUATBAN
INSTEAD OF DELETE
AS
BEGIN
DECLARE @MANXB CHAR(10),@COUNT INT
SELECT @MANXB=MANXB FROM deleted
DELETE SACH WHERE MANXB=@MANXB
DELETE NHAXUATBAN WHERE MANXB=@MANXB
END
GO
go
--trigger xoá ctkm UPDATE LẠI BẢNG SÁCH
CREATE TRIGGER XOACTKM ON CTKHUYENMAI
INSTEAD OF DELETE
AS
BEGIN
	DECLARE @MAKM CHAR(10)
	SELECT @MAKM=MAKM FROM deleted
	UPDATE SACH 
	SET MAKM=NULL,GIAMGIA=0
	WHERE MAKM=@MAKM
	DELETE CTKHUYENMAI WHERE MAKM=@MAKM
END
go
