
import {Footer, Header } from "../Components/Layout";
import {
  Home,
  InsuranceTypeList,
  InsuranceTypeUpsert,
  InsuranceTypeDetails,
  OrderConfirmed,
  OrderDetails,
} from "../Pages";
import { Routes, Route } from"react-router-dom";

function App() {
  return (
    <div>
      <Header />
      <div className="pb-5">
        <Routes>
          <Route path="/" element={<Home />}></Route> 
          <Route
            path="/insuranceTypeDetails/:insuranceTypeId"
            element={<InsuranceTypeDetails />}
          ></Route>
          <Route path="/insuranceType/insurancetypelist" element={<InsuranceTypeList />} />
          {/* <Route path="/insuranceType/insuranceTypeList" element={<InsuranceTypeList />} /> */}
          <Route
            path="/insuranceType/insuranceTypeUpsert/:id"
            element={<InsuranceTypeUpsert />}
          />
          <Route path="/insuranceType/insuranceTypeUpsert" element={<InsuranceTypeUpsert />} />
          <Route
            path="order/orderconfirmed/:id"
            element={<OrderConfirmed />}
          ></Route>
          <Route path="/order/orderDetails/:id" element={<OrderDetails />} />
        </Routes>
      </div>
      <Footer />
    </div>
  );
}

export default App;