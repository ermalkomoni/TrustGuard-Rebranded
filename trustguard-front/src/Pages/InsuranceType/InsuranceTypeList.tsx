import React from "react";
import {
  useDeleteInsuranceTypeMutation,
  useGetInsuranceTypesQuery,
} from "../../Apis/insuranceTypeApi";
import { toast } from "react-toastify";
import { MainLoader } from "../../Components/Page/Common";
import { insuranceTypeModel } from"../../Interfaces";
import { useNavigate } from "react-router";
function InsuranceTypeList() {
  const [deleteInsuranceType] = useDeleteInsuranceTypeMutation();
  const { data, isLoading } = useGetInsuranceTypesQuery(null);
  const navigate = useNavigate();

  const handleInsuranceTypeDelete = async (id: number) => {
    toast.promise(
      deleteInsuranceType(id),
      {
        pending: "Processing your request...",
        success: "Insurance Type Deleted Successfully!",
        error: "Error encoutnered!",
      },
      {
        theme: "dark",
      }
    );
  };

  return (
    <>
      {isLoading && <MainLoader />}
      {!isLoading && (
        <div className="table p-5">
          <div className="d-flex align-items-center justify-content-between">
            <h1 className="text-success">InsuranceType List</h1>
            <button style={{backgroundColor: '#F87C2D', color: '#fff'}}
              className="btn"
              onClick={() => navigate("/insuranceType/insurancetypeupsert")}
            >
              Add New Insurance Type
            </button>
          </div>
          <div className="p-2">
            <div className="row border">
              <div className="col-1">Image</div>
              <div className="col-1">ID</div>
              <div className="col-2">Name</div>
              <div className="col-2">Category</div>
              <div className="col-1">Price</div>
              <div className="col-2">Special Tag</div>
              <div className="col-1">Action</div>
            </div>

            {data.result.map((insuranceType: insuranceTypeModel) => {
              return (
                <div className="row border" key={insuranceType.id}>
                  <div className="col-1">
                    <img
                      src={`data:image/png;base64,${insuranceType.image}`}
                      alt="no content"
                      style={{ width: "100%", maxWidth: "50px" }}
                    />
                  </div>
                  <div className="col-1">{insuranceType.id}</div>
                  <div className="col-2">{insuranceType.name}</div>
                  <div className="col-2">{insuranceType.category}</div>
                  <div className="col-1">${insuranceType.price}</div>
                  <div className="col-2">{insuranceType.specialTag}</div>
                  <div className="col-1">
                    <button className="btn btn-success">
                      <i
                        className="bi bi-pencil-fill"
                        onClick={() =>
                          navigate("/insuranceType/insurancetypeupsert/" + insuranceType.id)
                        }
                      ></i>
                    </button>
                    <button
                      className="btn btn-danger mx-2"
                      onClick={() => handleInsuranceTypeDelete(insuranceType.id)}
                    >
                      <i className="bi bi-trash-fill"></i>
                    </button>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      )}
    </>
  );
}

export default InsuranceTypeList;
