const HtmlWebpackPlugin = require("html-webpack-plugin");
const { ModuleFederationPlugin } = require("webpack").container;

module.exports = {
  entry: "./src/index.tsx",
  mode: "development",
  devServer: { port: 3000 },
  output: {
    publicPath: "http://localhost:3000/",
  },
  plugins: [
    new ModuleFederationPlugin({
      name: "shell",
      remotes: {
        authMFE: "authMFE@http://localhost:3001/remoteEntry.js",
        userMFE: "userMFE@http://localhost:3002/remoteEntry.js",
        productMFE: "productMFE@http://localhost:3003/remoteEntry.js",
        orderMFE: "orderMFE@http://localhost:3004/remoteEntry.js",
      },
    }),
    new HtmlWebpackPlugin({
      template: "./public/index.html",
    }),
  ],
};
