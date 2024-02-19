import React from "react";
import { Banner } from "../Components/Page/Common";
import { InsuranceTypeList } from "../Components/Page/Home";
import ContactUs from "./ContactUs";

function Home() {
  return (
    <div>
      <Banner />
      <div className="container p-2">
        <InsuranceTypeList />
        <ContactUs/>
      </div>
    </div>
  );
}

export default Home;
