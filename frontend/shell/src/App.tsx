import React, { Suspense } from "react";

const RemoteAuth = React.lazy(() => import("authMFE/Login"));
const RemoteUser = React.lazy(() => import("userMFE/Profile"));
const RemoteProduct = React.lazy(() => import("productMFE/ProductList"));
const RemoteOrder = React.lazy(() => import("orderMFE/OrderList"));

function App() {
  return (
    <div>
      <h1>Shell App</h1>
      <Suspense fallback={<div>Loading...</div>}>
        <RemoteAuth />
        <RemoteUser />
        <RemoteProduct />
        <RemoteOrder />
      </Suspense>
    </div>
  );
}

export default App;
