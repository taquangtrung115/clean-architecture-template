import React, { Suspense } from "react";
const path = "/CleanAchitectureTemplate/frontend/microfrontends/";
const RemoteAuth = React.lazy(() => import(`${path}/auth-mfe/src/Login`));
const RemoteUser = React.lazy(() => import(`${path}/user-mfe/src/Login`));
const RemoteProduct = React.lazy(() => import(`${path}/product-mfe/src/Login`));
const RemoteOrder = React.lazy(() => import(`${path}/order-mfe/src/Login`));

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
