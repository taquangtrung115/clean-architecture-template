const HtmlWebpackPlugin = require("html-webpack-plugin");
const { ModuleFederationPlugin } = require("webpack").container;

module.exports = {
  entry: "./src/index.tsx",
  mode: "development",
  devServer: { port: 3001 },
  output: {
    publicPath: "http://localhost:3004/",
  },
  plugins: [
    new ModuleFederationPlugin({
      name: "orderMFE",
      filename: "remoteEntry.js",
      exposes: {
        //"./Login": "./src/Login",
      },
    }),
    new HtmlWebpackPlugin({
      template: "./public/index.html",
    }),
  ],
};
