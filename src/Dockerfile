# ======== BUILD BACKEND (.NET Core) ========
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS backend-build
WORKDIR /src

# Sao chép solution và file project
COPY clean-architecture-template.sln ./
COPY src/CleanArchitectureTemplate.API/CleanArchitectureTemplate.API.csproj src/CleanArchitectureTemplate.API/
COPY src/CleanArchitectureTemplate.Application/CleanArchitectureTemplate.Application.csproj src/CleanArchitectureTemplate.Application/
COPY src/CleanArchitectureTemplate.Domain/CleanArchitectureTemplate.Domain.csproj src/CleanArchitectureTemplate.Domain/
COPY src/CleanArchitectureTemplate.Infrastructure/CleanArchitectureTemplate.Infrastructure.csproj src/CleanArchitectureTemplate.Infrastructure/

# Khôi phục thư viện
RUN dotnet restore src/CleanArchitectureTemplate.API/CleanArchitectureTemplate.API.csproj

# Sao chép toàn bộ mã nguồn
COPY src/. ./src/

# Build và publish backend
WORKDIR /src/CleanArchitectureTemplate.API
RUN dotnet publish -c Release -o /app/out

# ======== BUILD FRONTEND (React) ========
FROM node:20 AS frontend-build
WORKDIR /app

# Sao chép package.json trước để tối ưu cache
COPY FE_React/package.json FE_React/package-lock.json ./

# Cài đặt dependencies
RUN npm install

# Sao chép toàn bộ source code của frontend
COPY FE_React/. .

# Build frontend
RUN npm run build

# ======== RUN BACKEND & FRONTEND ========
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS backend
WORKDIR /app
COPY --from=backend-build /app/out .

# Nginx để phục vụ frontend
FROM nginx:alpine AS frontend
COPY --from=frontend-build /app/build /usr/share/nginx/html
EXPOSE 80

# Sử dụng Docker Compose để chạy cả 2 service
