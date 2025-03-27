# Build ứng dụng React
FROM node:20 AS build
WORKDIR /app
COPY . ./
RUN npm install
RUN npm run build

# Chạy ứng dụng với Nginx
FROM nginx:alpine
COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
