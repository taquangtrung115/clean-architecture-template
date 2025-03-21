📌 Tổng quan thư mục
php
Copy code
frontend/
│── shell/                        # Ứng dụng chính (Container App)
│   ├── src/
│   │   ├── components/            # Các component dùng chung
│   │   ├── pages/                 # Các trang chính
│   │   ├── App.js                 # Root Component
│   │   ├── index.js               # Entry Point
│   ├── public/
│   ├── webpack.config.js          # Cấu hình Webpack cho Module Federation
│   ├── package.json
│
│── microfrontends/
│   ├── auth-mfe/                   # Microfrontend cho Authentication
│   │   ├── src/
│   │   │   ├── pages/Login.js
│   │   │   ├── services/authService.js
│   │   ├── public/
│   │   ├── webpack.config.js
│   │   ├── package.json
│   │
│   ├── user-mfe/                   # Microfrontend cho User Management
│   ├── product-mfe/                # Microfrontend cho Product Management
│   ├── order-mfe/                   # Microfrontend cho Order Management
│
│── docker-compose.frontend.yml      # Chạy frontend bằng Docker Compose
│── README.md


📌 Cấu trúc thư mục chi tiết
Thư mục	Chức năng
📂 Shell (shell/)	Ứng dụng chính (Container App) chứa các Microfrontends.
📂 Microfrontends (microfrontends/)	Chứa các ứng dụng nhỏ, mỗi ứng dụng hoạt động độc lập.
📂 Auth-MFE (auth-mfe/)	Microfrontend xử lý Authentication (Đăng nhập, Đăng ký).
📂 User-MFE (user-mfe/)	Microfrontend quản lý thông tin User.
📂 Product-MFE (product-mfe/)	Microfrontend quản lý sản phẩm.
📂 Order-MFE (order-mfe/)	Microfrontend quản lý đơn hàng.
📂 Docker (docker-compose.frontend.yml)	Cấu hình Docker để chạy từng Microfrontend.